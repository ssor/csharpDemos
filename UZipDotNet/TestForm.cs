/////////////////////////////////////////////////////////////////////
//
//	UZipDotNet
//	ZIP File processing
//
//	ZLibTestForm.cs
//	Dialog box allowing the user to test compression and
//	decompression of one file. The user selects a file, the
//	program will compress it using DeflateZLib, DeflateMethod
//	and DeflateTree classes. Immediately after the compression
//	is done the program will decompress the file using InflateZLib,
//	InflateMethod and InflateTree classes. It is a very effective
//	way to test the deflate/inflate software.
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace UZipDotNet
{
public partial class TestForm : Form
	{
	private Int32		CompLevel;
	private String		InputFileName;
	private String		CompFileName;
	private String		DecompFileName;
	private UInt32		InputFileLen;
	private UInt32		CompFileLen;
	private UInt32		DecompFileLen;
	private Int32		CompTime;
	private Int32		DecompTime;
	private String[]	ExceptionStack;

	////////////////////////////////////////////////////////////////////
	// Constructor
	////////////////////////////////////////////////////////////////////

	public TestForm()
		{
		InitializeComponent();
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Initialization
	////////////////////////////////////////////////////////////////////

	private void OnLoad
			(
			object sender,
			EventArgs e
			)
		{
		CompLevelUpDown.Value = ProgramState.State.CompressionLevel;
		if(ProgramState.State.FileType == (Int32) FileTypeCode.ZLib) ZLibRadioButton.Checked = true;
		else if(ProgramState.State.FileType == (Int32) FileTypeCode.NoHeader) NoHeaderRadioButton.Checked = true;
		CompareFilesCheckBox.Checked = ProgramState.State.CompareFiles;
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Test Single File
	////////////////////////////////////////////////////////////////////

	private void OnTest
			(
			object sender,
			EventArgs e
			)
		{
		OpenFileDialog OFD = new OpenFileDialog();
		OFD.Filter = "Test files (*.*)|*.*";
	    OFD.RestoreDirectory = true;
		if(OFD.ShowDialog() != DialogResult.OK) return;
		InputFileName = OFD.FileName;
		Trace.Write(InputFileName);

		// clear screen
		InputFileLen = 0;
		InputFileLenLabel.Text = String.Empty;
		InputFileLenLabel.Update();

		CompFileLen = 0;
		CompFileLenLabel.Text = String.Empty;
		CompFileLenLabel.Update();

		RatioLabel.Text = String.Empty;
		RatioLabel.Update();

		CompTime = 0;
		CompTimeLabel.Text = String.Empty;
		CompTimeLabel.Update();

		DecompFileLen = 0;
		DecompFileLenLabel.Text = String.Empty;
		DecompFileLenLabel.Update();

		DecompTime = 0;
		DecompTimeLabel.Text = String.Empty;
		DecompTimeLabel.Update();

		CompareTestLabel.Text = String.Empty;
		CompareTestLabel.Update();

		// name without path
		Int32 Ptr = InputFileName.LastIndexOf('\\');
		String NameOnly = Ptr >= 0 ? InputFileName.Substring(Ptr + 1) : InputFileName;
		InputFileNameLabel.Text = NameOnly;
		InputFileNameLabel.Update();

		// compressed file name (no extension yet)
		Ptr = NameOnly.LastIndexOf('.');
		CompFileName = NameOnly.Substring(0, Ptr);

		// output file
		DecompFileName = CompFileName + "Decomp" + NameOnly.Substring(Ptr);;

		// make a copy of current program state
		ProgramState NewState = new ProgramState(ProgramState.State);

		// get info
		NewState.CompressionLevel = CompLevel = (Int32) CompLevelUpDown.Value;
		if(ZipRadioButton.Checked) NewState.FileType = (Int32) FileTypeCode.Zip;
		else if(ZLibRadioButton.Checked) NewState.FileType = (Int32) FileTypeCode.ZLib;
		else NewState.FileType = (Int32) FileTypeCode.NoHeader;
		NewState.CompareFiles = CompareFilesCheckBox.Checked;

		// save program state
		ProgramState.SaveState(NewState);

		// start compression
		TestButton.Enabled = false;
		ExitButton.Enabled = false;
		Int32 Result = 0;

		switch((FileTypeCode) NewState.FileType)
			{
			// deflate zip
			case FileTypeCode.Zip:
				Result = TestZip();
				break;

			// deflate zlib
			case FileTypeCode.ZLib:
				Result = TestZLib();
				break;

			// deflate no header
			case FileTypeCode.NoHeader:
				Result = TestNoHeader();
				break;
			}


		// display compression results
		if(Result == 1)
			{
			MessageBox.Show("Compression Error\n" + ExceptionStack[0] + "\n" + ExceptionStack[1]);
			}
		else
			{
			InputFileLenLabel.Text = String.Format("{0:#,###}", InputFileLen);
			InputFileLenLabel.Update();
			Trace.Write("Input file size: " + InputFileLenLabel.Text);

			CompFileLenLabel.Text = String.Format("{0:#,###}", CompFileLen);
			CompFileLenLabel.Update();
			Trace.Write("Compressed file size: " + CompFileLenLabel.Text);

			if(InputFileLen != 0)
				{
				RatioLabel.Text = String.Format("{0:#,##0.00}%", 100.00 * (Double) CompFileLen / (Double) InputFileLen);
				RatioLabel.Update();
				Trace.Write("Compression ratio: " + RatioLabel.Text);
				}

			CompTimeLabel.Text = String.Format("{0:#,##0.00}", (Double) CompTime / 1000.0);
			CompTimeLabel.Update();
			Trace.Write("Compress Time: " + CompTimeLabel.Text);
		
			// display decompression results
			if(Result == 2)
				{
				MessageBox.Show("Decompression Error\n" + ExceptionStack[0] + "\n" + ExceptionStack[1]);
				}
			else if(Result == 3)
				{
				MessageBox.Show("No Header cannot decompress Stored file");
				}
			else
				{
				DecompFileLenLabel.Text = String.Format("{0:#,###}", DecompFileLen);
				DecompFileLenLabel.Update();
				Trace.Write("Decompressed file size: " + DecompFileLenLabel.Text);

				DecompTimeLabel.Text = String.Format("{0:#,##0.00}", (Double) DecompTime / 1000.0);
				DecompTimeLabel.Update();
				Trace.Write("Elapse Time: " + DecompTimeLabel.Text);
				}
			}

		// compare input file to decompress file
		if(Result == 0 && CompareFilesCheckBox.Checked)
			{
			if(CompareFiles())
				{
				MessageBox.Show("Compare file Error\n" + ExceptionStack[0] + "\n" + ExceptionStack[1]);
				CompareTestLabel.Text = "ERROR";
				}
			else
				{
				CompareTestLabel.Text = "OK";
				}
			}
		else
			{
			CompareTestLabel.Text = "Not Done";
			}

		TestButton.Enabled = true;
		ExitButton.Enabled = true;
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Test Single File
	////////////////////////////////////////////////////////////////////

	private Int32 TestZip()
		{
		// add extension to compressed file name
		CompFileName += ".zip";

		// create compressiom object
		DeflateZipFile Def = new DeflateZipFile(CompLevel);

		// create empty zip file
		if(Def.CreateArchive(CompFileName))
			{
			// save exception stack on error
			ExceptionStack = Def.ExceptionStack;
			return(1);
			}

		// split the input file name to path and name
		String FileName;
		Int32 Ptr = InputFileName.LastIndexOf('\\');
		FileName = Ptr < 0 ? InputFileName : InputFileName.Substring(Ptr + 1);

		// save start time
		Int32 StartTime = Environment.TickCount;

		// compress file
		if(Def.Compress(InputFileName, FileName))
			{
			// save exception stack on error
			ExceptionStack = Def.ExceptionStack;
			return(1);
			}

		// save compression elapse time
		CompTime = Environment.TickCount - StartTime;

		// save input and compressed file length
		InputFileLen = Def.ReadTotal;
		CompFileLen = Def.WriteTotal;

		// save archive
		if(Def.SaveArchive())
			{
			// save exception stack on error
			ExceptionStack = Def.ExceptionStack;
			return(1);
			}

		// save start time
		StartTime = Environment.TickCount;

		// create decompression object
		InflateZipFile Inf = new InflateZipFile();

		// open the zip file
		if(Inf.OpenZipFile(CompFileName))
			{
			// save exception stack on error
			ExceptionStack = Inf.ExceptionStack;
			return(2);
			}

		// decompress the file
		if(Inf.DecompressZipFile(Inf.ZipDir[0], null, DecompFileName, false, true))
			{
			// save exception stack on error
			ExceptionStack = Inf.ExceptionStack;
			return(2);
			}

		// save decompression elapse time
		DecompTime = Environment.TickCount - StartTime;

		// save restored file length
		DecompFileLen = Inf.WriteTotal;

		// close the zip file
		Inf.CloseZipFile();

		// successful exit
		return(0);
		}

	////////////////////////////////////////////////////////////////////
	// Test Single File
	////////////////////////////////////////////////////////////////////

	private Int32 TestZLib()
		{
		// add extension to compressed file name
		CompFileName += ".zlib";

		// create compressiom object
		DeflateZLib Def = new DeflateZLib(CompLevel);

		// save start time
		Int32 StartTime = Environment.TickCount;

		// compress file
		if(Def.Compress(InputFileName, CompFileName))
			{
			// save exception stack on error
			ExceptionStack = Def.ExceptionStack;
			return(1);
			}

		// save compression elapse time
		CompTime = Environment.TickCount - StartTime;

		// save input and compressed file length
		InputFileLen = Def.ReadTotal;
		CompFileLen = Def.WriteTotal;

		// save start time
		StartTime = Environment.TickCount;

		// create decompression object
		InflateZLib Inf = new InflateZLib();

		// decompress the file
		if(Inf.Decompress(CompFileName, DecompFileName))
			{
			// save exception stack on error
			ExceptionStack = Inf.ExceptionStack;
			return(2);
			}

		// save decompression elapse time
		DecompTime = Environment.TickCount - StartTime;

		// save restored file length
		DecompFileLen = Inf.WriteTotal;

		// successful exit
		return(0);
		}

	////////////////////////////////////////////////////////////////////
	// Test Single File
	////////////////////////////////////////////////////////////////////

	private Int32 TestNoHeader()
		{
		// add extension to compressed file name
		CompFileName += ".def";

		// create compressiom object
		DeflateNoHeader Def = new DeflateNoHeader(CompLevel);

		// save start time
		Int32 StartTime = Environment.TickCount;

		// compress file
		if(Def.Compress(InputFileName, CompFileName))
			{
			// save exception stack on error
			ExceptionStack = Def.ExceptionStack;
			return(1);
			}

		// save compression elapse time
		CompTime = Environment.TickCount - StartTime;

		// save input and compressed file length
		InputFileLen = Def.ReadTotal;
		CompFileLen = Def.WriteTotal;

		// no header cannot decompress stored file
		if(Def.CompFunction == DeflateMethod.CompFunc.Stored) return(3);

		// save start time
		StartTime = Environment.TickCount;

		// create decompression object
		InflateNoHeader Inf = new InflateNoHeader();

		// decompress the file
		if(Inf.Decompress(CompFileName, DecompFileName))
			{
			// save exception stack on error
			ExceptionStack = Inf.ExceptionStack;
			return(2);
			}

		// save decompression elapse time
		DecompTime = Environment.TickCount - StartTime;

		// save restored file length
		DecompFileLen = Inf.WriteTotal;

		// successful exit
		return(0);
		}

	////////////////////////////////////////////////////////////////////
	// Compare Files
	////////////////////////////////////////////////////////////////////

	private Boolean CompareFiles()
		{
		FileStream		Stream1, Stream2;
		BinaryReader	File1 = null, File2 = null;
		Byte[]			Buffer1, Buffer2;
 
		// trap errors
		try
			{
			// open file1 for reading
			Stream1 = new FileStream(InputFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
		
			// convert stream to binary reader
			File1 = new BinaryReader(Stream1, Encoding.UTF8);

			// open file2 for reading
			Stream2 = new FileStream(DecompFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
		
			// convert stream to binary reader
			File2 = new BinaryReader(Stream2, Encoding.UTF8);

			// length must be the same
			if(Stream1.Length != Stream2.Length) throw new ApplicationException("Compare files failed. Files lengths not the same");

			// allocate buffer
			Buffer1 = new Byte[32 * 1024];
			Buffer2 = new Byte[32 * 1024];

			// compare
			Int64 Len = Stream1.Length;
			while(Len > 0)
				{
				Int32 Count = Len < (Int64) Buffer1.Length ? (Int32) Len : Buffer1.Length;
				Int32 Act1 = File1.Read(Buffer1, 0, Count);
				if(Act1 != Count) throw new ApplicationException("Compare files failed. Reading file1 failed");
				Int32 Act2 = File2.Read(Buffer2, 0, Count);
				if(Act2 != Count) throw new ApplicationException("Compare files failed. Reading file2 failed");
				Int32 Index;
				for(Index = 0; Index < Count && Buffer1[Index] == Buffer2[Index]; Index++);
				if(Index != Count) throw new ApplicationException("Compare files failed. The two files are not the same");
				Len -= Count;
				}
			
			File1.Close();
			File2.Close();

			// successful exit
			return(false);
			}

		catch(Exception Ex)
			{
			// make sure read file is closed
			File1.Close();
			File2.Close();

			// error exit
			ExceptionStack = ExceptionReport.GetMessageAndStack(this, Ex);
			return(true);
			}

		}
	}
}