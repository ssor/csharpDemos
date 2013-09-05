namespace RfidReader
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button3 = new System.Windows.Forms.Button();
            this.portNames = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblEPC = new System.Windows.Forms.Label();
            this.lblEquip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(691, 426);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "playWav";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // portNames
            // 
            this.portNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portNames.FormattingEnabled = true;
            this.portNames.Location = new System.Drawing.Point(12, 25);
            this.portNames.Name = "portNames";
            this.portNames.Size = new System.Drawing.Size(227, 20);
            this.portNames.TabIndex = 11;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 67);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(95, 23);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button8_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(144, 67);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(95, 23);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblEPC
            // 
            this.lblEPC.AutoSize = true;
            this.lblEPC.Font = new System.Drawing.Font("宋体", 75.25F, System.Drawing.FontStyle.Bold);
            this.lblEPC.Location = new System.Drawing.Point(27, 161);
            this.lblEPC.Name = "lblEPC";
            this.lblEPC.Size = new System.Drawing.Size(355, 101);
            this.lblEPC.TabIndex = 14;
            this.lblEPC.Text = "label1";
            // 
            // lblEquip
            // 
            this.lblEquip.AutoSize = true;
            this.lblEquip.Font = new System.Drawing.Font("宋体", 75.25F, System.Drawing.FontStyle.Bold);
            this.lblEquip.Location = new System.Drawing.Point(22, 326);
            this.lblEquip.Name = "lblEquip";
            this.lblEquip.Size = new System.Drawing.Size(355, 101);
            this.lblEquip.TabIndex = 14;
            this.lblEquip.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 461);
            this.Controls.Add(this.lblEquip);
            this.Controls.Add(this.lblEPC);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.portNames);
            this.Controls.Add(this.button3);
            this.Name = "Form1";
            this.Text = "教学终端测试";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox portNames;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblEPC;
        private System.Windows.Forms.Label lblEquip;
    }
}

