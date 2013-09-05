namespace ZetaHtmlEditControl
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Drawing;
	using System.Globalization;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Runtime.InteropServices.ComTypes;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Windows.Forms;
	using mshtml;
	using Properties;

	/// <summary>
	/// Edit control, primarily designed to work in conjunction
	/// with the ZetaHelpDesk application.
	/// </summary>
	public partial class HtmlEditControl :
		ExtendedWebBrowser,
		UnsafeNativeMethods.IDocHostUIHandler,
		IDisposable
	{
		private const int WmKeydown = 0x100;
		private const int WmSyskeydown = 0x104;

		private void setMenuShortcutKeys()
		{
			deleteToolStripMenuItem.ShortcutKeys = Keys.Delete;
		}

		/// <summary>
		/// <c>Uwe Keim</c>, 2006-03-17.
		/// </summary>
		public override bool PreProcessMessage(
			ref Message msg)
		{
			if (msg.Msg == WmKeydown || msg.Msg == WmSyskeydown)
			{
				var isShift = (ModifierKeys & Keys.Shift) != 0;

				var key = ((Keys)((int)msg.WParam));

				var e = new PreviewKeyDownEventArgs(key | ModifierKeys);

				// Check all shortcuts that I handle by myself.
				if (doHandleShortcutKey(e, false))
				{
					return true;
				}
				else
				{
					if (key == Keys.Tab)
					{
						// TAB key.
						if (!e.Control && !e.Alt)
						{
							if (handleTabKeyInsideTable(isShift))
							{
								return true;
							}
							else
							{
								// Forward or backward?
								var forward = !isShift;

								var form = FindForm();
								if (form != null)
								{
									var c = form.GetNextControl(this, forward);

									while (c != null &&
										c != this &&
										!c.TabStop)
									{
										c = form.GetNextControl(c, forward);
									}

									if (c != null)
									{
										c.Focus();
									}
								}
								return false;
							}
						}
						else
						{
							return false;
						}
					}
					else
					{
						return false;
					}
				}
			}

			return base.PreProcessMessage(ref msg);
		}

		/// <summary>
		/// Give derived classes the chance to handle the TAB key
		/// when inside a table.
		/// Return TRUE if handled, FALSE if not handled.
		/// </summary>
		private bool handleTabKeyInsideTable(
			 bool isShift)
		{
			if (CanTableCellProperties || CanAddTableRow)
			{
				if (IsControlSelection)
				{
					// The whole table is selected, add row at the end.

					if (!isShift && CanAddTableRow)
					{
						ExecuteTableAddTableRow();
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					// A cell is selected. Move to next/previous cell
					// or add row if on last.

					var rowIndex = CurrentSelectionTableRowIndex;
					var columnIndex = CurrentSelectionTableColumnIndex;

					var rowCount = CurrentSelectionTableRowCount;
					var columnCount = CurrentSelectionTableColumnCount;

					if (isShift)
					{
						// Previous cell.

						if (columnIndex == 0 && rowIndex == 0)
						{
							return false;
						}
						else if (columnIndex > 0)
						{
							// Previous cell.
							var row = CurrentSelectionTableRow;

							var element =
								(IHTMLElement)
								row.cells.item(
								columnIndex - 1,
								columnIndex - 1);
							MoveCaretToElement(element);
							return true;
						}
						else
						{
							// Previous line, last cell.
							IHTMLTable table =
								CurrentSelectionTable as mshtml.IHTMLTable;
							Debug.Assert(table != null);
							IHTMLTableRow previousRow =
								table.rows.item(
								rowIndex - 1,
								rowIndex - 1) as mshtml.IHTMLTableRow;

							Debug.Assert(previousRow != null);
							var element =
								previousRow.cells.item(
								previousRow.cells.length - 1,
								previousRow.cells.length - 1)
								as mshtml.IHTMLElement;
							MoveCaretToElement(element);
							return true;
						}
					}
					else
					{
						// Next cell.

						if (columnIndex < columnCount - 1)
						{
							// Next cell.
							mshtml.IHTMLTableRow row =
								CurrentSelectionTableRow;

							mshtml.IHTMLElement element =
								row.cells.item(
								columnIndex + 1,
								columnIndex + 1) as mshtml.IHTMLElement;
							MoveCaretToElement(element);
							return true;
						}
						else if (columnIndex == columnCount - 1 &&
							rowIndex < rowCount - 1)
						{
							// Next row, first cell.
							mshtml.IHTMLTable table =
								CurrentSelectionTable as mshtml.IHTMLTable;
							Debug.Assert(table != null);
							mshtml.IHTMLTableRow nextRow =
								table.rows.item(
								rowIndex + 1,
								rowIndex + 1) as mshtml.IHTMLTableRow;

							Debug.Assert(nextRow != null);
							mshtml.IHTMLElement element =
								nextRow.cells.item(0, 0) as mshtml.IHTMLElement;
							MoveCaretToElement(element);
							return true;
						}
						else
						{
							// Add new row.
							ExecuteTableAddTableRow();
							return true;
						}
					}
				}
			}
			else
			{
				return false;
			}
		}

		#region private class attributes
		// ------------------------------------------------------------------

		Timer _timerTextChange = new Timer();

		private HtmlConversionHelper _htmlConversionHelper;

		private string _tmpFolderPath = string.Empty;
		private int _objectID = 1;
		private const string CssFontStyle =
			@"font-family: Verdana; font-size: {0}pt; ";

		private string _cssText = _defaultCssText;
		private string _htmlTemplate = _defaultHtmlTemplate;

		/// <summary>
		/// Gets or sets the default CSS text.
		/// </summary>
		/// <value>The default CSS text.</value>
		public static string DefaultCssText
		{
			get
			{
				return _defaultCssText;
			}
			set
			{
				_defaultCssText = value;
			}
		}

		/// <summary>
		/// Gets or sets the default HTML template.
		/// </summary>
		/// <value>The default HTML template.</value>
		public static string DefaultHtmlTemplate
		{
			get
			{
				return _defaultHtmlTemplate;
			}
			set
			{
				_defaultHtmlTemplate = value;
			}
		}

		private static string _defaultCssText = @"body { {font-style}; margin: 4px; line-height: 110%; } 
			li { margin-bottom: 5pt; } 
			a { color: blue; } 
			table { {font-style}; } 
			tr { {font-style}; } 
			th { padding:1px; border-top: 2px inset #777; border-right: 2px inset #fff; border-bottom: 2px inset #aaa; border-left: 2px inset #fff; font-weight: bold; {font-style}; } 
			td { padding: 1px; border: 2px inset #fff; {font-style}; }";
		private static string _defaultHtmlTemplate = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
			<html xmlns=""http://www.w3.org/1999/xhtml"">
				<head>
					<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
					<style type=""text/css"">##CSS##</style>
				</head>
				<body>##BODY##</body>
			</html>";

		// ------------------------------------------------------------------
		#endregion

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IExternalInformationProvider ExternalInformationProvider
		{
			get;
			set;
		}

		/// <summary>
		/// Makes the full HTML from body.
		/// </summary>
		/// <param name="body">The body.</param>
		/// <returns></returns>
		public static string MakeFullHtmlFromBody(
			string body)
		{
			return doBuildCompleteHtml(
				body,
				_defaultHtmlTemplate,
				_defaultCssText);
		}

		/// <summary>
		/// HtmlEditControl
		/// </summary>
		public HtmlEditControl()
		{
			InitializeComponent();

			AllowWebBrowserDrop = false;

			Navigate(@"about:blank");

			_htmlConversionHelper = new HtmlConversionHelper();

			//tmp dir
			_tmpFolderPath = Path.Combine(Path.GetTempPath(),
				Guid.NewGuid().ToString());

			if (!Directory.Exists(_tmpFolderPath))
			{
				Directory.CreateDirectory(_tmpFolderPath);
			}

			//timer:
			_timerTextChange.Tick += timerTextChange_Tick;

			_timerTextChange.Interval = 200;
			_timerTextChange.Start();

			// --

			setMenuShortcutKeys();
		}

		#region Public Members

		public int TextChangeCheckInterval
		{
			get
			{
				return _timerTextChange.Interval;
			}
			set
			{
				if (value < 1000) //1 min
				{
					_timerTextChange.Interval = value;
				}
			}
		}

		/// <summary>
		/// Assigns a style sheet to the HTML editor.
		/// Set<see cref="DocumentText"/>t to activate.
		/// </summary>
		public string CssText
		{
			set
			{
				_cssText = value;
			}
		}

		/// <summary>
		/// Set own HTML Code.
		/// This '##BODY##' Tag will be replaced with the Body.
		/// Optional: '##CSS##'
		/// Set <see cref="DocumentText"/> to activate.
		/// </summary>
		public string HtmlTemplate
		{
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException(
						@"value",
						Resources.SR_HtmlEditControl_HtmlTemplate_AvaluefortheHtmlTemplatemustbeprovided);
				}
				else if (!value.Contains(@"##BODY##"))
				{
					throw new ArgumentException(
						Resources.SR_HtmlEditControl_HtmlTemplate_MissingBODYinsidetheHtmlTemplatepropertyvalue,
						@"value");
				}
				else
				{
					_htmlTemplate = value;
				}
			}
		}

		public string CompleteDocumentText
		{
			get
			{
				return base.DocumentText;
			}
			set { base.DocumentText = value; }
		}

		/// <summary>
		/// Gets or sets the HTML content.
		/// </summary>
		public new string DocumentText
		{
			get
			{
				return prepareDocumentTextGet(base.DocumentText);
			}
			set
			{
				base.DocumentText = prepareDocumentTextSet(value);
			}
		}

		/// <summary>
		/// Gets the document text and stores images to the given folder.
		/// </summary>
		/// <param name="externalImagesFolderPath">Folder path to store the images.</param>
		/// <returns>Returns the HTML code of the body.</returns>
		public string GetDocumentText(
			string externalImagesFolderPath)
		{
			return GetDocumentText(externalImagesFolderPath, false);
		}

		public string GetDocumentText(
			string externalImagesFolderPath,
			bool useImagesFolderPathPlaceHolder)
		{
			return
				_htmlConversionHelper.ConvertGetHtml(
					DocumentText,
					Document == null ? null : Document.Url,
					externalImagesFolderPath,
					useImagesFolderPathPlaceHolder ? ImagesFolderPathPlaceHolder : null);
		}

		/// <summary>
		/// Stand-alone function to expand any placeholders inside
		/// a given HTML fragment.
		/// </summary>
		public static string ExpandImageFolderPathPlaceHolder(
			string html,
			string externalImagesFolderPath)
		{
			if (string.IsNullOrEmpty(html) || !html.Contains(ImagesFolderPathPlaceHolder))
			{
				return html;
			}
			else
			{
				using (var ch = new HtmlConversionHelper())
				{
					return ch.ConvertSetHtml(
						html,
						externalImagesFolderPath,
						ImagesFolderPathPlaceHolder);
				}
			}
		}

		public static string ImagesFolderPathPlaceHolder
		{
			get
			{
				return @"http://pseudo-image-folder-path";
			}
		}

		public void SetDocumentText(
			string text)
		{
			SetDocumentText(text, null, false);
		}

		public void SetDocumentText(
			string text,
			string externalImagesFolderPath,
			bool useImagesFolderPathPlaceHolder)
		{
			DocumentText =
				_htmlConversionHelper.ConvertSetHtml(
					text,
					externalImagesFolderPath,
					useImagesFolderPathPlaceHolder ? ImagesFolderPathPlaceHolder : null);
		}

		/// <summary>
		/// Gets the text-only content of the body. 
		/// </summary>
		public string TextOnlyFromDocumentBody
		{
			get
			{
				var tmp = getBodyFromHtmlCode(base.DocumentText);
				tmp = getOnlyTextFromHtmlCode(tmp);
				return tmp;
			}
		}
		#endregion

		#region PrepareDocumentTextSet + BuildCompleteHtml + ReplaceCss
		private string prepareDocumentTextSet(string html)
		{
			return buildCompleteHtml(getBodyFromHtmlCode(html));
		}

		private string buildCompleteHtml(string htmlBody)
		{
			return doBuildCompleteHtml(htmlBody, _htmlTemplate, _cssText);
		}

		private static string doBuildCompleteHtml(
			string htmlBody,
			string htmlTemplate,
			string cssText)
		{
			string tmpHtml;
			if (string.IsNullOrEmpty(htmlTemplate))
			{
				tmpHtml = htmlBody;
			}
			else
			{
				tmpHtml = htmlTemplate;
				tmpHtml = tmpHtml.Replace(@"##BODY##", htmlBody);
			}

			tmpHtml = tmpHtml.Replace(@"##CSS##", replaceCss(cssText));

			return tmpHtml;
		}

		private static string replaceCss(string cssText)
		{
			if (!string.IsNullOrEmpty(cssText) &&
				cssText.Contains(@"{font-style}"))
			{

				var f = DefaultFont.SizeInPoints;
				return cssText.Replace(@"{font-style}",
					string.Format(CssFontStyle, f.ToString(@"F2", CultureInfo.InvariantCulture)));
			}
			else
			{
				return cssText;
			}
		}
		#endregion

		#region PrepareDocumentTextGet
		private static string prepareDocumentTextGet(string html)
		{
			var s = getBodyFromHtmlCode(html);
			s = Regex.Replace(s, @"<![^>]*>", string.Empty, RegexOptions.Singleline);
			return s;
		}
		#endregion

		#region GetOnlyTextFromHtmlCode + RemoveHtmlChars + RemoveTagFromHtmlCode
		private static string getOnlyTextFromHtmlCode(string htmlCode)
		{
			//<br>
			htmlCode = htmlCode.Replace("\r\n", @" ");
			htmlCode = htmlCode.Replace("\r", @" ");
			htmlCode = htmlCode.Replace("\n", @" ");

			htmlCode = htmlCode.Replace(@"</p>", Environment.NewLine + Environment.NewLine);
			htmlCode = htmlCode.Replace(@"</P>", Environment.NewLine + Environment.NewLine);

			//html comment 
			htmlCode = Regex.Replace(
				htmlCode,
				@"<!--.*?-->",
				string.Empty,
				RegexOptions.Singleline | RegexOptions.IgnoreCase);

			//<p>
			htmlCode = Regex.Replace(htmlCode,
				@"<br[^>]*>",
				Environment.NewLine,
				RegexOptions.Singleline | RegexOptions.IgnoreCase);

			//tags
			htmlCode = removeTagFromHtmlCode(@"style", htmlCode);
			htmlCode = removeTagFromHtmlCode(@"script", htmlCode);

			//html
			htmlCode = Regex.Replace(
				htmlCode,
				"<(.|\n)+?>",
				string.Empty,
				RegexOptions.Singleline | RegexOptions.IgnoreCase);

			//umlaute
			htmlCode = unescapeHtmlEntities(htmlCode);

			//whitespaces
			htmlCode = Regex.Replace(
				htmlCode,
				@" +",
				@" ",
				RegexOptions.Singleline | RegexOptions.IgnoreCase);

			return htmlCode;
		}

		private static string unescapeHtmlEntities(
			string htmlCode)
		{
			htmlCode = htmlCode.Replace(@"&nbsp;", @" ");

			htmlCode = htmlCode.Replace(@"&Auml;", @"�");
			htmlCode = htmlCode.Replace(@"&absp;", @"�");
			htmlCode = htmlCode.Replace(@"&obsp;", @"�");
			htmlCode = htmlCode.Replace(@"&Obsp;", @"�");
			htmlCode = htmlCode.Replace(@"&ubsp;", @"�");
			htmlCode = htmlCode.Replace(@"&Ubsp;", @"�");
			htmlCode = htmlCode.Replace(@"&szlig;", @"�");

			htmlCode = htmlCode.Replace(@"&pound;", @"�");
			htmlCode = htmlCode.Replace(@"&sect;", @"�");
			htmlCode = htmlCode.Replace(@"&copy;", @"�");
			htmlCode = htmlCode.Replace(@"&reg;", @"�");
			htmlCode = htmlCode.Replace(@"&micro;", @"�");
			htmlCode = htmlCode.Replace(@"&para;", @"�");
			htmlCode = htmlCode.Replace(@"&Oslash;", @"�");
			htmlCode = htmlCode.Replace(@"&oslash;", @"�");
			htmlCode = htmlCode.Replace(@"&divide;", @"�");
			htmlCode = htmlCode.Replace(@"&times;", @"�");
			return htmlCode;
		}

		private static string removeTagFromHtmlCode(
			string tag,
			string htmlCode)
		{
			return Regex.Replace(
				htmlCode,
				string.Format(@"<{0}.*?</{1}>", tag, tag),
				string.Empty,
				RegexOptions.Singleline | RegexOptions.IgnoreCase);
		}
		#endregion

		private static string getBodyFromHtmlCode(
			string htmlCode)
		{
			if (string.IsNullOrEmpty(htmlCode))
			{
				return htmlCode;
			}
			else if (htmlCode.IndexOf(@"<body",
				StringComparison.InvariantCultureIgnoreCase) >= 0)
			{
				var regex = new Regex(
					@".*?<body[^>]*>(.*?)</body>",
					RegexOptions.Singleline | RegexOptions.IgnoreCase);

				var m = regex.Match(htmlCode);

				return m.Success ? m.Groups[1].Value : htmlCode;
			}
			else
			{
				return htmlCode;
			}
		}

		#region HandlePaste [CheckImages-GetSourceURLFromClipboardH...-GetHtmlFragmentFromC...]
		// ------------------------------------------------------------------

		private void handlePaste(
			bool asTextOnly)
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				if (isControlSelection)
				{
					doc.execCommand(@"Delete", false, null);
				}
				string html;

				if (Clipboard.ContainsImage())
				{
					var image = Clipboard.GetImage();
					var file = Path.Combine(_tmpFolderPath, _objectID.ToString());
					if (image != null)
					{
						//file += getExtension(image.RawFormat);

						image.Save(file, image.RawFormat);
					}
					_objectID++;
					html = string.Format(@"<img src=""{0}"" />", file);
				}
				else
				{
					if (!asTextOnly && Clipboard.ContainsText(TextDataFormat.Html))
					{
						// Get HTML from Clipboard.
						// Modified 2006-06-12, Uwe Keim.
						string clipText;
						byte[] originalBuffer;
						getHtmlFromClipboard(out clipText, out originalBuffer);

						//selected fragment
						html = getHtmlFragmentFromClipboardCode(clipText, originalBuffer);

						//only body from fragment
						html = getBodyFromHtmlCode(html);

						//images save or load from web
						html = checkImages(HtmlConversionHelper.FindImgs(html), html,
							getSourceUrlFromClipboardHtmlCode(clipText));
					}
					else if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
					{
						html = Clipboard.GetText(TextDataFormat.UnicodeText);

						if (asTextOnly)
						{
							html = getOnlyTextFromHtmlCode(html);
						}

						html = addNewLineToText(html);
					}
					else if (Clipboard.ContainsText(TextDataFormat.Text))
					{
						html = Clipboard.GetText(TextDataFormat.Text);

						if (asTextOnly)
						{
							html = getOnlyTextFromHtmlCode(html);
						}

						html = addNewLineToText(html);
					}
					else
					{
						html = string.Empty;
					}
				}

				var selection = doc.selection;
				var range = (IHTMLTxtRange)selection.createRange();
				range.pasteHTML(html);
			}
		}

		/*
				private static string getExtension(ImageFormat rawFormat)
				{
					if (rawFormat==null)
					{
						return string.Empty;
					}
					else if (rawFormat.Guid == ImageFormat.Bmp.Guid)
					{
						return @".bmp";
					}
					else if (rawFormat.Guid == ImageFormat.MemoryBmp.Guid)
					{
						// Save memory BMP as PNG.
						return @".png";
					}
					else if (rawFormat.Guid == ImageFormat.Gif.Guid)
					{
						return @".gif";
					}
					else if (rawFormat.Guid == ImageFormat.Jpeg.Guid)
					{
						return @".jpg";
					}
					else if (rawFormat.Guid == ImageFormat.Png.Guid)
					{
						return @".png";
					}
					else if (rawFormat.Guid == ImageFormat.Tiff.Guid)
					{
						return @".tif";
					}
					else
					{
						return string.Empty;
					}
				}
		*/

		/// <summary>
		/// See http://66.249.93.104/search?q=cache:yfQWT9XlYogJ:www.eggheadcafe.com/aspnet_answers/NETFrameworkNETWindowsForms/Apr2006/post26606306.asp+IDataObject+html+utf-8&hl=de&gl=de&ct=clnk&cd=1&client=firefox-a
		/// See http://bakamachine.blogspot.com/2006/05/workarond-for-dataobject-html.html
		/// </summary>
		/// <remarks>Added 2006-06-12, <c>Uwe Keim</c>.</remarks>
		/// <returns></returns>
		private static void getHtmlFromClipboard(
			out string clipText,
			out byte[] originalBuffer)
		{
			originalBuffer = getHtml(Clipboard.GetDataObject());
			clipText = Encoding.UTF8.GetString(originalBuffer);
		}

		private static string addNewLineToText(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			else
			{
				text = text.Replace("\r\n", "\n");
				text = text.Replace("\r", "\n");
				text = text.Replace("\n", @"<br />");

				return text;
			}
		}

		private static string checkImages(
			ICollection<string> originalNames,
			string html,
			string url)
		{
			if (originalNames != null && originalNames.Count > 0)
			{
				foreach (var s in originalNames)
				{
					if (!s.StartsWith(@"http") && !s.StartsWith(@"https"))
					{
						html = html.Replace(
							s,
							HtmlConversionHelper.GetPathFromFile(
							s,
							new Uri(url)));
					}
				}

			}
			return html;
		}

		private static string getSourceUrlFromClipboardHtmlCode(
			string htmlCode)
		{
			var htmlInfo = htmlCode.Substring(0, htmlCode.IndexOf('<') - 1);

			var i = htmlInfo.IndexOf(@"SourceURL:");
			var url = htmlInfo.Substring(i + 10);
			url = url.Substring(0, url.IndexOf('\r'));
			return url;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>Modified 2006-06-12, <c>Uwe Keim</c>.</remarks>
		private static string getHtmlFragmentFromClipboardCode(
			string htmlCode,
			byte[] originalBuffer)
		{
			//split Html to htmlInfo (and htmlSource)
			var htmlInfo = htmlCode.Substring(0, htmlCode.IndexOf('<') - 1);

			//get Fragment positions
			var tmp = htmlInfo.Substring(htmlInfo.IndexOf(@"StartFragment:") + 14);
			tmp = tmp.Substring(0, tmp.IndexOf('\r'));
			var posStartSelection = Convert.ToInt32(tmp);

			tmp = htmlInfo.Substring(htmlInfo.IndexOf(@"EndFragment:") + 12);
			tmp = tmp.Substring(0, tmp.IndexOf('\r'));
			var posEndSelection = Convert.ToInt32(tmp);

			// get Fragment. Always UTF-8 as of spec.
			var s = Encoding.UTF8.GetString(
				originalBuffer,
				posStartSelection,
				posEndSelection - posStartSelection);

			return s;
		}

		// ------------------------------------------------------------------
		#endregion

		#region SelectionType (Control, Text, None)

		private string selectionType
		{
			get
			{
				if (string.IsNullOrEmpty(DocumentText))
				{
					return string.Empty;
				}
				else
				{
					if (Document == null)
					{
						return string.Empty;
					}
					else
					{
						var selection =
							((HTMLDocument)Document.DomDocument).selection;
						return selection.type.ToLower();
					}
				}
			}
		}

		private bool isControlSelection
		{
			get
			{
				return selectionType == @"control";
			}
		}

		private bool isTextSelection
		{
			get
			{
				return selectionType == @"text";
			}
		}

		/*
				private bool IsNoneSelection
				{
					get
					{
						string seleType = SelectionType;
						return seleType == @"none" || string.IsNullOrEmpty( seleType );
					}
				}
		*/
		#endregion

		#region EndCleanMembers
		// ------------------------------------------------------------------

		void IDisposable.Dispose()
		{
			endClean();
		}

		~HtmlEditControl()
		{
			endClean();
		}

		private void endClean()
		{
			if (_timerTextChange != null)
			{
				_timerTextChange.Stop();
				_timerTextChange.Dispose();
				_timerTextChange = null;
			}

			if (!string.IsNullOrEmpty(_tmpFolderPath))
			{
				if (Directory.Exists(_tmpFolderPath))
				{
					Directory.Delete(_tmpFolderPath, true);
				}
				_tmpFolderPath = null;
			}

			if (_htmlConversionHelper != null)
			{
				((IDisposable)_htmlConversionHelper).Dispose();
				_htmlConversionHelper = null;
			}
		}

		// ------------------------------------------------------------------
		#endregion

		//protected override void OnPreviewKeyDown(
		//    PreviewKeyDownEventArgs e)
		//{
		//    //if (!doHandleShortcutKey(e, false))
		//    //{
		//    //    base.OnPreviewKeyDown(e);
		//    //}
		//}

		private bool doHandleShortcutKey(
			PreviewKeyDownEventArgs e,
			bool onlyCheck)
		{
			if (e.KeyCode == Keys.V && e.Control) //v + ctrl
			{
				if (!onlyCheck)
				{
					handlePaste(false);
				}
				return true;
			}
			else if (e.KeyCode == Keys.Delete) //del
			{
				if (!onlyCheck)
				{
					ExecuteDelete();
				}
				return true;
			}
			else if (e.KeyCode == Keys.X && e.Control) //x + ctrl
			{
				if (!onlyCheck)
				{
					ExecuteCut();
				}
				return true;
			}
			else if (e.KeyCode == Keys.C && e.Control) //c + ctrl
			{
				if (!onlyCheck)
				{
					ExecuteCopy();
				}
				return true;
			}
			else if (e.KeyCode == Keys.Z && e.Control) //z + ctrl
			{
				if (!onlyCheck)
				{
					ExecuteUndo();
				}
				return true;
			}
			else if (e.KeyCode == Keys.Y && e.Control) //y + ctrl
			{
				if (!onlyCheck)
				{
					ExecuteRedo();
				}
				return true;
			}
			else if (
				e.KeyCode == Keys.U && e.Control ||
					e.KeyCode == Keys.U && e.Control && e.Shift) //u+ ctrl+ shift
			{
				if (!onlyCheck)
				{
					ExecuteUnderline();
				}
				return true;
			}
			else if (
				e.KeyCode == Keys.I && e.Control ||
					e.KeyCode == Keys.K && e.Control && e.Shift) //k+ ctrl+ shift
			{
				if (!onlyCheck)
				{
					ExecuteItalic();
				}
				return true;
			}
			else if (
				e.KeyCode == Keys.B && e.Control ||
					e.KeyCode == Keys.F && e.Control && e.Shift) //f+ ctrl+ shift
			{
				if (!onlyCheck)
				{
					ExecuteBold();
				}
				return true;
			}
			else if (e.KeyCode == Keys.K && e.Control) //k+ ctrl
			{
				if (!onlyCheck)
				{
					ExecuteInsertHyperlink();
				}
				return true;
			}
			else if (e.KeyCode == Keys.A && e.Control) //a+ ctrl
			{
				if (!onlyCheck)
				{
					ExecuteSelectAll();
				}
				return true;
			}
			else if (e.KeyCode == Keys.E && e.Control) //E+ ctrl
			{
				if (!onlyCheck)
				{
					ExecuteJustifyCenter();
				}
				return true;
			}
			else if (e.KeyCode == Keys.L && e.Control) //l+ ctrl
			{
				if (!onlyCheck)
				{
					ExecuteJustifyLeft();
				}
				return true;
			}
			else if (e.KeyCode == Keys.R && e.Control) //r+ ctrl
			{
				if (!onlyCheck)
				{
					ExecuteJustifyRight();
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool IsBold
		{
			get
			{
				return (bool)DomDocument.queryCommandValue(@"Bold");
			}
		}

		public bool IsItalic
		{
			get
			{
				return (bool)DomDocument.queryCommandValue(@"Italic");
			}
		}

		public bool IsOrderedList
		{
			get
			{
				return (bool)DomDocument.queryCommandValue(@"InsertOrderedList");
			}
		}

		public bool IsBullettedList
		{
			get
			{
				return (bool)DomDocument.queryCommandValue(@"InsertUnorderedList");
			}
		}

		public bool IsJustifyLeft
		{
			get
			{
				return (bool)DomDocument.queryCommandValue(@"JustifyLeft");
			}
		}

		public bool IsJustifyCenter
		{
			get
			{
				return (bool)DomDocument.queryCommandValue(@"JustifyCenter");
			}
		}

		public bool IsJustifyRight
		{
			get
			{
				return (bool)DomDocument.queryCommandValue(@"JustifyRight");
			}
		}

		private void ExecuteSelectAll()
		{
			var doc = (HTMLDocument)Document.DomDocument;
			doc.execCommand(@"SelectAll", false, null);
		}

		private void ExecuteUnderline()
		{
			var doc = (HTMLDocument)Document.DomDocument;
			doc.execCommand(@"Underline", false, null);
		}

		private void ExecuteRedo()
		{
			Document.ExecCommand(@"Redo", false, null);
		}

		internal void ExecuteUndo()
		{
			Document.ExecCommand(@"Undo", false, null);
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);

			if (Document != null)
			{
				if (Document.Body != null)
				{
					Document.Body.Focus();
				}
			}
		}

		private bool _firstCreate = true;
		protected override void OnNavigated(WebBrowserNavigatedEventArgs e)
		{
			base.OnNavigated(e);

			_documentLoaded = true;

			if (_firstCreate)
			{
				_firstCreate = false;

				////WebBrowserDesignMode = true;
				//DocumentText = string.Empty;
			}

			// 2005-09-02: Can be null if showing a full-window PDF-viewer.
			if (DomDocument != null)
			{
				if (!_customDocUIHandlerSet)
				{
					// Do this here, too.
					// Enables this control to be called from the contained
					// JavaScript on the loaded HTML document.
					// See GetValueFromScript() and SetValueFromScript().
					//ObjectForScripting = this;

					_customDocUIHandlerSet = true;

					var doc = DomDocument;
					var cd = (UnsafeNativeMethods.ICustomDoc)doc;

					// Set the IDocHostUIHandler.
					cd.SetUIHandler(this);
				}
			}
		}

		private string _tmpCacheTextChange = string.Empty;
		//private bool _returnTextClipboard;

		private void timerTextChange_Tick(
			object sender,
			EventArgs e)
		{
			if (!IsDisposed) // Uwe Keim 2006-03-17.
			{
				var s = DocumentText ?? string.Empty;

				if (_tmpCacheTextChange != s)
				{
					//if (_returnTextClipboard)
					//{
					//    _returnTextClipboard = false;
					//}
					//else
					//{
					//    myClipboard = s;
					//}
					_tmpCacheTextChange = s;

					if (TextChanged != null)
					{
						TextChanged(this, new EventArgs());
					}
				}
			}
		}

		public new event EventHandler TextChanged;

		//#region MyClipboard

		//private int _clipboardPosition;
		//private const int ClipboardCapacity = 10;
		//private readonly List<string> _clipboard = new List<string>();
		private bool _documentLoaded;
		private bool _customDocUIHandlerSet;

		//private string myClipboard
		//{
		//    get
		//    {
		//        return _clipboard.Count > 0 ? _clipboard[_clipboardPosition] : null;
		//    }
		//    set
		//    {
		//        if (_clipboardPosition < _clipboard.Count - 1)
		//        {
		//            var i = _clipboardPosition;
		//            int c = _clipboard.Count - 2;
		//            while (c >= i)
		//            {
		//                _clipboard.Add(_clipboard[c]);
		//                c--;
		//            }
		//            _clipboard.Add(value);
		//        }
		//        else
		//        {
		//            _clipboard.Add(value);
		//        }
		//        while (_clipboard.Count > ClipboardCapacity)
		//        {
		//            _clipboard.RemoveAt(0);
		//        }
		//        _clipboardPosition = _clipboard.Count - 1;
		//    }
		//}
		//#endregion

		#region Helpers for HTML extraction.
		// ------------------------------------------------------------------

		/// <summary>
		/// Extracts data of type <c>Dataformat.Html</c> from an <c>IDataObject</c> data container
		/// This method shouldn't throw any exception but writes relevant exception informations in the debug window
		/// </summary>
		/// <param name="data">data container</param>
		/// <returns>A byte[] array with the decoded string or null if the method fails</returns>
		/// <remarks>Added 2006-06-12, <c>Uwe Keim</c>.</remarks>
		private static byte[] getHtml(
			System.Windows.Forms.IDataObject data)
		{
			var interopData = (System.Runtime.InteropServices.ComTypes.IDataObject)data;

			var format =
				new FORMATETC
				{
					cfFormat = ((short)DataFormats.GetFormat(DataFormats.Html).Id),
					dwAspect = DVASPECT.DVASPECT_CONTENT,
					lindex = (-1),
					tymed = TYMED.TYMED_HGLOBAL
				};

			STGMEDIUM stgmedium;
			stgmedium.tymed = TYMED.TYMED_HGLOBAL;
			stgmedium.pUnkForRelease = null;

			//try
			//{
			var queryResult = interopData.QueryGetData(ref format);
			//}
			//catch ( Exception exp )
			//{
			//	Debug.WriteLine( "HtmlFromIDataObject.GetHtml -> QueryGetData(ref format) threw an exception: "
			//		+ Environment.NewLine + exp.ToString() );
			//	return null;
			//}

			if (queryResult != 0)
			{
				Debug.WriteLine(
					string.Format(
						@"HtmlFromIDataObject.GetHtml -> QueryGetData(ref format) returned a code != 0 code: {0}",
						queryResult));
				return null;
			}

			//try
			//{
			interopData.GetData(ref format, out stgmedium);
			//}
			//catch ( Exception exp )
			//{
			//	System.Diagnostics.Debug.WriteLine( "HtmlFromIDataObject.GetHtml -> GetData(ref format, out stgmedium) threw this exception: "
			//		+ Environment.NewLine + exp.ToString() );
			//	return null;
			//}

			if (stgmedium.unionmember == IntPtr.Zero)
			{
				Debug.WriteLine(
					@"HtmlFromIDataObject.GetHtml -> stgmedium.unionmember returned an IntPtr pointing to zero");
				return null;
			}

			var pointer = stgmedium.unionmember;

			var handleRef = new HandleRef(null, pointer);

			byte[] rawArray;

			try
			{
				var ptr1 = GlobalLock(handleRef);

				var length = GlobalSize(handleRef);

				rawArray = new byte[length];

				Marshal.Copy(ptr1, rawArray, 0, length);
			}
			//catch ( Exception exp )
			//{
			//	Debug.WriteLine( "HtmlFromIDataObject.GetHtml -> Html Import threw an exception: " + Environment.NewLine + exp.ToString() );
			//}
			finally
			{
				GlobalUnlock(handleRef);

			}

			return rawArray;
		}

		[DllImport(@"kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GlobalLock(HandleRef handle);

		[DllImport(@"kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		private static extern bool GlobalUnlock(HandleRef handle);

		[DllImport(@"kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		private static extern int GlobalSize(HandleRef handle);

		// ------------------------------------------------------------------
		#endregion

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			updateUI();
		}

		private void updateUI()
		{
			if (Document != null && Document.DomDocument != null)
			{
				boldToolStripMenuItem.Enabled =
					CanBold;
				italicToolStripMenuItem.Enabled =
					CanItalic;

				cutToolStripMenuItem.Enabled =
					CanCut;

				copyToolStripMenuItem.Enabled =
					CanCopy;

				pasteAsTextToolStripMenuItem.Enabled =
					pasteToolStripMenuItem.Enabled =
					CanPaste;
				deleteToolStripMenuItem.Enabled =
					CanDelete;

				indentToolStripMenuItem.Enabled =
					CanIndent;
				justifyCenterToolStripMenuItem.Enabled =
					CanJustifyCenter;
				justifyLeftToolStripMenuItem.Enabled =
					CanJustifyLeft;
				justifyRightToolStripMenuItem.Enabled =
					CanJustifyRight;
				numberedListToolStripMenuItem.Enabled =
					CanOrderedList;
				outdentToolStripMenuItem.Enabled =
					CanOutdent;
				bullettedListToolStripMenuItem.Enabled =
					CanBullettedList;

				foreColorToolStripMenuItem.Enabled =
					CanForeColor;
				backColorToolStripMenuItem.Enabled =
					CanBackColor;

				hyperLinkToolStripMenuItem.Enabled =
					CanInsertHyperlink;

				htmlToolStripMenuItem.Enabled = CanShowSource;

				// --

				UpdateUIContextMenuTable();
			}
		}

		internal bool CanOutdent
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"Outdent");
			}
		}

		internal bool CanOrderedList
		{
			get
			{
				return Enabled &&
				       ((HTMLDocument) Document.DomDocument).queryCommandEnabled(@"InsertOrderedList");
			}
		}

		public bool CanUndo
		{
			get
			{
				return DomDocument.queryCommandEnabled(@"Undo");
			}
		}

		internal bool CanJustifyRight
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"JustifyRight");
			}
		}

		internal bool CanJustifyLeft
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"JustifyLeft");
			}
		}

		internal bool CanJustifyCenter
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"JustifyCenter");
			}
		}

		internal bool CanIndent
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"Indent");
			}
		}

		internal bool CanDelete
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"Delete");
			}
		}

		internal bool CanPaste
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"Paste");
			}
		}

		internal bool CanCopy
		{
			get { return ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"Copy"); }
		}

		private bool CanCut
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"Cut");
			}
		}

		internal bool CanItalic
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"Italic");
			}
		}

		internal bool CanBold
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"Bold");
			}
		}

		internal bool CanBullettedList
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"InsertUnorderedList");
			}
		}

		internal bool CanForeColor
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"ForeColor");
			}
		}

		internal bool CanBackColor
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"BackColor");
			}
		}

		internal bool CanInsertHyperlink
		{
			get
			{
				return Enabled &&
					   ((HTMLDocument)Document.DomDocument).queryCommandEnabled(@"CreateLink");
			}
		}

		internal bool CanShowSource
		{
			get { return true; }
		}

		private void boldToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteBold();
		}

		internal void ExecuteBold()
		{
			if (Document != null)
			{
				var doc = (HTMLDocument)Document.DomDocument;

				doc.execCommand(@"Bold", false, null);
			}
		}

		private void italicToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteItalic();
		}

		internal void ExecuteItalic()
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"Italic", false, null);
			}
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecutePaste();
		}

		internal void ExecutePaste()
		{
			handlePaste(false);
		}

		private void pasteAsTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecutePasteAsText();
		}

		internal void ExecutePasteAsText()
		{
			handlePaste(true);
		}

		private void htmlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteShowSource();
		}

		internal void ExecuteShowSource()
		{
			using (var form = new HtmlSourceTextEditForm(DocumentText))
			{
				form.ExternalInformationProvider = ExternalInformationProvider;

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					DocumentText = form.HtmlText;
					updateUI();
				}
			}
		}

		private void hyperLinkToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteInsertHyperlink();
		}

		internal void ExecuteInsertHyperlink()
		{
			if (Document != null)
			{
				var doc = (HTMLDocument)Document.DomDocument;

				doc.execCommand(@"CreateLink", true, null);
			}
		}

		private void indentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteIndent();
		}

		internal void ExecuteIndent()
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"Indent", false, null);
			}
		}

		private void justifyCenterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteJustifyCenter();
		}

		internal void ExecuteJustifyCenter()
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"JustifyCenter", false, null);
			}
		}

		private void justifyLeftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteJustifyLeft();
		}

		internal void ExecuteJustifyLeft()
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"JustifyLeft", false, null);
			}
		}

		private void justifyRightToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteJustifyRight();
		}

		internal void ExecuteJustifyRight()
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"JustifyRight", false, null);
			}
		}

		private void numberedListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteNumberedList();
		}

		internal void ExecuteNumberedList()
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"InsertOrderedList", false, null);
			}
		}

		private void outdentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteOutdent();
		}

		internal void ExecuteOutdent()
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"Outdent", false, null);
			}
		}

		private void bullettedListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteBullettedList();
		}

		internal void ExecuteBullettedList()
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"InsertUnorderedList", false, null);
			}
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteCopy();
		}

		internal void ExecuteCopy()
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"Copy", false, null);
			}
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteCut();
		}

		internal void ExecuteCut()
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				if (isTextSelection)
				{
					var range =
						(IHTMLTxtRange)doc.selection.createRange();

					Clipboard.SetText(range.htmlText);
				}
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteDelete();
		}

		internal void ExecuteDelete()
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"Delete", false, null);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void setForeColor(
			string color)
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"ForeColor", false, color);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void setBackColor(
			string color)
		{
			if (Document != null)
			{
				var doc =
					(HTMLDocument)Document.DomDocument;

				doc.execCommand(@"BackColor", false, color);
			}
		}

		private void foreColorNoneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetForeColorNone();
		}

		internal void ExecuteSetForeColorNone()
		{
			setForeColor(@"windowtext");
		}

		private void foreColor01ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetForeColor01();
		}

		internal void ExecuteSetForeColor01()
		{
			setForeColor(@"c00000");
		}

		private void foreColor02ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetForeColor02();
		}

		internal void ExecuteSetForeColor02()
		{
			setForeColor(@"ff0000");
		}

		private void foreColor03ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetForeColor03();
		}

		internal void ExecuteSetForeColor03()
		{
			setForeColor(@"ffc000");
		}

		private void foreColor04ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetForeColor04();
		}

		internal void ExecuteSetForeColor04()
		{
			setForeColor(@"ffff00");
		}

		private void foreColor05ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetForeColor05();
		}

		internal void ExecuteSetForeColor05()
		{
			setForeColor(@"92d050");
		}

		private void foreColor06ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetForeColor06();
		}

		internal void ExecuteSetForeColor06()
		{
			setForeColor(@"00b050");
		}

		private void foreColor07ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetForeColor07();
		}

		internal void ExecuteSetForeColor07()
		{
			setForeColor(@"00b0f0");
		}

		private void foreColor08ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetForeColor08();
		}

		internal void ExecuteSetForeColor08()
		{
			setForeColor(@"0070c0");
		}

		private void foreColor09ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetForeColor09();
		}

		internal void ExecuteSetForeColor09()
		{
			setForeColor(@"002060");
		}

		private void foreColor10ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetForeColor10();
		}

		internal void ExecuteSetForeColor10()
		{
			setForeColor(@"7030a0");
		}

		/// <summary>
		/// 
		/// </summary>
		private void backColorNoneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetBackColorNone();
		}

		internal void ExecuteSetBackColorNone()
		{
			setBackColor(@"window");
		}

		private void backColor01ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetBackColor01();
		}

		internal void ExecuteSetBackColor01()
		{
			setBackColor(@"ffff00");
		}

		private void backColor02ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetBackColor02();
		}

		internal void ExecuteSetBackColor02()
		{
			setBackColor(@"00ff00");
		}

		private void backColor03ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetBackColor03();
		}

		internal void ExecuteSetBackColor03()
		{
			setBackColor(@"00ffff");
		}

		private void backColor04ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetBackColor04();
		}

		internal void ExecuteSetBackColor04()
		{
			setBackColor(@"ff0000");
		}

		private void backColor05ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteSetBackColor05();
		}

		internal void ExecuteSetBackColor05()
		{
			setBackColor(@"ff00ff");
		}

		public void ExecuteInsertTable()
		{
			using (var form = new HtmlEditorTableNewForm())
			{
				form.ExternalInformationProvider = ExternalInformationProvider;

				if (form.ShowDialog(FindForm()) == DialogResult.OK)
				{
					InsertHtmlAtCurrentSelection(form.Html);
				}
			}
		}

		public void InsertHtmlAtCurrentSelection(
			string html)
		{
			if (IsControlSelection)
			{
				// if its a control range, it must be deleted before.
				var sel = CurrentSelectionControl;
				sel.execCommand(@"Delete", false, null);
			}

			var sel2 = CurrentSelectionText;
			sel2.pasteHTML(html);
		}

		/// <summary>
		/// Gets the current selection.
		/// </summary>
		/// <param name="selectionMPStart">The selection MP start.</param>
		/// <param name="selectionMPEnd">The selection MP end.</param>
		public void GetCurrentSelection(
			out IMarkupPointer selectionMPStart,
			out IMarkupPointer selectionMPEnd)
		{
			// get markup container of the whole document.
			IMarkupContainer mc = DomDocument as IMarkupContainer;
			Debug.Assert(mc != null);

			// get the markup services.
			IMarkupServices ms = DomDocument as IMarkupServices;
			Debug.Assert(ms != null);

			// create two markup pointers.
			ms.CreateMarkupPointer(out selectionMPStart);
			ms.CreateMarkupPointer(out selectionMPEnd);

			selectionMPStart.MoveToContainer(mc, NativeMethods.BOOL_TRUE);
			selectionMPEnd.MoveToContainer(mc, NativeMethods.BOOL_TRUE);

			// --
			// position start and end pointers around the current selection.

			IHTMLSelectionObject selection = DomDocument.selection;

			string selectionType = selection.type.ToLowerInvariant();

			if (selectionType == @"none")
			{
				IDisplayServices ds =
					DomDocument as IDisplayServices;
				Debug.Assert(ds != null);

				IHTMLCaret caret;
				ds.GetCaret(out caret);

				caret.MoveMarkupPointerToCaret(selectionMPStart);
				caret.MoveMarkupPointerToCaret(selectionMPEnd);

				// set gravity, as in "Introduction to Markup Services" in MSDN.
				selectionMPStart.SetGravity(_POINTER_GRAVITY.POINTER_GRAVITY_Right);
			}
			else if (selectionType == @"text")
			{
				// MoveToSelectionAnchor does only work with "text" selections.
				var selectionText = DomDocument.selection;

				var range = (IHTMLTxtRange)selectionText.createRange();

				ms.MovePointersToRange(range, selectionMPStart, selectionMPEnd);

				// swap if wrong direction.
				if (compareGt(selectionMPStart, selectionMPEnd))
				{
					var tmp = selectionMPStart;
					selectionMPStart = selectionMPEnd;
					selectionMPEnd = tmp;
				}

				// set gravity, as in "Introduction to Markup Services" in MSDN.
				selectionMPStart.SetGravity(_POINTER_GRAVITY.POINTER_GRAVITY_Right);
			}
			else if (selectionType == @"control")
			{
				// MoveToSelectionAnchor does only work with "text" selections.
				IHTMLSelectionObject selectionControl = DomDocument.selection;
				Debug.Assert(selection != null);

				var range = selectionControl.createRange()
					as IHTMLControlRange;

				// Strangly, range was null sometimes.
				// E.g. when I resized a table (=control selection)
				// and then did an undo.
				if (range != null)
				{
					if (range.length > 0)
					{
						var start = range.item(0);
						var end = range.item(range.length - 1);

						selectionMPStart.MoveAdjacentToElement(
							start,
							_ELEMENT_ADJACENCY.ELEM_ADJ_BeforeBegin);
						selectionMPEnd.MoveAdjacentToElement(
							end,
							_ELEMENT_ADJACENCY.ELEM_ADJ_AfterEnd);
					}
				}
			}
			else
			{
				// is there yet another selection type?
				Debug.Assert(false);
			}
		}

		/// <summary>
		/// Gets the current selected element.
		/// </summary>
		/// <value>The current selected element.</value>
		public IHTMLElement CurrentSelectedElement
		{
			get
			{

				if (DomDocument.all.length == 0)
				{
					return null;
				}
				else
				{
					IHTMLTxtRange txt = CurrentSelectionText;

					if (txt != null)
					{
						return txt.parentElement();
					}
					else
					{
						IHTMLControlRange ctrl = CurrentSelectionControl;
						Debug.Assert(ctrl != null);
						return ctrl.commonParentElement();
					}
				}
			}
		}

		public void ExecuteInsertTableRow()
		{
			IHTMLTable table = CurrentSelectionTable as IHTMLTable;

			if (table != null)
			{
				int rowIndex = CurrentSelectionTableRowIndex;

				IHTMLTableRow row =
					HtmlEditorTableNewForm.AddTableRowsAfterRow(
					table,
					rowIndex,
					1);

				// Set focus to first cell in the new line.
				if (row != null)
				{
					IHTMLTableCell cell = row.cells.item(0, 0)
						as IHTMLTableCell;
					MoveCaretToElement(cell as IHTMLElement);
				}
			}
		}

		private void MoveCaretToElement(
			IHTMLElement element)
		{
			if (element != null)
			{
				var ms = (IMarkupServices)DomDocument;
				IMarkupPointer mp;
				ms.CreateMarkupPointer(out mp);

				var mp2 = (IMarkupPointer2)mp;
				mp2.MoveToContent(element, NativeMethods.BOOL_TRUE);

				var ds = (IDisplayServices)DomDocument;
				IDisplayPointer dp;
				ds.CreateDisplayPointer(out dp);

				dp.MoveToMarkupPointer(mp, null);

				// --

				IHTMLCaret caret;
				ds.GetCaret(out caret);

				caret.MoveCaretToPointer(
					dp,
					NativeMethods.BOOL_TRUE,
					_CARET_DIRECTION.CARET_DIRECTION_SAME);
				caret.Show(NativeMethods.BOOL_TRUE);
			}
		}

		/// <summary>
		/// Executes the insert table column.
		/// </summary>
		public void ExecuteInsertTableColumn()
		{
			IHTMLTable table = CurrentSelectionTable as IHTMLTable;

			if (table != null)
			{
				int columnIndex = CurrentSelectionTableColumnIndex;

				HtmlEditorTableNewForm.AddTableColumnsAfterColumn(
					table,
					columnIndex,
					1);
			}
		}

		/// <summary>
		/// Executes the table add table row.
		/// </summary>
		public void ExecuteTableAddTableRow()
		{
			IHTMLTable table = CurrentSelectionTable as IHTMLTable;

			if (table != null)
			{
				IHTMLTableRow row =
					HtmlEditorTableNewForm.AddTableRowsAtBottom(
					table,
					1);

				MoveCaretToElement(row.cells.item(0, 0) as IHTMLElement);
			}
		}

		/// <summary>
		/// Executes the table add table column.
		/// </summary>
		public void ExecuteTableAddTableColumn()
		{
			var table = CurrentSelectionTable as IHTMLTable;

			if (table != null)
			{
				HtmlEditorTableNewForm.AddTableColumnsAtRight(
					table,
					1);
			}
		}

		/// <summary>
		/// Executes the table properties.
		/// </summary>
		public void ExecuteTableProperties()
		{
			var table = CurrentSelectionTable as IHTMLTable;

			if (table != null)
			{
				using (var form = new HtmlEditorTableNewForm())
				{
					form.ExternalInformationProvider = ExternalInformationProvider;

					form.Table = table;
					form.ShowDialog(FindForm());
				}
			}
		}

		/// <summary>
		/// Executes the table delete row.
		/// </summary>
		public void ExecuteTableDeleteRow()
		{
			var table = CurrentSelectionTable as IHTMLTable;
			var rowIndex = CurrentSelectionTableRowIndex;

			if (table != null && rowIndex != -1)
			{
				table.deleteRow(rowIndex);
			}
		}

		/// <summary>
		/// Executes the table delete column.
		/// </summary>
		public void ExecuteTableDeleteColumn()
		{
			IHTMLTable table = CurrentSelectionTable as IHTMLTable;
			int columnIndex = CurrentSelectionTableColumnIndex;

			if (table != null && columnIndex != -1)
			{
				IHTMLElementCollection rows = table.rows;

				if (rows != null)
				{
					for (int i = 0; i < rows.length; ++i)
					{
						IHTMLTableRow row = rows.item(i, i) as IHTMLTableRow;

						if (row != null)
						{
							row.deleteCell(columnIndex);
						}
					}
				}
			}
		}

		/// <summary>
		/// Executes the table delete table.
		/// </summary>
		public void ExecuteTableDeleteTable()
		{
			IHTMLTable table = CurrentSelectionTable as IHTMLTable;

			if (table != null)
			{
				IHTMLDOMNode tableNode = table as IHTMLDOMNode;

				if (tableNode != null)
				{
					tableNode.removeNode(true);
				}
			}
		}

		/// <summary>
		/// Executes the table row properties.
		/// </summary>
		public void ExecuteTableRowProperties()
		{
			IHTMLTableRow row = CurrentSelectionTableRow;

			if (row != null)
			{
				using (var form = new HtmlEditorCellPropertiesForm())
				{
					form.ExternalInformationProvider = ExternalInformationProvider;

					form.Initialize(row);
					form.ShowDialog(FindForm());
				}
			}
		}

		/// <summary>
		/// Executes the table column properties.
		/// </summary>
		public void ExecuteTableColumnProperties()
		{
			var table = CurrentSelectionTable as IHTMLTable;
			var columnIndex = CurrentSelectionTableColumnIndex;

			if (table != null && columnIndex >= 0)
			{
				using (var form = new HtmlEditorCellPropertiesForm())
				{
					form.ExternalInformationProvider = ExternalInformationProvider;

					form.Initialize(table, columnIndex);
					form.ShowDialog(FindForm());
				}
			}
		}

		/// <summary>
		/// Executes the table cell properties.
		/// </summary>
		public void ExecuteTableCellProperties()
		{
			var cells = CurrentSelectionTableCells;

			if (cells != null && cells.Length > 0)
			{
				using (var form = new HtmlEditorCellPropertiesForm())
				{
					form.ExternalInformationProvider = ExternalInformationProvider;

					form.Initialize(cells);
					form.ShowDialog(FindForm());
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance can table properties.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can table properties; otherwise, <c>false</c>.
		/// </value>
		public bool CanTableProperties
		{
			get
			{
				return
					IsTableCurrentSelectionInsideTable;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance can add table row.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can add table row; otherwise, <c>false</c>.
		/// </value>
		public bool CanAddTableRow
		{
			get
			{
				return
					IsTableCurrentSelectionInsideTable;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance can add table column.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can add table column; otherwise, <c>false</c>.
		/// </value>
		public bool CanAddTableColumn
		{
			get
			{
				return
					IsTableCurrentSelectionInsideTable;
			}
		}

		public bool CanInsertTable
		{
			get
			{
				return true;
			}
		}

		private void UpdateUIContextMenuTable()
		{
			insertNewTableToolStripMenuItem.Enabled = CanInsertTable;
			insertRowBeforeCurrentRowToolStripMenuItem.Enabled = CanInsertTableRow;
			insertColumnBeforeCurrentColumnToolStripMenuItem.Enabled = CanInsertTableColumn;
			addRowAfterTheLastTableRowToolStripMenuItem.Enabled = CanAddTableRow;
			addColumnAfterTheLastTableColumnToolStripMenuItem.Enabled = CanAddTableColumn;
			tablePropertiesToolStripMenuItem.Enabled = CanTableProperties;
			rowPropertiesToolStripMenuItem.Enabled = CanTableRowProperties;
			columnPropertiesToolStripMenuItem.Enabled = CanTableColumnProperties;
			cellPropertiesToolStripMenuItem.Enabled = CanTableCellProperties;
			deleteRowToolStripMenuItem.Enabled = CanTableDeleteRow;
			deleteColumnToolStripMenuItem.Enabled = CanTableDeleteColumn;
			deleteTableToolStripMenuItem.Enabled = CanTableDeleteTable;
		}

		/// <summary>
		/// Gets a value indicating whether this instance can insert table row.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can insert table row; otherwise, <c>false</c>.
		/// </value>
		public bool CanInsertTableRow
		{
			get
			{
				return
					IsTableCurrentSelectionInsideTable;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance can insert table column.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can insert table column; otherwise, <c>false</c>.
		/// </value>
		public bool CanInsertTableColumn
		{
			get
			{
				return
					IsTableCurrentSelectionInsideTable;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance can table delete row.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can table delete row; otherwise, <c>false</c>.
		/// </value>
		public bool CanTableDeleteRow
		{
			get
			{
				return
					CurrentSelectionTableCell != null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance can table delete column.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can table delete column; otherwise, <c>false</c>.
		/// </value>
		public bool CanTableDeleteColumn
		{
			get
			{
				return
					CurrentSelectionTableCell != null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance can table delete table.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can table delete table; otherwise, <c>false</c>.
		/// </value>
		public bool CanTableDeleteTable
		{
			get
			{
				return
					CurrentSelectionTable != null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance can table row properties.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can table row properties; otherwise, <c>false</c>.
		/// </value>
		public bool CanTableRowProperties
		{
			get
			{
				return
					CurrentSelectionTableRow != null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance can table column properties.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can table column properties; otherwise, <c>false</c>.
		/// </value>
		public bool CanTableColumnProperties
		{
			get
			{
				return
					CurrentSelectionTableCell != null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance can table cell properties.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can table cell properties; otherwise, <c>false</c>.
		/// </value>
		public bool CanTableCellProperties
		{
			get
			{
				IHTMLTableCell[] cells =
					CurrentSelectionTableCells;

				return cells != null && cells.Length > 0;
			}
		}

		public bool IsTableSelection
		{
			get
			{
				if (!IsControlSelection)
				{
					return false;
				}
				else
				{
					var rng = CurrentSelectionControl;

					if (rng.length <= 0)
					{
						return false;
					}
					else
					{
						var element = rng.item(0);

						var tagName = element.tagName.ToLowerInvariant();
						return tagName == @"table";
					}
				}
			}
		}

		private IHTMLTable2 CurrentSelectionTable
		{
			get
			{
				// A complete table.
				if (IsTableSelection)
				{
					var rng = CurrentSelectionControl;
					var element = rng.item(0);

					return element as IHTMLTable2;
				}
				// Inside a table (nested)?
				else
				{
					IHTMLElement element;

					if (IsControlSelection)
					{
						IHTMLControlRange rng = CurrentSelectionControl;
						element = rng.item(0);
					}
					else
					{
						IHTMLTxtRange rng = CurrentSelectionText;
						element = rng.parentElement();
					}

					while (element != null)
					{
						string tagName = element.tagName.ToLowerInvariant();

						if (tagName == @"table")
						{
							return element as IHTMLTable2;
						}
						else
						{
							// Go up.
							element = element.parentElement;
						}
					}

					// Not found.
					return null;
				}
			}
		}

		private bool IsTableCurrentSelectionInsideTable
		{
			get
			{
				return CurrentSelectionTable != null;
			}
		}

		private IHTMLTableRow CurrentSelectionTableRow
		{
			get
			{
				if (CurrentSelectionTable == null)
				{
					return null;
				}
				// A complete table.
				else if (IsTableSelection || IsControlSelection)
				{
					return null;
				}
				else
				{
					// --
					// Go up until the TR is found.

					IHTMLElement element;

					if (IsControlSelection)
					{
						IHTMLControlRange rng = CurrentSelectionControl;
						element = rng.item(0);
					}
					else
					{
						IHTMLTxtRange rng = CurrentSelectionText;
						element = rng.parentElement();
					}

					while (element != null)
					{
						string tagName = element.tagName.ToLowerInvariant();

						if (tagName == @"tr")
						{
							return element as IHTMLTableRow;
						}
						else
						{
							// Go up.
							element = element.parentElement;
						}
					}

					// --

					// Not found.
					return null;
				}
			}
		}

		private IHTMLTableCell CurrentSelectionTableCell
		{
			get
			{
				if (CurrentSelectionTable == null)
				{
					return null;
				}
				// A complete table.
				else if (IsTableSelection || IsControlSelection)
				{
					return null;
				}
				else
				{
					// --
					// Go up until the TH or TD is found.

					IHTMLElement element;

					if (IsControlSelection)
					{
						IHTMLControlRange rng = CurrentSelectionControl;
						element = rng.item(0);
					}
					else
					{
						IHTMLTxtRange rng = CurrentSelectionText;
						element = rng.parentElement();
					}

					while (element != null)
					{
						string tagName = element.tagName.ToLowerInvariant();

						if (tagName == @"th" || tagName == @"td")
						{
							return element as IHTMLTableCell;
						}
						else
						{
							// Go up.
							element = element.parentElement;
						}
					}

					// --

					// Not found.
					return null;
				}
			}
		}

		private IHTMLTableCell[] CurrentSelectionTableCells
		{
			get
			{
				var result = new List<IHTMLTableCell>();

				if (CurrentSelectionTable != null)
				{
					IMarkupPointer mp1;
					IMarkupPointer mp2;
					GetCurrentSelection(out mp1, out mp2);

					// --

					// Walk from left to right of the current selection, 
					// storing all TH and TD tags.

					IMarkupPointer walk = mp1;

					while (compareLte(walk, mp2))
					{
						// walk right.
						_MARKUP_CONTEXT_TYPE context;
						IHTMLElement element;
						int minus1 = -1;
						ushort unused;
						walk.right(
							NativeMethods.BOOL_TRUE,
							out context,
							out element,
							ref minus1,
							out unused);

						if (element != null)
						{
							if (context ==
								_MARKUP_CONTEXT_TYPE.CONTEXT_TYPE_EnterScope)
							{
								string tagName = element.tagName.ToLowerInvariant();

								if (tagName == @"th" || tagName == @"td")
								{
									result.Add(element as IHTMLTableCell);
								}
							}
						}
					}

					// Nothing selected, just the carret inside a table cell?
					if (result.Count <= 0)
					{
						// --
						// Go up until the TH or TD is found.

						IHTMLElement element;

						if (IsControlSelection)
						{
							var rng = CurrentSelectionControl;
							element = rng.item(0);
						}
						else
						{
							var rng = CurrentSelectionText;
							element = rng.parentElement();
						}

						while (element != null)
						{
							string tagName = element.tagName.ToLowerInvariant();

							if (tagName == @"th" || tagName == @"td")
							{
								result.Add(element as IHTMLTableCell);
								break;
							}
							else
							{
								// Go up.
								element = element.parentElement;
							}
						}
					}
				}

				return result.ToArray();
			}
		}

		/// <summary>
		/// Returns "-1" if none/not found.
		/// </summary>
		/// <value>The index of the current selection table row.</value>
		private int CurrentSelectionTableRowIndex
		{
			get
			{
				IHTMLTableRow row =
					CurrentSelectionTableRow;

				if (row == null)
				{
					return -1;
				}
				else
				{
					return row.rowIndex;
				}
			}
		}

		/// <summary>
		/// Returns "-1" if none/not found.
		/// </summary>
		/// <value>The index of the current selection table column.</value>
		private int CurrentSelectionTableColumnIndex
		{
			get
			{
				IHTMLTableCell cell =
					CurrentSelectionTableCell;

				if (cell == null)
				{
					return -1;
				}
				else
				{
					return cell.cellIndex;
				}
			}
		}

		/// <summary>
		/// Returns "0" if none/not found.
		/// </summary>
		/// <value>The current selection table row count.</value>
		private int CurrentSelectionTableRowCount
		{
			get
			{
				IHTMLTable table =
					CurrentSelectionTable as IHTMLTable;

				if (table == null)
				{
					return 0;
				}
				else
				{
					IHTMLElementCollection rows = table.rows;

					if (rows == null)
					{
						return 0;
					}
					else
					{
						return rows.length;
					}
				}
			}
		}

		/// <summary>
		/// Returns "0" if none/not found.
		/// </summary>
		/// <value>The current selection table column count.</value>
		private int CurrentSelectionTableColumnCount
		{
			get
			{
				IHTMLTableRow row =
					CurrentSelectionTableRow;

				if (row == null)
				{
					return 0;
				}
				else
				{
					IHTMLElementCollection cells = row.cells;

					if (cells == null)
					{
						return 0;
					}
					else
					{
						return cells.length;
					}
				}
			}
		}

		public IHTMLDocument2 DomDocument
		{
			get
			{
				if (Document == null || Document.DomDocument == null)
				{
					return null;
				}
				else
				{
					return Document.DomDocument as IHTMLDocument2;
				}
			}
		}

		public IHTMLTxtRange CurrentSelectionText
		{
			get
			{
				if (DomDocument == null || DomDocument.all.length == 0)
				{
					return null;
				}
				else
				{
					var selection = DomDocument.selection;
					var rangeDisp = selection.createRange();

					var textRange = rangeDisp as IHTMLTxtRange;

					return textRange;
				}
			}
		}

		public IHTMLControlRange CurrentSelectionControl
		{
			get
			{
				if (DomDocument == null || DomDocument.all.length == 0)
				{
					return null;
				}
				else
				{
					var selection = DomDocument.selection;
					var rangeDisp = selection.createRange();

					var textRange =
						rangeDisp as IHTMLControlRange;

					return textRange;
				}
			}
		}

		public bool IsControlSelection
		{
			get
			{
				IHTMLSelectionObject selection =
					DomDocument.selection;

				string selectionType = selection.type.ToLowerInvariant();

				return selectionType == @"control";
			}
		}

		public bool IsTextSelection
		{
			get
			{
				IHTMLSelectionObject selection =
					DomDocument.selection;

				string selectionType = selection.type.ToLowerInvariant();

				return selectionType == @"text";
			}
		}

		public bool IsNoneSelection
		{
			get
			{
				IHTMLSelectionObject selection =
					DomDocument.selection;

				string selectionType = selection.type.ToLowerInvariant();

				return selectionType == @"none";
			}
		}

		#region Comparison of IMarkupPointer interfaces.
		// ------------------------------------------------------------------

		public static bool compareLt(
			IMarkupPointer p1,
			IMarkupPointer p2)
		{
			int flag;
			p1.IsLeftOf(p2, out flag);
			return flag == NativeMethods.BOOL_TRUE;
		}

		public static bool compareLte(
			IMarkupPointer p1,
			IMarkupPointer p2)
		{
			int flag;
			p1.IsLeftOfOrEqualTo(p2, out flag);
			return flag == NativeMethods.BOOL_TRUE;
		}

		public static bool CompareE(
			IMarkupPointer p1,
			IMarkupPointer p2)
		{
			int flag;
			p1.IsEqualTo(p2, out flag);
			return flag == NativeMethods.BOOL_TRUE;
		}

		public static bool compareGte(
			IMarkupPointer p1,
			IMarkupPointer p2)
		{
			int flag;
			p1.IsRightOfOrEqualTo(p2, out flag);
			return flag == NativeMethods.BOOL_TRUE;
		}

		public static bool compareGt(
			IMarkupPointer p1,
			IMarkupPointer p2)
		{
			int flag;
			p1.IsRightOf(p2, out flag);
			return flag == NativeMethods.BOOL_TRUE;
		}

		// ------------------------------------------------------------------
		#endregion

		/// <summary>
		/// Handles the Click event of the insertNewTableToolStripMenuItem 
		/// control.
		/// </summary>
		private void insertNewTableToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteInsertTable();
		}

		/// <summary>
		/// Handles the Click event of the 
		/// insertRowBeforeCurrentRowToolStripMenuItem control.
		/// </summary>
		private void insertRowBeforeCurrentRowToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteInsertTableRow();
		}

		/// <summary>
		/// Handles the Click event of the i
		/// nsertColumnBeforeCurrentColumnToolStripMenuItem control.
		/// </summary>
		private void insertColumnBeforeCurrentColumnToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteInsertTableColumn();
		}

		/// <summary>
		/// Handles the Click event of the 
		/// addRowAfterTheLastTableRowToolStripMenuItem control.
		/// </summary>
		private void addRowAfterTheLastTableRowToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteTableAddTableRow();
		}

		/// <summary>
		/// Handles the Click event of the 
		/// addColumnAfterTheLastTableColumnToolStripMenuItem control.
		/// </summary>
		private void addColumnAfterTheLastTableColumnToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteTableAddTableColumn();
		}

		private void tablePropertiesToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteTableProperties();
		}

		private void rowPropertiesToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteTableRowProperties();
		}

		private void columnPropertiesToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteTableColumnProperties();
		}

		private void cellPropertiesToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteTableCellProperties();
		}

		private void deleteRowToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteTableDeleteRow();
		}

		private void deleteColumnToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteTableDeleteColumn();
		}

		private void deleteTableToolStripMenuItem_Click(
			object sender,
			EventArgs e)
		{
			ExecuteTableDeleteTable();
		}

		protected virtual void OnUpdateUI()
		{
			if (UINeedsUpdate != null)
			{
				UINeedsUpdate(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Event handler indicating that the GUI needs to be updated
		/// (enabled/disable controls etc.).
		/// </summary>
		public event EventHandler UINeedsUpdate;


		#region IDocHostUIHandler members.
		// ------------------------------------------------------------------

		public int ShowContextMenu(
			int dwID,
			NativeMethods.POINT pt,
			object pcmdtReserved,
			object pdispReserved)
		{
			NativeMethods.ContextMenuKind kind =
				NativeMethods.ContextMenuKind.CONTEXT_MENU_DEFAULT;

			if (dwID == 0x02)
			{
				kind = NativeMethods.ContextMenuKind.CONTEXT_MENU_DEFAULT;
			}
			else if (dwID == 0x04)
			{
				kind = NativeMethods.ContextMenuKind.CONTEXT_MENU_CONTROL;
			}
			else if (dwID == 0x08)
			{
				kind = NativeMethods.ContextMenuKind.CONTEXT_MENU_TABLE;
			}
			else if (dwID == 0x10)
			{
				kind = NativeMethods.ContextMenuKind.CONTEXT_MENU_TEXTSELECT;
			}
			else if (dwID == 0x30)
			{
				kind = NativeMethods.ContextMenuKind.CONTEXT_MENU_ANCHOR;
			}
			else if (dwID == 0x20)
			{
				kind = NativeMethods.ContextMenuKind.CONTEXT_MENU_UNKNOWN;
			}

			var queryForStatus = pcmdtReserved as NativeMethods.IUnknown;
			var objectAtScreenCoordinates = pdispReserved as NativeMethods.IDispatch;

			if (OnNeedShowContextMenu(
				kind,
				new Point(pt.x, pt.y),
				queryForStatus,
				objectAtScreenCoordinates))
			{
				// Don't show MSHTML context menu but the one that will be attached
				// in a derived class.
				return NativeMethods.SRESULTS.S_OK;
			}
			else
			{
				// Let MSHTML show the context menu.
				return NativeMethods.SRESULTS.S_FALSE;
			}
		}

		protected virtual bool OnNeedShowContextMenu(
			NativeMethods.ContextMenuKind contextMenuKind,
			Point position,
			NativeMethods.IUnknown queryForStatus,
			NativeMethods.IDispatch objectAtScreenCoordinates)
		{
			contextMenuStrip.Show(position);
			return true;
		}

		/// <summary>
		/// Gets the host info.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <returns></returns>
		public int GetHostInfo(
			NativeMethods.DOCHOSTUIINFO info)
		{
			info.cbSize = Marshal.SizeOf(typeof(NativeMethods.DOCHOSTUIINFO));
			info.dwFlags = (int)(
				NativeMethods.DOCHOSTUIFLAG.DOCHOSTUIFLAG_NO3DOUTERBORDER |
				NativeMethods.DOCHOSTUIFLAG.DOCHOSTUIFLAG_NO3DBORDER |

				// http://msdn.microsoft.com/library/default.asp?url=/workshop/browser/hosting/reference/enum/dochostuiflag.asp
				// set the DOCHOSTUIFLAG_THEME if you want your buttons to have the XP look.
				NativeMethods.DOCHOSTUIFLAG.DOCHOSTUIFLAG_THEME);

			// default indicates we don't have info.
			return NativeMethods.SRESULTS.S_OK;
		}

		/// <summary>
		/// Shows the UI.
		/// </summary>
		/// <param name="dwID">The dw ID.</param>
		/// <param name="activeObject">The active object.</param>
		/// <param name="commandTarget">The command target.</param>
		/// <param name="frame">The frame.</param>
		/// <param name="doc">The doc.</param>
		/// <returns></returns>
		public int ShowUI(
			int dwID,
			UnsafeNativeMethods.IOleInPlaceActiveObject activeObject,
			NativeMethods.IOleCommandTarget commandTarget,
			UnsafeNativeMethods.IOleInPlaceFrame frame,
			UnsafeNativeMethods.IOleInPlaceUIWindow doc)
		{
			// default means we don't have any UI, and control should show its UI
			return NativeMethods.SRESULTS.S_FALSE;
		}

		public int HideUI()
		{
			// we don't have UI by default, so just pretend we hid it
			return NativeMethods.SRESULTS.S_OK;
		}

		public virtual int UpdateUI()
		{
			if (_documentLoaded)
			{
				OnUpdateUI();
			}

			return NativeMethods.SRESULTS.S_OK;
		}

		public int EnableModeless(
			bool fEnable)
		{
			// We don't have any UI by default, so pretend we updated it.
			return NativeMethods.SRESULTS.S_OK;
		}

		public int OnDocWindowActivate(
			bool fActivate)
		{
			// We don't have any UI by default, so pretend we updated it.
			return NativeMethods.SRESULTS.S_OK;
		}

		public int OnFrameWindowActivate(
			bool fActivate)
		{
			// We don't have any UI by default, so pretend we updated it.
			return NativeMethods.SRESULTS.S_OK;
		}

		public int ResizeBorder(
			NativeMethods.COMRECT rect,
			UnsafeNativeMethods.IOleInPlaceUIWindow doc,
			bool fFrameWindow)
		{
			// We don't have any UI by default, so pretend we updated it.
			return NativeMethods.SRESULTS.S_OK;
		}

		public int TranslateAccelerator(
			ref NativeMethods.MSG msg,
			ref Guid group,
			int nCmdID)
		{
			// No translation here.
			return NativeMethods.SRESULTS.S_FALSE;
		}

		public int GetOptionKeyPath(
			string[] pbstrKey,
			int dw)
		{
			// No replacement option key.
			return NativeMethods.SRESULTS.S_FALSE;
		}

		public int GetDropTarget(
			UnsafeNativeMethods.IOleDropTarget pDropTarget,
			out UnsafeNativeMethods.IOleDropTarget ppDropTarget)
		{
			// no additional drop target
			ppDropTarget = pDropTarget;
			return NativeMethods.SRESULTS.S_FALSE;
		}

		/// <summary>
		/// Gets the external.
		/// </summary>
		/// <param name="ppDispatch">The pp dispatch.</param>
		/// <returns></returns>
		public int GetExternal(
			out object ppDispatch)
		{
			// window.external from JavaScript.

			ppDispatch = this;
			return NativeMethods.SRESULTS.S_OK;
		}

		public int TranslateUrl(
			int dwTranslate,
			string strUrlIn,
			out string pstrUrlOut)
		{
			// no translation happens by default
			pstrUrlOut = strUrlIn;
			return NativeMethods.SRESULTS.S_FALSE;
		}

		public int FilterDataObject(
			System.Runtime.InteropServices.ComTypes.IDataObject pDo,
			out System.Runtime.InteropServices.ComTypes.IDataObject ppDoRet)
		{
			// no data object by default
			ppDoRet = pDo;
			return NativeMethods.SRESULTS.S_FALSE;
		}

		// ------------------------------------------------------------------
		#endregion
	}
}