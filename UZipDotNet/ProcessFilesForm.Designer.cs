namespace UZipDotNet {
	partial class ProcessFilesForm {
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
		this.ListBox = new System.Windows.Forms.ListBox();
		this.ExitButton = new System.Windows.Forms.Button();
		this.ProgressLabel = new System.Windows.Forms.Label();
		this.ButtonsGroupBox = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.ErrorLabel = new System.Windows.Forms.Label();
		this.ButtonsGroupBox.SuspendLayout();
		this.SuspendLayout();
		// 
		// ListBox
		// 
		this.ListBox.FormattingEnabled = true;
		this.ListBox.ItemHeight = 16;
		this.ListBox.Location = new System.Drawing.Point(8, 12);
		this.ListBox.Name = "ListBox";
		this.ListBox.Size = new System.Drawing.Size(813, 580);
		this.ListBox.TabIndex = 0;
		// 
		// ExitButton
		// 
		this.ExitButton.Location = new System.Drawing.Point(25, 16);
		this.ExitButton.Name = "ExitButton";
		this.ExitButton.Size = new System.Drawing.Size(95, 35);
		this.ExitButton.TabIndex = 0;
		this.ExitButton.Text = "Abort";
		this.ExitButton.UseVisualStyleBackColor = true;
		this.ExitButton.Click += new System.EventHandler(this.OnAbort);
		// 
		// ProgressLabel
		// 
		this.ProgressLabel.BackColor = System.Drawing.SystemColors.Info;
		this.ProgressLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.ProgressLabel.Location = new System.Drawing.Point(137, 18);
		this.ProgressLabel.Name = "ProgressLabel";
		this.ProgressLabel.Size = new System.Drawing.Size(179, 31);
		this.ProgressLabel.TabIndex = 1;
		this.ProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// ButtonsGroupBox
		// 
		this.ButtonsGroupBox.Controls.Add(this.label2);
		this.ButtonsGroupBox.Controls.Add(this.ErrorLabel);
		this.ButtonsGroupBox.Controls.Add(this.ExitButton);
		this.ButtonsGroupBox.Controls.Add(this.ProgressLabel);
		this.ButtonsGroupBox.Location = new System.Drawing.Point(196, 598);
		this.ButtonsGroupBox.Name = "ButtonsGroupBox";
		this.ButtonsGroupBox.Size = new System.Drawing.Size(520, 60);
		this.ButtonsGroupBox.TabIndex = 1;
		this.ButtonsGroupBox.TabStop = false;
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(342, 27);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(43, 16);
		this.label2.TabIndex = 2;
		this.label2.Text = "Errors";
		// 
		// ErrorLabel
		// 
		this.ErrorLabel.BackColor = System.Drawing.SystemColors.Info;
		this.ErrorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.ErrorLabel.Location = new System.Drawing.Point(392, 18);
		this.ErrorLabel.Name = "ErrorLabel";
		this.ErrorLabel.Size = new System.Drawing.Size(104, 31);
		this.ErrorLabel.TabIndex = 3;
		this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// ProcessFilesForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(834, 664);
		this.Controls.Add(this.ButtonsGroupBox);
		this.Controls.Add(this.ListBox);
		this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.MinimumSize = new System.Drawing.Size(850, 700);
		this.Name = "ProcessFilesForm";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Process Files";
		this.Load += new System.EventHandler(this.OnLoad);
		this.Resize += new System.EventHandler(this.OnResize);
		this.ButtonsGroupBox.ResumeLayout(false);
		this.ButtonsGroupBox.PerformLayout();
		this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox ListBox;
		private System.Windows.Forms.Button ExitButton;
		private System.Windows.Forms.Label ProgressLabel;
		private System.Windows.Forms.GroupBox ButtonsGroupBox;
		private System.Windows.Forms.Label ErrorLabel;
		private System.Windows.Forms.Label label2;
	}
}