namespace SharpView
{
    partial class ConnectionPane
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing&&( components!=null ) )
            {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionPane));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.themePane1 = new SharpView.ThemePane();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.themePane1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // themePane1
            // 
            this.themePane1.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.themePane1.Colors = new SharpView.Bloom[0];
            this.themePane1.Controls.Add(this.button3);
            this.themePane1.Controls.Add(this.label3);
            this.themePane1.Controls.Add(this.label10);
            this.themePane1.Controls.Add(this.pictureBox1);
            this.themePane1.Controls.Add(this.label1);
            this.themePane1.Controls.Add(this.panel2);
            this.themePane1.Customization = "";
            this.themePane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.themePane1.Font = new System.Drawing.Font("Verdana", 8F);
            this.themePane1.Image = global::SharpView.Properties.Resources._1487687785_Remote_Desktop;
            this.themePane1.Location = new System.Drawing.Point(0, 0);
            this.themePane1.Movable = true;
            this.themePane1.Name = "themePane1";
            this.themePane1.NoRounding = true;
            this.themePane1.Sizable = false;
            this.themePane1.Size = new System.Drawing.Size(284, 109);
            this.themePane1.SmartBounds = true;
            this.themePane1.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
            this.themePane1.TabIndex = 0;
            this.themePane1.Text = "themePane1";
            this.themePane1.TransparencyKey = System.Drawing.Color.Empty;
            this.themePane1.Transparent = false;
            this.themePane1.Click += new System.EventHandler(this.themePane1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(78)))), ((int)(((byte)(178)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(116)))), ((int)(((byte)(175)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(170)))), ((int)(((byte)(239)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Webdings", 8F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(255, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(26, 25);
            this.button3.TabIndex = 22;
            this.button3.Text = "r";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label3.Location = new System.Drawing.Point(82, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "{PeerIP}";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.label10.Font = new System.Drawing.Font("Open Sans", 8F, System.Drawing.FontStyle.Italic);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(57, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "A session is in progress.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.pictureBox1.Image = global::SharpView.Properties.Resources.Untitled;
            this.pictureBox1.Location = new System.Drawing.Point(7, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(43, 39);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.label1.Font = new System.Drawing.Font("Open Sans", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(54, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "SharpView";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Session id";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::SharpView.Properties.Resources.Untitled2;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(8, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(77, 30);
            this.panel2.TabIndex = 23;
            // 
            // ConnectionPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 109);
            this.Controls.Add(this.themePane1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectionPane";
            this.Text = "ConnectionPane";
            this.themePane1.ResumeLayout(false);
            this.themePane1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ThemePane themePane1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
    }
}