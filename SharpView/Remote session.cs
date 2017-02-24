using AxRDPCOMAPILib;
using MSTSCLib;
using RDPCOMAPILib;
using SharpViewCore.EventArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpView
{
    public partial class Remote_session: Form
    {
        Form1 form;
        public Remote_session( string invitation , string username , string password , Form1 form , SharpViewCore.RemoteDesktopInfo2 info2 = null , bool allowMouse = true )
        {
            InitializeComponent ();
            this.remoteDesktopControl1.HasServiceConsole=false;
            Connect (invitation , this.remoteDesktopControl1 , username , password);
            this.remoteDesktopControl1.FatelError+=RemoteDesktopControl1_FatelError;
            this.remoteDesktopControl1.ConnectingFailed+=RemoteDesktopControl1_ConnectingFailed;
            this.remoteDesktopControl1.SetDesktopInfo (info2);
            this.remoteDesktopControl1.SetDesktopInfo2 (new SharpViewCore.RemoteDesktopInfo4 (allowMouse , true , -1 , true , true , true , true));
            this.form=form;
        }


        private void RemoteDesktopControl1_ConnectingFailed( RemoteValueChanged<object> eventArgs )
        {
            pane.Close ();
            MessageBox.Show ("Failed to connect to the remote session!");
        }

        private void RemoteDesktopControl1_FatelError( RemoteValueChanged<object> eventArgs )
        {
            disconnect (this.remoteDesktopControl1);
            pane.Close ();
            MessageBox.Show ("You have been disconnected to your session ,due to an error!");
        }

        private void Remote_session_Load( object sender , EventArgs e )
        {
              
        }
        ConnectionPane pane = null;
        public void Connect( string invitation ,SharpViewCore.RemoteDesktop display , string userName , string password )
        {
            display.Connect (new SharpViewCore.RemoteDesktopInfo () { Invitation=invitation , UserName=userName , Password=password });

        }

        public void disconnect( SharpViewCore.IRemoteDesktop display )
        {
            display.Disconnect ();
        }


        private void axMsRdpClient9NotSafeForScripting1_OnConnecting( object sender , EventArgs e )
        {
            this.Text="Remote session - Connecting";
        }

        private void axMsRdpClient9NotSafeForScripting1_OnConnected( object sender , EventArgs e )
        {
        }

        private void newConnectionToolStripMenuItem_Click( object sender , EventArgs e )
        {
            Form1.ActiveForm.Show ();
        }

        private void closeConnectionToolStripMenuItem_Click( object sender , EventArgs e )
        {
            Close ();
        }

        private void axMsRdpClient9NotSafeForScripting1_OnFatalError( object sender , AxMSTSCLib.IMsTscAxEvents_OnFatalErrorEvent e )
        {
            MessageBox.Show (e.errorCode.ToString ());
        }

        private void axMsRdpClient9NotSafeForScripting1_OnDisconnected( object sender , AxMSTSCLib.IMsTscAxEvents_OnDisconnectedEvent e )
        {
            Close ();
        }

        private void Remote_session_FormClosed( object sender , FormClosedEventArgs e )
        {
            try
            {
                pane.Close ();
            }
            catch ( Exception ) { }
            remoteDesktopControl1.Disconnect ();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams paramse = base.CreateParams;
                paramse.ClassStyle|=0x00020000;
                return paramse;
            }
        }
        private void axRDPViewer1_OnAttendeeDisconnected( object sender , _IRDPSessionEvents_OnAttendeeDisconnectedEvent e )
        {
            IRDPSRAPIAttendeeDisconnectInfo info = (IRDPSRAPIAttendeeDisconnectInfo)e.pDisconnectInfo;
            MessageBox.Show ("The server has been disconnected\n\nCode: "+info.Code.ToString ()+"\nReason: "+info.Reason.ToString ());
        }

        private void button8_Click( object sender , EventArgs e )
        {
            Close ();
            ConnectionPane.ActiveForm.Close ();
        }
    }
}
