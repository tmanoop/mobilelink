namespace PreventSleep
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
            this.txtBatteryIdleTimeOut = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtACTimeout = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtScreenOff = new System.Windows.Forms.TextBox();
            this.miQuit = new System.Windows.Forms.MenuItem();
            this.resetTimer = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miQuit);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 20);
            this.label1.Text = "Battery Idle Time out";
            // 
            // txtBatteryIdleTimeOut
            // 
            this.txtBatteryIdleTimeOut.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBatteryIdleTimeOut.Location = new System.Drawing.Point(0, 20);
            this.txtBatteryIdleTimeOut.Name = "txtBatteryIdleTimeOut";
            this.txtBatteryIdleTimeOut.Size = new System.Drawing.Size(240, 21);
            this.txtBatteryIdleTimeOut.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 20);
            this.label2.Text = "AC Time out";
            // 
            // txtACTimeout
            // 
            this.txtACTimeout.Location = new System.Drawing.Point(3, 67);
            this.txtACTimeout.Name = "txtACTimeout";
            this.txtACTimeout.Size = new System.Drawing.Size(237, 21);
            this.txtACTimeout.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(234, 20);
            this.label3.Text = "Screen Power Off";
            // 
            // txtScreenOff
            // 
            this.txtScreenOff.Location = new System.Drawing.Point(3, 114);
            this.txtScreenOff.Name = "txtScreenOff";
            this.txtScreenOff.Size = new System.Drawing.Size(234, 21);
            this.txtScreenOff.TabIndex = 5;
            // 
            // miQuit
            // 
            this.miQuit.Text = "Quit";
            this.miQuit.Click += new System.EventHandler(this.miQuit_Click);
            // 
            // resetTimer
            // 
            this.resetTimer.Tick += new System.EventHandler(this.resetTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtScreenOff);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtACTimeout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBatteryIdleTimeOut);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBatteryIdleTimeOut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtACTimeout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtScreenOff;
        private System.Windows.Forms.MenuItem miQuit;
        private System.Windows.Forms.Timer resetTimer;
    }
}

