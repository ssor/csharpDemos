namespace ToggleDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.switchbutton3 = new NextUI.Bar.Switchbutton();
            this.switchbutton2 = new NextUI.Bar.Switchbutton();
            this.switchbutton1 = new NextUI.Bar.Switchbutton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // switchbutton3
            // 
            this.switchbutton3.Blink = false;
            this.switchbutton3.BlinkRate = 500;
            this.switchbutton3.HaloEffect = false;
            this.switchbutton3.LitColor = System.Drawing.Color.Blue;
            this.switchbutton3.Location = new System.Drawing.Point(190, 24);
            this.switchbutton3.MainColor = System.Drawing.Color.DarkGray;
            this.switchbutton3.Name = "switchbutton3";
            this.switchbutton3.NonLitColor = System.Drawing.Color.Black;
            this.switchbutton3.OffImage = ((System.Drawing.Image)(resources.GetObject("switchbutton3.OffImage")));
            this.switchbutton3.OnImage = ((System.Drawing.Image)(resources.GetObject("switchbutton3.OnImage")));
            this.switchbutton3.Size = new System.Drawing.Size(58, 57);
            this.switchbutton3.TabIndex = 2;
            // 
            // switchbutton2
            // 
            this.switchbutton2.Blink = true;
            this.switchbutton2.BlinkRate = 500;
            this.switchbutton2.HaloEffect = true;
            this.switchbutton2.LitColor = System.Drawing.Color.DeepPink;
            this.switchbutton2.Location = new System.Drawing.Point(103, 24);
            this.switchbutton2.MainColor = System.Drawing.Color.DarkGray;
            this.switchbutton2.Name = "switchbutton2";
            this.switchbutton2.NonLitColor = System.Drawing.Color.White;
            this.switchbutton2.OffImage = ((System.Drawing.Image)(resources.GetObject("switchbutton2.OffImage")));
            this.switchbutton2.OnImage = ((System.Drawing.Image)(resources.GetObject("switchbutton2.OnImage")));
            this.switchbutton2.Size = new System.Drawing.Size(58, 57);
            this.switchbutton2.TabIndex = 1;
            // 
            // switchbutton1
            // 
            this.switchbutton1.Blink = true;
            this.switchbutton1.BlinkRate = 500;
            this.switchbutton1.HaloEffect = false;
            this.switchbutton1.LitColor = System.Drawing.Color.DeepPink;
            this.switchbutton1.Location = new System.Drawing.Point(23, 24);
            this.switchbutton1.MainColor = System.Drawing.Color.DarkGray;
            this.switchbutton1.Name = "switchbutton1";
            this.switchbutton1.NonLitColor = System.Drawing.Color.White;
            this.switchbutton1.OffImage = ((System.Drawing.Image)(resources.GetObject("switchbutton1.OffImage")));
            this.switchbutton1.OnImage = ((System.Drawing.Image)(resources.GetObject("switchbutton1.OnImage")));
            this.switchbutton1.Size = new System.Drawing.Size(58, 57);
            this.switchbutton1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.switchbutton3);
            this.Controls.Add(this.switchbutton2);
            this.Controls.Add(this.switchbutton1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NextUI.Bar.Switchbutton switchbutton1;
        private NextUI.Bar.Switchbutton switchbutton2;
        private NextUI.Bar.Switchbutton switchbutton3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

