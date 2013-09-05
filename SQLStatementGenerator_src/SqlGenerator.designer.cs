using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace SqlStatementGenerator
{
    partial class SqlGenerator
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
            this.txtSelectSQL = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblIncludeFields = new System.Windows.Forms.Label();
            this.chklstIncludeFields = new System.Windows.Forms.CheckedListBox();
            this.lblTargetTable = new System.Windows.Forms.Label();
            this.txtTargetTable = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbDataType1 = new System.Windows.Forms.ComboBox();
            this.txtColumnName1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAliasName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSelectSQL
            // 
            this.txtSelectSQL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSelectSQL.Location = new System.Drawing.Point(470, 21);
            this.txtSelectSQL.Multiline = true;
            this.txtSelectSQL.Name = "txtSelectSQL";
            this.txtSelectSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSelectSQL.Size = new System.Drawing.Size(281, 519);
            this.txtSelectSQL.TabIndex = 5;
            this.txtSelectSQL.TextChanged += new System.EventHandler(this.txtSelectSQL_TextChanged);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(766, 20);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(196, 35);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblIncludeFields
            // 
            this.lblIncludeFields.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIncludeFields.Location = new System.Drawing.Point(759, 262);
            this.lblIncludeFields.Name = "lblIncludeFields";
            this.lblIncludeFields.Size = new System.Drawing.Size(186, 15);
            this.lblIncludeFields.TabIndex = 9;
            this.lblIncludeFields.Text = "Check the fields to include:";
            // 
            // chklstIncludeFields
            // 
            this.chklstIncludeFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chklstIncludeFields.Location = new System.Drawing.Point(761, 280);
            this.chklstIncludeFields.Name = "chklstIncludeFields";
            this.chklstIncludeFields.Size = new System.Drawing.Size(201, 260);
            this.chklstIncludeFields.TabIndex = 10;
            // 
            // lblTargetTable
            // 
            this.lblTargetTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTargetTable.Location = new System.Drawing.Point(12, 23);
            this.lblTargetTable.Name = "lblTargetTable";
            this.lblTargetTable.Size = new System.Drawing.Size(199, 15);
            this.lblTargetTable.TabIndex = 7;
            this.lblTargetTable.Text = "数据表名：";
            // 
            // txtTargetTable
            // 
            this.txtTargetTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetTable.Location = new System.Drawing.Point(13, 41);
            this.txtTargetTable.Name = "txtTargetTable";
            this.txtTargetTable.Size = new System.Drawing.Size(337, 21);
            this.txtTargetTable.TabIndex = 8;
            this.txtTargetTable.TextChanged += new System.EventHandler(this.txtTargetTable_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(766, 62);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(196, 35);
            this.btnClose.TabIndex = 74;
            this.btnClose.Text = "退出";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAliasName);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cmbDataType1);
            this.groupBox1.Controls.Add(this.txtColumnName1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTargetTable);
            this.groupBox1.Controls.Add(this.lblTargetTable);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 527);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据表设计";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(356, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "增加列";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbDataType1
            // 
            this.cmbDataType1.FormattingEnabled = true;
            this.cmbDataType1.Items.AddRange(new object[] {
            "varchar(32)",
            "int"});
            this.cmbDataType1.Location = new System.Drawing.Point(319, 90);
            this.cmbDataType1.Name = "cmbDataType1";
            this.cmbDataType1.Size = new System.Drawing.Size(112, 20);
            this.cmbDataType1.TabIndex = 3;
            // 
            // txtColumnName1
            // 
            this.txtColumnName1.Location = new System.Drawing.Point(13, 90);
            this.txtColumnName1.Name = "txtColumnName1";
            this.txtColumnName1.Size = new System.Drawing.Size(148, 21);
            this.txtColumnName1.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(321, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "数据类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "列名：";
            // 
            // txtAliasName
            // 
            this.txtAliasName.Location = new System.Drawing.Point(177, 90);
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.Size = new System.Drawing.Size(124, 21);
            this.txtAliasName.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(175, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "别名：";
            // 
            // SqlGenerator
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(977, 579);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtSelectSQL);
            this.Controls.Add(this.chklstIncludeFields);
            this.Controls.Add(this.lblIncludeFields);
            this.Controls.Add(this.btnGenerate);
            this.Name = "SqlGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Statement Generator";
            this.Load += new System.EventHandler(this.SqlGenerator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtSelectSQL;
        private System.Windows.Forms.Label lblIncludeFields;
        private System.Windows.Forms.CheckedListBox chklstIncludeFields;
        private System.Windows.Forms.Label lblTargetTable;
        private System.Windows.Forms.TextBox txtTargetTable;
        private Button btnClose;
        private GroupBox groupBox1;
        private Label label4;
        private Label label3;
        private Button button1;
        private ComboBox cmbDataType1;
        private TextBox txtColumnName1;
        private TextBox txtAliasName;
        private Label label1;
            

    }
}
