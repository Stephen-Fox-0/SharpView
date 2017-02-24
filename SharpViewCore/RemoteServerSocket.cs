using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpViewCore.EventArgs;
using System.Net.Sockets;
using System.Net;

namespace SharpViewCore
{
    public class RemoteServerSocket: IRemoteServerSocket
    {
        Socket socket = null;
        bool connected;

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SharpViewCore.RemoteServerSocket"/> is connected.
        /// </summary>
        /// <value><c>true</c> if is connected; otherwise, <c>false</c>.</value>
        public bool IsConnected
        {
            get
            {
                return connected;
            }
        }

        /// <summary>
        /// Occurs when connected value chaned.
        /// </summary>
        public event AxEventHandler<RemoteServerSocketConnectedValueChanged> ConnectedValueChaned;

        /// <summary>
        /// Bind the specified hostNameOrIpaddress.
        /// </summary>
        /// <param name="hostNameOrIpaddress">Host name or ipaddress.</param>
        public void Bind( string hostNameOrIpaddress )
        {
            socket.Bind (new IPEndPoint (IPAddress.Parse(hostNameOrIpaddress), 8080));
        }

        /// <summary>
        /// Bind the specified hostNameOrIpAddress and port.
        /// </summary>
        /// <param name="hostNameOrIpAddress">Host name or ip address.</param>
        /// <param name="port">Port.</param>
        public void Bind( string hostNameOrIpAddress , int port = 8080 )
        {
            socket.Bind (new IPEndPoint (IPAddress.Parse (hostNameOrIpAddress) , port));
        }

        /// <summary>
        /// Connect the specified hostNameOrIpAddress and port.
        /// </summary>
        /// <param name="hostNameOrIpAddress">Host name or ip address.</param>
        /// <param name="port">Port.</param>
        public void Connect( string hostNameOrIpAddress, int port )
        {
            socket.Connect (hostNameOrIpAddress , 8080);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.RemoteServerSocket"/> class.
        /// </summary>
        public RemoteServerSocket()
        {
            this.socket=new Socket (SocketType.Stream , ProtocolType.Tcp);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer ();
            timer.Tick+=( sender , e ) =>
            {
                 this.connected=socket.Connected;

                 AxEventHandler<RemoteServerSocketConnectedValueChanged> handler = this.ConnectedValueChaned;
                 if ( handler!=null )
                     handler (new RemoteServerSocketConnectedValueChanged (this.connected));
            };
            timer.Enabled=true;
        }
    }
}
