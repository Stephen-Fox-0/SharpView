using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    namespace Network
    {
        public interface INetworkDevice
        {
            /// <summary>
            /// Gets the name of this network device.
            /// </summary>
            string Name { get; }
            /// <summary>
            /// Gets a value indercating weather this device is connected.
            /// </summary>
            bool IsConnected { get;  }

            /// <summary>
            /// Gets the ipAddress.
            /// </summary>
            string IPAddresss { get; }
        }
    }
}
