using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpViewCore
{
    public interface IRemoteDesktop : IRemoteDesktop2, IRemoteDesktop3, IRemoteDesktop4, IRemoteDesktop5
    {
        /// <summary>
        /// Gets the attende manager.
        /// </summary>
        /// <value>The attende manager.</value>
        RDPCOMAPILib.IRDPSRAPIAttendeeManager AttendeManager { get; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SharpViewCore.IRemoteDesktop"/> smart sizing.
        /// </summary>
        /// <value><c>true</c> if smart sizing; otherwise, <c>false</c>.</value>
        bool SmartSizing { get; set; }
        /// <summary>
        /// Connect the specified invitation, userName and password.
        /// </summary>
        /// <param name="invitation">Invitation.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        void Connect( string invitation , string userName , string password );
        /// <summary>
        /// Starts the reverse connect listener.
        /// </summary>
        /// <param name="invitation">Invitation.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        void StartReverseConnectListener( string invitation , string userName , string password );
        /// <summary>
        /// Creates the virtual channel.
        /// </summary>
        /// <param name="channelName">Channel name.</param>
        /// <param name="priority">Priority.</param>
        /// <param name="flags">Flags.</param>
        RemoteVirtualChannel CreateVirtualChannel( string channelName , RemoteChannelPriority priority , uint flags );
        /// <summary>
        /// Shows the property pages.
        /// </summary>
        void ShowPropertyPages();
        /// <summary>
        /// Shows the property pages.
        /// </summary>
        /// <param name="control">Control.</param>
        void ShowPropertyPages( Control control );
        /// <summary>
        /// Requests the control.
        /// </summary>
        /// <param name="controlLevel">Control level.</param>
        void RequestControl( RemoteControlLevel controlLevel );
        /// <summary>
        /// Requests the color depth change.
        /// </summary>
        /// <param name="dpp">Dpp.</param>
        void RequestColorDepthChange( uint dpp );
        /// <summary>
        /// Gets the ocx.
        /// </summary>
        /// <returns>The ocx.</returns>
        object GetOcx();
        /// <summary>
        /// Disconnect this instance.
        /// </summary>
        void Disconnect();

    }
}
