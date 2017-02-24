using SharpViewCore.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public interface IRemoteServer : IRemoteServer2, IRemoteServer3, IRemoteServer4, IRemoteServer5
    {
        /// <summary>
        /// Occurs when incoming connection.
        /// </summary>
        event AxEventHandler<RemoteServerIncomingConnectionEventArgs> IncomingConnection;
        /// <summary>
        /// Occurs when connected.
        /// </summary>
        event AxEventHandler<RemoteServerConnectedEventArgs> Connected;
        /// <summary>
        /// Occurs when disconnected.
        /// </summary>
        event AxEventHandler<RemoteServerDisconnectEventArgs> Disconnected;
    }
}
