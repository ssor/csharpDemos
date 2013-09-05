namespace ZetaHtmlEditControl
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;

	internal partial class HtmlSourceTextEditForm :
		Form
	{
		public HtmlSourceTextEditForm(
			string htmlCode )
		{
			InitializeComponent();

			if ( string.IsNullOrEmpty( htmlCode ) )
			{
				textboxEdit.Text = string.Empty;
			}
			else
			{
				textboxEdit.Text = htmlCode.Trim();
			}
		}

		private void HtmlSourceTextEditForm_Load( object sender, EventArgs e )
		{
			if ( ExternalInformationProvider != null )
			{
				Width = Convert.ToInt32(
					ExternalInformationProvider.RestorePerUserPerWorkstationValue(
						StoreID + @".Width",
						Width.ToString() ) );
				Height = Convert.ToInt32(
					ExternalInformationProvider.RestorePerUserPerWorkstationValue(
						StoreID + @".Height",
						Height.ToString() ) );

				wordWrapCheckBox.Checked =
					Convert.ToBoolean(
					ExternalInformationProvider.RestorePerUserPerWorkstationValue(
						StoreID + @".WordWrap",
						wordWrapCheckBox.Checked.ToString() ) );
			}
			CenterToParent();

			if ( !DesignMode )
			{
				if ( !_hasConsolasChecked )
				{
					_hasConsolasChecked = true;

					var families = FontFamily.Families;

					if ( families != null )
					{
						foreach ( var family in families )
						{
							if ( string.Compare( family.Name, @"Consolas", true ) == 0 )
							{
								_hasConsolas = true;
								break;
							}
						}
					}
				}

				if ( _hasConsolas )
				{
					textboxEdit.Font = new Font( @"Consolas", textboxEdit.Font.Size );
				}
			}

			textboxEdit.Select( 0, 0 );
		}

		private static bool _hasConsolas;
		private static bool _hasConsolasChecked;

		[Browsable( false )]
		[EditorBrowsable( EditorBrowsableState.Never )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		internal IExternalInformationProvider ExternalInformationProvider
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the store ID.
		/// </summary>
		/// <value>The store ID.</value>
		internal string StoreID
		{
			get
			{
				return string.Format(
					@"{0}.{1}.{2}",
					GetType().Name,
					Name,
					Text );
			}
		}

		private void HtmlSourceTextEditForm_FormClosing(
			object sender,
			FormClosingEventArgs e )
		{
			if ( ExternalInformationProvider != null )
			{
				ExternalInformationProvider.SavePerUserPerWorkstationValue(
					StoreID + @".Width",
					Width.ToString() );
				ExternalInformationProvider.SavePerUserPerWorkstationValue(
					StoreID + @".Height",
					Height.ToString() );
				ExternalInformationProvider.SavePerUserPerWorkstationValue(
					StoreID + @".WordWrap",
					wordWrapCheckBox.Checked.ToString() );
			}
		}

		private void buttonCancel_Click( object sender, EventArgs e )
		{
			Close();
		}

		private void buttonOK_Click( object sender, EventArgs e )
		{
			Close();
		}

		public string HtmlText
		{
			get
			{
				return textboxEdit.Text.Trim();
			}
		}

		private void wordWrapCheckBox_CheckedChanged(
			object sender,
			EventArgs e )
		{
			textboxEdit.WordWrap = wordWrapCheckBox.Checked;
		}

		private void textboxEdit_KeyDown( object sender, KeyEventArgs e )
		{
			if ( e.Control && e.KeyCode == Keys.A )
			{
				textboxEdit.SelectAll();
			}
		}
	}
}