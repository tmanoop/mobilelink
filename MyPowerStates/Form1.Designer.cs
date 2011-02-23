namespace MyPowerStates
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
            this.cboPowerState = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstPowerStateInfo = new System.Windows.Forms.ListView();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chValue = new System.Windows.Forms.ColumnHeader();
            this.miQuit = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miQuit);
            // 
            // cboPowerState
            // 
            this.cboPowerState.Location = new System.Drawing.Point(3, 23);
            this.cboPowerState.Name = "cboPowerState";
            this.cboPowerState.Size = new System.Drawing.Size(234, 22);
            this.cboPowerState.TabIndex = 0;
            this.cboPowerState.SelectedIndexChanged += new System.EventHandler(this.cboPowerState_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 20);
            this.label1.Text = "Device Power States";
            // 
            // lstPowerStateInfo
            // 
            this.lstPowerStateInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPowerStateInfo.Columns.Add(this.chName);
            this.lstPowerStateInfo.Columns.Add(this.chValue);
            this.lstPowerStateInfo.Location = new System.Drawing.Point(3, 51);
            this.lstPowerStateInfo.Name = "lstPowerStateInfo";
            this.lstPowerStateInfo.Size = new System.Drawing.Size(234, 200);
            this.lstPowerStateInfo.TabIndex = 2;
            this.lstPowerStateInfo.View = System.Windows.Forms.View.Details;
            this.lstPowerStateInfo.SelectedIndexChanged += new System.EventHandler(this.lstPowerStateInfo_SelectedIndexChanged);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 120;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 99;
            // 
            // miQuit
            // 
            this.miQuit.Text = "Quit";
            this.miQuit.Click += new System.EventHandler(this.miQuit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lstPowerStateInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboPowerState);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboPowerState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstPowerStateInfo;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.MenuItem miQuit;
    }
}

