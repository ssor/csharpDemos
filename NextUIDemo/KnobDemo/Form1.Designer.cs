namespace KnobDemo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.controlKnob3 = new NextUI.Bar.ControlKnob();
            this.controlKnob2 = new NextUI.Bar.ControlKnob();
            this.controlKnob1 = new NextUI.Bar.ControlKnob();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // controlKnob3
            // 
            this.controlKnob3.BackImage = global::KnobDemo.Properties.Resources.Knob1;
            this.controlKnob3.DisplayFont = new System.Drawing.Font("Courier New", 8F);
            this.controlKnob3.FontColor = System.Drawing.Color.White;
            this.controlKnob3.KnobHandleImage = global::KnobDemo.Properties.Resources.Knob2;
            this.controlKnob3.Location = new System.Drawing.Point(255, 30);
            this.controlKnob3.Marking = NextUI.Bar.KnobPanel.Marking.CONT;
            this.controlKnob3.MarkingImageType = NextUI.Bar.KnobPanel.MarkingImage.IMAGE;
            this.controlKnob3.Name = "controlKnob3";
            this.controlKnob3.Size = new System.Drawing.Size(107, 108);
            this.controlKnob3.TabIndex = 2;
            // 
            // controlKnob2
            // 
            this.controlKnob2.BackImage = global::KnobDemo.Properties.Resources.Knob1;
            this.controlKnob2.DisplayFont = new System.Drawing.Font("Courier New", 8F);
            this.controlKnob2.FontColor = System.Drawing.Color.White;
            this.controlKnob2.KnobHandleImage = null;
            this.controlKnob2.Location = new System.Drawing.Point(142, 30);
            this.controlKnob2.Marking = NextUI.Bar.KnobPanel.Marking.LINE;
            this.controlKnob2.MarkingImageType = NextUI.Bar.KnobPanel.MarkingImage.FONT;
            this.controlKnob2.Name = "controlKnob2";
            this.controlKnob2.Size = new System.Drawing.Size(107, 108);
            this.controlKnob2.TabIndex = 1;
            // 
            // controlKnob1
            // 
            this.controlKnob1.BackImage = global::KnobDemo.Properties.Resources.Knob1;
            this.controlKnob1.DisplayFont = new System.Drawing.Font("Courier New", 8F);
            this.controlKnob1.FontColor = System.Drawing.Color.White;
            this.controlKnob1.KnobHandleImage = null;
            this.controlKnob1.Location = new System.Drawing.Point(29, 30);
            this.controlKnob1.Marking = NextUI.Bar.KnobPanel.Marking.BOTH;
            this.controlKnob1.MarkingImageType = NextUI.Bar.KnobPanel.MarkingImage.FONT;
            this.controlKnob1.Name = "controlKnob1";
            this.controlKnob1.Size = new System.Drawing.Size(107, 108);
            this.controlKnob1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 273);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.controlKnob3);
            this.Controls.Add(this.controlKnob2);
            this.Controls.Add(this.controlKnob1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NextUI.Bar.ControlKnob controlKnob1;
        private NextUI.Bar.ControlKnob controlKnob2;
        private NextUI.Bar.ControlKnob controlKnob3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

