using RDPCOMAPILib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SharpViewCore.EventArgs;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SharpViewCore
{
    [Guid ("02131FBB-0B0F-4ED9-9A19-6DACC1091586")]
    public class RemoteServer : Component, IRemoteServer
    {
        /// <summary>
        /// Occurs when incoming connection.
        /// </summary>
        public event AxEventHandler<RemoteServerIncomingConnectionEventArgs> IncomingConnection;
        /// <summary>
        /// Occurs when connected.
        /// </summary>
        public event AxEventHandler<RemoteServerConnectedEventArgs> Connected;
        /// <summary>
        /// Occurs when disconnected.
        /// </summary>
        public event AxEventHandler<RemoteServerDisconnectEventArgs> Disconnected;

        /// <summary>
        /// Gets or sets the applications.
        /// You only need to supply the full path of the application.
        /// </summary>
        public List<String> Applications
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indercating weather <see cref="RemoteServer"/> can start all the applications on connect./>
        /// </summary>
        public bool StartApplicationsOnConnect
        {
            get;
            set;
        }

        private RDPSession currentSession = null;
        private void createSession()
        {
            currentSession=new RDPSession ();
        }

        private void Connect( RDPSession session )
        {
            session.OnAttendeeConnected+=Incoming;
            session.Open ();

            AxEventHandler<RemoteServerConnectedEventArgs> handler = this.Connected;

            if ( handler!=null )
                handler (new RemoteServerConnectedEventArgs (session));
        }

        private void Disconnect( RDPSession session )
        {
            session.Close ();

            AxEventHandler<RemoteServerDisconnectEventArgs> handler = this.Disconnected;

            if ( handler!=null )
                handler (new  RemoteServerDisconnectEventArgs (session));
        }

        private RemoteDesktopInvitation getConnectionString( RDPSession session , String authString ,
            string group , string password , int clientLimit )
        {
            IRDPSRAPIInvitation invitation =
                session.Invitations.CreateInvitation
                (authString , group , password , clientLimit);

            return new SharpViewCore.RemoteDesktopInvitation (invitation.ConnectionString , invitation.GroupName , invitation.Password) { AttendeeLimit=invitation.AttendeeLimit , Revoked=invitation.Revoked };
        }

        private void Incoming( object Guest )
        {
            IRDPSRAPIAttendee MyGuest = (IRDPSRAPIAttendee)Guest;
            MyGuest.ControlLevel=CTRL_LEVEL.CTRL_LEVEL_MAX;
            RDPSRAPITcpConnectionInfo info = (RDPSRAPITcpConnectionInfo)MyGuest.ConnectivityInfo;

            AxEventHandler<RemoteServerIncomingConnectionEventArgs> handler = this.IncomingConnection;

            if ( handler!=null )
                handler (new RemoteServerIncomingConnectionEventArgs (new SharpViewCore.RemoteConnectionInfo(info.LocalIP, info.PeerIP, info.LocalPort, info.PeerPort, info.Protocol)));

            this.Guests.Add (new RemoteDesktopGuest (MyGuest.ConnectivityInfo , MyGuest.RemoteName , MyGuest.Id , MyGuest.Flags , MyGuest.Invitation) { ControlLevel=MyGuest.ControlLevel });

            if(this.StartApplicationsOnConnect)
            {
                for ( int i = 0; i<Applications.Count; i++ )
                {
                    Process.Start (this.Applications[ i ]);
                }
            }
        }

        /// <summary>
        /// Create the specified password.
        /// </summary>
        /// <param name="password">Password.</param>
        public void Create(string password)
        {
            currentSession=new RDPSession ();
            Connect (currentSession);
            var invitation = DesktopInvitation =  getConnectionString (currentSession , Environment.UserDomainName , "SharpView" , password , 5);
            ConnectionId=invitation.ConnectionString;
        }

        /// <summary>
        /// Open this instance.
        /// </summary>
        public void Open()
        {
            this.currentSession.Open ();
        }

        /// <summary>
        /// Pause this instance.
        /// </summary>
        public void Pause()
        {
            this.currentSession.Pause ();
        }

        /// <summary>
        /// Resume this instance.
        /// </summary>
        public void Resume()
        {
            this.currentSession.Resume ();
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <returns>The property.</returns>
        /// <param name="propertyName">Property name.</param>
        public object GetProperty( string propertyName )
        {
            return this.currentSession.Properties[ propertyName ];
        }

        /// <summary>
        /// Gets the connection identifier.
        /// </summary>
        /// <value>The connection identifier.</value>
        public string ConnectionId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the desktop invitation.
        /// </summary>
        /// <value>The desktop invitation.</value>
        public RemoteDesktopInvitation DesktopInvitation
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the guests.
        /// </summary>
        /// <value>The guests.</value>
        public List<RemoteDesktopGuest> Guests
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.RemoteServer"/> class.
        /// </summary>
        public RemoteServer() { this.Guests=new List<RemoteDesktopGuest> (); }
    }

    namespace EventArgs
    {
        public class RemoteServerIncomingConnectionEventArgs
        {
            /// <summary>
            /// Gets the info.
            /// </summary>
            /// <value>The info.</value>
            public RemoteConnectionInfo Info { get; private set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="T:SharpViewCore.EventArgs.IncomingConnectionEventArgs"/> class.
            /// </summary>
            /// <param name="info">Info.</param>
            public RemoteServerIncomingConnectionEventArgs( RemoteConnectionInfo info )
            {
                this.Info=info;
            }
        }
        public class RemoteServerConnectedEventArgs
        {
            /// <summary>
            /// Gets the session.
            /// </summary>
            /// <value>The session.</value>
            public RDPSession Session { get; private set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="T:SharpViewCore.EventArgs.ConnectedEventArgs"/> class.
            /// </summary>
            /// <param name="session">Session.</param>
            public RemoteServerConnectedEventArgs( RDPSession session )
            {
                this.Session=session;
            }
        }
        public class RemoteServerDisconnectEventArgs
        {
            /// <summary>
            /// Gets the session.
            /// </summary>
            /// <value>The session.</value>
            public RDPSession Session { get; private set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="T:SharpViewCore.EventArgs.DisconnectEventArgs"/> class.
            /// </summary>
            /// <param name="session">Session.</param>
            public RemoteServerDisconnectEventArgs( RDPSession session )
            {
                this.Session=session;
            }
        }
    }
}
