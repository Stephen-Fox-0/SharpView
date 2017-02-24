using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore.Network
{
    public sealed class NetworkDevice: INetworkDevice
    {
        bool isConnect;
        string name, ipAddress;

        public bool IsConnected
        {
            get
            {
                return isConnect;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string IPAddresss
        {
            get
            {
                return ipAddress;
            }
        }

        public NetworkDevice(string name, bool isConnected, string ipAddress)
        {
            this.isConnect=isConnected;
            this.name=name;
            this.ipAddress=ipAddress;
        }
    }
}
