using System.Windows.Forms;

namespace ZetaHtmlEditControl
{
	using System;

	public partial class HtmlEditUserControl : UserControl
	{
		private readonly float _initialTopHeight;

		public HtmlEditUserControl()
		{
			InitializeComponent();

			_initialTopHeight = tableLayoutPanel.RowStyles[0].Height;

			htmlEditControl.UINeedsUpdate += htmlEditControl_UINeedsUpdate;
		}

		private void htmlEditControl_UINeedsUpdate(
			object sender,
			EventArgs e)
		{
			updateButtons();
		}

		/// <summary>
		/// Give access to the underlying HTML edit control.
		/// </summary>
		public HtmlEditControl HtmlEditControl
		{
			get { return htmlEditControl; }
		}

		public bool IsToolbarVisible
		{
			get { return topToolStrip.Visible; }
			set
			{
				if (topToolStrip.Visible != value)
				{
					topToolStrip.Visible = value;

					tableLayoutPanel.RowStyles[0].Height = value ? _initialTopHeight : 0;
				}
			}
		}

		private void updateButtons()
		{
			boldToolStripMenuItem.Enabled = htmlEditControl.CanBold;
			italicToolStripMenuItem.Enabled = htmlEditControl.CanItalic;
			bullettedListToolStripMenuItem.Enabled = htmlEditControl.CanBullettedList;
			numberedListToolStripMenuItem.Enabled = htmlEditControl.CanOrderedList;
			indentToolStripMenuItem.Enabled = htmlEditControl.CanIndent;
			outdentToolStripMenuItem.Enabled = htmlEditControl.CanOutdent;
			insertTableToolStripMenuItem.Enabled = htmlEditControl.CanInsertTable;
			foreColorToolStripMenuItem.Enabled = htmlEditControl.CanForeColor;
			backColorToolStripMenuItem.Enabled = htmlEditControl.CanBackColor;
			undoToolStripButton.Enabled = htmlEditControl.CanUndo;
			justifyLeftToolStripButton.Enabled = htmlEditControl.CanJustifyLeft;
			justifyCenterToolStripButton.Enabled = htmlEditControl.CanJustifyCenter;
			justifyRightToolStripButton.Enabled = htmlEditControl.CanJustifyRight;

			// --

			boldToolStripMenuItem.Checked = htmlEditControl.IsBold;
			italicToolStripMenuItem.Checked = htmlEditControl.IsItalic;
			numberedListToolStripMenuItem.Checked = htmlEditControl.IsBullettedList;
			bullettedListToolStripMenuItem.Checked = htmlEditControl.IsOrderedList;
			justifyLeftToolStripButton.Checked = htmlEditControl.IsJustifyLeft;
			justifyCenterToolStripButton.Checked = htmlEditControl.IsJustifyCenter;
			justifyRightToolStripButton.Checked = htmlEditControl.IsJustifyRight;
		}

		private void boldToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteBold();
		}

		private void italicToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteItalic();
		}

		private void bullettedListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteBullettedList();
		}

		private void numberedListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteNumberedList();
		}

		private void indentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteIndent();
		}

		private void outdentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteOutdent();
		}

		private void insertTableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteInsertTable();
		}

		private void foreColorNoneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetForeColorNone();
		}

		private void foreColor01ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetForeColor01();
		}

		private void foreColor02ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetForeColor02();
		}

		private void foreColor03ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetForeColor03();
		}

		private void foreColor04ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetForeColor04();
		}

		private void foreColor05ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetForeColor05();
		}

		private void foreColor06ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetForeColor06();
		}

		private void foreColor07ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetForeColor07();
		}

		private void foreColor08ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetForeColor08();
		}

		private void foreColor09ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetForeColor09();
		}

		private void foreColor10ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetForeColor10();
		}

		private void BackColorNoneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetBackColorNone();
		}

		private void BackColor01ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetBackColor01();
		}

		private void BackColor02ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetBackColor02();
		}

		private void BackColor03ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetBackColor03();
		}

		private void BackColor04ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetBackColor04();
		}

		private void BackColor05ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteSetBackColor05();
		}

		private void HtmlEditUserControl_Load(object sender, EventArgs e)
		{
			updateButtons();
		}

		private void justifyLeftToolStripButton_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteJustifyLeft();
		}

		private void justifyCenterToolStripButton_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteJustifyCenter();
		}

		private void justifyRightToolStripButton_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteJustifyRight();
		}

		private void undoToolStripButton_Click(object sender, EventArgs e)
		{
			htmlEditControl.ExecuteUndo();
		}
	}
}