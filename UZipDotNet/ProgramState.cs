/////////////////////////////////////////////////////////////////////
//
//	UZipDotNet
//	ZIP File processing
//
//	ProgramState.cs
//	Class designed to save persistent program information to
//	UZipDotNetState.xml file.
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
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace UZipDotNet
{
public enum OverwriteFiles
	{
	Yes,
	No,
	Ask
	}

public enum FileTypeCode
	{
	NoHeader,
	ZLib,
	Zip
	}

public class ProgramState
	{
	public String	ExtractToFolder;
	public Boolean	ExtractAll;
	public Int32	Overwrite;
	public Boolean	IgnoreFolderInfo;
	public Boolean	SkipReadOnly;
	public Boolean	SkipHidden;
	public Boolean	SkipSystem;
	public Boolean	SkipOlder;

	public Int32	CompressionLevel;
	public Int32	FileType;
	public Boolean	CompareFiles;

	public  static ProgramState	State;
	private static String FileName = "UZipDotNetState.xml";

	////////////////////////////////////////////////////////////////////
	// Constructor
	////////////////////////////////////////////////////////////////////

	public ProgramState()
		{
		ExtractToFolder = String.Empty;
		ExtractAll = true;
		Overwrite = (Int32) OverwriteFiles.Yes;
		IgnoreFolderInfo = false;
		SkipReadOnly = false;
		SkipHidden = false;
		SkipSystem = false;
		SkipOlder = false;
		CompressionLevel = 6;
		FileType = (Int32) FileTypeCode.ZLib;
		CompareFiles = false;
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Copy Constructor
	////////////////////////////////////////////////////////////////////

	public ProgramState
			(
			ProgramState Other
			)
		{
		this.ExtractToFolder = Other.ExtractToFolder;
		this.ExtractAll = Other.ExtractAll;
		this.Overwrite = Other.Overwrite;
		this.IgnoreFolderInfo = Other.IgnoreFolderInfo;
		this.SkipReadOnly = Other.SkipReadOnly;
		this.SkipHidden = Other.SkipHidden;
		this.SkipSystem = Other.SkipSystem;
		this.SkipOlder = Other.SkipOlder;
		this.CompressionLevel = Other.CompressionLevel;
		this.FileType = Other.FileType;
		this.CompareFiles = Other.CompareFiles;
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Compare two objects
	////////////////////////////////////////////////////////////////////

	public Boolean IsEqual
			(
			ProgramState Other
			)
		{
		return(this.ExtractToFolder == Other.ExtractToFolder &&
			this.ExtractAll == Other.ExtractAll &&
			this.Overwrite == Other.Overwrite &&
			this.IgnoreFolderInfo == Other.IgnoreFolderInfo &&
			this.SkipReadOnly == Other.SkipReadOnly &&
			this.SkipHidden == Other.SkipHidden &&
			this.SkipSystem == Other.SkipSystem &&
			this.SkipOlder == Other.SkipOlder &&
			this.CompressionLevel == Other.CompressionLevel &&
			this.FileType == Other.FileType &&
			this.CompareFiles == Other.CompareFiles);
		}

	////////////////////////////////////////////////////////////////////
	// Save Program State
	////////////////////////////////////////////////////////////////////

	public static void SaveState
			(
			ProgramState	NewState
			)
		{
		// test for change
		if(!State.IsEqual(NewState))
			{
			// replace state
			State = NewState;

			// save it
			SaveState();
			}

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Save Program State
	////////////////////////////////////////////////////////////////////

	public static void SaveState()
		{
		// create a new program state file
		XmlTextWriter TextFile = new XmlTextWriter(FileName, null);

		// create xml serializing object
		XmlSerializer XmlFile = new XmlSerializer(typeof(ProgramState));

		// serialize the program state
		XmlFile.Serialize(TextFile, State);

		// close the file
		TextFile.Close();

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Load Program State
	////////////////////////////////////////////////////////////////////

	public static void LoadState()
		{
		XmlTextReader
			TextFile = null;

		// program state file exist
		if(File.Exists(FileName))
			{
			try
				{
				// read program state file
				TextFile = new XmlTextReader(FileName);

				// create xml serializing object
				XmlSerializer XmlFile = new XmlSerializer(typeof(ProgramState));

				// deserialize the program state
				State = (ProgramState) XmlFile.Deserialize(TextFile);
				}
			catch
				{
				State = null;
				}

			// close the file
			if(TextFile != null) TextFile.Close();
			}

		// we have no program state file
		if(State == null)
			{
			// create new default program state
			State = new ProgramState();

			// save default
			SaveState();
			}

		// exit
		return;
		}
	}
}
