namespace UZipDotNet {
	partial class TestForm {
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
		this.label1 = new System.Windows.Forms.Label();
		this.InputFileLenLabel = new System.Windows.Forms.Label();
		this.CompFileLenLabel = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.DecompFileLenLabel = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.RatioLabel = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.DecompTimeLabel = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.CompTimeLabel = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.TestButton = new System.Windows.Forms.Button();
		this.ExitButton = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.CompLevelUpDown = new System.Windows.Forms.NumericUpDown();
		this.label3 = new System.Windows.Forms.Label();
		this.ZipRadioButton = new System.Windows.Forms.RadioButton();
		this.ZLibRadioButton = new System.Windows.Forms.RadioButton();
		this.label5 = new System.Windows.Forms.Label();
		this.InputFileNameLabel = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.CompareFilesCheckBox = new System.Windows.Forms.CheckBox();
		this.NoHeaderRadioButton = new System.Windows.Forms.RadioButton();
		this.CompareTestLabel = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)(this.CompLevelUpDown)).BeginInit();
		this.SuspendLayout();
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(24, 64);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(84, 16);
		this.label1.TabIndex = 2;
		this.label1.Text = "Input file size";
		// 
		// InputFileLenLabel
		// 
		this.InputFileLenLabel.BackColor = System.Drawing.SystemColors.Info;
		this.InputFileLenLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.InputFileLenLabel.Location = new System.Drawing.Point(171, 60);
		this.InputFileLenLabel.Name = "InputFileLenLabel";
		this.InputFileLenLabel.Size = new System.Drawing.Size(140, 24);
		this.InputFileLenLabel.TabIndex = 3;
		this.InputFileLenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// CompFileLenLabel
		// 
		this.CompFileLenLabel.BackColor = System.Drawing.SystemColors.Info;
		this.CompFileLenLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.CompFileLenLabel.Location = new System.Drawing.Point(171, 90);
		this.CompFileLenLabel.Name = "CompFileLenLabel";
		this.CompFileLenLabel.Size = new System.Drawing.Size(140, 24);
		this.CompFileLenLabel.TabIndex = 5;
		this.CompFileLenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// label4
		// 
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(24, 94);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(129, 16);
		this.label4.TabIndex = 4;
		this.label4.Text = "Compressed file size";
		// 
		// DecompFileLenLabel
		// 
		this.DecompFileLenLabel.BackColor = System.Drawing.SystemColors.Info;
		this.DecompFileLenLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.DecompFileLenLabel.Location = new System.Drawing.Point(171, 120);
		this.DecompFileLenLabel.Name = "DecompFileLenLabel";
		this.DecompFileLenLabel.Size = new System.Drawing.Size(140, 24);
		this.DecompFileLenLabel.TabIndex = 9;
		this.DecompFileLenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// label6
		// 
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(24, 124);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(143, 16);
		this.label6.TabIndex = 8;
		this.label6.Text = "Decompressed file size";
		// 
		// RatioLabel
		// 
		this.RatioLabel.BackColor = System.Drawing.SystemColors.Info;
		this.RatioLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.RatioLabel.Location = new System.Drawing.Point(367, 90);
		this.RatioLabel.Name = "RatioLabel";
		this.RatioLabel.Size = new System.Drawing.Size(59, 24);
		this.RatioLabel.TabIndex = 7;
		this.RatioLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// label8
		// 
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(323, 94);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(38, 16);
		this.label8.TabIndex = 6;
		this.label8.Text = "Ratio";
		// 
		// DecompTimeLabel
		// 
		this.DecompTimeLabel.BackColor = System.Drawing.SystemColors.Info;
		this.DecompTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.DecompTimeLabel.Location = new System.Drawing.Point(171, 180);
		this.DecompTimeLabel.Name = "DecompTimeLabel";
		this.DecompTimeLabel.Size = new System.Drawing.Size(86, 24);
		this.DecompTimeLabel.TabIndex = 13;
		this.DecompTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// label10
		// 
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(24, 184);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(110, 16);
		this.label10.TabIndex = 12;
		this.label10.Text = "Decompress time";
		// 
		// CompTimeLabel
		// 
		this.CompTimeLabel.BackColor = System.Drawing.SystemColors.Info;
		this.CompTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.CompTimeLabel.Location = new System.Drawing.Point(171, 150);
		this.CompTimeLabel.Name = "CompTimeLabel";
		this.CompTimeLabel.Size = new System.Drawing.Size(86, 24);
		this.CompTimeLabel.TabIndex = 11;
		this.CompTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// label12
		// 
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(24, 154);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(96, 16);
		this.label12.TabIndex = 10;
		this.label12.Text = "Compress time";
		// 
		// TestButton
		// 
		this.TestButton.Location = new System.Drawing.Point(107, 360);
		this.TestButton.Name = "TestButton";
		this.TestButton.Size = new System.Drawing.Size(102, 39);
		this.TestButton.TabIndex = 24;
		this.TestButton.Text = "Open File";
		this.TestButton.UseVisualStyleBackColor = true;
		this.TestButton.Click += new System.EventHandler(this.OnTest);
		// 
		// ExitButton
		// 
		this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ExitButton.Location = new System.Drawing.Point(242, 360);
		this.ExitButton.Name = "ExitButton";
		this.ExitButton.Size = new System.Drawing.Size(102, 39);
		this.ExitButton.TabIndex = 25;
		this.ExitButton.Text = "Exit";
		this.ExitButton.UseVisualStyleBackColor = true;
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(216, 257);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(222, 16);
		this.label2.TabIndex = 18;
		this.label2.Text = "0-Store, 1-3 Fast, 4-9 Slow, 6-Default";
		// 
		// CompLevelUpDown
		// 
		this.CompLevelUpDown.AllowDrop = true;
		this.CompLevelUpDown.Location = new System.Drawing.Point(171, 255);
		this.CompLevelUpDown.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
		this.CompLevelUpDown.Name = "CompLevelUpDown";
		this.CompLevelUpDown.Size = new System.Drawing.Size(37, 22);
		this.CompLevelUpDown.TabIndex = 17;
		this.CompLevelUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.CompLevelUpDown.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
		// 
		// label3
		// 
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(24, 257);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(113, 16);
		this.label3.TabIndex = 16;
		this.label3.Text = "Compression level";
		// 
		// ZipRadioButton
		// 
		this.ZipRadioButton.AutoSize = true;
		this.ZipRadioButton.Checked = true;
		this.ZipRadioButton.Location = new System.Drawing.Point(171, 287);
		this.ZipRadioButton.Name = "ZipRadioButton";
		this.ZipRadioButton.Size = new System.Drawing.Size(45, 20);
		this.ZipRadioButton.TabIndex = 20;
		this.ZipRadioButton.TabStop = true;
		this.ZipRadioButton.Text = "ZIP";
		this.ZipRadioButton.UseVisualStyleBackColor = true;
		// 
		// ZLibRadioButton
		// 
		this.ZLibRadioButton.AutoSize = true;
		this.ZLibRadioButton.Location = new System.Drawing.Point(236, 287);
		this.ZLibRadioButton.Name = "ZLibRadioButton";
		this.ZLibRadioButton.Size = new System.Drawing.Size(52, 20);
		this.ZLibRadioButton.TabIndex = 21;
		this.ZLibRadioButton.Text = "ZLIB";
		this.ZLibRadioButton.UseVisualStyleBackColor = true;
		// 
		// label5
		// 
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(24, 289);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(116, 16);
		this.label5.TabIndex = 19;
		this.label5.Text = "Compress file type";
		// 
		// InputFileNameLabel
		// 
		this.InputFileNameLabel.BackColor = System.Drawing.SystemColors.Info;
		this.InputFileNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.InputFileNameLabel.Location = new System.Drawing.Point(171, 30);
		this.InputFileNameLabel.Name = "InputFileNameLabel";
		this.InputFileNameLabel.Size = new System.Drawing.Size(255, 24);
		this.InputFileNameLabel.TabIndex = 1;
		this.InputFileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		// 
		// label9
		// 
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(24, 34);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(92, 16);
		this.label9.TabIndex = 0;
		this.label9.Text = "Input file name";
		// 
		// CompareFilesCheckBox
		// 
		this.CompareFilesCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.CompareFilesCheckBox.Location = new System.Drawing.Point(23, 321);
		this.CompareFilesCheckBox.Name = "CompareFilesCheckBox";
		this.CompareFilesCheckBox.Size = new System.Drawing.Size(162, 22);
		this.CompareFilesCheckBox.TabIndex = 23;
		this.CompareFilesCheckBox.Text = "Compare files";
		this.CompareFilesCheckBox.UseVisualStyleBackColor = true;
		// 
		// NoHeaderRadioButton
		// 
		this.NoHeaderRadioButton.AutoSize = true;
		this.NoHeaderRadioButton.Location = new System.Drawing.Point(308, 287);
		this.NoHeaderRadioButton.Name = "NoHeaderRadioButton";
		this.NoHeaderRadioButton.Size = new System.Drawing.Size(87, 20);
		this.NoHeaderRadioButton.TabIndex = 22;
		this.NoHeaderRadioButton.Text = "No Header";
		this.NoHeaderRadioButton.UseVisualStyleBackColor = true;
		// 
		// CompareTestLabel
		// 
		this.CompareTestLabel.BackColor = System.Drawing.SystemColors.Info;
		this.CompareTestLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.CompareTestLabel.Location = new System.Drawing.Point(171, 210);
		this.CompareTestLabel.Name = "CompareTestLabel";
		this.CompareTestLabel.Size = new System.Drawing.Size(86, 24);
		this.CompareTestLabel.TabIndex = 15;
		this.CompareTestLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// label11
		// 
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(24, 214);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(88, 16);
		this.label11.TabIndex = 14;
		this.label11.Text = "Compare Test";
		// 
		// TestForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(450, 415);
		this.Controls.Add(this.CompareTestLabel);
		this.Controls.Add(this.label11);
		this.Controls.Add(this.NoHeaderRadioButton);
		this.Controls.Add(this.CompareFilesCheckBox);
		this.Controls.Add(this.InputFileNameLabel);
		this.Controls.Add(this.label9);
		this.Controls.Add(this.label5);
		this.Controls.Add(this.ZLibRadioButton);
		this.Controls.Add(this.ZipRadioButton);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.CompLevelUpDown);
		this.Controls.Add(this.label3);
		this.Controls.Add(this.ExitButton);
		this.Controls.Add(this.TestButton);
		this.Controls.Add(this.DecompTimeLabel);
		this.Controls.Add(this.label10);
		this.Controls.Add(this.CompTimeLabel);
		this.Controls.Add(this.label12);
		this.Controls.Add(this.RatioLabel);
		this.Controls.Add(this.label8);
		this.Controls.Add(this.DecompFileLenLabel);
		this.Controls.Add(this.label6);
		this.Controls.Add(this.CompFileLenLabel);
		this.Controls.Add(this.label4);
		this.Controls.Add(this.InputFileLenLabel);
		this.Controls.Add(this.label1);
		this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "TestForm";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Single File Test";
		this.Load += new System.EventHandler(this.OnLoad);
		((System.ComponentModel.ISupportInitialize)(this.CompLevelUpDown)).EndInit();
		this.ResumeLayout(false);
		this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label InputFileLenLabel;
		private System.Windows.Forms.Label CompFileLenLabel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label DecompFileLenLabel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label RatioLabel;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label DecompTimeLabel;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label CompTimeLabel;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button TestButton;
		private System.Windows.Forms.Button ExitButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown CompLevelUpDown;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton ZLibRadioButton;
		private System.Windows.Forms.RadioButton ZipRadioButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label InputFileNameLabel;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox CompareFilesCheckBox;
		private System.Windows.Forms.RadioButton NoHeaderRadioButton;
		private System.Windows.Forms.Label CompareTestLabel;
		private System.Windows.Forms.Label label11;
	}
}