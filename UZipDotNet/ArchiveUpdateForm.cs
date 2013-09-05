/////////////////////////////////////////////////////////////////////
//
//	UZipDotNet
//	ZIP File processing
//
//	ArchiveUpdateForm.cs
//	Dialog box. This warning dialog box will be displayed when
//	a ZIP archive is open for extraction and the user wants to
//	update the archive. It gives the user a choice to save the
//	existing archive in the same directory or a copy in the
//	recycle bin.
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
using System.Windows.Forms;
using System.IO;
using MSFileIO = Microsoft.VisualBasic.FileIO;

namespace UZipDotNet
{
public partial class ArchiveUpdateForm : Form
	{
	public InflateZipFile	Inflate;

	public ArchiveUpdateForm()
		{
		InitializeComponent();
		return;
		}

	private void OnClosing
			(
			object sender,
			FormClosingEventArgs e
			)
		{
		// user pressed cancel
		if(DialogResult == DialogResult.Cancel) return;

		// user does not want to save the file
		if(NoSaveRadioButton.Checked)
			{
			// close zip file
			Inflate.CloseZipFile();
			return;
			}

		// create backup copy name
		Int32 Ptr = Inflate.ArchiveName.LastIndexOf('.');
		String BackupName;
		for(Int32 No = 0;; No++)
			{
			String CopyMsg = No == 0 ? " - Copy" : String.Format(" - Copy{0}", No);
			BackupName = Inflate.ArchiveName.Insert(Ptr, CopyMsg);
			if(!File.Exists(BackupName)) break;
			}

		try
			{
			// make a copy of the open archive file
			File.Copy(Inflate.ArchiveName, BackupName);
			}
		catch(Exception Ex)
			{
			// copy failed
			MessageBox.Show(this, "Backup copy failed\n" + Ex.Message,
				"Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			e.Cancel = true;
			return;
			}

		// close zip file
		Inflate.CloseZipFile();

		// save to recycle bin
		if(RecycleBinRadioButton.Checked)
			{
			try
				{
				// save file to recycle bin
				MSFileIO.FileSystem.DeleteFile(Inflate.ArchiveName, MSFileIO.UIOption.OnlyErrorDialogs, MSFileIO.RecycleOption.SendToRecycleBin);
				}
			catch(Exception Ex)
				{
				// save file to recycle bin failed
				MessageBox.Show(this, "Send to recycle bin failed\n" +
						"File saved as: " + BackupName + "\n" + Ex.Message,
					"Send to Recycle Bin Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
				}

			try
				{
				// rename the backup copy to original name
				File.Move(BackupName, Inflate.ArchiveName);
				}
			catch(Exception Ex)
				{
				// rename failed
				MessageBox.Show(this, "Rename failed\n" + Ex.Message,
					"Rename Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				throw new ApplicationException("Archive file is not available\n" + Inflate.ArchiveName);
				}
			}

		// successful return with DialogResult.OK
		return;
		}
	}
}