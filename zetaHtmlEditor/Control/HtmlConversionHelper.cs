namespace ZetaHtmlEditControl
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Text.RegularExpressions;
	using System.Xml;
	using Sgml;

	/// <summary>
	/// 
	/// </summary>
	internal sealed class HtmlConversionHelper :
		IDisposable
	{
		private readonly List<string> _cleanPaths = new List<string>();

		/// <summary>
		/// Converts the HTML.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <param name="baseUri">The base URI.</param>
		/// <param name="saveFolderPath">The save folder path.</param>
		/// <param name="imagesFolderPathPlaceHolder"></param>
		/// <returns></returns>
		internal string ConvertGetHtml(
			string html,
			Uri baseUri,
			string saveFolderPath,
			string imagesFolderPathPlaceHolder)
		{
			if (string.IsNullOrEmpty(html))
			{
				return html;
			}
			else
			{
				if (string.IsNullOrEmpty(saveFolderPath))
				{
					saveFolderPath =
						Path.Combine(
							Path.GetTempPath(),
							Guid.NewGuid().ToString());
					Directory.CreateDirectory(saveFolderPath);

					if (!_cleanPaths.Contains(saveFolderPath))
					{
						_cleanPaths.Add(saveFolderPath);
					}
				}
				else
				{
					if (!Directory.Exists(saveFolderPath))
					{
						Directory.CreateDirectory(saveFolderPath);
					}
				}

				var images = FindImgs(html);

				foreach (var s in images)
				{
					// pfad bauen
					var filePath =
						Path.Combine(
							saveFolderPath,
							Guid.NewGuid().ToString());

					// holen
					byte[] image = null;
					if (!s.StartsWith(Uri.UriSchemeHttp) &&
						!s.StartsWith(Uri.UriSchemeHttps) &&
						!s.StartsWith(Uri.UriSchemeFtp))
					{
						string readFrom;

						// 2006-12-03, Uwe Keim.
						if (s.StartsWith(Uri.UriSchemeFile))
						{
							readFrom = PathHelper.ConvertFileUrlToFilePath(s);
						}
						else
						{
							readFrom = s;
						}

						var pf = GetPathFromFile(readFrom, baseUri);

						if (File.Exists(pf))
						{
							image = File.ReadAllBytes(pf);
						}
					}
					else
					{
						image = new WebClient().DownloadData(
							getWebAddressFromFile(s, baseUri));
					}

					if (image != null)
					{
						//schreiben
						File.WriteAllBytes(filePath, image);

						//ersetzen
						var pattern = string.Format(
							@"([""']){0}([""'])",
							escapeRegularExpressionCharacters(s));

						var fileUrlPath =
							PathHelper.ConvertFilePathToFileUrl(filePath);

						// If requested to have placeholder, put it now.
						if (!string.IsNullOrEmpty(imagesFolderPathPlaceHolder))
						{
							var folderUrlPath =
								PathHelper.ConvertFilePathToFileUrl(saveFolderPath);

							fileUrlPath =
								PathHelper.CombineVirtual(
									imagesFolderPathPlaceHolder,
									fileUrlPath.Substring(folderUrlPath.Length));
						}

						var replacement = string.Format(
							@"$1{0}$2",
							fileUrlPath);

						html = Regex.Replace(
							html,
							pattern,
							replacement,
							RegexOptions.IgnoreCase);
					}

				}

				return html;
			}
		}

		internal string ConvertSetHtml(
			string html,
			string saveFolderPath,
			string imagesFolderPathPlaceHolder)
		{
			if (string.IsNullOrEmpty(html))
			{
				return html;
			}
			else
			{
				if (string.IsNullOrEmpty(imagesFolderPathPlaceHolder))
				{
					return html;
				}
				else
				{
					var folderUrlPath =
						PathHelper.ConvertFilePathToFileUrl(
						saveFolderPath).TrimEnd('/');

					imagesFolderPathPlaceHolder =
						imagesFolderPathPlaceHolder.TrimEnd('/');

					html =
						html.Replace(
							imagesFolderPathPlaceHolder,
							folderUrlPath);

					return html;
				}
			}
		}

		/// <summary>
		/// Finds the images.
		/// </summary>
		/// <param name="htmlCode">The HTML code.</param>
		/// <returns></returns>
		internal static string[] FindImgs(
			string htmlCode)
		{
			var r =
				new SgmlReader
					{
						DocType = @"HTML",
						InputStream = new StringReader(htmlCode)
					};
			var al = new List<string>();

			//find <img src=""
			while (r.Read())
			{
				if (r.NodeType == XmlNodeType.Element)
				{
					if (string.Compare(r.Name, @"img", true) == 0)
					{
						if (r.HasAttributes)
						{
							while (r.MoveToNextAttribute())
							{
								if (r.Name.ToLower() == @"src")
								{
									if (!al.Contains(r.Value))
									{
										al.Add(r.Value);
									}
								}
							}
						}
					}
				}
			}

			return al.ToArray();
		}

		/// <summary>
		/// Gets the web address from file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="baseUri">The base URI.</param>
		/// <returns></returns>
		private static string getWebAddressFromFile(
			string file,
			Uri baseUri)
		{
			if (file.StartsWith(@"http") || file.StartsWith(@"https"))
			{
				return file;
			}
			else if (file.IndexOf(@"\") == 0)
			{
				return baseUri + file;
			}
			else if (file.IndexOf(@"/") == 0)
			{
				return baseUri + file;
			}
			else if (file.StartsWith(@"file")
				|| file.Substring(1, 2) == @":\"
				|| baseUri.AbsolutePath == @"about:blank")
			{
				return file;
			}
			else
			{
				return baseUri + @"/" + file;
			}
		}

		/// <summary>
		/// Escapes the regular expression characters.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		private static string escapeRegularExpressionCharacters(
			string text)
		{
			text = text.Replace(@"\", @"\\");
			text = text.Replace(@"+", @"\+");
			text = text.Replace(@"?", @"\?");
			text = text.Replace(@".", @"\.");
			text = text.Replace(@"*", @"\*");
			text = text.Replace(@"^", @"\^");
			text = text.Replace(@"$", @"\$");
			text = text.Replace(@"(", @"\(");
			text = text.Replace(@")", @"\)");
			text = text.Replace(@"[", @"\[");
			text = text.Replace(@"]", @"\]");
			text = text.Replace(@"{", @"\{");
			text = text.Replace(@"}", @"\}");
			text = text.Replace(@"|", @"\|");
			return text;
		}

		#region IDisposable member.

		/// <summary>
		/// Performs application-defined tasks associated with freeing, 
		/// releasing, or resetting unmanaged resources.
		/// </summary>
		void IDisposable.Dispose()
		{
			endClean();
		}

		#endregion

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations 
		/// before the
		/// <see cref="HtmlConversionHelper"/> is reclaimed by garbage collection.
		/// </summary>
		~HtmlConversionHelper()
		{
			endClean();
		}

		/// <summary>
		/// Ends the clean.
		/// </summary>
		private void endClean()
		{
			foreach (var s in _cleanPaths)
			{
				if (!string.IsNullOrEmpty(s))
				{
					if (Directory.Exists(s))
					{
						Directory.Delete(s, true);
					}
				}
			}
			_cleanPaths.Clear();
		}

		/// <summary>
		/// Gets the path from file.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <param name="baseUri">The base URI.</param>
		/// <returns></returns>
		internal static string GetPathFromFile(
			string s,
			Uri baseUri)
		{
			// 2006-12-03 Uwe Keim, fix for not copying images.
			if (string.Compare(
				baseUri.OriginalString, @"about:blank", true) == 0)
			{
				return s;
			}
			else
			{
				var result = Path.Combine(baseUri.AbsolutePath, s);

				return result;
			}
		}
	}
}
