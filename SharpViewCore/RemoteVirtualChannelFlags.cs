using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public enum RemoteVirtualChannelFlags : uint
    {
        /// <summary>
        /// Remote Desktop Channel,
        /// </summary>
        RemoteChannel = 0xD2F000,
        /// <summary>
        /// Default Server channel
        /// </summary>
        DefaultChannel = 0x000f1,
    }
}
