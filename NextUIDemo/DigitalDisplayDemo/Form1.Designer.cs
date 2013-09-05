namespace DigitalDisplayDemo
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
            this.digitalDisplay3 = new NextUI.Display.DigitalDisplay();
            this.digitalDisplay2 = new NextUI.Display.DigitalDisplay();
            this.digitalDisplay1 = new NextUI.Display.DigitalDisplay();
            this.SuspendLayout();
            // 
            // digitalDisplay3
            // 
            this.digitalDisplay3.Alignment = NextUI.Display.DigitalDisplay.location.FILL;
            this.digitalDisplay3.BackGrdColor = System.Drawing.Color.Black;
            this.digitalDisplay3.BackGroundImage = null;
            this.digitalDisplay3.EnableGlare = true;
            this.digitalDisplay3.FontColor = System.Drawing.Color.DarkTurquoise;
            this.digitalDisplay3.Location = new System.Drawing.Point(36, 154);
            this.digitalDisplay3.Name = "digitalDisplay3";
            this.digitalDisplay3.Number = 0F;
            this.digitalDisplay3.PanelColor = System.Drawing.Color.Black;
            this.digitalDisplay3.PanelNumber = 6;
            this.digitalDisplay3.Size = new System.Drawing.Size(197, 53);
            this.digitalDisplay3.TabIndex = 2;
            // 
            // digitalDisplay2
            // 
            this.digitalDisplay2.Alignment = NextUI.Display.DigitalDisplay.location.CENTER;
            this.digitalDisplay2.BackGrdColor = System.Drawing.Color.DarkRed;
            this.digitalDisplay2.BackGroundImage = global::DigitalDisplayDemo.Properties.Resources.digital1;
            this.digitalDisplay2.EnableGlare = true;
            this.digitalDisplay2.FontColor = System.Drawing.Color.Azure;
            this.digitalDisplay2.Location = new System.Drawing.Point(36, 95);
            this.digitalDisplay2.Name = "digitalDisplay2";
            this.digitalDisplay2.Number = 0F;
            this.digitalDisplay2.PanelColor = System.Drawing.Color.Black;
            this.digitalDisplay2.PanelNumber = 6;
            this.digitalDisplay2.Size = new System.Drawing.Size(197, 53);
            this.digitalDisplay2.TabIndex = 1;
            // 
            // digitalDisplay1
            // 
            this.digitalDisplay1.Alignment = NextUI.Display.DigitalDisplay.location.LEFT;
            this.digitalDisplay1.BackGrdColor = System.Drawing.Color.Maroon;
            this.digitalDisplay1.BackGroundImage = global::DigitalDisplayDemo.Properties.Resources.digital2;
            this.digitalDisplay1.EnableGlare = false;
            this.digitalDisplay1.FontColor = System.Drawing.Color.Ivory;
            this.digitalDisplay1.Location = new System.Drawing.Point(36, 36);
            this.digitalDisplay1.Name = "digitalDisplay1";
            this.digitalDisplay1.Number = 0F;
            this.digitalDisplay1.PanelColor = System.Drawing.Color.DarkRed;
            this.digitalDisplay1.PanelNumber = 7;
            this.digitalDisplay1.Size = new System.Drawing.Size(197, 53);
            this.digitalDisplay1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.digitalDisplay3);
            this.Controls.Add(this.digitalDisplay2);
            this.Controls.Add(this.digitalDisplay1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private NextUI.Display.DigitalDisplay digitalDisplay1;
        private NextUI.Display.DigitalDisplay digitalDisplay2;
        private NextUI.Display.DigitalDisplay digitalDisplay3;
    }
}

