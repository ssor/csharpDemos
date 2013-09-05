using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;


namespace WorkerThread
{
    //根据具体业务，修改此类
    public class ComandMail
    {
        public string command;
        public string arg;
        public ComandMail(string name, string arg)
        {
            this.command = name;
            this.arg = arg;
        }
        public string toString()
        {
            return command + " " + arg;
        }

    }
    /// <summary>
    /// Class emulates long process which runs in worker thread
    /// and makes synchronous user UI operations.
    /// </summary>
    public class LongProcess
    {
        #region Members
        public string Process_name = "default";
        List<ComandMail> mailbox = new List<ComandMail>();
        ManualResetEvent m_mailbox_sync = new ManualResetEvent(true);
        // Main thread sets this event to stop worker thread:
        ManualResetEvent m_EventStop = new ManualResetEvent(false);

        // Worker thread sets this event when it is stopped:
        ManualResetEvent m_EventStopped = new ManualResetEvent(false);

        // Reference to main form used to make syncronous user interface calls:
        //MainForm m_form;
       public Thread Work_thread = null;
        #endregion

        #region Functions

        public LongProcess()
        {

        }
        public LongProcess(string name)
        {
            this.Process_name = name;
        }
        public void Send_mail(ComandMail mail)
        {
            m_mailbox_sync.WaitOne();
            m_mailbox_sync.Reset();

            mailbox.Add(mail);

            m_mailbox_sync.Set();
        }
        public void Stop()
        {
            if (this.Work_thread != null && this.Work_thread.IsAlive)  // thread is active
            {
                // set event "Stop"
                m_EventStop.Set();

                // wait when thread  will stop or finish
                while (this.Work_thread.IsAlive)
                {
                    // We cannot use here infinite wait because our thread
                    // makes syncronous calls to main form, this will cause deadlock.
                    // Instead of this we wait for event some appropriate time
                    // (and by the way give time to worker thread) and
                    // process events. These events may contain Invoke calls.
                    if (WaitHandle.WaitAll(
                        (new ManualResetEvent[] { this.m_EventStopped }),
                        100,
                        true))
                    {
                        break;
                    }

                    Application.DoEvents();
                }
            }
        }
        // Function runs in worker thread and emulates long process.
        public void Run()
        {
            while (true)
            {
                // check if thread is cancelled
                if (m_EventStop.WaitOne(0, true))
                {
                    // clean-up operations may be placed here
                    // ...

                    // inform main thread that this thread stopped
                    m_EventStopped.Set();

                    return;
                }

                // make step
                //s = "Step number " + i.ToString() + " executed";
                m_mailbox_sync.WaitOne();
                m_mailbox_sync.Reset();
                ComandMail cm = null;
                if (mailbox.Count > 0)
                {
                    cm = mailbox[0];
                    mailbox.RemoveAt(0);
                }

                m_mailbox_sync.Set();
                if (cm == null)
                {
                    continue;
                }
                //执行命令,修改此处执行具体功能
                Debug.WriteLine(cm.toString());
                Thread.Sleep(300);

                // Make synchronous call to main form.
                // MainForm.AddString function runs in main thread.
                // To make asynchronous call use BeginInvoke
                //m_form.Invoke(m_form.m_DelegateAddString, new Object[] { s });



            }

            // Make asynchronous call to main form
            // to inform it that thread finished
            //m_form.Invoke(m_form.m_DelegateThreadFinished, null);
        }

        #endregion
    }
}
