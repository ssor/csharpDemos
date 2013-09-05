/////////////////////////////////////////////////////////////////////
//
//	UZipDotNet
//	ZIP File processing
//
//	ExtractSelectionForm.cs
//	Windows form allowing the user to select compressed files for
//	extraction from the open ZIP archive.
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

namespace UZipDotNet
{
public enum DispIcon
	{
	MyComputer,
	MyComputerSel,
	DiskDrive,
	DiskDriveSel,
	Folder,
	FolderSel,
	File,
	}

public partial class ExtractSelectionForm : Form
	{
	public Int32		ArchiveFiles;
	public UInt32		ArchiveSize;
	public Int32		SelectionFiles;
	public UInt32		SelectionSize;
	
	private Timer		LoadTimer;
	private Boolean		Lock;

	////////////////////////////////////////////////////////////////////
	//	Constructor
	////////////////////////////////////////////////////////////////////

	public ExtractSelectionForm()
		{
		InitializeComponent();
		return;
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
		// force resize
		OnResize(this, null);

		// start load timer to allow the screen to be displayed before directory tree is loaded
		LoadTimer = new Timer();
		LoadTimer.Tick += new EventHandler(OnLoadTimer);
		LoadTimer.Interval = 100;
		LoadTimer.Start();

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	On Load Timer
	////////////////////////////////////////////////////////////////////

	private void OnLoadTimer
			(
			object sender,
			EventArgs e
			)
		{
		// remove load timer
		LoadTimer.Stop();
		LoadTimer.Dispose();
		LoadTimer = null;

		// create directory tree
		CreateDirectoryTree();

		// load directory tree
		LoadDirectoryTree();

		// set screen
		SetScreenOptions();

		Update();

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	Create directory tree
	////////////////////////////////////////////////////////////////////

	private void CreateDirectoryTree()
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

		// other options
		DirectoryTree.ShowLines = true;
		DirectoryTree.ShowRootLines = false;
		DirectoryTree.Scrollable = true;
		DirectoryTree.BorderStyle = BorderStyle.FixedSingle;
		DirectoryTree.ShowPlusMinus = true;
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	Load directory tree
	////////////////////////////////////////////////////////////////////

	private void LoadDirectoryTree()
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
	//	Set screen options from program state
	////////////////////////////////////////////////////////////////////

	private void SetScreenOptions()
		{
		// set screen
		Lock = true;
		FolderTextBox.Text = ProgramState.State.ExtractToFolder;
		Lock = false;
		if(!ProgramState.State.ExtractAll || SelectionFiles > 1) ExtractSelectionRadioButton.Checked = true;
		if(ProgramState.State.Overwrite == (Int32) OverwriteFiles.No) OverwriteNoRadioButton.Checked = true;
		else if(ProgramState.State.Overwrite == (Int32) OverwriteFiles.Ask) OverwriteAskRadioButton.Checked = true;
		IgnoreFolderCheckBox.Checked = ProgramState.State.IgnoreFolderInfo;
		SkipReadOnlyCheckBox.Checked = ProgramState.State.SkipReadOnly;
		SkipHiddenCheckBox.Checked = ProgramState.State.SkipHidden;
		SkipSystemCheckBox.Checked = ProgramState.State.SkipSystem;
		SkipOlderCheckBox.Checked = ProgramState.State.SkipOlder;

		// display number of files selected by the user
		AllFilesLabel.Text = ArchiveFiles.ToString();
		AllSizeLabel.Text = ArchiveSize.ToString("#,##0");
		SelFilesLabel.Text = SelectionFiles.ToString();
		SelSizeLabel.Text = SelectionSize.ToString("#,##0");

		// disable extract selection option
		if(SelectionFiles == 0) ExtractSelectionRadioButton.Enabled = false;

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
	//	Extract to folder text is changing
	////////////////////////////////////////////////////////////////////

	private void OnFolderTextChanged
			(
			object sender,
			EventArgs e
			)
		{
		// start load timer to allow the screen to be displayed before directory tree is loaded
		if(LoadTimer == null)
			{
			LoadTimer = new Timer();
			LoadTimer.Tick += new EventHandler(OnSearchForPath);
			LoadTimer.Interval = 10;
			LoadTimer.Start();
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	On Load Timer
	////////////////////////////////////////////////////////////////////

	private void OnSearchForPath
			(
			object sender,
			EventArgs e
			)
		{
		// remove load timer
		LoadTimer.Stop();
		LoadTimer.Dispose();
		LoadTimer = null;

		// adjust directory tree
		SearchForPath(DirectoryTree.Nodes[0], FolderTextBox.Text);

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	//	Search for path
	////////////////////////////////////////////////////////////////////

	private void SearchForPath
			(
			TreeNode	ParentNode,
			String		Path
			)
		{
		// loop for all nodes of the parent node
		for(Int32 N = 0; N < ParentNode.Nodes.Count; N++)
			{
			TreeNode ChildNode = ParentNode.Nodes[N];
			if(Path.StartsWith((String) ChildNode.Tag, true, null))
				{
				// NOTE: changing the selected node will activate OnAfterSelectDirectory()
				Lock = true;
				DirectoryTree.SelectedNode = ChildNode;
				Lock = false;

				// this node has children or needs to add subdirectories
				if(ChildNode.Nodes.Count != 0 && !AddSubdirectories(ChildNode))
					{
					// current selected node in view tree
					if(!DirectoryTree.SelectedNode.IsExpanded) DirectoryTree.SelectedNode.Expand();

					// search again
					SearchForPath(ChildNode, Path);
					}
				break;
				}
			}
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
	//	On after select directory
	////////////////////////////////////////////////////////////////////

	private void OnAfterSelectDirectory
			(
			object sender,
			TreeViewEventArgs e
			)
		{
		if(!Lock) FolderTextBox.Text = (String) e.Node.Tag;
		return;
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

		// margin
		Int32 Margin = ExtractButton.Height / 2;

		// extract button
		ExtractButton.Left = ClientSize.Width / 2 - ExtractButton.Width - Margin;
		ExtractButton.Top = ClientSize.Height - ExtractButton.Height - Margin;

		// cancel button
		ExitButton.Left = ClientSize.Width / 2 + Margin;
		ExitButton.Top = ClientSize.Height - ExitButton.Height - Margin;

		// folder area
		DirectoryTree.Width = ClientSize.Width - DirectoryTree.Left - Margin;
		DirectoryTree.Height = ExitButton.Top - DirectoryTree.Top - Margin;

		// exit
		return;
		}

	/////////////////////////////////////////////////////////////////
	// On Closing
	/////////////////////////////////////////////////////////////////

	private void OnClosing
			(
			object sender,
			FormClosingEventArgs e
			)
		{
		// exit
		if(DialogResult == DialogResult.OK && TestData()) e.Cancel = true;
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Test data
	////////////////////////////////////////////////////////////////////

	private Boolean TestData()
		{
		// make a copy of current program state
		ProgramState NewState = new ProgramState(ProgramState.State);

		// get folder name
		NewState.ExtractToFolder = FolderTextBox.Text.Trim();

		// empty name
		if(NewState.ExtractToFolder.Length == 0)
			{
			MessageBox.Show(this, "Extract to folder is empty", "Extract Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return(true);
			}

		// single character \ or :
		if(NewState.ExtractToFolder.Length == 1 && (NewState.ExtractToFolder[0] == '\\' || NewState.ExtractToFolder[0] == ':'))
			{
			MessageBox.Show(this, "Extract to folder name is invalid", "Extract Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return(true);
			}

		// make sure directory exists
		if(!Directory.Exists(NewState.ExtractToFolder))
			{
			if(MessageBox.Show(this, "Extract to folder does not exist\n" +
									"Do you want to create a new folder?", "Extract Folder",
									MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return(true);

			// make new folder
			try
				{
				Directory.CreateDirectory(NewState.ExtractToFolder);
				}
			catch(Exception Ex)
				{
				MessageBox.Show(this, "Create new directory failed\n" + Ex.Message,
					"New Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return(true);
				}
			}

		// extract options
		NewState.ExtractAll = ExtractAllRadioButton.Checked;
		if(OverwriteYesRadioButton.Checked) NewState.Overwrite = (Int32) OverwriteFiles.Yes;
		else if(OverwriteNoRadioButton.Checked) NewState.Overwrite = (Int32) OverwriteFiles.No;
		else NewState.Overwrite = (Int32) OverwriteFiles.Ask;
		NewState.IgnoreFolderInfo = IgnoreFolderCheckBox.Checked;
		NewState.SkipReadOnly = SkipReadOnlyCheckBox.Checked;
		NewState.SkipHidden = SkipHiddenCheckBox.Checked;
		NewState.SkipSystem = SkipSystemCheckBox.Checked;
		NewState.SkipOlder = SkipOlderCheckBox.Checked;

		// save program state
		ProgramState.SaveState(NewState);

		// successful return
		return(false);
		}
	}
}