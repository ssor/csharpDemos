namespace UZipDotNet {
	partial class UZipDotNet {
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
		this.ButtonsGroupBox = new System.Windows.Forms.GroupBox();
		this.PosHexCheckBox = new System.Windows.Forms.CheckBox();
		this.DeleteButton = new System.Windows.Forms.Button();
		this.TestButton = new System.Windows.Forms.Button();
		this.AddButton = new System.Windows.Forms.Button();
		this.NewButton = new System.Windows.Forms.Button();
		this.ExtractButton = new System.Windows.Forms.Button();
		this.ExitButton = new System.Windows.Forms.Button();
		this.OpenButton = new System.Windows.Forms.Button();
		this.ArchiveNameLabel = new System.Windows.Forms.Label();
		this.CopyrightTextBox = new System.Windows.Forms.RichTextBox();
		this.ButtonsGroupBox.SuspendLayout();
		this.SuspendLayout();
		// 
		// ButtonsGroupBox
		// 
		this.ButtonsGroupBox.Controls.Add(this.PosHexCheckBox);
		this.ButtonsGroupBox.Controls.Add(this.DeleteButton);
		this.ButtonsGroupBox.Controls.Add(this.TestButton);
		this.ButtonsGroupBox.Controls.Add(this.AddButton);
		this.ButtonsGroupBox.Controls.Add(this.NewButton);
		this.ButtonsGroupBox.Controls.Add(this.ExtractButton);
		this.ButtonsGroupBox.Controls.Add(this.ExitButton);
		this.ButtonsGroupBox.Controls.Add(this.OpenButton);
		this.ButtonsGroupBox.Location = new System.Drawing.Point(20, 411);
		this.ButtonsGroupBox.Name = "ButtonsGroupBox";
		this.ButtonsGroupBox.Size = new System.Drawing.Size(704, 66);
		this.ButtonsGroupBox.TabIndex = 0;
		this.ButtonsGroupBox.TabStop = false;
		// 
		// PosHexCheckBox
		// 
		this.PosHexCheckBox.AutoSize = true;
		this.PosHexCheckBox.Location = new System.Drawing.Point(617, 26);
		this.PosHexCheckBox.Name = "PosHexCheckBox";
		this.PosHexCheckBox.Size = new System.Drawing.Size(77, 20);
		this.PosHexCheckBox.TabIndex = 7;
		this.PosHexCheckBox.Text = "Pos Hex";
		this.PosHexCheckBox.UseVisualStyleBackColor = true;
		this.PosHexCheckBox.CheckedChanged += new System.EventHandler(this.OnPosHexChanged);
		// 
		// DeleteButton
		// 
		this.DeleteButton.Location = new System.Drawing.Point(362, 18);
		this.DeleteButton.Name = "DeleteButton";
		this.DeleteButton.Size = new System.Drawing.Size(72, 34);
		this.DeleteButton.TabIndex = 4;
		this.DeleteButton.Text = "Delete";
		this.DeleteButton.UseVisualStyleBackColor = true;
		this.DeleteButton.Click += new System.EventHandler(this.OnDelete);
		// 
		// TestButton
		// 
		this.TestButton.Location = new System.Drawing.Point(449, 18);
		this.TestButton.Name = "TestButton";
		this.TestButton.Size = new System.Drawing.Size(72, 34);
		this.TestButton.TabIndex = 5;
		this.TestButton.Text = "Test";
		this.TestButton.UseVisualStyleBackColor = true;
		this.TestButton.Click += new System.EventHandler(this.OnTest);
		// 
		// AddButton
		// 
		this.AddButton.Location = new System.Drawing.Point(275, 18);
		this.AddButton.Name = "AddButton";
		this.AddButton.Size = new System.Drawing.Size(72, 34);
		this.AddButton.TabIndex = 3;
		this.AddButton.Text = "Add";
		this.AddButton.UseVisualStyleBackColor = true;
		this.AddButton.Click += new System.EventHandler(this.OnAdd);
		// 
		// NewButton
		// 
		this.NewButton.Location = new System.Drawing.Point(188, 18);
		this.NewButton.Name = "NewButton";
		this.NewButton.Size = new System.Drawing.Size(72, 34);
		this.NewButton.TabIndex = 2;
		this.NewButton.Text = "New";
		this.NewButton.UseVisualStyleBackColor = true;
		this.NewButton.Click += new System.EventHandler(this.OnNew);
		// 
		// ExtractButton
		// 
		this.ExtractButton.Location = new System.Drawing.Point(101, 18);
		this.ExtractButton.Name = "ExtractButton";
		this.ExtractButton.Size = new System.Drawing.Size(72, 34);
		this.ExtractButton.TabIndex = 1;
		this.ExtractButton.Text = "Extract";
		this.ExtractButton.UseVisualStyleBackColor = true;
		this.ExtractButton.Click += new System.EventHandler(this.OnExtract);
		// 
		// ExitButton
		// 
		this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ExitButton.Location = new System.Drawing.Point(536, 18);
		this.ExitButton.Name = "ExitButton";
		this.ExitButton.Size = new System.Drawing.Size(72, 34);
		this.ExitButton.TabIndex = 6;
		this.ExitButton.Text = "Exit";
		this.ExitButton.UseVisualStyleBackColor = true;
		this.ExitButton.Click += new System.EventHandler(this.OnExit);
		this.ExitButton.Resize += new System.EventHandler(this.OnResize);
		// 
		// OpenButton
		// 
		this.OpenButton.Location = new System.Drawing.Point(14, 18);
		this.OpenButton.Name = "OpenButton";
		this.OpenButton.Size = new System.Drawing.Size(72, 34);
		this.OpenButton.TabIndex = 0;
		this.OpenButton.Text = "Open";
		this.OpenButton.UseVisualStyleBackColor = true;
		this.OpenButton.Click += new System.EventHandler(this.OnOpen);
		// 
		// ArchiveNameLabel
		// 
		this.ArchiveNameLabel.AutoEllipsis = true;
		this.ArchiveNameLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.ArchiveNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.ArchiveNameLabel.Location = new System.Drawing.Point(176, 8);
		this.ArchiveNameLabel.Name = "ArchiveNameLabel";
		this.ArchiveNameLabel.Size = new System.Drawing.Size(400, 24);
		this.ArchiveNameLabel.TabIndex = 1;
		this.ArchiveNameLabel.Text = "UZipDotNet";
		this.ArchiveNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// CopyrightTextBox
		// 
		this.CopyrightTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.CopyrightTextBox.Location = new System.Drawing.Point(98, 97);
		this.CopyrightTextBox.MaxLength = 2048;
		this.CopyrightTextBox.Name = "CopyrightTextBox";
		this.CopyrightTextBox.ReadOnly = true;
		this.CopyrightTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
		this.CopyrightTextBox.Size = new System.Drawing.Size(548, 256);
		this.CopyrightTextBox.TabIndex = 2;
		this.CopyrightTextBox.Text = "UZipDotNet";
		// 
		// UZipDotNet
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.CancelButton = this.ExitButton;
		this.ClientSize = new System.Drawing.Size(744, 484);
		this.Controls.Add(this.CopyrightTextBox);
		this.Controls.Add(this.ArchiveNameLabel);
		this.Controls.Add(this.ButtonsGroupBox);
		this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.MinimumSize = new System.Drawing.Size(760, 520);
		this.Name = "UZipDotNet";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "UZipDotNet";
		this.Load += new System.EventHandler(this.OnLoad);
		this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
		this.Resize += new System.EventHandler(this.OnResize);
		this.ButtonsGroupBox.ResumeLayout(false);
		this.ButtonsGroupBox.PerformLayout();
		this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox ButtonsGroupBox;
		private System.Windows.Forms.Button OpenButton;
		private System.Windows.Forms.Button ExitButton;
		private System.Windows.Forms.Button TestButton;
		private System.Windows.Forms.Button ExtractButton;
		private System.Windows.Forms.Button NewButton;
		private System.Windows.Forms.Button DeleteButton;
		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.Label ArchiveNameLabel;
		private System.Windows.Forms.RichTextBox CopyrightTextBox;
		private System.Windows.Forms.CheckBox PosHexCheckBox;
	}
}

