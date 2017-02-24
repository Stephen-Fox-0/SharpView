using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    namespace Network
    {
        public struct NetworkState
        {
            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="T:SharpViewCore.NetworkState"/> is connected.
            /// </summary>
            /// <value><c>true</c> if is connected; otherwise, <c>false</c>.</value>
            public bool IsConnected { get; set; }

            /// <summary>
            /// Gets or sets the network address.
            /// </summary>
            /// <value>The network address.</value>
            public string NetworkAddress { get; set; }

            /// <summary>
            /// Gets or sets the network address family.
            /// </summary>
            /// <value>The network address family.</value>
            public object NetworkAddressFamily { get; set; }

            /// <summary>
            /// Gets or sets the network devices.
            /// </summary>
            /// <value>The network devices.</value>
            public INetworkDevice[] Devices { get; set; }

            /// <summary>
            /// Gets the machine name.
            /// </summary>
            public string PublicMachineName { get { return Environment.MachineName; } }

            /// <summary>
            /// Gets the user-name that is logged in on this machine.
            /// </summary>
            public string PublicUserName { get { return Environment.UserName; } }

            /// <summary>
            /// Gets the domain-name of the current logged in machine.
            /// </summary>
            public string PublicDomain { get { return Environment.UserDomainName; } }

            /// <summary>
            /// Gets the network state.
            /// </summary>
            /// <returns></returns>
            public static NetworkState Get()
            {
                NetworkState state = new NetworkState ();
                for ( int i = 0; i<GetNetworkAddress (Dns.GetHostName ()).Length; i++ )
                {
                    var address = GetNetworkAddress (Dns.GetHostName ())[ i ];
                    if ( address.Address.Contains (".") )
                    {
                        state.NetworkAddress=address.Address;
                        break;
                    }
                }

                state.NetworkAddressFamily=Dns.GetHostAddresses (state.NetworkAddress)[ 0 ].AddressFamily.ToString ();

                if ( System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces ()[ 0 ].OperationalStatus==System.Net.NetworkInformation.OperationalStatus.Up )
                    state.IsConnected=true;
                else
                    state.IsConnected=false;

                List<INetworkDevice> devices = new List<Network.INetworkDevice> ();
                foreach ( NetworkInterface Netinterface in NetworkInterface.GetAllNetworkInterfaces () )
                {
                    if ( Netinterface.NetworkInterfaceType.ToString ().Contains ("Ethernet")||Netinterface.NetworkInterfaceType.ToString ().Contains ("Wireless80211") )
                    {
                        devices.Add (new NetworkDevice (Netinterface.Name , Netinterface.OperationalStatus==OperationalStatus.Up ? true : false , Netinterface.GetIPProperties ().DnsAddresses[ 0 ].ToString ()));
                    }
                }
                state.Devices=devices.ToArray ();
                return state;
            }

            /// <summary>
            /// Gets the network address.
            /// </summary>
            /// <returns>The network address.</returns>
            /// <param name="hostName">Host name.</param>
            public static NetworkAddress[] GetNetworkAddress( string hostName )
            {
                return NetworkLookup.GetAddresses (hostName: hostName);
            }

            /// <summary>
            /// Send a ping request package.
            /// </summary>
            /// <param name="hostName">host-name.</param>
            /// <param name="timeOut">timeout.</param>
            /// <returns></returns>
            public static NetworkPingResult SendPingRequest( string hostName , int timeOut )
            {
                try
                {
                    Ping ping = new Ping ();
                    var reply = ping.Send (hostName , timeOut);
                    if ( reply.Status==IPStatus.Success )
                        return NetworkPingResult.Success;
                    else
                        return NetworkPingResult.Unsucessfull;
                }
                catch
                {
                    return NetworkPingResult.Unsucessfull;
                }
            }
        }
    }
}
