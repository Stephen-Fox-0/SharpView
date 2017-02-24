using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpView
{
    public partial class PersonalPasswordEntry: Form
    {
        public PersonalPasswordEntry()
        {
            InitializeComponent ();
        }

        public bool PasswordEntered { get; set; }
        private void button1_Click( object sender , EventArgs e )
        {
            if(textBox1.Text == Properties.Settings.Default.personalpassword)
            {
                DialogResult=DialogResult.OK;
                PasswordEntered=true;
                this.Close ();
            }
            else
            {
                PasswordEntered=false;
                MessageBox.Show ("The password that was entered, was not the correct password given\nif you have forgotten your password, press Forgotten Password and it show you the password hint given." , "Incorrect Password" , MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click( object sender , EventArgs e )
        {
            DialogResult=DialogResult.Cancel;
            Close ();
        }

        private void button4_Click( object sender , EventArgs e )
        {
            MessageBox.Show ("There hint given:\n\n" + Properties.Settings.Default.personalpasswordHint , "Password hint" , MessageBoxButtons.OK , MessageBoxIcon.Information);
        }

        private void panel3_Paint( object sender , PaintEventArgs e )
        {
            e.Graphics.FillRectangle (new SolidBrush (Color.FromArgb (233 , 233 , 233)) , new Rectangle (
panel3.Width-1 , 0 , 1 , panel3.Height-1));
        }

        private void PersonalPasswordEntry_Load( object sender , EventArgs e )
        {

        }

        private void button3_Click( object sender , EventArgs e )
        {
            Close ();
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

    }
}
