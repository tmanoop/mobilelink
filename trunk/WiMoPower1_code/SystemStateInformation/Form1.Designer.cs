namespace SystemStateInformation
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
            this.lvBatteryState = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.miQuit = new System.Windows.Forms.MenuItem();
            this.chPropertyName = new System.Windows.Forms.ColumnHeader();
            this.chValue = new System.Windows.Forms.ColumnHeader();
            this.miRefresh = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miQuit);
            this.mainMenu1.MenuItems.Add(this.miRefresh);
            // 
            // lvBatteryState
            // 
            this.lvBatteryState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvBatteryState.Columns.Add(this.chPropertyName);
            this.lvBatteryState.Columns.Add(this.chValue);
            this.lvBatteryState.Location = new System.Drawing.Point(3, 23);
            this.lvBatteryState.Name = "lvBatteryState";
            this.lvBatteryState.Size = new System.Drawing.Size(234, 242);
            this.lvBatteryState.TabIndex = 0;
            this.lvBatteryState.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 20);
            this.label1.Text = "Battery Properties";
            // 
            // miQuit
            // 
            this.miQuit.Text = "&Quit";
            this.miQuit.Click += new System.EventHandler(this.miQuit_Click);
            // 
            // chPropertyName
            // 
            this.chPropertyName.Text = "Property Name";
            this.chPropertyName.Width = 128;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 97;
            // 
            // miRefresh
            // 
            this.miRefresh.Text = "Refresh";
            this.miRefresh.Click += new System.EventHandler(this.miRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvBatteryState);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvBatteryState;
        private System.Windows.Forms.MenuItem miQuit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader chPropertyName;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.MenuItem miRefresh;
    }
}

