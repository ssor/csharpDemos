namespace ZetaHtmlEditControl
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Net;
	using System.Net.NetworkInformation;
	using System.Text;
	using HttpServer;
	using HttpServer.HttpModules;
	using HttpServer.Sessions;

	internal sealed class WebServer :
		IExternalWebServer
	{
		private class MyLogWriter :
			ILogWriter
		{
			private static void traceLog(
				string type,
				string message)
			{
				Trace.WriteLine(
					string.Format(
						@"[Preview web server, {0}] {1}",
						type,
						message));
			}

			#region Implementation of ILogWriter

			public void Write(object source, LogPrio priority, string message)
			{
				traceLog(priority.ToString(), message);
			}

			#endregion
		}

		private class MyModule :
			HttpModule
		{
			private readonly WebServer _owner;

			public MyModule(WebServer owner)
			{
				_owner = owner;
			}

			public override bool Process(
				IHttpRequest request,
				IHttpResponse response,
				IHttpSession session)
			{
				var urlAbsolute = request.Uri.AbsolutePath;

				if (urlAbsolute.StartsWith(@"/texts"))
				{
					var text = _owner.getDictionary(cleanUriEnd(removeUriStart(@"/texts/", urlAbsolute)));
					checkSendText(request, response, text.Html);
					return true;
				}
				else
				{
					// Ignore several known URLs.
					if (urlAbsolute.StartsWith(@"/favicon.ico"))
					{
						return false;
					}
					else
					{
						throw new Exception(string.Format("Unexpected path '{0}'.", urlAbsolute));
					}
				}
			}
		}

		private TextInfo getDictionary(string key)
		{
			lock (_thisLock)
			{
				foreach (var info in _texts)
				{
					if (info.Key == key)
					{
						return info;
					}
				}

				// Not found.
				return null;
			}
		}

		private int _port;
		private HttpServer _server;

		public void Uninitialize()
		{
			if (_server != null)
			{
				var listener = _server;
				_server = null;
				listener.Stop();
			}
		}

		public void Initialize()
		{
			if (_server == null)
			{
				_port = getFreePort();

				_server = new HttpServer(new MyLogWriter());

				_server.Add(new MyModule(this));
				_server.Start(IPAddress.Loopback, _port);

				Trace.WriteLine(
					string.Format(
						@"[Web server] Started local web server for URL '{0}'.",
						baseUrl));
			}
		}

		private string baseUrl
		{
			get
			{
				return string.Format(
					@"http://127.0.0.1:{0}/texts/",
					_port);
			}
		}

		private static void checkSendText(
			IHttpRequest request,
			IHttpResponse response,
			string text)
		{
			response.ContentType = @"text/html";

			if (!string.IsNullOrEmpty(request.Headers[@"if-Modified-Since"]))
			{
#pragma warning disable 162
				response.Status = HttpStatusCode.OK;
#pragma warning restore 162
			}

			addNeverCache(response);

			if (request.Method != @"Headers" && response.Status != HttpStatusCode.NotModified)
			{
				Trace.WriteLine(
					string.Format(
						@"[Web server] Sending text for URL '{0}': '{1}'.",
						request.Uri.AbsolutePath,
						text));

				var buffer2 = getBytesWithBom(text);

				response.ContentLength = buffer2.Length;
				response.SendHeaders();

				response.SendBody(buffer2, 0, buffer2.Length);
			}
			else
			{
				response.ContentLength = 0;
				response.SendHeaders();

				Trace.WriteLine(@"[Web server] Not sending.");
			}
		}

		private static byte[] getBytesWithBom(string text)
		{
			return Encoding.UTF8.GetBytes(text);
		}

		private static void addNeverCache(IHttpResponse response)
		{
			response.AddHeader(@"Last-modified", new DateTime(2005, 1, 1).ToUniversalTime().ToString(@"r"));

			response.AddHeader(@"Cache-Control", @"no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
			response.AddHeader(@"Pragma", @"no-cache");
		}

		private static string removeUriStart(string uriStart, string uri)
		{
			return uri.StartsWith(uriStart) ? uri.Substring(uriStart.Length) : uri;
		}

		private static string cleanUriEnd(string uri)
		{
			var index = uri.IndexOf('?');
			return index >= 0 ? uri.Substring(0, index) : uri;
		}

		private static int getFreePort()
		{
			var random = new Random(Guid.NewGuid().GetHashCode());

			for (var i = 0; i < 10; ++i)
			{
				var port = random.Next(9000, 15000);
				if (isPortFree(port))
				{
					return port;
				}
			}

			throw new Exception("Unable to acquire free port.");
		}

		private static bool isPortFree(int port)
		{
			// http://stackoverflow.com/questions/570098/in-c-how-to-check-if-a-tcp-port-is-available

			var globalProperties = IPGlobalProperties.GetIPGlobalProperties();
			var informations = globalProperties.GetActiveTcpConnections();

			foreach (var information in informations)
			{
				if (information.LocalEndPoint.Port == port)
				{
					return false;
				}
			}

			return true;
		}

		public string SetDocumentText(object sender, string html)
		{
			lock (_thisLock)
			{
				var hc = sender.GetHashCode();

				// Look first, whether already present.
				foreach (var info in _texts)
				{
					if (info.SenderHashCode == hc)
					{
						info.Html = html;
						var r = baseUrl + info.Key;

						Trace.WriteLine(
							string.Format(
								@"[Web server] Setting EXISTING document text for URL '{0}': '{1}'. Stack: {2}.",
								r,
								html,
								new StackTrace()));
						return r;
					}
				}

				// --

				var key = (++_counter).ToString();
				_texts.Add(new TextInfo { SenderHashCode = hc, Key = key, Html = html });

				var s = baseUrl + key;

				Trace.WriteLine(
					string.Format(
						@"[Web server] Setting NEW document text for URL '{0}': '{1}'. Stack: {2}.",
						s,
						html,
						new StackTrace()));

				return s;
			}
		}

		private class TextInfo
		{
			public int SenderHashCode { get; set; }
			public string Key { get; set; }
			public string Html { get; set; }
		}

		private readonly List<TextInfo> _texts = new List<TextInfo>();
		private int _counter;
		private readonly object _thisLock = new object();
	}
}