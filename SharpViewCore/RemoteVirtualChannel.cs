using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDPCOMAPILib;

namespace SharpViewCore
{
    public class RemoteVirtualChannel
    {
        RemoteChannelPriority channel = RemoteChannelPriority.Low;
        RemoteVirtualChannelFlags flags = RemoteVirtualChannelFlags.DefaultChannel;
        string name;

        /// <summary>
        /// Gets the flags.
        /// </summary>
        /// <value>The flags.</value>
        public RemoteVirtualChannelFlags Flags
        {
            get
            {
                return flags;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public RemoteChannelPriority Priority
        {
            get
            {
                return channel;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.RemoteVirtualChannel"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="channelPriority">Channel priority.</param>
        /// <param name="flag">Flag.</param>
        public RemoteVirtualChannel(string name, RemoteChannelPriority channelPriority, RemoteVirtualChannelFlags flag)
        {
            this.name=name;
            this.channel=channelPriority;
            this.flags=flag;
        }
    }
}
