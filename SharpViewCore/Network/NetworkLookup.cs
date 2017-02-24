using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    namespace Network
    {
        public class NetworkAddress
        {
            /// <summary>
            /// Gets the address.
            /// </summary>
            /// <value>The address.</value>
            public string Address
            {
                get;
                private set;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:SharpViewCore.NetworkAddress"/> class.
            /// </summary>
            /// <param name="address">Address.</param>
            public NetworkAddress( string address )
            {
                this.Address=address;
            }
        }


        public class NetworkLookup
        {
            /// <summary>
            /// Gets the addresses.
            /// </summary>
            /// <returns>The addresses.</returns>
            /// <param name="hostName">Host name.</param>
            public static NetworkAddress[] GetAddresses( string hostName )
            {
                List<NetworkAddress> addresses = new List<NetworkAddress> ();

                foreach ( IPAddress address in Dns.GetHostAddresses (hostName) )
                {
                    addresses.Add (new NetworkAddress (address.ToString ()));
                }

                return addresses.ToArray ();
            }
        }
    }
}
