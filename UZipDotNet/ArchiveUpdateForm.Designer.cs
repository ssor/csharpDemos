namespace UZipDotNet {
	partial class ArchiveUpdateForm {
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
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.SaveRadioButton = new System.Windows.Forms.RadioButton();
		this.NoSaveRadioButton = new System.Windows.Forms.RadioButton();
		this.UpdateButton = new System.Windows.Forms.Button();
		this.ExitButton = new System.Windows.Forms.Button();
		this.RecycleBinRadioButton = new System.Windows.Forms.RadioButton();
		this.groupBox1.SuspendLayout();
		this.SuspendLayout();
		// 
		// label1
		// 
		this.label1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.label1.Location = new System.Drawing.Point(84, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(207, 39);
		this.label1.TabIndex = 0;
		this.label1.Text = "Archive Update";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(17, 96);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(340, 16);
		this.label2.TabIndex = 1;
		this.label2.Text = "Do you want to update (add and delete) the open archive?";
		// 
		// groupBox1
		// 
		this.groupBox1.Controls.Add(this.RecycleBinRadioButton);
		this.groupBox1.Controls.Add(this.SaveRadioButton);
		this.groupBox1.Controls.Add(this.NoSaveRadioButton);
		this.groupBox1.Location = new System.Drawing.Point(17, 124);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(340, 114);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		// 
		// SaveRadioButton
		// 
		this.SaveRadioButton.AutoSize = true;
		this.SaveRadioButton.Location = new System.Drawing.Point(19, 50);
		this.SaveRadioButton.Name = "SaveRadioButton";
		this.SaveRadioButton.Size = new System.Drawing.Size(179, 20);
		this.SaveRadioButton.TabIndex = 1;
		this.SaveRadioButton.Text = "Save a copy before update";
		this.SaveRadioButton.UseVisualStyleBackColor = true;
		// 
		// NoSaveRadioButton
		// 
		this.NoSaveRadioButton.AutoSize = true;
		this.NoSaveRadioButton.Location = new System.Drawing.Point(19, 81);
		this.NoSaveRadioButton.Name = "NoSaveRadioButton";
		this.NoSaveRadioButton.Size = new System.Drawing.Size(261, 20);
		this.NoSaveRadioButton.TabIndex = 2;
		this.NoSaveRadioButton.Text = "Update the open archive without saving it";
		this.NoSaveRadioButton.UseVisualStyleBackColor = true;
		// 
		// UpdateButton
		// 
		this.UpdateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.UpdateButton.Location = new System.Drawing.Point(69, 275);
		this.UpdateButton.Name = "UpdateButton";
		this.UpdateButton.Size = new System.Drawing.Size(101, 42);
		this.UpdateButton.TabIndex = 3;
		this.UpdateButton.Text = "OK";
		this.UpdateButton.UseVisualStyleBackColor = true;
		// 
		// ExitButton
		// 
		this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ExitButton.Location = new System.Drawing.Point(205, 275);
		this.ExitButton.Name = "ExitButton";
		this.ExitButton.Size = new System.Drawing.Size(101, 42);
		this.ExitButton.TabIndex = 4;
		this.ExitButton.Text = "Cancel";
		this.ExitButton.UseVisualStyleBackColor = true;
		// 
		// RecycleBinRadioButton
		// 
		this.RecycleBinRadioButton.AutoSize = true;
		this.RecycleBinRadioButton.Checked = true;
		this.RecycleBinRadioButton.Location = new System.Drawing.Point(19, 19);
		this.RecycleBinRadioButton.Name = "RecycleBinRadioButton";
		this.RecycleBinRadioButton.Size = new System.Drawing.Size(227, 20);
		this.RecycleBinRadioButton.TabIndex = 0;
		this.RecycleBinRadioButton.Text = "Send to Recycle Bin before update";
		this.RecycleBinRadioButton.UseVisualStyleBackColor = true;
		// 
		// ArchiveUpdateForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.CancelButton = this.ExitButton;
		this.ClientSize = new System.Drawing.Size(374, 344);
		this.Controls.Add(this.ExitButton);
		this.Controls.Add(this.UpdateButton);
		this.Controls.Add(this.groupBox1);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.label1);
		this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "ArchiveUpdateForm";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Archive Update";
		this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.ResumeLayout(false);
		this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton SaveRadioButton;
		private System.Windows.Forms.RadioButton NoSaveRadioButton;
		private System.Windows.Forms.Button UpdateButton;
		private System.Windows.Forms.Button ExitButton;
		private System.Windows.Forms.RadioButton RecycleBinRadioButton;
	}
}