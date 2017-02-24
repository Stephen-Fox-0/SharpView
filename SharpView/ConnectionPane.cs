using SharpViewCore;
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
    public partial class ConnectionPane: Form
    {
        IRemoteDesktop Session;
        System.Diagnostics.Stopwatch stopWatch;
        public ConnectionPane(IRemoteDesktop desktopSession, string username, string id)
        {
            InitializeComponent ();
            this.Session=desktopSession;
            ID=id;
            label3.Text=""+username;
         
           // stopWatch = new System.Diagnostics.Stopwatch ();
          //  stopWatch.Start ();
        }

        public ConnectionPane()
        {
            InitializeComponent ();
        }
        private void label4_Click( object sender , EventArgs e )
        {
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams paramse =base.CreateParams;
                paramse.ClassStyle|=0x00020000;
                return paramse;
            }
        }

        public string ID { get; set; }


        private void timer1_Tick( object sender , EventArgs e )
        {
            //label2.Text=stopWatch.Elapsed.ToString ().Replace ("." , "");
        }

        private void themePane1_Click( object sender , EventArgs e )
        {

        }

        private void button3_Click( object sender , EventArgs e )
        {
            Remote_session.ActiveForm.Close ();
            this.Close ();
        }
    }

    class ThemePane: ThemeContainer154
    {
        protected override void ColorHook()
        {
        }

        protected override void PaintHook()
        {
            G.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            G.TextContrast=1;
            G.TextRenderingHint=System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            G.Clear (Color.White);

            G.FillRectangle (new SolidBrush (Color.FromArgb (0 , 100 , 200)) , new Rectangle (0 , 0 , this.Width , 70));
            G.FillRectangle (new SolidBrush (ColorTranslator.FromHtml ("#0052A5")) , new Rectangle (
          0 , this.Height-1 , this.Width-1 , 1));
            DrawBorders (new Pen (Color.FromArgb (156 , 156 , 156)));
        }
    }
    class ThemeWindow: ThemeContainer154
    {
        protected override void ColorHook()
        {
        }

        protected override void PaintHook()
        {
            G.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            G.TextContrast=1;
            G.TextRenderingHint=System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            G.Clear (Color.White);

            G.FillRectangle (new SolidBrush (Color.FromArgb (0 , 100 , 200)) , new Rectangle (0 , 0 , this.Width , 30));
            G.FillRectangle (new SolidBrush (ColorTranslator.FromHtml ("#0052A5")) , new Rectangle (
          0 , this.Height-1 , this.Width-1 , 1));
            DrawBorders (new Pen (Color.FromArgb (0 , 100 , 200)));
            G.DrawString (FindForm().Text,
                       new Font ("Open Sans" , 10) ,
                       new SolidBrush (Color.White) ,
                       new Rectangle (5 ,0 , this.Width , 30) ,
                       new StringFormat () { Alignment=StringAlignment.Near , LineAlignment=StringAlignment.Center });
        }

 
    }
}
