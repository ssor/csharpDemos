namespace AnalogControlSample
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
            this.analogCounter3 = new NextUI.Display.AnalogCounter();
            this.analogCounter2 = new NextUI.Display.AnalogCounter();
            this.analogCounter1 = new NextUI.Display.AnalogCounter();
            this.SuspendLayout();
            // 
            // analogCounter3
            // 
            this.analogCounter3.alignment = NextUI.Display.AnalogCounter.Alignment.Right;
            this.analogCounter3.CounterNumber = 7;
            this.analogCounter3.DisplayFont = new System.Drawing.Font("Courier New", 7F);
            this.analogCounter3.FrontImage = global::AnalogControlSample.Properties.Resources.ademo3;
            this.analogCounter3.Location = new System.Drawing.Point(21, 136);
            this.analogCounter3.MainColor = System.Drawing.Color.DimGray;
            this.analogCounter3.MinumumDistanceFromH = 20;
            this.analogCounter3.Name = "analogCounter3";
            this.analogCounter3.Number = 20;
            this.analogCounter3.ScrollEffect = false;
            this.analogCounter3.Size = new System.Drawing.Size(206, 49);
            this.analogCounter3.TabIndex = 2;
            // 
            // analogCounter2
            // 
            this.analogCounter2.alignment = NextUI.Display.AnalogCounter.Alignment.Center;
            this.analogCounter2.CounterNumber = 6;
            this.analogCounter2.DisplayFont = new System.Drawing.Font("Century", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analogCounter2.FrontImage = global::AnalogControlSample.Properties.Resources.ademo4;
            this.analogCounter2.Location = new System.Drawing.Point(21, 72);
            this.analogCounter2.MainColor = System.Drawing.Color.Maroon;
            this.analogCounter2.MinumumDistanceFromH = 20;
            this.analogCounter2.Name = "analogCounter2";
            this.analogCounter2.Number = 20;
            this.analogCounter2.ScrollEffect = false;
            this.analogCounter2.Size = new System.Drawing.Size(206, 47);
            this.analogCounter2.TabIndex = 1;
            // 
            // analogCounter1
            // 
            this.analogCounter1.alignment = NextUI.Display.AnalogCounter.Alignment.Left;
            this.analogCounter1.CounterNumber = 5;
            this.analogCounter1.DisplayFont = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analogCounter1.FrontImage = global::AnalogControlSample.Properties.Resources.ademo1;
            this.analogCounter1.Location = new System.Drawing.Point(21, 12);
            this.analogCounter1.MainColor = System.Drawing.Color.Honeydew;
            this.analogCounter1.MinumumDistanceFromH = 20;
            this.analogCounter1.Name = "analogCounter1";
            this.analogCounter1.Number = 20;
            this.analogCounter1.ScrollEffect = false;
            this.analogCounter1.Size = new System.Drawing.Size(206, 54);
            this.analogCounter1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.analogCounter3);
            this.Controls.Add(this.analogCounter2);
            this.Controls.Add(this.analogCounter1);
            this.Name = "Form1";
            this.Text = "Analog Control Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private NextUI.Display.AnalogCounter analogCounter1;
        private NextUI.Display.AnalogCounter analogCounter2;
        private NextUI.Display.AnalogCounter analogCounter3;
    }
}

