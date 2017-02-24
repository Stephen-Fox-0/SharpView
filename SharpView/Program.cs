using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpView
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles ();
            Application.SetCompatibleTextRenderingDefault (false);
            if ( args.Length>0 )
            {
                if ( args[ 0 ]=="-r" )
                {
                    Application.Run (new Remote_session(
                        args[1], args[2], args[3], null));
                }
                if ( args[ 0 ]=="-t" )
                {
                    Application.Run (new Tools());
                }
                if ( args[ 0 ]=="-rs" )
                {
                    Form1 form1 = new Form1 ();
                    var f = new Remote_session (args[ 1 ] , Environment.UserName , args[ 2 ] , form1 , new SharpViewCore.RemoteDesktopInfo2 ()
                    { ColorDepth=32 , Fullscreen=true , FullscreenText="" , Height=720 , Width=1025,
                    } , true);
                    f.ShowDialog ();
                    f.FormClosed+=F_FormClosed;
                    

                }
            }
            else
            {
                Application.Run (new Form1 ());
            }
        }

        private static void F_FormClosed( object sender , FormClosedEventArgs e )
        {
            return;
        }
    }
}
