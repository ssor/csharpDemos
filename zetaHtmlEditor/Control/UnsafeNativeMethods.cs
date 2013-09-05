namespace ZetaHtmlEditControl
{
	using System;
	using System.Runtime.CompilerServices;
	using System.Runtime.InteropServices;
	using System.Security;
	using System.Windows.Forms;
	using IDataObject = System.Runtime.InteropServices.ComTypes.IDataObject;

	public sealed class UnsafeNativeMethods
	{
		[ComImport, Guid( @"BD3F23C0-D43E-11CF-893B-00AA00BDCE1A" ), ComVisible( true ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
		public interface IDocHostUIHandler
		{
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int ShowContextMenu( [In, MarshalAs( UnmanagedType.U4 )] int dwID, [In] NativeMethods.POINT pt, [In, MarshalAs( UnmanagedType.Interface )] object pcmdtReserved, [In, MarshalAs( UnmanagedType.Interface )] object pdispReserved );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int GetHostInfo( [In, Out] NativeMethods.DOCHOSTUIINFO info );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int ShowUI( [In, MarshalAs( UnmanagedType.I4 )] int dwID, [In] IOleInPlaceActiveObject activeObject, [In] NativeMethods.IOleCommandTarget commandTarget, [In] IOleInPlaceFrame frame, [In] IOleInPlaceUIWindow doc );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int HideUI();
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int UpdateUI();
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int EnableModeless( [In, MarshalAs( UnmanagedType.Bool )] bool fEnable );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int OnDocWindowActivate( [In, MarshalAs( UnmanagedType.Bool )] bool fActivate );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int OnFrameWindowActivate( [In, MarshalAs( UnmanagedType.Bool )] bool fActivate );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int ResizeBorder( [In] NativeMethods.COMRECT rect, [In] IOleInPlaceUIWindow doc, bool fFrameWindow );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int TranslateAccelerator( [In] ref NativeMethods.MSG msg, [In] ref Guid group, [In, MarshalAs( UnmanagedType.I4 )] int nCmdID );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int GetOptionKeyPath( [Out, MarshalAs( UnmanagedType.LPArray )] string[] pbstrKey, [In, MarshalAs( UnmanagedType.U4 )] int dw );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int GetDropTarget( [In, MarshalAs( UnmanagedType.Interface )] IOleDropTarget pDropTarget, [MarshalAs( UnmanagedType.Interface )] out IOleDropTarget ppDropTarget );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int GetExternal( [MarshalAs( UnmanagedType.Interface )] out object ppDispatch );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int TranslateUrl( [In, MarshalAs( UnmanagedType.U4 )] int dwTranslate, [In, MarshalAs( UnmanagedType.LPWStr )] string strURLIn, [MarshalAs( UnmanagedType.LPWStr )] out string pstrURLOut );
			[return: MarshalAs( UnmanagedType.I4 )]
			[PreserveSig]
			int FilterDataObject( IDataObject pDO, out IDataObject ppDORet );
		}

		[ComImport, InterfaceType( ComInterfaceType.InterfaceIsIUnknown ),
			Guid( @"3050f3f0-98b5-11cf-bb82-00aa00bdce0b" )]
		internal interface ICustomDoc
		{
			[PreserveSig]
			void SetUIHandler( IDocHostUIHandler pUIHandler );
		}

		[ComImport, Guid( @"00000117-0000-0000-C000-000000000046" ), SuppressUnmanagedCodeSecurity, InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
		public interface IOleInPlaceActiveObject
		{
			[PreserveSig]
			int GetWindow( out IntPtr hwnd );
			void ContextSensitiveHelp( int fEnterMode );
			[PreserveSig]
			int TranslateAccelerator( [In] ref NativeMethods.MSG lpmsg );
			void OnFrameWindowActivate( bool fActivate );
			void OnDocWindowActivate( int fActivate );
			void ResizeBorder( [In] NativeMethods.COMRECT prcBorder, [In] IOleInPlaceUIWindow pUIWindow, bool fFrameWindow );
			void EnableModeless( int fEnable );
		}

		[ComImport, InterfaceType( ComInterfaceType.InterfaceIsIUnknown ), Guid( @"00000115-0000-0000-C000-000000000046" )]
		public interface IOleInPlaceUIWindow
		{
			IntPtr GetWindow();
			[PreserveSig]
			int ContextSensitiveHelp( int fEnterMode );
			[PreserveSig]
			int GetBorder( [Out] NativeMethods.COMRECT lprectBorder );
			[PreserveSig]
			int RequestBorderSpace( [In] NativeMethods.COMRECT pborderwidths );
			[PreserveSig]
			int SetBorderSpace( [In] NativeMethods.COMRECT pborderwidths );
			void SetActiveObject( [In, MarshalAs( UnmanagedType.Interface )] IOleInPlaceActiveObject pActiveObject, [In, MarshalAs( UnmanagedType.LPWStr )] string pszObjName );
		}




		/*	[Serializable]
			public enum UnmanagedType
			{
				// Fields
				AnsiBStr = 0x23,
				AsAny = 40,
				Bool = 2,
				BStr = 0x13,
				ByValArray = 30,
				ByValTStr = 0x17,
				Currency = 15,
				CustomMarshaler = 0x2c,
				Error = 0x2d,
				FunctionPtr = 0x26,
				I1 = 3,
				I2 = 5,
				I4 = 7,
				I8 = 9,
				IDispatch = 0x1a,
				Interface = 0x1c,
				IUnknown = 0x19,
				LPArray = 0x2a,
				LPStr = 20,
				LPStruct = 0x2b,
				LPTStr = 0x16,
				LPWStr = 0x15,
				R4 = 11,
				R8 = 12,
				SafeArray = 0x1d,
				Struct = 0x1b,
				SysInt = 0x1f,
				SysUInt = 0x20,
				TBStr = 0x24,
				U1 = 4,
				U2 = 6,
				U4 = 8,
				U8 = 10,
				VariantBool = 0x25,
				VBByRefStr = 0x22
			}
			*/
		[ComImport, Guid( @"00000122-0000-0000-C000-000000000046" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
		public interface IOleDropTarget
		{
			[PreserveSig]
			int OleDragEnter( [In, MarshalAs( UnmanagedType.Interface )] object pDataObj, [In, MarshalAs( UnmanagedType.U4 )] int grfKeyState, [In, MarshalAs( UnmanagedType.U8 )] long pt, [In, Out] ref int pdwEffect );
			[PreserveSig]
			int OleDragOver( [In, MarshalAs( UnmanagedType.U4 )] int grfKeyState, [In, MarshalAs( UnmanagedType.U8 )] long pt, [In, Out] ref int pdwEffect );
			[PreserveSig]
			int OleDragLeave();
			[PreserveSig]
			int OleDrop( [In, MarshalAs( UnmanagedType.Interface )] object pDataObj, [In, MarshalAs( UnmanagedType.U4 )] int grfKeyState, [In, MarshalAs( UnmanagedType.U8 )] long pt, [In, Out] ref int pdwEffect );
		}

		[ComImport, Guid( @"00000116-0000-0000-C000-000000000046" ), InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
		public interface IOleInPlaceFrame
		{
			IntPtr GetWindow();
			[PreserveSig]
			int ContextSensitiveHelp( int fEnterMode );
			[PreserveSig]
			int GetBorder( [Out] NativeMethods.COMRECT lprectBorder );
			[PreserveSig]
			int RequestBorderSpace( [In] NativeMethods.COMRECT pborderwidths );
			[PreserveSig]
			int SetBorderSpace( [In] NativeMethods.COMRECT pborderwidths );
			[PreserveSig]
			int SetActiveObject( [In, MarshalAs( UnmanagedType.Interface )] IOleInPlaceActiveObject pActiveObject, [In, MarshalAs( UnmanagedType.LPWStr )] string pszObjName );
			[PreserveSig]
			int InsertMenus( [In] IntPtr hmenuShared, [In, Out] NativeMethods.tagOleMenuGroupWidths lpMenuWidths );
			[PreserveSig]
			int SetMenu( [In] IntPtr hmenuShared, [In] IntPtr holemenu, [In] IntPtr hwndActiveObject );
			[PreserveSig]
			int RemoveMenus( [In] IntPtr hmenuShared );
			[PreserveSig]
			int SetStatusText( [In, MarshalAs( UnmanagedType.LPWStr )] string pszStatusText );
			[PreserveSig]
			int EnableModeless( bool fEnable );
			[PreserveSig]
			int TranslateAccelerator( [In] ref NativeMethods.MSG lpmsg, [In, MarshalAs( UnmanagedType.U2 )] short wID );
		}



		[DllImport( @"user32.dll", CharSet = CharSet.Auto, ExactSpelling = true )]
		public static extern int MapWindowPoints( HandleRef hWndFrom, HandleRef hWndTo, [In, Out] NativeMethods.POINT pt, int cPoints );
	}
}
