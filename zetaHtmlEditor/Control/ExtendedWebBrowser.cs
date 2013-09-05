namespace ZetaHtmlEditControl
{
	using System;
	using System.ComponentModel;
	using System.Runtime.InteropServices;
	using System.Windows.Forms;

	public class ExtendedWebBrowser :
		WebBrowser
	{
		public ExtendedWebBrowser()
		{
			if(!DesignMode)
			{
				turnOffProxy();
			}
		}

		public struct Struct_INTERNET_PROXY_INFO
		{
			public int dwAccessType;
			public IntPtr proxy;
			public IntPtr proxyBypass;
		};

		[DllImport(@"wininet.dll", SetLastError = true)]
		private static extern bool InternetSetOption(
			IntPtr hInternet,
			int dwOption,
			IntPtr lpBuffer,
			int lpdwBufferLength); 

		private void turnOffProxy()
		{
			// http://blogs.msdn.com/b/wndp/archive/2005/07/20/441060.aspx
			// http://support.microsoft.com/kb/226473/en-us
		}

		public new void Navigate(string url)
		{
			// This Application.DoEvents() is necessary, 
			// otherwise the webbrowser gets a 
			// AccessViolationException, whyever.
			Application.DoEvents();

			// Turn off before navigating to get rid of the "Document was modified" message box.
			// http://social.msdn.microsoft.com/Forums/en/winforms/thread/4928c061-951a-43cc-aad2-8844084c148d
			turnWebBrowserDesignModeOff();

			// This Application.DoEvents() is necessary, 
			// otherwise the webbrowser gets a 
			// AccessViolationException, whyever.
			Application.DoEvents();

			base.Navigate(url);
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string DocumentText
		{
			get
			{
				return DesignMode ? string.Empty : base.DocumentText;
			}
			set
			{
				if (!DesignMode)
				{
					Navigate(webServer.SetDocumentText(this, value));
				}
			}
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get { return DocumentText; }
			set
			{
				DocumentText = value;
			}
		}

		protected override void OnDocumentCompleted(
			WebBrowserDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);

			turnWebBrowserDesignModeOn();

			// This Application.DoEvents() is necessary, 
			// otherwise the webbrowser gets a 
			// AccessViolationException, whyever.
			Application.DoEvents();
		}

		private static readonly object TypeLock = new object();
		private static IExternalWebServer _webServer;
		private bool _wasOn;
		private static IExternalWebServer _externalWebServer;

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public static IExternalWebServer ExternalWebServer
		{
			get
			{
				lock (TypeLock)
				{
					return _externalWebServer;
				}
			}
			set
			{
				lock (TypeLock)
				{
					_externalWebServer = value;
				}
			}
		}

		private static IExternalWebServer webServer
		{
			get
			{
				lock (TypeLock)
				{
					if (_externalWebServer != null)
					{
						return _externalWebServer;
					}
					else
					{
						if (_webServer == null)
						{
							var ws = new WebServer();
							ws.Initialize();
							_webServer = ws;
						}

						return _webServer;
					}
				}
			}
		}

		private void turnWebBrowserDesignModeOn()
		{
			var instance =
				Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateGet(
					ActiveXInstance,
					null,
					@"Document",
					new object[0],
					null,
					null,
					null);

			Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateSetComplex(
				instance,
				null,
				@"designMode",
				new object[] { @"On" },
				null,
				null,
				false,
				true);

			_wasOn = true;
		}

		private void turnWebBrowserDesignModeOff()
		{
			if (!_wasOn)
			{
				return;
			}

			var instance =
				Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateGet(
					ActiveXInstance,
					null,
					@"Document",
					new object[0],
					null,
					null,
					null);

			Microsoft.VisualBasic.CompilerServices.NewLateBinding.LateSetComplex(
				instance,
				null,
				@"designMode",
				new object[] { @"Off" },
				null,
				null,
				false,
				true);
		}
	}
}