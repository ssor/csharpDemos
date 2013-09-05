using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;


namespace WorkerThread
{
    #region Public Delegates

    // delegates used to call MainForm functions from worker thread
    public delegate void DelegateAddString(String s);
    public delegate void DelegateThreadFinished();

    #endregion

    /// <summary>
    /// Main form for WorkerThread Sample
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        #region Designer Code

        private System.Windows.Forms.Button btnStartThread;
        private System.Windows.Forms.Button btnStopThread;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListBox listBox1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        #endregion

        #region Members

        // worker thread
        //Thread m_WorkerThread;
        LongProcess longProcess;
        // events used to stop worker thread
        //ManualResetEvent m_EventStopThread;
        //ManualResetEvent m_EventThreadStopped;

        // Delegate instances used to cal user interface functions 
        // from worker thread:
        public DelegateAddString m_DelegateAddString;
        private Button button1;
        public DelegateThreadFinished m_DelegateThreadFinished;


        #endregion

        #region Constructor, Destructor

        public MainForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // initialize delegates
            m_DelegateAddString = new DelegateAddString(this.AddString);
            m_DelegateThreadFinished = new DelegateThreadFinished(this.ThreadFinished);

            // initialize events
            //m_EventStopThread = new ManualResetEvent(false);
            //m_EventThreadStopped = new ManualResetEvent(false);

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion


        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartThread = new System.Windows.Forms.Button();
            this.btnStopThread = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartThread
            // 
            this.btnStartThread.Location = new System.Drawing.Point(416, 60);
            this.btnStartThread.Name = "btnStartThread";
            this.btnStartThread.Size = new System.Drawing.Size(104, 30);
            this.btnStartThread.TabIndex = 0;
            this.btnStartThread.Text = "Start Thread";
            this.btnStartThread.Click += new System.EventHandler(this.btnStartThread_Click);
            // 
            // btnStopThread
            // 
            this.btnStopThread.Enabled = false;
            this.btnStopThread.Location = new System.Drawing.Point(416, 156);
            this.btnStopThread.Name = "btnStopThread";
            this.btnStopThread.Size = new System.Drawing.Size(104, 30);
            this.btnStopThread.TabIndex = 1;
            this.btnStopThread.Text = "Stop Thread";
            this.btnStopThread.Click += new System.EventHandler(this.btnStopThread_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(416, 209);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(104, 30);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // listBox1
            // 
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(8, 7);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(352, 244);
            this.listBox1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(416, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "添加命令";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(552, 296);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStopThread);
            this.Controls.Add(this.btnStartThread);
            this.Name = "MainForm";
            this.Text = "Worker thread sample";
            this.Closed += new System.EventHandler(this.MainForm_Closed);
            this.ResumeLayout(false);

        }
        #endregion

        #region Function Main

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new MainForm());
        }

        #endregion

        #region Message Handlers

        // Start thread button is pressed
        private void btnStartThread_Click(object sender, System.EventArgs e)
        {
            listBox1.Items.Clear();
            btnStartThread.Enabled = false;
            btnStopThread.Enabled = true;

            #region 初始化代码
            longProcess = new LongProcess();
            ThreadStart dele = delegate()
                        {
                            longProcess.Run();
                        };
            // create worker thread instance
            Thread m_WorkerThread = new Thread(dele);
            m_WorkerThread.Name = "Worker Thread Sample";	// looks nice in Output window
            this.longProcess.Work_thread = m_WorkerThread;
            m_WorkerThread.Start();
            #endregion

        }

        // Stop Thread button is pressed
        private void btnStopThread_Click(object sender, System.EventArgs e)
        {
            StopThread();
        }

        // Exit button is pressed.
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        // Form is closed.
        // Stop thread if it is active.
        private void MainForm_Closed(object sender, System.EventArgs e)
        {
            StopThread();
        }

        #endregion


        #region Other Functions



        // Stop worker thread if it is running.
        // Called when user presses Stop button of form is closed.
        private void StopThread()
        {
            #region 停止运行
            longProcess.Stop();
            #endregion

            ThreadFinished();		// set initial state of buttons
        }

        // Add string to list box.
        // Called from worker thread using delegate and Control.Invoke
        private void AddString(String s)
        {
            listBox1.Items.Add(s);


        }

        // Set initial state of controls.
        // Called from worker thread using delegate and Control.Invoke
        private void ThreadFinished()
        {
            btnStartThread.Enabled = true;
            btnStopThread.Enabled = false;
        }

        #endregion
        int index = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            #region 模拟发送命令
            this.longProcess.Send_mail(new ComandMail("date", index.ToString()));
            #endregion

            index++;
        }


    }
}
