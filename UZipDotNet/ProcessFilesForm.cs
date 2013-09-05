/////////////////////////////////////////////////////////////////////
//
//	UZipDotNet
//	ZIP File processing
//
//	ProcessFilesForm.cs
//	Windows form allowing the user to view the progress of either
//	compression or decompression to and from ZIP archive.
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
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UZipDotNet
{
public partial class ProcessFilesForm : Form
	{
	public List<FileHeader>	ZipDir;
	public InflateZipFile		Inflate;
	public DeflateZipFile		Deflate;
	public Boolean				UpdateMode;
	public String				RootDir;
	public Int32				CompLevel;

	private Int32				DirIndex;
	private Timer				ProcessTimer;
	private Boolean				AbortFlag;
	private Int32				ErrorCount;

	/////////////////////////////////////////////////////////////////
	// Constructor
	/////////////////////////////////////////////////////////////////

	public ProcessFilesForm()
		{
		InitializeComponent();
		return;
		}

	/////////////////////////////////////////////////////////////////
	// On load
	/////////////////////////////////////////////////////////////////

	private void OnLoad
			(
			object sender,
			EventArgs e
			)
		{
		// reset extract index
		DirIndex = 0;
		ErrorCount = 0;
		AbortFlag = false;

		// create extract timer
		ProcessTimer = new Timer();
		ProcessTimer.Tick += new EventHandler(OnExtractTimer);
		ProcessTimer.Interval = 10;
		ProcessTimer.Start();
		return;
		}

	/////////////////////////////////////////////////////////////////
	// Extract Timer
	/////////////////////////////////////////////////////////////////

	private void OnExtractTimer
			(
			object sender,
			EventArgs e
			)
		{
		// stop the timer
		ProcessTimer.Stop();

		// test for end
		if(AbortFlag || DirIndex == ZipDir.Count)
			{
			ExitButton.Text = "Exit";
			ProcessTimer.Dispose();
			ProcessTimer = null;
			return;
			}

		// display current file
		DispStatus(ZipDir[DirIndex].FileName);

		// update file
		if(UpdateMode)
			{
			if(UpdateFile()) ErrorCount++;
			}
		// extract file
		else
			{
			if(ExtractFile()) ErrorCount++;
			}

		// update index
		DirIndex++;

		// display progress
		ProgressLabel.Text = String.Format("{0}/{1}", DirIndex, ZipDir.Count);
		if(ErrorCount > 0) ErrorLabel.Text = ErrorCount.ToString();

		// restart the timer
		ProcessTimer.Start();
		return;
		}

	/////////////////////////////////////////////////////////////////
	// Extract file
	/////////////////////////////////////////////////////////////////

	private Boolean ExtractFile()
		{
		// translate to file header
		FileHeader FH = ZipDir[DirIndex];

		// test skip read only
		if(ProgramState.State.SkipReadOnly && (FH.FileAttr & FileAttributes.ReadOnly) != 0)
			{
			AppendStatus("Skip read only");
			return(true);
			}

		// test skip hidden file
		if(ProgramState.State.SkipHidden && (FH.FileAttr & FileAttributes.Hidden) != 0)
			{
			AppendStatus("Skip hidden file");
			return(true);
			}

		// test skip system file
		if(ProgramState.State.SkipSystem && (FH.FileAttr & FileAttributes.System) != 0)
			{
			AppendStatus("Skip system file");
			return(true);
			}

		// file name
		String FileName = (FH.Path && ProgramState.State.IgnoreFolderInfo) ?
			FH.FileName.Substring(FH.FileName.LastIndexOf('\\') + 1) : FH.FileName;

		// full ourput file name
		String OutputFile = ProgramState.State.ExtractToFolder + "\\" + FileName;

		// path part of file name
		Int32 Ptr = OutputFile.LastIndexOf('\\');

		// we have a path, make sure path exists
		if(Ptr >= 0)
			{
			// skip older files
			if(ProgramState.State.SkipOlder)
				{
				// exiting file
				DateTime ExistingFileTime = File.GetLastWriteTime(OutputFile);

				// compressed file
				// convert dos file date and time to DateTime format
				DateTime CompFileTime = new DateTime(1980 + ((FH.FileDate >> 9) & 0x7f), (FH.FileDate >> 5) & 0xf, FH.FileDate & 0x1f,
					(FH.FileTime >> 11) & 0x1f, (FH.FileTime >> 5) & 0x3f, 2 * (FH.FileTime & 0x1f));

				// compare times
				if(CompFileTime < ExistingFileTime)
					{
					AppendStatus("Skip too old");
					return(true);
					}
				}

			// make sure directory exists
			if(!Directory.Exists(OutputFile.Substring(0, Ptr)))
				{
				// make new folder
				try
					{
					Directory.CreateDirectory(OutputFile.Substring(0, Ptr));
					}
				catch
					{
					AppendStatus("Path Error");
					return(true);
					}
				}

			// directory
			if((FH.FileAttr & FileAttributes.Directory) != 0)
				{
				AppendStatus("Dir-OK");
				return(false);
				}
			}

		// test if file exists
		if(File.Exists(OutputFile))
			{
			// no overwrite
			if(ProgramState.State.Overwrite == (Int32) OverwriteFiles.No)
				{
				AppendStatus("No overwrite");
				return(true);
				}

			// ask overwrite permission
			if(ProgramState.State.Overwrite == (Int32) OverwriteFiles.Ask)
				{
				if(MessageBox.Show(this, "Do you want to overwrite: " + OutputFile + " ?\n(Press Cancel to abort extraction)", "Overwrite warning",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
					{
					AppendStatus("No overwrite");
					return(true);
					}
				}

			// check for read only file
			if(!ProgramState.State.SkipReadOnly)
				{
				FileAttributes Attr = File.GetAttributes(OutputFile);
				if((Attr & FileAttributes.ReadOnly) != 0)
					{
					try
						{
						File.SetAttributes(OutputFile, Attr & ~FileAttributes.ReadOnly);
						}
					catch{}
					}
				}

			// delete file
			try
				{
				File.Delete(OutputFile);
				}
			catch
				{
				AppendStatus("Overwrite failed");
				return(true);
				}
			}

		// decompress file
		if(Inflate.DecompressZipFile(FH, null, OutputFile, true, true))
			{
			Trace.Write("Decompression Error\n" + Inflate.ExceptionStack[0] + "\n" + Inflate.ExceptionStack[1]);
			AppendStatus("Decompression failed [" + Deflate.ExceptionStack[0] + "]");
			return(true);
			}

		// successful return
		AppendStatus("File-OK");
		return(false);
		}

	/////////////////////////////////////////////////////////////////
	// Update file
	/////////////////////////////////////////////////////////////////

	private Boolean UpdateFile()
		{
		// translate to file header
		FileHeader FH = ZipDir[DirIndex];

		String FullFileName = RootDir.EndsWith("\\") ? RootDir + FH.FileName : RootDir + "\\" + FH.FileName; 

		// save directory name in zip file
		if((FH.FileAttr & FileAttributes.Directory) != 0)
			{
			if(Deflate.SaveDirectoryPath(FullFileName, FH.FileName))
				{
				Trace.Write("Save directory path error\n" + Deflate.ExceptionStack[0] + "\n" + Deflate.ExceptionStack[1]);
				AppendStatus("Save directory failed [" + Deflate.ExceptionStack[0] + "]");
				return(true);
				}
			}

		// compress file
		else
			{
			Deflate.CompressionLevel = CompLevel;
			if(Deflate.Compress(FullFileName, FH.FileName))
				{
				Trace.Write("Compression Error\n" + Deflate.ExceptionStack[0] + "\n" + Deflate.ExceptionStack[1]);
				AppendStatus("Compression failed [" + Deflate.ExceptionStack[0] + "]");
				return(true);
				}
			}

		// successful return
		AppendStatus("OK");
		return(false);
		}

	////////////////////////////////////////////////////////////////////
	// Display Status
	////////////////////////////////////////////////////////////////////

	private void DispStatus
			(
			String		Message
			)
		{
		ListBox.SelectedIndex = ListBox.Items.Add(Message);
		ListBox.ClearSelected();
		ListBox.Refresh();
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Append Status
	////////////////////////////////////////////////////////////////////

	private void AppendStatus
			(
			String		Message
			)
		{
		ListBox.Items[ListBox.Items.Count - 1] += " - " + Message;
		return;
		}

	/////////////////////////////////////////////////////////////////
	// Abort/Exit button
	/////////////////////////////////////////////////////////////////

	private void OnAbort
			(
			object sender,
			EventArgs e
			)
		{
		// test for end
		if(AbortFlag || DirIndex == ZipDir.Count)
			{
			Close();
			}
		else
			{
			AbortFlag = true;
			}
		return;
		}

	/////////////////////////////////////////////////////////////////
	// Resize screen
	/////////////////////////////////////////////////////////////////

	private void OnResize
			(
			object sender,
			EventArgs e
			)
		{
		// protect against minimize button
		if(ClientSize.Width == 0) return;

		// exit button
		ButtonsGroupBox.Left = ClientSize.Width / 2 - ButtonsGroupBox.Width / 2;
		ButtonsGroupBox.Top = ClientSize.Height - ButtonsGroupBox.Height;

		// status list box
		ListBox.Width = ClientSize.Width - 2 * ListBox.Left;
		ListBox.Height = ButtonsGroupBox.Top - ListBox.Top;

		// exit
		return;
		}
	}
}