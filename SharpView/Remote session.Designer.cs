namespace SharpView
{
    partial class Remote_session
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Remote_session));
            this.axRDPViewer1 = new AxRDPCOMAPILib.AxRDPViewer();
            this.remoteDesktopControl1 = new SharpViewCore.RemoteDesktop();
            ((System.ComponentModel.ISupportInitialize)(this.axRDPViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // axRDPViewer1
            // 
            this.axRDPViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axRDPViewer1.Enabled = true;
            this.axRDPViewer1.Location = new System.Drawing.Point(0, 0);
            this.axRDPViewer1.Name = "axRDPViewer1";
            this.axRDPViewer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axRDPViewer1.OcxState")));
            this.axRDPViewer1.Size = new System.Drawing.Size(1294, 818);
            this.axRDPViewer1.TabIndex = 0;
            this.axRDPViewer1.OnAttendeeDisconnected += new AxRDPCOMAPILib._IRDPSessionEvents_OnAttendeeDisconnectedEventHandler(this.axRDPViewer1_OnAttendeeDisconnected);
            // 
            // remoteDesktopControl1
            // 
            this.remoteDesktopControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteDesktopControl1.Location = new System.Drawing.Point(0, 0);
            this.remoteDesktopControl1.Name = "remoteDesktopControl1";
            this.remoteDesktopControl1.Size = new System.Drawing.Size(1294, 818);
            this.remoteDesktopControl1.SmartSizing = false;
            this.remoteDesktopControl1.TabIndex = 1;
            this.remoteDesktopControl1.Text = "remoteDesktopControl1";
            // 
            // Remote_session
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 818);
            this.Controls.Add(this.remoteDesktopControl1);
            this.Controls.Add(this.axRDPViewer1);
            this.MinimizeBox = false;
            this.Name = "Remote_session";
            this.ShowIcon = false;
            this.Text = "Remote Session";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Remote_session_FormClosed);
            this.Load += new System.EventHandler(this.Remote_session_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axRDPViewer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxRDPCOMAPILib.AxRDPViewer axRDPViewer1;
        private SharpViewCore.RemoteDesktop remoteDesktopControl1;
    }
}