namespace WorkQuietly
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSoundPath = new System.Windows.Forms.TextBox();
            this.miFile = new System.Windows.Forms.MenuItem();
            this.miOpen = new System.Windows.Forms.MenuItem();
            this.miQuit = new System.Windows.Forms.MenuItem();
            this.miRun = new System.Windows.Forms.MenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.cboStartTime = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miFile);
            this.mainMenu1.MenuItems.Add(this.miRun);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 20);
            this.label1.Text = "Sound File";
            // 
            // txtSoundPath
            // 
            this.txtSoundPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoundPath.Location = new System.Drawing.Point(3, 23);
            this.txtSoundPath.Name = "txtSoundPath";
            this.txtSoundPath.Size = new System.Drawing.Size(234, 21);
            this.txtSoundPath.TabIndex = 1;
            // 
            // miFile
            // 
            this.miFile.MenuItems.Add(this.miOpen);
            this.miFile.MenuItems.Add(this.miQuit);
            this.miFile.Text = "&File";
            // 
            // miOpen
            // 
            this.miOpen.Text = "&Open";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miQuit
            // 
            this.miQuit.Text = "&Quit";
            this.miQuit.Click += new System.EventHandler(this.miQuit_Click);
            // 
            // miRun
            // 
            this.miRun.Text = "Run";
            this.miRun.Click += new System.EventHandler(this.miRun_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(3, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 20);
            this.label2.Text = "Start Time";
            // 
            // cboStartTime
            // 
            this.cboStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStartTime.Items.Add("10");
            this.cboStartTime.Items.Add("20");
            this.cboStartTime.Items.Add("30");
            this.cboStartTime.Items.Add("60");
            this.cboStartTime.Items.Add("120");
            this.cboStartTime.Items.Add("180");
            this.cboStartTime.Items.Add("300");
            this.cboStartTime.Location = new System.Drawing.Point(3, 70);
            this.cboStartTime.Name = "cboStartTime";
            this.cboStartTime.Size = new System.Drawing.Size(234, 22);
            this.cboStartTime.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.cboStartTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSoundPath);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSoundPath;
        private System.Windows.Forms.MenuItem miFile;
        private System.Windows.Forms.MenuItem miOpen;
        private System.Windows.Forms.MenuItem miQuit;
        private System.Windows.Forms.MenuItem miRun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboStartTime;
    }
}

