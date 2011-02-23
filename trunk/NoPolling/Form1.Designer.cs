namespace NoPolling
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.miQuit = new System.Windows.Forms.MenuItem();
            this.miWork = new System.Windows.Forms.MenuItem();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtFeedback = new System.Windows.Forms.Label();
            this.pbWorkStatus = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miQuit);
            this.mainMenu1.MenuItems.Add(this.miWork);
            // 
            // miQuit
            // 
            this.miQuit.Text = "&Quit";
            this.miQuit.Click += new System.EventHandler(this.miQuit_Click);
            // 
            // miWork
            // 
            this.miWork.Text = "&Work";
            this.miWork.Click += new System.EventHandler(this.miWork_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(240, 24);
            this.lblStatus.Text = "Status";
            // 
            // txtFeedback
            // 
            this.txtFeedback.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFeedback.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Regular);
            this.txtFeedback.Location = new System.Drawing.Point(3, 24);
            this.txtFeedback.Name = "txtFeedback";
            this.txtFeedback.Size = new System.Drawing.Size(234, 221);
            // 
            // pbWorkStatus
            // 
            this.pbWorkStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbWorkStatus.Location = new System.Drawing.Point(0, 248);
            this.pbWorkStatus.Name = "pbWorkStatus";
            this.pbWorkStatus.Size = new System.Drawing.Size(240, 20);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pbWorkStatus);
            this.Controls.Add(this.txtFeedback);
            this.Controls.Add(this.lblStatus);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miQuit;
        private System.Windows.Forms.MenuItem miWork;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label txtFeedback;
        private System.Windows.Forms.ProgressBar pbWorkStatus;
    }
}

