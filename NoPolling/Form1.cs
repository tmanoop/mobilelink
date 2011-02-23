using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace NoPolling
{
    public partial class Form1 : Form
    {

        
        delegate void UpdateStatusDelegate(int progress);
        delegate void VoidDelegate();

        UpdateStatusDelegate _updateStatus;
        VoidDelegate _taskComplete;
        WaitCallback _longRunningTask;

        

        public Form1()
        {
            InitializeComponent();
            _longRunningTask = new WaitCallback(LongRunningTask);
            _updateStatus = new UpdateStatusDelegate(UpdateStatus);
            _taskComplete = new VoidDelegate(TaskComplete);
        }

        // We cannot update UI elements from secondary threads. However
        // the Control.Invoke method can be used to execute a delegate 
        // on the main thread. If UpdateStatus is called from a secondary
        // thread it will automatically call itself through Control.Invoke
        // to ensure its work is performed on the primary (UI) thread.
        void UpdateStatus(int progress)
        {
            if (this.InvokeRequired)
                BeginInvoke(_updateStatus, new object[] { progress });
            else
            {
                this.txtFeedback.Text = progress.ToString();
                pbWorkStatus.Value = progress;
            }
        }


        // The long running task is contained within this method. 
        // as the task runs it will pass progress updates back to
        // the UI through the UpdateStatus method.  Upon completion
        // of the task a call is made to TaskComplete
        void LongRunningTask(object o)
        {
            try
            {
                for (int i = 0; i < 100; ++i)
                {
                    Thread.Sleep(100);
                    UpdateStatus(i);
                }
                TaskComplete();
            }
            catch (ThreadAbortException exc)
            {
                //The task is being cancelled.  
            }          
        }

        // Since the actions the program takes at completion of the long
        // running task touch UI elements the TaskComplete method will 
        // also call itself (if necessary) through Control.Invoke to 
        // ensure that it is executing on the primary (UI) thread.
        void TaskComplete()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(_taskComplete);
            }
            else
            {
                pbWorkStatus.Value = 0;
                miWork.Enabled = true;
                txtFeedback.Text = "Complete";
            }
        }

        // When the user selects the menu item to begin working
        // I will disable the work menu item to prevent concurrent
        // request and start the long running progress on a secondary
        // thread.
        private void miWork_Click(object sender, EventArgs e)
        {
            
            miWork.Enabled = false;
            ThreadPool.QueueUserWorkItem(_longRunningTask);            
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            this.Close();           
        }
    }
}