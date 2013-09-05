namespace DragScrollDemo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectControl3 = new NextUI.Bar.SelectControl();
            this.selectControl2 = new NextUI.Bar.SelectControl();
            this.selectControl1 = new NextUI.Bar.SelectControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectControl3
            // 
            this.selectControl3.BackImage = global::DragScrollDemo.Properties.Resources.select1;
            this.selectControl3.DisplayFont = new System.Drawing.Font("Courier New", 8F);
            this.selectControl3.Flipsize = NextUI.Bar.SelectControl.Flip.Down;
            this.selectControl3.FontColor = System.Drawing.Color.White;
            this.selectControl3.Location = new System.Drawing.Point(12, 136);
            this.selectControl3.MarkingType = NextUI.Bar.SelectControl.Marking.BOTH;
            this.selectControl3.Name = "selectControl3";
            this.selectControl3.PointerBodyColor = System.Drawing.Color.Pink;
            this.selectControl3.PointerColor = System.Drawing.Color.White;
            this.selectControl3.PointerHoverColor = System.Drawing.Color.Red;
            this.selectControl3.Size = new System.Drawing.Size(250, 48);
            this.selectControl3.TabIndex = 2;
            // 
            // selectControl2
            // 
            this.selectControl2.BackImage = null;
            this.selectControl2.DisplayFont = new System.Drawing.Font("Courier New", 8F);
            this.selectControl2.Flipsize = NextUI.Bar.SelectControl.Flip.Up;
            this.selectControl2.FontColor = System.Drawing.Color.Black;
            this.selectControl2.Location = new System.Drawing.Point(12, 69);
            this.selectControl2.MarkingType = NextUI.Bar.SelectControl.Marking.BOTH;
            this.selectControl2.Name = "selectControl2";
            this.selectControl2.PointerBodyColor = System.Drawing.Color.Black;
            this.selectControl2.PointerColor = System.Drawing.Color.Blue;
            this.selectControl2.PointerHoverColor = System.Drawing.Color.DarkGray;
            this.selectControl2.Size = new System.Drawing.Size(250, 47);
            this.selectControl2.TabIndex = 1;
            // 
            // selectControl1
            // 
            this.selectControl1.BackImage = global::DragScrollDemo.Properties.Resources.select1;
            this.selectControl1.DisplayFont = new System.Drawing.Font("Courier New", 7F);
            this.selectControl1.Flipsize = NextUI.Bar.SelectControl.Flip.Up;
            this.selectControl1.FontColor = System.Drawing.Color.White;
            this.selectControl1.Location = new System.Drawing.Point(12, 12);
            this.selectControl1.MarkingType = NextUI.Bar.SelectControl.Marking.LINE;
            this.selectControl1.Name = "selectControl1";
            this.selectControl1.PointerBodyColor = System.Drawing.Color.DimGray;
            this.selectControl1.PointerColor = System.Drawing.Color.White;
            this.selectControl1.PointerHoverColor = System.Drawing.Color.DarkGray;
            this.selectControl1.Size = new System.Drawing.Size(250, 43);
            this.selectControl1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 273);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectControl3);
            this.Controls.Add(this.selectControl2);
            this.Controls.Add(this.selectControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NextUI.Bar.SelectControl selectControl1;
        private NextUI.Bar.SelectControl selectControl2;
        private NextUI.Bar.SelectControl selectControl3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

