/////////////////////////////////////////////////////////////////////
//
//	UZipDotNet
//	ZIP File processing
//
//	UZipDotNet main program
//
//	Granotech Limited
//	Author: Uzi Granot
//	Version 1.0
//	March 30, 2012
//	Copyright (C) 2012 Granotech Limited. All Rights Reserved
//
//	UZipDotNet application is a free software.
//	It is distributed under the Code Project Open License (CPOL).
//	The document UZipDotNetReadmeAndLicense.pdf contained within
//	the distribution specify the license agreement and other
//	conditions and notes. You must read this document and agree
//	with the conditions specified in order to use this software.
//
/////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UZipDotNet
{
public enum FileHeaderColumn
	{
	FileName,
	FileAttr,
	FileTime,
	FilePos,
	FileSize,
	CompSize,
	CompRatio,
	CompMethod,
	BitFlags,
	Version,
	FileSys,
	Columns,
	}

public partial class UZipDotNet : Form
	{
	private DataGridView		DataGrid;
	private InflateZipFile		Inflate;
	private DeflateZipFile		Deflate;
	private List<FileHeader>	ZipDir;
	private Font				Courier;

	////////////////////////////////////////////////////////////////////
	// Constructor
	////////////////////////////////////////////////////////////////////

	public UZipDotNet()
		{
		InitializeComponent();
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Program initialization
	////////////////////////////////////////////////////////////////////

	private void OnLoad(object sender, EventArgs e)
		{
		Trace.Open("UZipDotNetTrace.txt");
		Trace.Write("----");

		// program title
		Text = "UZipDotNet - Revision 1.0 2012/03/30 - \u00a9 2012 Granotech Limited";

		// copyright box
		CopyrightTextBox.Rtf =
			"{\\rtf1\\ansi\\deff0\\deftab720{\\fonttbl{\\f0\\fswiss\\fprq2 Verdana;}}" +
			"\\par\\plain\\fs24\\b UZipDotNet\\plain \\fs20 \\par\\par \n" +
			"Computer program designed to read and write ZIP files.\\par\\par \n" +
			"Version Number: 1.0\\par \n" +
			"Version Date: 2012/03/30\\par \n" +
			"Author: Uzi Granot\\par\\par \n" +
			"Copyright \u00a9 2012 Granotech Limited. All rights reserved.\\par\\par \n" +
			"Free software distributed under the Code Project Open License (CPOL) 1.02.\\par \n" +
			"As per UZipDotNetReadmeAndLicense.pdf file attached to this distribution.\\par \n" +
			"You must read and agree with the terms specified to use this program.}";

		// load state
		ProgramState.LoadState();

		// clear archive file name 
		DisplayArchiveName();

		// diable position in hex
		PosHexCheckBox.Enabled = false;

		// force resize
		OnResize(this, null);
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Open zip file
	////////////////////////////////////////////////////////////////////

	private void OnOpen
			(
			object sender,
			EventArgs e
			)
		{
		// get zip file name from operator
		OpenFileDialog OFD = new OpenFileDialog();
		OFD.Filter = "Zip files (*.ZIP)|*.ZIP";
		OFD.CheckFileExists = true;
		OFD.CheckPathExists = true;
	    OFD.RestoreDirectory = true;
		if(OFD.ShowDialog() != DialogResult.OK) return;

		// display empty datagrid
		AddDataGrid();

		// trace
		Trace.Write("Open exising ZIP archive: " + OFD.FileName);

		// create inflate object
		if(Inflate == null)
			{
			Inflate = new InflateZipFile();
			}

		// inflate is already open
		else if(InflateZipFile.IsOpen(Inflate))
			{
			// delete zip directory
			ZipDir = null;

			// close previous zip archive (if open)
			Inflate.CloseZipFile();

			// display empty directory in data grid view
			LoadDataGrid();
			}

		// open zip file and load zip file directory
		if(Inflate.OpenZipFile(OFD.FileName))
			{
			MessageBox.Show("Open ZIP file Error\n" + Inflate.ExceptionStack[0] + "\n" + Inflate.ExceptionStack[1]);
			return;
			}

		// get zip directory
		ZipDir = Inflate.ZipDir;

		// display zip file directory in data grid view
		LoadDataGrid();
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Decompress file one or more files
	////////////////////////////////////////////////////////////////////

	private void OnExtract
			(
			object sender,
			EventArgs e
			)
		{
		// display extract control dialog box
		ExtractSelectionForm SelectDialog = new ExtractSelectionForm();

		// get selected rows
		DataGridViewSelectedRowCollection RowCollection = DataGrid.SelectedRows;

		// count archive files (not directories) with deflate compression or no compression
		SelectDialog.ArchiveFiles = 0;
		SelectDialog.ArchiveSize = 0;
		foreach(DataGridViewRow VR in DataGrid.Rows)
			{
			FileHeader FH = ZipDir[(Int32) VR.Tag];
			if((FH.CompMethod == 0 || FH.CompMethod == 8) && (FH.FileAttr & FileAttributes.Directory) == 0)
				{
				SelectDialog.ArchiveFiles++;
				SelectDialog.ArchiveSize += FH.FileSize;
				}
			}

		// count selection files (not directories) with deflate compression or no compression
		SelectDialog.SelectionFiles = 0;
		SelectDialog.SelectionSize = 0;
		foreach(DataGridViewRow VR in RowCollection)
			{
			FileHeader FH = ZipDir[(Int32) VR.Tag];
			if((FH.CompMethod == 0 || FH.CompMethod == 8) && (FH.FileAttr & FileAttributes.Directory) == 0)
				{
				SelectDialog.SelectionFiles++;
				SelectDialog.SelectionSize += FH.FileSize;
				}
			}

		// show dialog
		if(SelectDialog.ShowDialog(this) == DialogResult.Cancel) return;

		// create extract dialog
		ProcessFilesForm ExtractDialog = new ProcessFilesForm();

		// user selected extract all
		if(ProgramState.State.ExtractAll)
			{
			ExtractDialog.ZipDir = ZipDir;
			}

		// create extract list if the user extract by selection
		else
			{
			ExtractDialog.ZipDir = new List<FileHeader>();
			foreach(DataGridViewRow VR in RowCollection) ExtractDialog.ZipDir.Add(ZipDir[(Int32) VR.Tag]);
			ExtractDialog.ZipDir.Sort();
			}

		// display extract dialog
		ExtractDialog.UpdateMode = false;
		ExtractDialog.Inflate = Inflate;
		ExtractDialog.Show(this);
		return;
		}

	////////////////////////////////////////////////////////////////////
	// New/Save archive file
	////////////////////////////////////////////////////////////////////

	private void OnNew
			(
			object sender,
			EventArgs e
			)
		{
		// test for save mode
		if(DeflateZipFile.IsOpen(Deflate))
			{
			OnSave();
			return;
			}

		// get zip file name from operator
		SaveFileDialog SFD = new SaveFileDialog();
		SFD.Filter = "Zip files (*.zip)|*.zip";
		SFD.CheckFileExists = false;
		SFD.CheckPathExists = true;
	    SFD.RestoreDirectory = true;
		SFD.OverwritePrompt = true;
		SFD.DefaultExt = "zip";
		SFD.AddExtension = true;
		if(SFD.ShowDialog() != DialogResult.OK) return;

		// display empty datagrid
		AddDataGrid();

		// trace
		Trace.Write("Create ZIP archive: " + SFD.FileName);

		// Inflate file is open
		if(InflateZipFile.IsOpen(Inflate))
			{
			// release zip directory object
			ZipDir = null;

			// close inflate zip file if open
			Inflate.CloseZipFile();

			// display zip file directory in data grid view
			LoadDataGrid();
			}

		// create deflate object
		if(Deflate == null) Deflate = new DeflateZipFile();

		// create empty zip file
		if(Deflate.CreateArchive(SFD.FileName))
			{
			MessageBox.Show("Create ZIP file Error\n" + Deflate.ExceptionStack[0] + "\n" + Deflate.ExceptionStack[1]);
			return;
			}

		// get link to the empty zip directory
		ZipDir = Deflate.ZipDir;

		// display zip file directory in data grid view
		LoadDataGrid();
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Save new archive file
	////////////////////////////////////////////////////////////////////

	private void OnSave()
		{
		// clear local directory
		ZipDir = null;

		// Write zip directory and close deflate zip file
		if(Deflate.SaveArchive())
			{
			MessageBox.Show("Save ZIP file Error\n" + Deflate.ExceptionStack[0] + "\n" + Deflate.ExceptionStack[1]);
			}

		// display zip file directory in data grid view
		LoadDataGrid();
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Add file to archive
	////////////////////////////////////////////////////////////////////

	private void OnAdd
			(
			object sender,
			EventArgs e
			)
		{
		// if inflate is open, switch to deflate mode
		if(SwitchMode()) return;

		// display files and folders selection screen
		AddFilesAndFoldersForm AddFilesDialog = new AddFilesAndFoldersForm();
		if(AddFilesDialog.ShowDialog(this) == DialogResult.Cancel) return;

		// create update dialog
		ProcessFilesForm UpdateDialog = new ProcessFilesForm();

		// display extract dialog
		UpdateDialog.UpdateMode = true;
		UpdateDialog.ZipDir = AddFilesDialog.AddDir;
		UpdateDialog.RootDir = AddFilesDialog.RootDir;
		UpdateDialog.CompLevel = AddFilesDialog.CompLevel;
		UpdateDialog.Deflate = Deflate;
		UpdateDialog.ShowDialog(this);

		// display zip file directory in data grid view
		LoadDataGrid();
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Delete file from archive
	////////////////////////////////////////////////////////////////////

	private void OnDelete
			(
			object sender,
			EventArgs e
			)
		{
		// no selection
		if(DataGrid.SelectedRows.Count == 0)
			{
			MessageBox.Show(this, "Select one or more files to delete", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
			}

		// if inflate is open, switch to deflate mode
		if(SwitchMode()) return;

		// the usual warning
		if(MessageBox.Show(this, "Do you want to permanently remove the\nselected files from the ZIP archive?",
			"Delete Files", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

		// delete file names from zip directory
		foreach(DataGridViewRow VR in DataGrid.SelectedRows)
			{
			// delete the entry
			if(Deflate.Delete((Int32) VR.Tag))
				{
				MessageBox.Show("Delete ZIP directory entry Error\n" + Deflate.ExceptionStack[0] + "\n" + Deflate.ExceptionStack[1]);
				return;
				}
			}

		// display zip file directory in data grid view
		LoadDataGrid();
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Switch from extract zip to update zip
	////////////////////////////////////////////////////////////////////

	private Boolean SwitchMode()
		{
		// inflate archive is not open
		if(!InflateZipFile.IsOpen(Inflate)) return(false);

		// ask operator
		ArchiveUpdateForm Dialog = new ArchiveUpdateForm();
		Dialog.Inflate = Inflate;
		if(Dialog.ShowDialog(this) == DialogResult.Cancel) return(true);

		// create deflate object
		if(Deflate == null) Deflate = new DeflateZipFile();

		// reopen exiting zip file for read and write
		if(Deflate.OpenArchive(Inflate.ArchiveName))
			{
			MessageBox.Show("Reopen ZIP file Error\n" + Deflate.ExceptionStack[0] + "\n" + Deflate.ExceptionStack[1]);

			// get link to the empty zip directory
			ZipDir = null;

			// display zip file directory in data grid view
			LoadDataGrid();
			return(true);
			}

		// get zip directory
		ZipDir = Deflate.ZipDir;

		// display zip file directory in data grid view
		LoadDataGrid();

		// successful switch
		return(false);
		}

	////////////////////////////////////////////////////////////////////
	// Test compress and decompress functions
	////////////////////////////////////////////////////////////////////

	private void OnTest
			(
			object sender,
			EventArgs e
			)
		{
		TestForm Dialog = new TestForm();
		Dialog.ShowDialog(this);
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Add Data Grid View control
	////////////////////////////////////////////////////////////////////

	private void AddDataGrid()
		{
		if(DataGrid != null) return;

		// remove copyright text box
		CopyrightTextBox.Hide();

		// add data grid
		DataGrid = new DataGridView();
		DataGrid.Name = "DataGrid";
		DataGrid.AllowUserToAddRows = false;
		DataGrid.AllowUserToDeleteRows = false;
		DataGrid.AllowUserToOrderColumns = true;
		DataGrid.AllowUserToResizeRows = false;
		DataGrid.RowHeadersVisible = false;
		DataGrid.MultiSelect = true;
		DataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		DataGrid.BackgroundColor = SystemColors.GradientInactiveCaption;
		DataGrid.BorderStyle = BorderStyle.None;
		DataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
		DataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		DataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
		DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		DataGrid.TabStop = true;
		DataGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

		// add columns
		DataGrid.Columns.Add("FileName", "File Name");
		DataGrid.Columns.Add("FileAttr", "Attr");
		DataGrid.Columns.Add("LastModified", "Last Modified");
		DataGrid.Columns.Add("FilePos", "File Pos");
		DataGrid.Columns.Add("FileSize", "File Size");
		DataGrid.Columns.Add("CompSize", "Comp Size");
		DataGrid.Columns.Add("CompRatio", "Comp Ratio");
		DataGrid.Columns.Add("Method", "Method");
		DataGrid.Columns.Add("Flags", "Flags");
		DataGrid.Columns.Add("Version", "Version");
		DataGrid.Columns.Add("FileSys", "FileSys");

		// format right justified headers
		DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
		CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		CellStyle.WrapMode = DataGridViewTriState.False;
		DataGrid.Columns[(Int32) FileHeaderColumn.FilePos].HeaderCell.Style = CellStyle;
		DataGrid.Columns[(Int32) FileHeaderColumn.FileSize].HeaderCell.Style = CellStyle;
		DataGrid.Columns[(Int32) FileHeaderColumn.CompSize].HeaderCell.Style = CellStyle;
		DataGrid.Columns[(Int32) FileHeaderColumn.CompRatio].HeaderCell.Style = CellStyle;

		// format file size and compressed size columns
		CellStyle = new DataGridViewCellStyle();
		CellStyle.Format = "#,##0";
		CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		CellStyle.WrapMode = DataGridViewTriState.False;
		DataGrid.Columns[(Int32) FileHeaderColumn.FilePos].DefaultCellStyle = CellStyle;

		// format file size and compressed size columns
		CellStyle = new DataGridViewCellStyle();
		CellStyle.Format = "#,##0";
		CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		CellStyle.WrapMode = DataGridViewTriState.False;
		DataGrid.Columns[(Int32) FileHeaderColumn.FileSize].DefaultCellStyle = CellStyle;
		DataGrid.Columns[(Int32) FileHeaderColumn.CompSize].DefaultCellStyle = CellStyle;

		// format compress ratio column
		CellStyle = new DataGridViewCellStyle();
		CellStyle.Format = "0.00%";
		CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		CellStyle.WrapMode = DataGridViewTriState.False;
		DataGrid.Columns[(Int32) FileHeaderColumn.CompRatio].DefaultCellStyle = CellStyle;

		// format version and compression method columns
		CellStyle = new DataGridViewCellStyle();
		CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
		CellStyle.WrapMode = DataGridViewTriState.False;
		DataGrid.Columns[(Int32) FileHeaderColumn.CompMethod].DefaultCellStyle = CellStyle;
		DataGrid.Columns[(Int32) FileHeaderColumn.BitFlags].DefaultCellStyle = CellStyle;
		DataGrid.Columns[(Int32) FileHeaderColumn.Version].DefaultCellStyle = CellStyle;
		DataGrid.Columns[(Int32) FileHeaderColumn.FileSys].DefaultCellStyle = CellStyle;

		// add sort compare handler
		DataGrid.SortCompare += new DataGridViewSortCompareEventHandler(OnSortCompare);

		// add the data grid to the list of controls of the parent form
		Controls.Add(DataGrid);

		// enable position in hex
		PosHexCheckBox.Enabled = true;

		// force resize
		OnResize(this, null);
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Position column numeric display mode
	////////////////////////////////////////////////////////////////////

	private void OnPosHexChanged
			(
			object sender,
			 EventArgs e)
		{
		if(PosHexCheckBox.Checked)
			{
			DataGrid.Columns[(Int32) FileHeaderColumn.FilePos].DefaultCellStyle.Format = "x8";
			DataGrid.Columns[(Int32) FileHeaderColumn.FilePos].DefaultCellStyle.Font = Courier;
			}
		else
			{
			DataGrid.Columns[(Int32) FileHeaderColumn.FilePos].DefaultCellStyle.Format = "#,##0";
			DataGrid.Columns[(Int32) FileHeaderColumn.FilePos].DefaultCellStyle.Font = DataGrid.Font;
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Load Data Grid
	////////////////////////////////////////////////////////////////////

	private void LoadDataGrid()
		{
		// clear data grid view
		DataGrid.Rows.Clear();

		// load grid
		if(ZipDir != null && ZipDir.Count > 0)
			{
			// add rows
			DataGrid.Rows.Add(ZipDir.Count);

			// load one row at a time to data grid
			for(Int32 Row = 0; Row < ZipDir.Count; Row++) LoadDataGridRow(Row);

			// select first row
			DataGrid.Rows[0].Selected = true;
			}

		// adjust parent width and height
		AdjustParent(0, ButtonsGroupBox.Bounds.Width, ButtonsGroupBox.Bounds.Height + 2, 0);

		// move all controls are to their right place
		OnResize(null, null);

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Load Data Grid Row
	////////////////////////////////////////////////////////////////////

	private void LoadDataGridRow
			(
			Int32		Row
			)
		{
		// data grid row
		DataGridViewRow ViewRow = DataGrid.Rows[Row];

		// matching file header
		FileHeader FH = ZipDir[Row];

		// save file info pointer
		ViewRow.Tag = Row;

		// file name
		ViewRow.Cells[(Int32) FileHeaderColumn.FileName].Value = FH.FileName;

		// file date and time
		ViewRow.Cells[(Int32) FileHeaderColumn.FileTime].Value =
			String.Format("{0:0000}/{1:00}/{2:00} {3:00}:{4:00}:{5:00} ",
				1980 + ((FH.FileDate >> 9) & 0x7f), (FH.FileDate >> 5) & 0xf, FH.FileDate & 0x1f,
				(FH.FileTime >> 11) & 0x1f, (FH.FileTime >> 5) & 0x3f, 2 * (FH.FileTime & 0x1f));

		// directory
		if((FH.FileAttr & FileAttributes.Directory) != 0)
			{
			ViewRow.Cells[(Int32) FileHeaderColumn.FileAttr].Value = "Dir";

			// file position
			ViewRow.Cells[(Int32) FileHeaderColumn.FilePos].Value = FH.FilePos;
			}
		else
			{
			// file attributes
			String Attr = String.Empty;
			if((FH.FileAttr & FileAttributes.ReadOnly) != 0) Attr += "R";
			if((FH.FileAttr & FileAttributes.Hidden) != 0) Attr += "H";
			if((FH.FileAttr & FileAttributes.System) != 0) Attr += "S";
			ViewRow.Cells[(Int32) FileHeaderColumn.FileAttr].Value = Attr;

			// file position
			ViewRow.Cells[(Int32) FileHeaderColumn.FilePos].Value = FH.FilePos;

			// file size
			ViewRow.Cells[(Int32) FileHeaderColumn.FileSize].Value = FH.FileSize;

			// compress size
			ViewRow.Cells[(Int32) FileHeaderColumn.CompSize].Value = FH.CompSize;

			// compress ratio
			if(FH.FileSize != 0) ViewRow.Cells[(Int32) FileHeaderColumn.CompRatio].Value = (Double) FH.CompSize / (Double) FH.FileSize;

			// compression method
			String CompMethod;
			switch(FH.CompMethod)
				{
				case 0: CompMethod = "Store"; break;
				case 1: CompMethod = "Shrunk"; break;
				case 6: CompMethod = "Implode"; break;
				case 8: CompMethod = "Deflate"; break;
				default: CompMethod = FH.CompMethod.ToString(); break;
				}
			ViewRow.Cells[(Int32) FileHeaderColumn.CompMethod].Value = CompMethod;
			}
		
		// Bit Flags
		ViewRow.Cells[(Int32) FileHeaderColumn.BitFlags].Value = FH.BitFlags;
		
		// version
		ViewRow.Cells[(Int32) FileHeaderColumn.Version].Value = String.Format("{0:0.0}", (Double) ((Byte)FH.Version) / 10.0);
		
		// file system
		FileSystem FS = (FileSystem) ((Byte) FH.Version >> 8);
		ViewRow.Cells[(Int32) FileHeaderColumn.FileSys].Value = FS <= FileSystem.Unused ? FS.ToString() : ((Int32) FS).ToString();
		
		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Adjust parent width to fit grid
	////////////////////////////////////////////////////////////////////

	public void AdjustParent
			(
			Int32	ExtraWidth,
			Int32	MinWidth,
			Int32	ExtraHeight,
			Int32	MinHeight
			)
		{
		// calculate columns width plus a little extra
		Int32 ReqWidth = ColumnsWidth() + ExtraWidth;

		// make sure it is not less than the minimum width requirement
		if(ReqWidth < MinWidth) ReqWidth = MinWidth;

		// required height
		Int32 ReqHeight = DataGrid.ColumnHeadersHeight + ExtraHeight;
		if(DataGrid.Rows.Count == 0) ReqHeight += 2 * DataGrid.ColumnHeadersHeight;
		else ReqHeight += (DataGrid.Rows.Count < 4 ? 4 : DataGrid.Rows.Count) * (DataGrid.Rows[0].Height + DataGrid.Rows[0].DividerHeight);

		// make sure it is not less than minimum
		if(ReqHeight < MinHeight) ReqHeight = MinHeight;

		// find the form under the grid
		Form ParentForm = FindForm();

		// add non client area to requirement
		ReqWidth += ParentForm.Bounds.Width - ParentForm.ClientRectangle.Width;
		ReqHeight += ParentForm.Bounds.Height - ParentForm.ClientRectangle.Height;

		// get screen area
		Rectangle ScreenWorkingArea = Screen.FromControl(ParentForm).WorkingArea;

		// make sure required width is less than screen width
		if(ReqWidth > ScreenWorkingArea.Width) ReqWidth = ScreenWorkingArea.Width;

		// make sure required height is less than screen height
		if(ReqHeight > ScreenWorkingArea.Height) ReqHeight = ScreenWorkingArea.Height;

		// set bounds of parent form
		ParentForm.SetBounds(ScreenWorkingArea.Left + (ScreenWorkingArea.Width - ReqWidth) / 2,
			ScreenWorkingArea.Top + (ScreenWorkingArea.Height - ReqHeight) / 2, ReqWidth, ReqHeight);
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Calculate Columns Width
	////////////////////////////////////////////////////////////////////

	private Int32 ColumnsWidth()
		{
		// get graphics object
		Graphics GR = CreateGraphics();

		// get font
		Font GridFont = Font;

		// define extra width
		Int32 ExtraWidth = (Int32) Math.Ceiling(GR.MeasureString("0", GridFont).Width);

		// file position hex width
		if(Courier == null) Courier = new Font("Courier New", GridFont.Size);
		Int32 HexWidth = (Int32) Math.Ceiling(GR.MeasureString("00000000", Courier).Width);

		// add up total width
		Int32 TotalWidth = 0;

		// loop for columns
		for(Int32 ColNo = 0; ColNo < (Int32) FileHeaderColumn.Columns; ColNo++)
			{
			// short cut
			DataGridViewTextBoxColumn Col = (DataGridViewTextBoxColumn) DataGrid.Columns[ColNo];

			// header width
			Int32 ColWidth = (Int32) Math.Ceiling(GR.MeasureString(Col.HeaderText, GridFont).Width);

			// loop for all rows of one column
			for(Int32 Row = 0; Row < DataGrid.Rows.Count; Row++)
				{
				// cell width
				Int32 CellWidth = (Int32) Math.Ceiling(GR.MeasureString((String) DataGrid[ColNo, Row].FormattedValue, GridFont).Width);
				if(CellWidth > ColWidth) ColWidth = CellWidth;
				}

			// file position
			if(ColNo == (Int32) FileHeaderColumn.FilePos)
				{
				if(HexWidth > ColWidth) ColWidth = HexWidth;
				}

			// set column width
			ColWidth += ExtraWidth;
			Col.Width = ColWidth;
			Col.FillWeight = ColWidth;
			Col.MinimumWidth = ColWidth;

			// add up total width
			TotalWidth += ColWidth;
			}

		// exit
		return(TotalWidth + SystemInformation.VerticalScrollBarWidth + 1);
		}

	////////////////////////////////////////////////////////////////////
	// Sort on multiple columns
	////////////////////////////////////////////////////////////////////

	private void OnSortCompare
			(
			object sender,
			DataGridViewSortCompareEventArgs e
			)
		{
		// all columns but the first one
		Int32 ColIndex = e.Column.Index;
		if(ColIndex == (Int32) FileHeaderColumn.FileName || (e.SortResult = Compare(e.CellValue1, e.CellValue2)) == 0)
			{
			FileHeader FH1 = ZipDir[(Int32) DataGrid.Rows[e.RowIndex1].Tag];
			FileHeader FH2 = ZipDir[(Int32) DataGrid.Rows[e.RowIndex2].Tag];
			if(FH1.Path != FH2.Path)
				{
				e.SortResult = FH1.Path ? 1 : -1;
				}
			else
				{
				e.SortResult = String.Compare(FH1.FileName, FH2.FileName);
				}
			}

		// this routine will handle the compare
		e.Handled = true;

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Compare Two Objects
	////////////////////////////////////////////////////////////////////

	private Int32 Compare
			(
			Object		One,
			Object		Other
			)
		{
		if(One == null) return(Other == null ? 0 : -1);
		if(Other == null) return(1);
		if(One.GetType() == typeof(Int32)) return((Int32) One - (Int32) Other);
		if(One.GetType() == typeof(UInt32)) return((UInt32) One > (UInt32) Other ? 1 : ((UInt32) One < (UInt32) Other) ? -1 : 0);
		if(One.GetType() == typeof(Double)) return((Double) One > (Double) Other ? 1 : ((Double) One < (Double) Other) ? -1 : 0);
		return(String.Compare(One.ToString(), Other.ToString()));
		}

	////////////////////////////////////////////////////////////////////
	// Resize Screen
	////////////////////////////////////////////////////////////////////

	private void OnResize
			(
			object sender,
			EventArgs e
			)
		{
		// protect against minimize button
		if(ClientSize.Width == 0) return;

		// archive name
		DisplayArchiveName();

		// postion buttons
		ButtonsGroupBox.Left = (ClientSize.Width - ButtonsGroupBox.Width) / 2;
		ButtonsGroupBox.Top = ClientSize.Height - ButtonsGroupBox.Height;

		// position copyright
		if(CopyrightTextBox.Visible)
			{
			CopyrightTextBox.Left = (ClientSize.Width - CopyrightTextBox.Width) / 2;
			CopyrightTextBox.Top = ArchiveNameLabel.Bottom + (ButtonsGroupBox.Top - ArchiveNameLabel.Bottom  - CopyrightTextBox.Height) / 2;
			}

		// position datagrid
		if(DataGrid != null)
			{
			DataGrid.Left = 0;
			DataGrid.Top = ArchiveNameLabel.Bottom + 8;
			DataGrid.Width = ClientSize.Width;
			DataGrid.Height = ButtonsGroupBox.Top - DataGrid.Top;
			}

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Display archive name
	////////////////////////////////////////////////////////////////////

	private void DisplayArchiveName()
		{
		// archive is open for reading
		if(InflateZipFile.IsOpen(Inflate))
			{
			ArchiveNameLabel.BackColor = Color.FromArgb(192,255, 255);
			ArchiveNameLabel.Text = Inflate.ArchiveName;
			}

		// archive is open for writing
		else if(DeflateZipFile.IsOpen(Deflate))
			{
			ArchiveNameLabel.BackColor = Color.FromArgb(255, 192, 255);
			ArchiveNameLabel.Text = Deflate.ArchiveName;
			}

		// archive is not open
		else
			{
			ArchiveNameLabel.BackColor = SystemColors.Control;
			ArchiveNameLabel.Text = "UZipDotNet";
			}

		// calculate required width
		Int32 Width = (Int32) Math.Ceiling(CreateGraphics().MeasureString(ArchiveNameLabel.Text, ArchiveNameLabel.Font).Width) + 50;

		// archive name position
		ArchiveNameLabel.Width = Math.Min(Math.Max(Width, 200), ClientSize.Width - 50);
		ArchiveNameLabel.Left = (ClientSize.Width - ArchiveNameLabel.Width) / 2;
		ArchiveNameLabel.Top = 8;

		// enable/disable some buttons
		OpenButton.Enabled = !DeflateZipFile.IsOpen(Deflate);
		ExtractButton.Enabled = InflateZipFile.IsOpen(Inflate);
		AddButton.Enabled = InflateZipFile.IsOpen(Inflate) || DeflateZipFile.IsOpen(Deflate);
		DeleteButton.Enabled = AddButton.Enabled;

		// change button name
		NewButton.Text = DeflateZipFile.IsOpen(Deflate) ? "Save" : "New";

		// update control
		Refresh();
		return;
		}

	////////////////////////////////////////////////////////////////////
	// User pressed exit button
	////////////////////////////////////////////////////////////////////

	private void OnExit
			(
			object sender,
			EventArgs e
			)
		{
		DialogResult = DialogResult.Cancel;
		Close();
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Form is closing
	////////////////////////////////////////////////////////////////////

	private void OnClosing
			(
			object sender,
			FormClosingEventArgs e
			)
		{
		// Inflate is active. Close the zip archive file
		if(InflateZipFile.IsOpen(Inflate)) Inflate.CloseZipFile();

		// deflate is active
		if(Deflate != null)
			{
			// new archive is not empty
			if(!Deflate.IsEmpty)
				{
				switch(MessageBox.Show(this, "Do you want to save your ZIP archive?\n" +
					"If you select NO, the archive will be deleted", "Save ZIP Archive",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
					{
					case DialogResult.Cancel:
						e.Cancel = true;
						return;

					case DialogResult.Yes:
						OnSave();
						break;

					case DialogResult.No:
						// clearing the zip directory will make SaveArchive delete the file
						Deflate.ClearArchive();
						break;
					}
				}

			// deflate is empty
			else
				{
				// save archive will delete the archive because it is empty
				Deflate.ClearArchive();
				}
			}

		// trace exit
		Trace.Write("UZipDotNet exit");
		return;
		}
	}
}