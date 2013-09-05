// This code was written by MP
// (c) by Maciej Pirog 2002

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace NotifyChart
{
	/// <summary>
	/// NotifyIconChart example
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		// NotifyIconChart object
		private NotifyIconChart notifyChart = new NotifyIconChart();
		// Performance counter used to read CPU usage
		private System.Diagnostics.PerformanceCounter CpuUsageCounter = new System.Diagnostics.PerformanceCounter("Processor","% Processor Time","0");
		// GUI elements:
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.PropertyGrid pGrid;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage properties;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label cpu;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
				if (components != null) 
					components.Dispose();
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.pGrid = new System.Windows.Forms.PropertyGrid();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.properties = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.cpu = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.properties.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.Text = "";
			this.notifyIcon.Visible = true;
			// 
			// pGrid
			// 
			this.pGrid.CommandsVisibleIfAvailable = true;
			this.pGrid.HelpVisible = false;
			this.pGrid.LargeButtons = false;
			this.pGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pGrid.Location = new System.Drawing.Point(8, 8);
			this.pGrid.Name = "pGrid";
			this.pGrid.Size = new System.Drawing.Size(216, 152);
			this.pGrid.TabIndex = 0;
			this.pGrid.Text = "PropertyGrid";
			this.pGrid.ToolbarVisible = false;
			this.pGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.properties,
																					  this.tabPage2});
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(240, 192);
			this.tabControl1.TabIndex = 1;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// properties
			// 
			this.properties.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.pGrid});
			this.properties.Location = new System.Drawing.Point(4, 22);
			this.properties.Name = "properties";
			this.properties.Size = new System.Drawing.Size(232, 166);
			this.properties.TabIndex = 0;
			this.properties.Text = "Properties";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.label2,
																				   this.cpu,
																				   this.progressBar1,
																				   this.label1});
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(232, 166);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "CPU Usage";
			// 
			// timer1
			// 
			this.timer1.Interval = 750;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "The current CPU usage:";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(8, 48);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(216, 16);
			this.progressBar1.TabIndex = 1;
			// 
			// cpu
			// 
			this.cpu.Location = new System.Drawing.Point(136, 24);
			this.cpu.Name = "cpu";
			this.cpu.Size = new System.Drawing.Size(40, 16);
			this.cpu.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 32);
			this.label2.TabIndex = 3;
			this.label2.Text = "Take a look at the System Tray too !";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(258, 216);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabControl1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "NotifyIconChart example";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.properties.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			notifyChart.NotifyIconObject = this.notifyIcon;
			notifyChart.ChartType = NotifyIconChart.ChartTypeEnum.twoBars;
			notifyChart.Value1 = 85;
			notifyChart.Value2 = 50;
			pGrid.SelectedObject = notifyChart;
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(tabControl1.SelectedIndex == 0)
				timer1.Enabled = false;
			if(tabControl1.SelectedIndex == 1)
				timer1.Enabled = true;
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			int i = (int)CpuUsageCounter.NextValue();
			notifyChart.Value1 = i;
			this.progressBar1.Value = i;
			this.cpu.Text = i.ToString() + "%";
			this.notifyIcon.Text = "Current CPU Usage: " +  i.ToString() + "%";
		}

	}
}
