namespace UZipDotNet {
	partial class ExtractSelectionForm {
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
		this.DirectoryTree = new System.Windows.Forms.TreeView();
		this.label1 = new System.Windows.Forms.Label();
		this.FolderTextBox = new System.Windows.Forms.TextBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.AllFilesLabel = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.SelFilesLabel = new System.Windows.Forms.Label();
		this.ExtractSelectionRadioButton = new System.Windows.Forms.RadioButton();
		this.ExtractAllRadioButton = new System.Windows.Forms.RadioButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.SkipOlderCheckBox = new System.Windows.Forms.CheckBox();
		this.SkipSystemCheckBox = new System.Windows.Forms.CheckBox();
		this.SkipHiddenCheckBox = new System.Windows.Forms.CheckBox();
		this.SkipReadOnlyCheckBox = new System.Windows.Forms.CheckBox();
		this.IgnoreFolderCheckBox = new System.Windows.Forms.CheckBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.OverwriteAskRadioButton = new System.Windows.Forms.RadioButton();
		this.OverwriteNoRadioButton = new System.Windows.Forms.RadioButton();
		this.OverwriteYesRadioButton = new System.Windows.Forms.RadioButton();
		this.ExtractButton = new System.Windows.Forms.Button();
		this.ExitButton = new System.Windows.Forms.Button();
		this.AllSizeLabel = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.SelSizeLabel = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.SuspendLayout();
		// 
		// DirectoryTree
		// 
		this.DirectoryTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.DirectoryTree.Location = new System.Drawing.Point(378, 38);
		this.DirectoryTree.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.DirectoryTree.Name = "DirectoryTree";
		this.DirectoryTree.Size = new System.Drawing.Size(328, 464);
		this.DirectoryTree.TabIndex = 5;
		this.DirectoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.OnBeforeExpand);
		this.DirectoryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterSelectDirectory);
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(13, 32);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(104, 16);
		this.label1.TabIndex = 0;
		this.label1.Text = "Extract to folder:";
		// 
		// FolderTextBox
		// 
		this.FolderTextBox.Location = new System.Drawing.Point(13, 56);
		this.FolderTextBox.Name = "FolderTextBox";
		this.FolderTextBox.Size = new System.Drawing.Size(350, 22);
		this.FolderTextBox.TabIndex = 1;
		this.FolderTextBox.TextChanged += new System.EventHandler(this.OnFolderTextChanged);
		// 
		// groupBox1
		// 
		this.groupBox1.Controls.Add(this.AllSizeLabel);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.SelSizeLabel);
		this.groupBox1.Controls.Add(this.AllFilesLabel);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.SelFilesLabel);
		this.groupBox1.Controls.Add(this.ExtractSelectionRadioButton);
		this.groupBox1.Controls.Add(this.ExtractAllRadioButton);
		this.groupBox1.Location = new System.Drawing.Point(13, 93);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(350, 110);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Extraction Mode";
		// 
		// AllFilesLabel
		// 
		this.AllFilesLabel.BackColor = System.Drawing.SystemColors.Info;
		this.AllFilesLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.AllFilesLabel.Location = new System.Drawing.Point(134, 36);
		this.AllFilesLabel.Name = "AllFilesLabel";
		this.AllFilesLabel.Size = new System.Drawing.Size(60, 24);
		this.AllFilesLabel.TabIndex = 3;
		this.AllFilesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// label3
		// 
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(145, 18);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(36, 16);
		this.label3.TabIndex = 0;
		this.label3.Text = "Files";
		// 
		// SelFilesLabel
		// 
		this.SelFilesLabel.BackColor = System.Drawing.SystemColors.Info;
		this.SelFilesLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.SelFilesLabel.Location = new System.Drawing.Point(134, 67);
		this.SelFilesLabel.Name = "SelFilesLabel";
		this.SelFilesLabel.Size = new System.Drawing.Size(60, 24);
		this.SelFilesLabel.TabIndex = 6;
		this.SelFilesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// ExtractSelectionRadioButton
		// 
		this.ExtractSelectionRadioButton.AutoSize = true;
		this.ExtractSelectionRadioButton.Location = new System.Drawing.Point(11, 69);
		this.ExtractSelectionRadioButton.Name = "ExtractSelectionRadioButton";
		this.ExtractSelectionRadioButton.Size = new System.Drawing.Size(108, 20);
		this.ExtractSelectionRadioButton.TabIndex = 5;
		this.ExtractSelectionRadioButton.Text = "Selection only";
		this.ExtractSelectionRadioButton.UseVisualStyleBackColor = true;
		// 
		// ExtractAllRadioButton
		// 
		this.ExtractAllRadioButton.AutoSize = true;
		this.ExtractAllRadioButton.Checked = true;
		this.ExtractAllRadioButton.Location = new System.Drawing.Point(11, 36);
		this.ExtractAllRadioButton.Name = "ExtractAllRadioButton";
		this.ExtractAllRadioButton.Size = new System.Drawing.Size(68, 20);
		this.ExtractAllRadioButton.TabIndex = 2;
		this.ExtractAllRadioButton.TabStop = true;
		this.ExtractAllRadioButton.Text = "All files";
		this.ExtractAllRadioButton.UseVisualStyleBackColor = true;
		// 
		// groupBox2
		// 
		this.groupBox2.Controls.Add(this.SkipOlderCheckBox);
		this.groupBox2.Controls.Add(this.SkipSystemCheckBox);
		this.groupBox2.Controls.Add(this.SkipHiddenCheckBox);
		this.groupBox2.Controls.Add(this.SkipReadOnlyCheckBox);
		this.groupBox2.Controls.Add(this.IgnoreFolderCheckBox);
		this.groupBox2.Location = new System.Drawing.Point(13, 334);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(350, 167);
		this.groupBox2.TabIndex = 4;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Options";
		// 
		// SkipOlderCheckBox
		// 
		this.SkipOlderCheckBox.AutoSize = true;
		this.SkipOlderCheckBox.Location = new System.Drawing.Point(21, 133);
		this.SkipOlderCheckBox.Name = "SkipOlderCheckBox";
		this.SkipOlderCheckBox.Size = new System.Drawing.Size(112, 20);
		this.SkipOlderCheckBox.TabIndex = 4;
		this.SkipOlderCheckBox.Text = "Skip older files";
		this.SkipOlderCheckBox.UseVisualStyleBackColor = true;
		// 
		// SkipSystemCheckBox
		// 
		this.SkipSystemCheckBox.AutoSize = true;
		this.SkipSystemCheckBox.Location = new System.Drawing.Point(21, 107);
		this.SkipSystemCheckBox.Name = "SkipSystemCheckBox";
		this.SkipSystemCheckBox.Size = new System.Drawing.Size(127, 20);
		this.SkipSystemCheckBox.TabIndex = 3;
		this.SkipSystemCheckBox.Text = "Skip system files";
		this.SkipSystemCheckBox.UseVisualStyleBackColor = true;
		// 
		// SkipHiddenCheckBox
		// 
		this.SkipHiddenCheckBox.AutoSize = true;
		this.SkipHiddenCheckBox.Location = new System.Drawing.Point(21, 81);
		this.SkipHiddenCheckBox.Name = "SkipHiddenCheckBox";
		this.SkipHiddenCheckBox.Size = new System.Drawing.Size(122, 20);
		this.SkipHiddenCheckBox.TabIndex = 2;
		this.SkipHiddenCheckBox.Text = "Skip hidden files";
		this.SkipHiddenCheckBox.UseVisualStyleBackColor = true;
		// 
		// SkipReadOnlyCheckBox
		// 
		this.SkipReadOnlyCheckBox.AutoSize = true;
		this.SkipReadOnlyCheckBox.Location = new System.Drawing.Point(21, 55);
		this.SkipReadOnlyCheckBox.Name = "SkipReadOnlyCheckBox";
		this.SkipReadOnlyCheckBox.Size = new System.Drawing.Size(137, 20);
		this.SkipReadOnlyCheckBox.TabIndex = 1;
		this.SkipReadOnlyCheckBox.Text = "Skip read only files";
		this.SkipReadOnlyCheckBox.UseVisualStyleBackColor = true;
		// 
		// IgnoreFolderCheckBox
		// 
		this.IgnoreFolderCheckBox.AutoSize = true;
		this.IgnoreFolderCheckBox.Location = new System.Drawing.Point(21, 29);
		this.IgnoreFolderCheckBox.Name = "IgnoreFolderCheckBox";
		this.IgnoreFolderCheckBox.Size = new System.Drawing.Size(164, 20);
		this.IgnoreFolderCheckBox.TabIndex = 0;
		this.IgnoreFolderCheckBox.Text = "Ignore folder information";
		this.IgnoreFolderCheckBox.UseVisualStyleBackColor = true;
		// 
		// groupBox3
		// 
		this.groupBox3.Controls.Add(this.OverwriteAskRadioButton);
		this.groupBox3.Controls.Add(this.OverwriteNoRadioButton);
		this.groupBox3.Controls.Add(this.OverwriteYesRadioButton);
		this.groupBox3.Location = new System.Drawing.Point(13, 250);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(350, 66);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Overwrite Files";
		// 
		// OverwriteAskRadioButton
		// 
		this.OverwriteAskRadioButton.AutoSize = true;
		this.OverwriteAskRadioButton.Location = new System.Drawing.Point(179, 27);
		this.OverwriteAskRadioButton.Name = "OverwriteAskRadioButton";
		this.OverwriteAskRadioButton.Size = new System.Drawing.Size(49, 20);
		this.OverwriteAskRadioButton.TabIndex = 2;
		this.OverwriteAskRadioButton.Text = "Ask";
		this.OverwriteAskRadioButton.UseVisualStyleBackColor = true;
		// 
		// OverwriteNoRadioButton
		// 
		this.OverwriteNoRadioButton.AutoSize = true;
		this.OverwriteNoRadioButton.Location = new System.Drawing.Point(99, 27);
		this.OverwriteNoRadioButton.Name = "OverwriteNoRadioButton";
		this.OverwriteNoRadioButton.Size = new System.Drawing.Size(42, 20);
		this.OverwriteNoRadioButton.TabIndex = 1;
		this.OverwriteNoRadioButton.Text = "No";
		this.OverwriteNoRadioButton.UseVisualStyleBackColor = true;
		// 
		// OverwriteYesRadioButton
		// 
		this.OverwriteYesRadioButton.AutoSize = true;
		this.OverwriteYesRadioButton.Checked = true;
		this.OverwriteYesRadioButton.Location = new System.Drawing.Point(13, 27);
		this.OverwriteYesRadioButton.Name = "OverwriteYesRadioButton";
		this.OverwriteYesRadioButton.Size = new System.Drawing.Size(48, 20);
		this.OverwriteYesRadioButton.TabIndex = 0;
		this.OverwriteYesRadioButton.TabStop = true;
		this.OverwriteYesRadioButton.Text = "Yes";
		this.OverwriteYesRadioButton.UseVisualStyleBackColor = true;
		// 
		// ExtractButton
		// 
		this.ExtractButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.ExtractButton.Location = new System.Drawing.Point(250, 521);
		this.ExtractButton.Name = "ExtractButton";
		this.ExtractButton.Size = new System.Drawing.Size(94, 35);
		this.ExtractButton.TabIndex = 6;
		this.ExtractButton.Text = "Extract";
		this.ExtractButton.UseVisualStyleBackColor = true;
		// 
		// ExitButton
		// 
		this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ExitButton.Location = new System.Drawing.Point(378, 521);
		this.ExitButton.Name = "ExitButton";
		this.ExitButton.Size = new System.Drawing.Size(94, 35);
		this.ExitButton.TabIndex = 7;
		this.ExitButton.Text = "Cancel";
		this.ExitButton.UseVisualStyleBackColor = true;
		// 
		// AllSizeLabel
		// 
		this.AllSizeLabel.BackColor = System.Drawing.SystemColors.Info;
		this.AllSizeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.AllSizeLabel.Location = new System.Drawing.Point(200, 36);
		this.AllSizeLabel.Name = "AllSizeLabel";
		this.AllSizeLabel.Size = new System.Drawing.Size(134, 24);
		this.AllSizeLabel.TabIndex = 4;
		this.AllSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// label5
		// 
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(251, 18);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(34, 16);
		this.label5.TabIndex = 1;
		this.label5.Text = "Size";
		// 
		// SelSizeLabel
		// 
		this.SelSizeLabel.BackColor = System.Drawing.SystemColors.Info;
		this.SelSizeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.SelSizeLabel.Location = new System.Drawing.Point(200, 67);
		this.SelSizeLabel.Name = "SelSizeLabel";
		this.SelSizeLabel.Size = new System.Drawing.Size(134, 24);
		this.SelSizeLabel.TabIndex = 7;
		this.SelSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// ExtractForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(723, 574);
		this.Controls.Add(this.groupBox3);
		this.Controls.Add(this.ExitButton);
		this.Controls.Add(this.ExtractButton);
		this.Controls.Add(this.groupBox2);
		this.Controls.Add(this.groupBox1);
		this.Controls.Add(this.FolderTextBox);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.DirectoryTree);
		this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.MinimumSize = new System.Drawing.Size(739, 610);
		this.Name = "ExtractForm";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Extract Files";
		this.Load += new System.EventHandler(this.OnLoad);
		this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
		this.Resize += new System.EventHandler(this.OnResize);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.ResumeLayout(false);
		this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView DirectoryTree;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox FolderTextBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton ExtractSelectionRadioButton;
		private System.Windows.Forms.RadioButton ExtractAllRadioButton;
		private System.Windows.Forms.Label SelFilesLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox SkipOlderCheckBox;
		private System.Windows.Forms.CheckBox SkipSystemCheckBox;
		private System.Windows.Forms.CheckBox SkipHiddenCheckBox;
		private System.Windows.Forms.CheckBox SkipReadOnlyCheckBox;
		private System.Windows.Forms.CheckBox IgnoreFolderCheckBox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton OverwriteAskRadioButton;
		private System.Windows.Forms.RadioButton OverwriteNoRadioButton;
		private System.Windows.Forms.RadioButton OverwriteYesRadioButton;
		private System.Windows.Forms.Button ExtractButton;
		private System.Windows.Forms.Button ExitButton;
		private System.Windows.Forms.Label AllFilesLabel;
		private System.Windows.Forms.Label AllSizeLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label SelSizeLabel;
	}
}