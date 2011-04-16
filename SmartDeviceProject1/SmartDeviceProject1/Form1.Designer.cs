namespace SmartDeviceProject1
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.roundTripTime = new System.Windows.Forms.TextBox();
            this.RTT = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loc = new System.Windows.Forms.ComboBox();
            this.id = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem5);
            this.menuItem1.MenuItems.Add(this.menuItem6);
            this.menuItem1.MenuItems.Add(this.menuItem7);
            this.menuItem1.MenuItems.Add(this.menuItem8);
            this.menuItem1.MenuItems.Add(this.menuItem11);
            this.menuItem1.Text = "LINK Menu";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.MenuItems.Add(this.menuItem10);
            this.menuItem3.MenuItems.Add(this.menuItem9);
            this.menuItem3.Text = "Coupon";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click_1);
            // 
            // menuItem10
            // 
            this.menuItem10.Text = "Single test";
            this.menuItem10.Click += new System.EventHandler(this.menuItem3_Click_1);
            // 
            // menuItem9
            // 
            this.menuItem9.Text = "Multiple tests";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "Hash and Sign";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "WiFi RTT";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click_2);
            // 
            // menuItem5
            // 
            this.menuItem5.Checked = true;
            this.menuItem5.Text = "BT Disc";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Text = "BT RTT";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Text = "Sign";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Text = "Verify";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 102);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(228, 142);
            this.textBox1.TabIndex = 0;
            // 
            // roundTripTime
            // 
            this.roundTripTime.Location = new System.Drawing.Point(75, 250);
            this.roundTripTime.Name = "roundTripTime";
            this.roundTripTime.Size = new System.Drawing.Size(109, 30);
            this.roundTripTime.TabIndex = 6;
            // 
            // RTT
            // 
            this.RTT.Location = new System.Drawing.Point(3, 250);
            this.RTT.Name = "RTT";
            this.RTT.Size = new System.Drawing.Size(69, 30);
            this.RTT.Text = "RTT:";
            this.RTT.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 30);
            this.label2.Text = "Loc:";
            // 
            // loc
            // 
            this.loc.Items.Add("12");
            this.loc.Items.Add("13");
            this.loc.Items.Add("14");
            this.loc.Items.Add("221");
            this.loc.Items.Add("222");
            this.loc.Items.Add("223");
            this.loc.Location = new System.Drawing.Point(36, 69);
            this.loc.Name = "loc";
            this.loc.Size = new System.Drawing.Size(52, 30);
            this.loc.TabIndex = 9;
            // 
            // id
            // 
            this.id.Items.Add("0");
            this.id.Items.Add("1");
            this.id.Items.Add("2");
            this.id.Items.Add("3");
            this.id.Items.Add("4");
            this.id.Items.Add("5");
            this.id.Items.Add("6");
            this.id.Items.Add("7");
            this.id.Items.Add("8");
            this.id.Items.Add("9");
            this.id.Items.Add("10");
            this.id.Items.Add("11");
            this.id.Items.Add("12");
            this.id.Items.Add("13");
            this.id.Items.Add("14");
            this.id.Location = new System.Drawing.Point(26, 39);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(35, 30);
            this.id.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 30);
            this.label3.Text = "ID:";
            // 
            // menuItem11
            // 
            this.menuItem11.Text = "Test BT DISC Async";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(131F, 131F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(230, 256);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RTT);
            this.Controls.Add(this.roundTripTime);
            this.Controls.Add(this.textBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox roundTripTime;
        private System.Windows.Forms.Label RTT;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox loc;
        private System.Windows.Forms.ComboBox id;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem11;
    }
}

