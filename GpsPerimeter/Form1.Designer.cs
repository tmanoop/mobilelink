namespace GpsPerimeter
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDistance = new System.Windows.Forms.ComboBox();
            this.lblCurrentLocation = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTravelDirection = new System.Windows.Forms.Label();
            this.compassControl = new GpsPerimeter.Compas();
            this.lblAlarmMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miQuit);
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // miQuit
            // 
            this.miQuit.Text = "&Quit";
            this.miQuit.Click += new System.EventHandler(this.miQuit_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "&Start";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 20);
            this.label1.Text = "Notification Distance";
            // 
            // cboDistance
            // 
            this.cboDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDistance.Items.Add("50 meters");
            this.cboDistance.Items.Add("100 meters");
            this.cboDistance.Items.Add("250 meters");
            this.cboDistance.Items.Add("1 kilometer");
            this.cboDistance.Items.Add("2 kilometers");
            this.cboDistance.Items.Add("5 kilometers");
            this.cboDistance.Items.Add("");
            this.cboDistance.Location = new System.Drawing.Point(3, 20);
            this.cboDistance.Name = "cboDistance";
            this.cboDistance.Size = new System.Drawing.Size(234, 22);
            this.cboDistance.TabIndex = 1;
            this.cboDistance.SelectedIndexChanged += new System.EventHandler(this.cboDistance_SelectedIndexChanged);
            // 
            // lblCurrentLocation
            // 
            this.lblCurrentLocation.Location = new System.Drawing.Point(109, 42);
            this.lblCurrentLocation.Name = "lblCurrentLocation";
            this.lblCurrentLocation.Size = new System.Drawing.Size(128, 20);
            this.lblCurrentLocation.Text = "?";
            // 
            // lblDistance
            // 
            this.lblDistance.Location = new System.Drawing.Point(109, 62);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(128, 20);
            this.lblDistance.Text = "?";
            // 
            // lblLocation
            // 
            this.lblLocation.Location = new System.Drawing.Point(4, 42);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(99, 20);
            this.lblLocation.Text = "Location:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.Text = "Distance";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.Text = "Travel Direction";
            // 
            // lblTravelDirection
            // 
            this.lblTravelDirection.Location = new System.Drawing.Point(109, 82);
            this.lblTravelDirection.Name = "lblTravelDirection";
            this.lblTravelDirection.Size = new System.Drawing.Size(128, 20);
            this.lblTravelDirection.Text = "?";
            // 
            // compassControl
            // 
            this.compassControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.compassControl.BackgroundColor = System.Drawing.Color.White;
            this.compassControl.Location = new System.Drawing.Point(3, 125);
            this.compassControl.Name = "compassControl";
            this.compassControl.PointerColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.compassControl.RingColor = System.Drawing.Color.Red;
            this.compassControl.Size = new System.Drawing.Size(139, 140);
            this.compassControl.TabIndex = 2;
            this.compassControl.TailColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.compassControl.Text = "compas1";
            // 
            // lblAlarmMessage
            // 
            this.lblAlarmMessage.Location = new System.Drawing.Point(4, 102);
            this.lblAlarmMessage.Name = "lblAlarmMessage";
            this.lblAlarmMessage.Size = new System.Drawing.Size(233, 20);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblAlarmMessage);
            this.Controls.Add(this.lblTravelDirection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.lblCurrentLocation);
            this.Controls.Add(this.compassControl);
            this.Controls.Add(this.cboDistance);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Gps Perimeter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDistance;
        private System.Windows.Forms.MenuItem miQuit;
        private System.Windows.Forms.MenuItem menuItem1;
        private Compas compassControl;
        private System.Windows.Forms.Label lblCurrentLocation;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTravelDirection;
        private System.Windows.Forms.Label lblAlarmMessage;
    }
}

