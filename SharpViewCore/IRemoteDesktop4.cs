using SharpViewCore.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public interface IRemoteDesktop4
    {
        /// <summary>
        /// Gets all connected addresses.
        /// </summary>
        /// <returns>The all connected addresses.</returns>
        NetworkAddress[] GetAllConnectedAddresses();
        /// <summary>
        /// Lookups the address.
        /// </summary>
        /// <returns>The address.</returns>
        /// <param name="hostnameOrIpAddress">Hostname or ip address.</param>
        NetworkAddress[] LookupAddress( string hostnameOrIpAddress );
        /// <summary>
        /// Gets the state of the net.
        /// </summary>
        /// <returns>The net state.</returns>
        NetworkState GetNetState();
        /// <summary>
        /// Gets the network is available.
        /// </summary>
        /// <returns><c>true</c>, if network is available was gotten, <c>false</c> otherwise.</returns>
        bool GetNetworkIsAvailable();
    }
}
