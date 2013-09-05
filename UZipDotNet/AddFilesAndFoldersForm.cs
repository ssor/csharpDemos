/////////////////////////////////////////////////////////////////////
//
//	UZipDotNet
//	ZIP File processing
//
//	AddFilesAndFoldersForm.cs
//	Windows form allowing the user to select files and folders
//	to be included in the ZIP archive.
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
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace UZipDotNet
{
public partial class AddFilesAndFoldersForm : Form
	{
	////////////////////////////////////////////////////////////////////
	//	Members
	////////////////////////////////////////////////////////////////////

	public  List<FileHeader>	AddDir;
	public  String				RootDir;
	public  Int32				RootDirPtr;
	public	Int32				CompLevel;

	private FileSystemWatcher	DirWatcher;
	private Timer				FileListTimer;
	private Boolean				RefreshFileList;
	private Boolean				ResizeLock;

	////////////////////////////////////////////////////////////////////
	//	Constructor
	////////////////////////////////////////////////////////////////////

	public AddFilesAndFoldersForm()
		{
		InitializeComponent();
		}

	////////////////////////////////////////////////////////////////////
	//	On Load
	////////////////////////////////////////////////////////////////////

	private void OnLoad
			(
			object sender,
			EventArgs e
			)
		{
		// program initialization
		ProgramInitialization();

		// Directory tree initialization
		DirectoryTreeInit();

		// resize
		OnResize(this, null);
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	One time program initialization
	////////////////////////////////////////////////////////////////////

	private void ProgramInitialization()
		{
		// create image list
		DirectoryTree.ImageList = new ImageList();
		DirectoryTree.ImageList.Images.Add(Properties.Resources.MyComputerBmp);
		DirectoryTree.ImageList.Images.Add(Properties.Resources.MyComputerSelBmp);
		DirectoryTree.ImageList.Images.Add(Properties.Resources.DiskDriveBmp);
		DirectoryTree.ImageList.Images.Add(Properties.Resources.DiskDriveSelBmp);
		DirectoryTree.ImageList.Images.Add(Properties.Resources.FolderBmp);
		DirectoryTree.ImageList.Images.Add(Properties.Resources.FolderSelBmp);
		DirectoryTree.ImageList.Images.Add(Properties.Resources.FileBmp);

		// attach the same list to the file tree
		FileList.SmallImageList = DirectoryTree.ImageList;

		// other options
		DirectoryTree.ShowLines = true;
		DirectoryTree.ShowRootLines = false;
		DirectoryTree.Scrollable = true;
		DirectoryTree.BorderStyle = BorderStyle.FixedSingle;
		DirectoryTree.ShowPlusMinus = true;

		// create a new FileSystemWatcher
		DirWatcher = new FileSystemWatcher();

		// watch for changes in LastAccess and LastWrite times, and renaming of files or directories
		DirWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.LastWrite |
			NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.DirectoryName;

		// Add event handlers.
		DirWatcher.Changed += new FileSystemEventHandler(OnDirWatcherEvent);
		DirWatcher.Created += new FileSystemEventHandler(OnDirWatcherEvent);
		DirWatcher.Deleted += new FileSystemEventHandler(OnDirWatcherEvent);
		DirWatcher.Renamed += new RenamedEventHandler(OnDirWatcherEvent);

		// create file list timer
		FileListTimer = new Timer();
		FileListTimer.Tick += new EventHandler(OnFileListTimer);
		FileListTimer.Interval = 500;

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	Build initial tree. My computer plus drives
	////////////////////////////////////////////////////////////////////

	private void DirectoryTreeInit()
		{
		// root node
		TreeNode RootNode = new TreeNode();
		RootNode.ImageIndex = (Int32) DispIcon.MyComputer;
		RootNode.SelectedImageIndex = (Int32) DispIcon.MyComputerSel;
		RootNode.Text = "My Computer";
		RootNode.Tag = String.Empty;
		DirectoryTree.Nodes.Add(RootNode);

		// drives child nodes
		DriveInfo[] Drives = DriveInfo.GetDrives();
        foreach(DriveInfo DI in Drives)
			{
			// ignore CD/DVD and drives not ready
			if(!DI.IsReady || DI.DriveType == DriveType.CDRom) continue;

			// build node
			TreeNode DriveNode = new TreeNode();
			DriveNode.ImageIndex = (Int32) DispIcon.DiskDrive;
			DriveNode.SelectedImageIndex = (Int32) DispIcon.DiskDriveSel;
			DriveNode.Text = String.Format("{0} [{1}]", String.IsNullOrEmpty(DI.VolumeLabel) ? "Drive" : DI.VolumeLabel, DI.Name);
			DriveNode.Tag = DI.RootDirectory.FullName;
			RootNode.Nodes.Add(DriveNode);

			// add one dummy child node
			DriveNode.Nodes.Add(new TreeNode());
			}

		// expend first level
		RootNode.Expand();

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	User double click a node in order to expand it
	////////////////////////////////////////////////////////////////////

	private void OnBeforeExpand
			(
			Object sender,
			TreeViewCancelEventArgs e
			)
		{
		// do not expand if there are no subdirectories
		if(AddSubdirectories(e.Node)) e.Cancel = true;

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	Add one level of child nodes
	////////////////////////////////////////////////////////////////////

	private Boolean AddSubdirectories
			(
			TreeNode	ParentNode
			)
		{
		// nothing to do if we have subdirectories
		if(ParentNode.Nodes[0].Tag != null) return(false);

		// remove the existing dummy record
		ParentNode.Nodes.RemoveAt(0);

		// directory info for current node
		DirectoryInfo DirInfo = new DirectoryInfo((String) ParentNode.Tag);

		// loop for all sub-directories
        foreach(DirectoryInfo Dir in DirInfo.GetDirectories())
			{
			// test for two special cases
			if(IsSpecialDirectory(Dir.Name)) continue;

			// build node
			TreeNode ChildNode = new TreeNode();
			ChildNode.ImageIndex = (Int32) DispIcon.Folder;
			ChildNode.SelectedImageIndex = (Int32) DispIcon.FolderSel;
			ChildNode.Text = Dir.Name;
			ChildNode.Tag = Dir.FullName;
			ParentNode.Nodes.Add(ChildNode);

			// add dummy grandchild record
			ChildNode.Nodes.Add(new TreeNode());
			}

		// exit
		return(ParentNode.Nodes.Count == 0);
		}

	////////////////////////////////////////////////////////////////////
	//	Directory watcher detected a change
	////////////////////////////////////////////////////////////////////

	void OnDirWatcherEvent
			(
			object sender,
			FileSystemEventArgs e
			)
		{
		// set the refresh request
		RefreshFileList = true;
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	Simulate after select directory event
	////////////////////////////////////////////////////////////////////

	void OnFileListTimer
			(
			object sender,
			EventArgs e
			)
		{
		if(RefreshFileList) PopulateFileListView(DirectoryTree.SelectedNode);
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	On after select directory
	////////////////////////////////////////////////////////////////////

	private void OnAfterSelectDirectory
			(
			object sender,
			TreeViewEventArgs e
			)
		{
		PopulateFileListView(e.Node);
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	Populate File List View
	////////////////////////////////////////////////////////////////////

	private void PopulateFileListView
			(
			TreeNode ParentNode
			)
		{
		// stop the timer
		FileListTimer.Stop();

		// stop the directory watch program
		DirWatcher.EnableRaisingEvents = false;

		// reset the refresh flag
		RefreshFileList = false;

		// clear file list
		// this routine will call resize if the file list area has scroll bars
		FileList.Items.Clear();

		// root directory
		if(ParentNode.Level == 0)
			{
			// drives child nodes
			foreach(DriveInfo Drv in DriveInfo.GetDrives())
				{
				// ignore CD/DVD and drives not ready
				if(!Drv.IsReady || Drv.DriveType == DriveType.CDRom) continue;

				// add disk drive to file list
				ListViewItem Item = new ListViewItem(
					String.Format("{0} [{1}]", String.IsNullOrEmpty(Drv.VolumeLabel) ? "Drive" : Drv.VolumeLabel, Drv.Name),
					(Int32) DispIcon.DiskDrive);
				Item.Tag = Drv;
				FileList.Items.Add(Item);
				}
			}

		// not root
		else
			{
			// directory info for current node
			DirectoryInfo DirInfo = new DirectoryInfo((String) ParentNode.Tag);

			// loop for all subdirectories
			foreach(DirectoryInfo Dir in DirInfo.GetDirectories())
				{
				// test for special cases
				if(IsSpecialDirectory(Dir.Name)) continue;

				// directory item
				ListViewItem Item = new ListViewItem(Dir.Name, (Int32) DispIcon.Folder);
				Item.Tag = Dir;
				Item.SubItems.Add(FormatFileDateTime(Dir.LastAccessTime));
				Item.SubItems.Add(FormatFileAttributes(Dir.Attributes));
				FileList.Items.Add(Item);
				}

			// loop for all files
			foreach(FileInfo File in DirInfo.GetFiles())
				{
				// file item
				ListViewItem Item = new ListViewItem(File.Name, (Int32) DispIcon.File);
				Item.Tag = File;
				Item.SubItems.Add(FormatFileDateTime(File.LastAccessTime));
				Item.SubItems.Add(FormatFileAttributes(File.Attributes));
				Item.SubItems.Add(FormatFileSize(File.Length));
				FileList.Items.Add(Item);
				}

			// start the directory watch program
			DirWatcher.Path = (String) ParentNode.Tag;
			DirWatcher.EnableRaisingEvents = true;

			// start the timer
			FileListTimer.Start();
			}

		// adjust columns
		OnFileListResize(null, null);
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	User double click a directory within file list
	////////////////////////////////////////////////////////////////////

	private void OnFileDoubleClick
			(
			object sender,
			EventArgs e
			)
		{
		// get selected list
		ListView.SelectedListViewItemCollection SelectedList = FileList.SelectedItems;

		// for double click only one is acceptable
		if(SelectedList.Count != 1) return;

		// get selected item
		ListViewItem Item = SelectedList[0];

		// path
		String Path;

		// double click on a directory
		if(Item.Tag.GetType() == typeof(DirectoryInfo))
			{
			Path = ((DirectoryInfo) Item.Tag).FullName;
			}

		// double click on a drive
		else if(Item.Tag.GetType() == typeof(DriveInfo))
			{
			Path = ((DriveInfo) Item.Tag).Name;
			}

		// double click on a file
		else
			{
			return;
			}

		// current selected node in view tree
		if(!DirectoryTree.SelectedNode.IsExpanded) DirectoryTree.SelectedNode.Expand();

		// search
		for(TreeNode ChildNode = DirectoryTree.SelectedNode.FirstNode; ChildNode != null; ChildNode = ChildNode.NextNode)
			{
			if((String) ChildNode.Tag == Path)
				{
				// NOTE: changing the selected node will activate OnAfterSelectDirectory()
				DirectoryTree.SelectedNode = ChildNode;
				break;
				}
			}

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	Select all Ctrl-A
	////////////////////////////////////////////////////////////////////

	private void OnKeyDown(object sender, KeyEventArgs e)
		{
		if(FileList.Items.Count != 0 && e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.A)
			{
			foreach(ListViewItem LVI in FileList.Items) LVI.Selected = true;
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	On add button
	////////////////////////////////////////////////////////////////////

	private void OnAddButton
			(
			object sender,
			EventArgs e
			)
		{
		// get selected list
		ListView.SelectedListViewItemCollection SelectedList = FileList.SelectedItems;

		// the list is empty
		if(SelectedList.Count == 0) return;

		// trap duplicate file errors
		try
			{
			// zip file root directory
			RootDir = (String) DirectoryTree.SelectedNode.Tag;
			RootDirPtr = RootDir.Length + 1;

			// create empty file list
			AddDir = new List<FileHeader>();

			// get all selected directories
			foreach(ListViewItem Item in SelectedList)
				{
				if(Item.Tag.GetType() == typeof(DirectoryInfo)) ProcessDirectory((DirectoryInfo) Item.Tag);
				}

			// get all selected files in the zip file root directory
			foreach(ListViewItem Item in SelectedList)
				{
				if(Item.Tag.GetType() == typeof(FileInfo))
					{
					// shortcut for file information
					FileInfo FI = (FileInfo) Item.Tag;

					// create zip directory file header record with name date time and attributes
					FileHeader FH = new FileHeader(FI.FullName.Substring(RootDirPtr), FI.LastWriteTime, FI.Attributes, 0, FI.Length);

					// find item poition in the list
					Int32 Index = AddDir.BinarySearch(FH);
					if(Index >= 0) throw new ApplicationException("Duplicate file name");

					// add to the list
					AddDir.Insert(~Index, FH);
					}
				}

			// save compression level
			CompLevel = (Int32) CompLevelUpDown.Value;

			// close screen
			DialogResult = DialogResult.OK;
			return;
			}

		// error
		catch(Exception Ex)
			{
			// error exit
			String[] ExceptionStack = ExceptionReport.GetMessageAndStack(this, Ex);
			MessageBox.Show(this, "Add files and folders error\n" + ExceptionStack[0] + "\n" + ExceptionStack[1],
				"Add Files Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
			}
		}

	////////////////////////////////////////////////////////////////////
	//	Process selected directory
	////////////////////////////////////////////////////////////////////

	private void ProcessDirectory
			(
			DirectoryInfo	DirInfo
			)
		{
		// process all subdirectories
		foreach(DirectoryInfo Dir in DirInfo.GetDirectories()) ProcessDirectory(Dir);

		// process all files in this directory
		foreach(FileInfo FI in DirInfo.GetFiles())
			{
			// create zip directory file header record with name date time and attributes
			FileHeader FH = new FileHeader(FI.FullName.Substring(RootDirPtr), FI.LastWriteTime, FI.Attributes, 0, FI.Length);

			// find item poition in the list
			Int32 Index = AddDir.BinarySearch(FH);
			if(Index >= 0) throw new ApplicationException("Duplicate file name");

			// add to the list
			AddDir.Insert(~Index, FH);
			}

		// add the directory name to the list
		// create zip directory file header record with name date time and attributes
		FileHeader DH = new FileHeader(DirInfo.FullName.Substring(RootDirPtr) + "\\", DirInfo.LastWriteTime, DirInfo.Attributes, 0, 0);

		// find item poition in the list
		Int32 DirIndex = AddDir.BinarySearch(DH);
		if(DirIndex >= 0) throw new ApplicationException("Duplicate directory name");

		// add to the list
		AddDir.Insert(~DirIndex, DH);

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Test for special system directories
	////////////////////////////////////////////////////////////////////

	private Boolean IsSpecialDirectory
			(
			String	Name
			)
		{
		// test for two special cases
		if(String.Compare(Name, "$Recycle.Bin", true) == 0) return(true);
		if(String.Compare(Name, "System Volume Information", true) == 0) return(true);
		return(false);
		}

	////////////////////////////////////////////////////////////////////
	// Format file size
	////////////////////////////////////////////////////////////////////

	private static String FormatFileSize
			(
			Int64		Size
			)
		{
		return(String.Format("{0:#,##0} KB", (Size + 999) / 1000));
		}

	////////////////////////////////////////////////////////////////////
	// Format file date and time
	////////////////////////////////////////////////////////////////////

	private static String FormatFileDateTime
			(
			DateTime	FileDate
			)
		{
		return(String.Format("{0:yyyy}/{0:MM}/{0:dd} {0:HH}:{0:mm}:{0:ss}", FileDate));
		}

	////////////////////////////////////////////////////////////////////
	// Format file attributes
	////////////////////////////////////////////////////////////////////

	private static String FormatFileAttributes
			(
			FileAttributes Attr
			)
		{
		return(String.Format("{0}{1}{2}",
			(Attr & FileAttributes.System) != 0 ? "S" : "",
			(Attr & FileAttributes.Hidden) != 0 ? "H" : "",
			(Attr & FileAttributes.ReadOnly) != 0 ? "R" : ""));
		}

	////////////////////////////////////////////////////////////////////
	//	On Resize
	////////////////////////////////////////////////////////////////////

	private void OnResize
			(
			object sender,
			EventArgs e
			)
		{
		// protect against minimize button
		if(ClientSize.Width == 0) return;

		// button group
		ButtonsGroupBox.Left = (ClientSize.Width - ButtonsGroupBox.Width) / 2;
		ButtonsGroupBox.Top = ClientSize.Height - ButtonsGroupBox.Height;

		// split container
		FileArea.Left = 0;
		FileArea.Width = ClientSize.Width;
		FileArea.Top = 0;
		FileArea.Height = ButtonsGroupBox.Top;

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	On Resize
	////////////////////////////////////////////////////////////////////

	private void OnFileAreaResize
			(
			object sender,
			EventArgs e
			)
		{
		// change splitter line
		FileArea.SplitterDistance = FileArea.Width / 3;

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Resize panel 1
	////////////////////////////////////////////////////////////////////

	private void OnFileAreaPanel1Resize
			(
			object sender,
			EventArgs e
			)
		{
		DirectoryTree.Left = 0;
		DirectoryTree.Width = FileArea.Panel1.Width - 1;
		DirectoryTree.Top = 0;
		DirectoryTree.Height = FileArea.Panel1.Height;
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Resize panel 2
	////////////////////////////////////////////////////////////////////

	private void OnFileAreaPanel2Resize
			(
			object sender,
			EventArgs e
			)
		{
		FileList.Left = 1;
		FileList.Width = FileArea.Panel2.Width - 1;
		FileList.Top = 0;
		FileList.Height = FileArea.Panel2.Height;
		return;
		}

	////////////////////////////////////////////////////////////////////
	// On resize file list
	////////////////////////////////////////////////////////////////////

	private void OnFileListResize
			(
			object sender,
			EventArgs e
			)
		{
		if(ResizeLock) return;
		ResizeLock = true;

		// get graphics object
		Graphics GR = CreateGraphics();

		// file list font
		Font ListFont = FileList.Font;

		// file name
		Int32 MinWidth = (Int32) Math.Ceiling(GR.MeasureString("File Name0000", ListFont).Width);

		// Date and time width
		FileList.Columns[1].Width = (Int32) Math.Ceiling(GR.MeasureString("2000/12/12 00:00:000", ListFont).Width);

		// attribute
		FileList.Columns[2].Width = (Int32) Math.Ceiling(GR.MeasureString("Attr.0", ListFont).Width);

		// size
		FileList.Columns[3].Width = (Int32) Math.Ceiling(GR.MeasureString("0,000,000 KB", ListFont).Width);

		// list is empty
		// Note: C# has a bug.
		// When FileList.Items.Clear() is executed, the system removes all items,
		// calls the resize routine while the count is unchanged.
		if(FileList.Items.Count == 0 || FileList.Items[0] == null)
			{
			FileList.Columns[0].Width = MinWidth;
			ResizeLock = false;
			return;
			}

		// check file name
		Int32 Space = FileList.ClientRectangle.Width - FileList.Columns[1].Width - FileList.Columns[2].Width - FileList.Columns[3].Width;
		if(Space < MinWidth) Space = MinWidth;
		FileList.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
		if(FileList.Columns[0].Width > Space) FileList.Columns[0].Width = Space;
		ResizeLock = false;
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	On Closing
	////////////////////////////////////////////////////////////////////

	private void OnClosing
			(
			object sender,
			FormClosingEventArgs e
			)
		{
		return;
		}
	}
}