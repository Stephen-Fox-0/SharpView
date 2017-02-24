using SharpViewCore;
using SharpViewCore.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpView
{
    public partial class Tools: Form
    {
        public Tools()
        {
            InitializeComponent ();
        }

        private void panel22_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , 0 , panel22.Width-1 , panel22.Height-1));
        }

        private void panel6_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , -1 , panel6.Width-1 , panel6.Height));
        }

        private void panel5_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , -1 , panel5.Width-1 , panel5.Height));
        }

        private void label16_Click( object sender , EventArgs e )
        {

        }

        private void panel23_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , 0 , panel23.Width-1 , panel23.Height-1));
        }

        private void button1_Click( object sender , EventArgs e )
        {
            button1.Enabled=false;
            listBox1.Items.Clear ();
            foreach ( NetworkAddress address in NetworkLookup.GetAddresses (textBox22.Text) )
            {
                if ( address.Address.Contains (".") )
                {
                    listBox1.Items.Add (address.Address);
                }
            }
            button1.Enabled=true;
        }

        private void button5_Click( object sender , EventArgs e )
        {
            button5.Enabled=false;
            if ( NetworkState.SendPingRequest (textBox24.Text , int.Parse (textBox21.Text))==NetworkPingResult.Success )
            {
                MessageBox.Show ("Ping request was successful");
            }
            button5.Enabled=true;
        }

        private void listBox1_DrawItem( object sender , DrawItemEventArgs e )
        {
            try
            {
                var item = listBox1.Items[ e.Index ];
                if ( e.State.ToString ().Contains ("Selected") )
                {
                    e.Graphics.FillRectangle (new SolidBrush (Color.FromArgb (0 , 100 , 200)) , e.Bounds);
                    e.Graphics.DrawString (item.ToString () ,
                             new Font ("Consolas" , 8) ,
                             new SolidBrush (Color.White) ,
                             e.Bounds ,
                             new StringFormat () { Alignment=StringAlignment.Near , LineAlignment=StringAlignment.Center });
                }
                else
                {
                    e.Graphics.FillRectangle (new SolidBrush (Color.FromArgb (255 , 255 , 255)) , e.Bounds);
                    e.Graphics.DrawString (item.ToString () ,
                             new Font ("Consolas" , 8) ,
                             new SolidBrush (Color.Black) ,
                             e.Bounds ,
                             new StringFormat () { Alignment=StringAlignment.Near , LineAlignment=StringAlignment.Center });
                }
            }
            catch { }
        }

        private void panel8_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , 0 , panel8.Width-1 , panel8.Height-1));
        }

        private void label5_Click( object sender , EventArgs e )
        {

        }

        private void panel9_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , 0 , panel9.Width-1 , panel9.Height-1));
        }

        private void panel12_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , 0 , panel12.Width-1 , panel12.Height-1));
        }

        private void panel1_Paint( object sender , PaintEventArgs e )
        {

        }

        private void button8_Click( object sender , EventArgs e )
        {
            Close ();
        }

        private void button9_Click( object sender , EventArgs e )
        {
            this.WindowState=FormWindowState.Minimized;
        }
        [DllImport ("Gdi32.dll" , EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
(
  int nLeftRect , // x-coordinate of upper-left corner
  int nTopRect , // y-coordinate of upper-left corner
  int nRightRect , // x-coordinate of lower-right corner
  int nBottomRect , // y-coordinate of lower-right corner
  int nWidthEllipse , // height of ellipse
  int nHeightEllipse // width of ellipse
);

        [DllImport ("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea( IntPtr hWnd , ref MARGINS pMarInset );

        [DllImport ("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute( IntPtr hwnd , int attr , ref int attrValue , int attrSize );

        [DllImport ("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled( ref int pfEnabled );

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool CheckAeroEnabled()
        {
            if ( Environment.OSVersion.Version.Major>=6 )
            {
                int enabled = 0;
                DwmIsCompositionEnabled (ref enabled);
                return ( enabled==1 ) ? true : false;
            }
            return false;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled=CheckAeroEnabled ();

                CreateParams cp = base.CreateParams;
                cp.ClassStyle|=CS_DROPSHADOW;

                return cp;
            }
        }
        protected override void WndProc( ref Message m )
        {
            switch ( m.Msg )
            {
                case WM_NCPAINT:                        // box shadow
                    if ( m_aeroEnabled )
                    {
                        var v = 2;
                        DwmSetWindowAttribute (this.Handle , 2 , ref v , 4);
                        MARGINS margins = new MARGINS ()
                        {
                            bottomHeight=1 ,
                            leftWidth=1 ,
                            rightWidth=1 ,
                            topHeight=1
                        };
                        DwmExtendFrameIntoClientArea (this.Handle , ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc (ref m);

            if ( m.Msg==WM_NCHITTEST&&(int)m.Result==HTCLIENT )     // drag the form
                m.Result=(IntPtr)HTCAPTION;

        }


        private void themeWindow1_Click( object sender , EventArgs e )
        {

        }

        private void button2_Click( object sender , EventArgs e )
        {
            if ( File.Exists (Environment.CurrentDirectory+"\\SharpViewService.exe") )
            {
                if ( comboBox1.SelectedItem.ToString ()==@"Remote Socket Service" )
                {
                    System.Diagnostics.Process.Start ("SharpViewService.exe" , "-remote -rsk "+textBox23.Text);
                }
                else if ( comboBox1.SelectedItem.ToString ()==@"Remote Desktop Service" )
                {

                    System.Diagnostics.Process.Start ("SharpViewService.exe" , "-remote "+textBox23.Text);
                }
                else if ( comboBox1.SelectedItem.ToString ()==@"Service Window" )
                {

                    System.Diagnostics.Process.Start ("SharpViewService.exe" , "-window "+textBox23.Text);
                }
                else { }
            }
        }

        private void panel13_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , 0 , panel13.Width-1 , panel13.Height-1));
        }

        private void panel14_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , 0 , panel14.Width-1 , panel14.Height-1));
        }

        private void panel17_Paint( object sender , PaintEventArgs e )
        {
            //e.Graphics.DrawRectangle (new Pen (
            //    Color.FromArgb (0 , 133 , 244)) ,
            //    new Rectangle (0 , -1 , panel17.Width-1 , panel17.Height));
        }

        private void panel18_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , 0 , panel18.Width-1 , panel18.Height-1));
        }

        private void panel19_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , 0 , panel18.Width-1 , panel18.Height-1));
        }

        private void button3_Click( object sender , EventArgs e )
        {
            System.Diagnostics.Process.Start ("SharpViewService.exe", "-window");
        }


        public List<string> addresses = new List<string> ();
        public List<string> enableds = new List<string> ();
        private void listBox2_DrawItem( object sender , DrawItemEventArgs e )
        {
            try
            {
                var item = listBox2.Items[ e.Index ];
                if ( e.State.ToString ().Contains ("Selected") )
                {
                    e.Graphics.FillRectangle (new SolidBrush (Color.FromArgb (0 , 100 , 200)) , e.Bounds);
                    e.Graphics.DrawString (item.ToString () ,
                             new Font ("Consolas" , 8) ,
                             new SolidBrush (Color.White) ,
                             new Point (e.Bounds.Location.X , e.Bounds.Location.Y+2));
                    e.Graphics.DrawString (addresses[ e.Index ] ,
                             new Font ("Consolas" , 7) ,
                             new SolidBrush (Color.White) ,
                             new Point (e.Bounds.Location.X , e.Bounds.Location.Y+12));
                    e.Graphics.DrawString (enableds[ e.Index ] ,
                        new Font ("Consolas" , 7) ,
                        new SolidBrush (Color.White) ,
                        new Point (e.Bounds.Location.X , e.Bounds.Location.Y+21));
                }
                else
                {
                    e.Graphics.FillRectangle (new SolidBrush (Color.FromArgb (255 , 255 , 255)) , e.Bounds);
                    e.Graphics.DrawString (item.ToString () ,
                              new Font ("Consolas" , 8) ,
                              new SolidBrush (Color.Black) ,
                              new Point (e.Bounds.Location.X , e.Bounds.Location.Y+2));
                    e.Graphics.DrawString (addresses[ e.Index ] ,
                        new Font ("Consolas" , 7) ,
                        new SolidBrush (Color.Gray) ,
                        new Point (e.Bounds.Location.X , e.Bounds.Location.Y+12));
                    e.Graphics.DrawString (enableds[ e.Index ] ,
                     new Font ("Consolas" , 7) ,
                     new SolidBrush (Color.Gray) ,
                     new Point (e.Bounds.Location.X , e.Bounds.Location.Y+21));
                }
            }
            catch { }
        }

        private void panel21_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.DrawRectangle (new Pen (Color.FromArgb (222 , 222 , 222)) , new Rectangle (0 , 0 , panel21.Width-1 , panel21.Height-1));
        }

        private void button3_Click_1( object sender , EventArgs e )
        {
            listBox2.Items.Clear ();
            var devices = NetworkState.Get ();

            foreach ( INetworkDevice device in devices.Devices )
            {
                if ( device.IsConnected )
                {
                    listBox2.Items.Add (device.Name);
                    addresses.Add (device.IPAddresss);
                    if ( device.IsConnected )
                        enableds.Add ("Enabled: Yes");
                    else
                        enableds.Add ("Enabled: No");
                }
            }
        }

        private void listBox2_SelectedIndexChanged( object sender , EventArgs e )
        {

        }
    }
}
