namespace MeterDemo
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
            this.thermoDisplay2 = new NextUI.Display.ThermoDisplay();
            this.thermoDisplay3 = new NextUI.Display.ThermoDisplay();
            this.thermoDisplay1 = new NextUI.Display.ThermoDisplay();
            this.SuspendLayout();
            // 
            // thermoDisplay2
            // 
            this.thermoDisplay2.Alignment = NextUI.Display.ThermoPanel.Alignment.left;
            this.thermoDisplay2.BackGrdColor = System.Drawing.Color.LightGray;
            this.thermoDisplay2.BackGroundImage = null;
            this.thermoDisplay2.Flip = NextUI.Display.ThermoPanel.Flip.right;
            this.thermoDisplay2.IndicatorColor = System.Drawing.Color.DarkBlue;
            this.thermoDisplay2.LabelFont = new System.Drawing.Font("Courier New", 7F);
            this.thermoDisplay2.LabelFontColor = System.Drawing.Color.Black;
            this.thermoDisplay2.Location = new System.Drawing.Point(96, 31);
            this.thermoDisplay2.Marking = NextUI.Display.ThermoPanel.Marking.CONT;
            this.thermoDisplay2.Name = "thermoDisplay2";
            this.thermoDisplay2.Number = 0;
            this.thermoDisplay2.Size = new System.Drawing.Size(47, 222);
            this.thermoDisplay2.TabIndex = 1;
            // 
            // thermoDisplay3
            // 
            this.thermoDisplay3.Alignment = NextUI.Display.ThermoPanel.Alignment.right;
            this.thermoDisplay3.BackGrdColor = System.Drawing.Color.LightGray;
            this.thermoDisplay3.BackGroundImage = global::MeterDemo.Properties.Resources.themo6;
            this.thermoDisplay3.Flip = NextUI.Display.ThermoPanel.Flip.Left;
            this.thermoDisplay3.IndicatorColor = System.Drawing.Color.DarkBlue;
            this.thermoDisplay3.LabelFont = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thermoDisplay3.LabelFontColor = System.Drawing.Color.White;
            this.thermoDisplay3.Location = new System.Drawing.Point(162, 31);
            this.thermoDisplay3.Marking = NextUI.Display.ThermoPanel.Marking.BOTH;
            this.thermoDisplay3.Name = "thermoDisplay3";
            this.thermoDisplay3.Number = 0;
            this.thermoDisplay3.Size = new System.Drawing.Size(81, 221);
            this.thermoDisplay3.TabIndex = 2;
            // 
            // thermoDisplay1
            // 
            this.thermoDisplay1.Alignment = NextUI.Display.ThermoPanel.Alignment.left;
            this.thermoDisplay1.BackGrdColor = System.Drawing.Color.Black;
            this.thermoDisplay1.BackGroundImage = global::MeterDemo.Properties.Resources.themo1;
            this.thermoDisplay1.Flip = NextUI.Display.ThermoPanel.Flip.right;
            this.thermoDisplay1.IndicatorColor = System.Drawing.Color.Red;
            this.thermoDisplay1.LabelFont = new System.Drawing.Font("Courier New", 7F, System.Drawing.FontStyle.Bold);
            this.thermoDisplay1.LabelFontColor = System.Drawing.Color.White;
            this.thermoDisplay1.Location = new System.Drawing.Point(23, 26);
            this.thermoDisplay1.Marking = NextUI.Display.ThermoPanel.Marking.BOTH;
            this.thermoDisplay1.Name = "thermoDisplay1";
            this.thermoDisplay1.Number = 0;
            this.thermoDisplay1.Size = new System.Drawing.Size(58, 228);
            this.thermoDisplay1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.thermoDisplay3);
            this.Controls.Add(this.thermoDisplay2);
            this.Controls.Add(this.thermoDisplay1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private NextUI.Display.ThermoDisplay thermoDisplay1;
        private NextUI.Display.ThermoDisplay thermoDisplay2;
        private NextUI.Display.ThermoDisplay thermoDisplay3;
    }
}

