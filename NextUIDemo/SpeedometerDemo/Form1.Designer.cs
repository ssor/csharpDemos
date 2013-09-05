namespace SpeedometerDemo
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
            this.pointerMeter4 = new NextUI.Display.PointerMeter();
            this.pointerMeter2 = new NextUI.Display.PointerMeter();
            this.pointerMeter1 = new NextUI.Display.PointerMeter();
            this.SuspendLayout();
            // 
            // pointerMeter4
            // 
            this.pointerMeter4.BackGrdColor = System.Drawing.Color.LightGray;
            this.pointerMeter4.BackGroundImage = global::SpeedometerDemo.Properties.Resources.speeddemo4;
            this.pointerMeter4.DisplayFont = new System.Drawing.Font("Courier New", 8F);
            this.pointerMeter4.GapWidth = 180F;
            this.pointerMeter4.InnerBorderRing = false;
            this.pointerMeter4.LabelFontColor = System.Drawing.Color.RosyBrown;
            this.pointerMeter4.Location = new System.Drawing.Point(318, 12);
            this.pointerMeter4.Name = "pointerMeter4";
            this.pointerMeter4.Number = 0;
            this.pointerMeter4.PointerColor = System.Drawing.Color.RosyBrown;
            this.pointerMeter4.PointerHandleColor = System.Drawing.Color.DimGray;
            this.pointerMeter4.Size = new System.Drawing.Size(147, 139);
            this.pointerMeter4.StartGapAngle = 0F;
            this.pointerMeter4.TabIndex = 3;
            // 
            // pointerMeter2
            // 
            this.pointerMeter2.BackGrdColor = System.Drawing.Color.Gray;
            this.pointerMeter2.BackGroundImage = global::SpeedometerDemo.Properties.Resources.speeddemo3;
            this.pointerMeter2.DisplayFont = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointerMeter2.GapWidth = 20F;
            this.pointerMeter2.InnerBorderRing = true;
            this.pointerMeter2.LabelFontColor = System.Drawing.Color.White;
            this.pointerMeter2.Location = new System.Drawing.Point(165, 12);
            this.pointerMeter2.Name = "pointerMeter2";
            this.pointerMeter2.Number = 0;
            this.pointerMeter2.PointerColor = System.Drawing.Color.White;
            this.pointerMeter2.PointerHandleColor = System.Drawing.Color.White;
            this.pointerMeter2.Size = new System.Drawing.Size(147, 139);
            this.pointerMeter2.StartGapAngle = 80F;
            this.pointerMeter2.TabIndex = 1;
            // 
            // pointerMeter1
            // 
            this.pointerMeter1.BackGrdColor = System.Drawing.Color.LightGray;
            this.pointerMeter1.BackGroundImage = null;
            this.pointerMeter1.DisplayFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointerMeter1.GapWidth = 20F;
            this.pointerMeter1.InnerBorderRing = true;
            this.pointerMeter1.LabelFontColor = System.Drawing.Color.RosyBrown;
            this.pointerMeter1.Location = new System.Drawing.Point(12, 12);
            this.pointerMeter1.Name = "pointerMeter1";
            this.pointerMeter1.Number = 0;
            this.pointerMeter1.PointerColor = System.Drawing.Color.RosyBrown;
            this.pointerMeter1.PointerHandleColor = System.Drawing.Color.DimGray;
            this.pointerMeter1.Size = new System.Drawing.Size(147, 139);
            this.pointerMeter1.StartGapAngle = 80F;
            this.pointerMeter1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 188);
            this.Controls.Add(this.pointerMeter4);
            this.Controls.Add(this.pointerMeter2);
            this.Controls.Add(this.pointerMeter1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private NextUI.Display.PointerMeter pointerMeter1;
        private NextUI.Display.PointerMeter pointerMeter2;
        private NextUI.Display.PointerMeter pointerMeter4;
    }
}

