namespace UZipDotNet {
	partial class AddFilesAndFoldersForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
		components.Dispose();
		}
		base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
		this.FileArea = new System.Windows.Forms.SplitContainer();
		this.DirectoryTree = new System.Windows.Forms.TreeView();
		this.FileList = new System.Windows.Forms.ListView();
		this.NameHeader = new System.Windows.Forms.ColumnHeader();
		this.DateHeader = new System.Windows.Forms.ColumnHeader();
		this.AttrHeader = new System.Windows.Forms.ColumnHeader();
		this.SizeHeader = new System.Windows.Forms.ColumnHeader();
		this.AddButton = new System.Windows.Forms.Button();
		this.ExitButton = new System.Windows.Forms.Button();
		this.ButtonsGroupBox = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.CompLevelUpDown = new System.Windows.Forms.NumericUpDown();
		this.label2 = new System.Windows.Forms.Label();
		this.FileArea.Panel1.SuspendLayout();
		this.FileArea.Panel2.SuspendLayout();
		this.FileArea.SuspendLayout();
		this.ButtonsGroupBox.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.CompLevelUpDown)).BeginInit();
		this.SuspendLayout();
		// 
		// FileArea
		// 
		this.FileArea.Location = new System.Drawing.Point(7, 14);
		this.FileArea.Name = "FileArea";
		// 
		// FileArea.Panel1
		// 
		this.FileArea.Panel1.Controls.Add(this.DirectoryTree);
		this.FileArea.Panel1.Resize += new System.EventHandler(this.OnFileAreaPanel1Resize);
		// 
		// FileArea.Panel2
		// 
		this.FileArea.Panel2.Controls.Add(this.FileList);
		this.FileArea.Panel2.Resize += new System.EventHandler(this.OnFileAreaPanel2Resize);
		this.FileArea.Size = new System.Drawing.Size(729, 395);
		this.FileArea.SplitterDistance = 242;
		this.FileArea.TabIndex = 0;
		this.FileArea.Resize += new System.EventHandler(this.OnFileAreaResize);
		// 
		// DirectoryTree
		// 
		this.DirectoryTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.DirectoryTree.Location = new System.Drawing.Point(11, 14);
		this.DirectoryTree.Name = "DirectoryTree";
		this.DirectoryTree.Size = new System.Drawing.Size(222, 365);
		this.DirectoryTree.TabIndex = 0;
		this.DirectoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.OnBeforeExpand);
		this.DirectoryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterSelectDirectory);
		// 
		// FileList
		// 
		this.FileList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.FileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameHeader,
            this.DateHeader,
            this.AttrHeader,
            this.SizeHeader});
		this.FileList.FullRowSelect = true;
		this.FileList.Location = new System.Drawing.Point(17, 14);
		this.FileList.Name = "FileList";
		this.FileList.Size = new System.Drawing.Size(455, 365);
		this.FileList.TabIndex = 0;
		this.FileList.UseCompatibleStateImageBehavior = false;
		this.FileList.View = System.Windows.Forms.View.Details;
		this.FileList.Resize += new System.EventHandler(this.OnFileListResize);
		this.FileList.DoubleClick += new System.EventHandler(this.OnFileDoubleClick);
		this.FileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
		// 
		// NameHeader
		// 
		this.NameHeader.Text = "File Name";
		this.NameHeader.Width = 200;
		// 
		// DateHeader
		// 
		this.DateHeader.Text = "Last Modified";
		this.DateHeader.Width = 120;
		// 
		// AttrHeader
		// 
		this.AttrHeader.Text = "Attr.";
		// 
		// SizeHeader
		// 
		this.SizeHeader.Text = "Size";
		this.SizeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		// 
		// AddButton
		// 
		this.AddButton.Location = new System.Drawing.Point(276, 17);
		this.AddButton.Name = "AddButton";
		this.AddButton.Size = new System.Drawing.Size(113, 37);
		this.AddButton.TabIndex = 2;
		this.AddButton.Text = "Add Selection";
		this.AddButton.UseVisualStyleBackColor = true;
		this.AddButton.Click += new System.EventHandler(this.OnAddButton);
		// 
		// ExitButton
		// 
		this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ExitButton.Location = new System.Drawing.Point(413, 17);
		this.ExitButton.Name = "ExitButton";
		this.ExitButton.Size = new System.Drawing.Size(113, 37);
		this.ExitButton.TabIndex = 3;
		this.ExitButton.Text = "Cancel";
		this.ExitButton.UseVisualStyleBackColor = true;
		// 
		// ButtonsGroupBox
		// 
		this.ButtonsGroupBox.Controls.Add(this.label2);
		this.ButtonsGroupBox.Controls.Add(this.CompLevelUpDown);
		this.ButtonsGroupBox.Controls.Add(this.label1);
		this.ButtonsGroupBox.Controls.Add(this.ExitButton);
		this.ButtonsGroupBox.Controls.Add(this.AddButton);
		this.ButtonsGroupBox.Location = new System.Drawing.Point(99, 415);
		this.ButtonsGroupBox.Name = "ButtonsGroupBox";
		this.ButtonsGroupBox.Size = new System.Drawing.Size(547, 64);
		this.ButtonsGroupBox.TabIndex = 1;
		this.ButtonsGroupBox.TabStop = false;
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(18, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(117, 16);
		this.label1.TabIndex = 0;
		this.label1.Text = "Compression Level";
		// 
		// CompLevelUpDown
		// 
		this.CompLevelUpDown.AllowDrop = true;
		this.CompLevelUpDown.Location = new System.Drawing.Point(179, 25);
		this.CompLevelUpDown.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
		this.CompLevelUpDown.Name = "CompLevelUpDown";
		this.CompLevelUpDown.Size = new System.Drawing.Size(37, 22);
		this.CompLevelUpDown.TabIndex = 1;
		this.CompLevelUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.CompLevelUpDown.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(18, 38);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(142, 16);
		this.label2.TabIndex = 4;
		this.label2.Text = "(0-no, 6-default, 9-max)";
		// 
		// AddFilesAndFolders
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(744, 484);
		this.Controls.Add(this.ButtonsGroupBox);
		this.Controls.Add(this.FileArea);
		this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.MinimumSize = new System.Drawing.Size(760, 520);
		this.Name = "AddFilesAndFolders";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Add Files and Folders to ZIP archive";
		this.Load += new System.EventHandler(this.OnLoad);
		this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
		this.Resize += new System.EventHandler(this.OnResize);
		this.FileArea.Panel1.ResumeLayout(false);
		this.FileArea.Panel2.ResumeLayout(false);
		this.FileArea.ResumeLayout(false);
		this.ButtonsGroupBox.ResumeLayout(false);
		this.ButtonsGroupBox.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.CompLevelUpDown)).EndInit();
		this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer FileArea;
		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.TreeView DirectoryTree;
		private System.Windows.Forms.ListView FileList;
		private System.Windows.Forms.ColumnHeader NameHeader;
		private System.Windows.Forms.ColumnHeader DateHeader;
		private System.Windows.Forms.ColumnHeader SizeHeader;
		private System.Windows.Forms.ColumnHeader AttrHeader;
		private System.Windows.Forms.Button ExitButton;
		private System.Windows.Forms.GroupBox ButtonsGroupBox;
		private System.Windows.Forms.NumericUpDown CompLevelUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

