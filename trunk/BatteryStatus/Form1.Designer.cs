namespace BatteryStatus
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu;

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.miQuit = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mi1sec = new System.Windows.Forms.MenuItem();
            this.mi5sec = new System.Windows.Forms.MenuItem();
            this.mi10sec = new System.Windows.Forms.MenuItem();
            this.miPause = new System.Windows.Forms.MenuItem();
            this.miRefresh = new System.Windows.Forms.MenuItem();
            this.lstBatteryStats = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colValue = new System.Windows.Forms.ColumnHeader();
            this.updateTimer = new System.Windows.Forms.Timer();
            this.pbACLine = new System.Windows.Forms.PictureBox();
            this.pbBatteryFlag = new System.Windows.Forms.PictureBox();
            this.txtBatteryPercentage = new System.Windows.Forms.TextBox();
            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.txtBatteryVoltage = new System.Windows.Forms.TextBox();
            this.txtBatteryCurrent = new System.Windows.Forms.TextBox();
            this.txtBatteryChem = new System.Windows.Forms.Label();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.miQuit);
            this.mainMenu.MenuItems.Add(this.menuItem1);
            this.mainMenu.MenuItems.Add(this.menuItem2);
            // 
            // miQuit
            // 
            this.miQuit.Text = "&Quit";
            this.miQuit.Click += new System.EventHandler(this.miQuit_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.mi1sec);
            this.menuItem1.MenuItems.Add(this.mi5sec);
            this.menuItem1.MenuItems.Add(this.mi10sec);
            this.menuItem1.MenuItems.Add(this.miPause);
            this.menuItem1.MenuItems.Add(this.miRefresh);
            this.menuItem1.Text = "&Update Speed";
            // 
            // mi1sec
            // 
            this.mi1sec.Text = "1 second";
            this.mi1sec.Click += new System.EventHandler(this.mi1sec_Click);
            // 
            // mi5sec
            // 
            this.mi5sec.Text = "5 seconds";
            this.mi5sec.Click += new System.EventHandler(this.mi5sec_Click);
            // 
            // mi10sec
            // 
            this.mi10sec.Text = "10 Seconds";
            this.mi10sec.Click += new System.EventHandler(this.mi10sec_Click);
            // 
            // miPause
            // 
            this.miPause.Text = "&Pause";
            this.miPause.Click += new System.EventHandler(this.miPause_Click);
            // 
            // miRefresh
            // 
            this.miRefresh.Text = "&Refresh";
            this.miRefresh.Click += new System.EventHandler(this.miRefresh_Click);
            // 
            // lstBatteryStats
            // 
            this.lstBatteryStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBatteryStats.Columns.Add(this.colName);
            this.lstBatteryStats.Columns.Add(this.colValue);
            this.lstBatteryStats.Location = new System.Drawing.Point(3, 79);
            this.lstBatteryStats.Name = "lstBatteryStats";
            this.lstBatteryStats.Size = new System.Drawing.Size(234, 186);
            this.lstBatteryStats.TabIndex = 0;
            this.lstBatteryStats.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 117;
            // 
            // colValue
            // 
            this.colValue.Text = "Value";
            this.colValue.Width = 110;
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 5000;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // pbACLine
            // 
            this.pbACLine.Location = new System.Drawing.Point(3, 3);
            this.pbACLine.Name = "pbACLine";
            this.pbACLine.Size = new System.Drawing.Size(32, 32);
            // 
            // pbBatteryFlag
            // 
            this.pbBatteryFlag.Location = new System.Drawing.Point(3, 41);
            this.pbBatteryFlag.Name = "pbBatteryFlag";
            this.pbBatteryFlag.Size = new System.Drawing.Size(32, 32);
            // 
            // txtBatteryPercentage
            // 
            this.txtBatteryPercentage.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.txtBatteryPercentage.Location = new System.Drawing.Point(68, 2);
            this.txtBatteryPercentage.Name = "txtBatteryPercentage";
            this.txtBatteryPercentage.ReadOnly = true;
            this.txtBatteryPercentage.Size = new System.Drawing.Size(73, 31);
            this.txtBatteryPercentage.TabIndex = 2;
            // 
            // txtTemperature
            // 
            this.txtTemperature.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.txtTemperature.Location = new System.Drawing.Point(68, 41);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.ReadOnly = true;
            this.txtTemperature.Size = new System.Drawing.Size(73, 31);
            this.txtTemperature.TabIndex = 3;
            // 
            // txtBatteryVoltage
            // 
            this.txtBatteryVoltage.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.txtBatteryVoltage.Location = new System.Drawing.Point(147, 2);
            this.txtBatteryVoltage.Name = "txtBatteryVoltage";
            this.txtBatteryVoltage.ReadOnly = true;
            this.txtBatteryVoltage.Size = new System.Drawing.Size(90, 31);
            this.txtBatteryVoltage.TabIndex = 4;
            // 
            // txtBatteryCurrent
            // 
            this.txtBatteryCurrent.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.txtBatteryCurrent.Location = new System.Drawing.Point(147, 41);
            this.txtBatteryCurrent.Name = "txtBatteryCurrent";
            this.txtBatteryCurrent.ReadOnly = true;
            this.txtBatteryCurrent.Size = new System.Drawing.Size(90, 31);
            this.txtBatteryCurrent.TabIndex = 5;
            // 
            // txtBatteryChem
            // 
            this.txtBatteryChem.Location = new System.Drawing.Point(41, 46);
            this.txtBatteryChem.Name = "txtBatteryChem";
            this.txtBatteryChem.Size = new System.Drawing.Size(21, 26);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Save to file";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lstBatteryStats);
            this.Controls.Add(this.txtBatteryChem);
            this.Controls.Add(this.txtBatteryCurrent);
            this.Controls.Add(this.txtBatteryVoltage);
            this.Controls.Add(this.txtTemperature);
            this.Controls.Add(this.txtBatteryPercentage);
            this.Controls.Add(this.pbBatteryFlag);
            this.Controls.Add(this.pbACLine);
            this.Menu = this.mainMenu;
            this.Name = "Form1";
            this.Text = "Battery Status";
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstBatteryStats;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.MenuItem miQuit;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mi1sec;
        private System.Windows.Forms.MenuItem mi5sec;
        private System.Windows.Forms.MenuItem mi10sec;
        private System.Windows.Forms.MenuItem miPause;
        private System.Windows.Forms.MenuItem miRefresh;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.PictureBox pbACLine;
        private System.Windows.Forms.PictureBox pbBatteryFlag;
        private System.Windows.Forms.TextBox txtBatteryPercentage;
        private System.Windows.Forms.TextBox txtTemperature;
        private System.Windows.Forms.TextBox txtBatteryVoltage;
        private System.Windows.Forms.TextBox txtBatteryCurrent;
        private System.Windows.Forms.Label txtBatteryChem;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}

