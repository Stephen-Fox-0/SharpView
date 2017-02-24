using RDPCOMAPILib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    [Serializable]
    public class RemoteDesktopGuest
    {
        dynamic connectionInfo;
        /// <summary>
        /// Gets the connectivity info.
        /// </summary>
        /// <value>The connectivity info.</value>
        public dynamic ConnectivityInfo
        {
            get
            {
                return connectionInfo;
            }
        }
        /// <summary>
        /// Gets or sets the control level.
        /// </summary>
        /// <value>The control level.</value>
        public CTRL_LEVEL ControlLevel
        {
            get;
            set;
        }

        int flags;
        /// <summary>
        /// Gets the flags.
        /// </summary>
        /// <value>The flags.</value>
        public int Flags
        {
            get
            {
                return flags;
            }
        }

        int id;
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id
        {
            get
            {
                return id;
            }
        }

        RDPSRAPIInvitation invitation;
        /// <summary>
        /// Gets the invitation.
        /// </summary>
        /// <value>The invitation.</value>
        public RDPSRAPIInvitation Invitation
        {
            get
            {
                return invitation;
            }
        }

        string remoteName;
        /// <summary>
        /// Gets the name of the remote.
        /// </summary>
        /// <value>The name of the remote.</value>
        public string RemoteName
        {
            get
            {
                return remoteName;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.RemoteDesktopGuest"/> class.
        /// </summary>
        /// <param name="remoteName">Remote name.</param>
        /// <param name="id">Identifier.</param>
        /// <param name="flags">Flags.</param>
        /// <param name="invitation">Invitation.</param>
        public RemoteDesktopGuest(
            dynamic connectionInfo, string remoteName, int id, int flags, RDPSRAPIInvitation invitation)
        {
            this.remoteName=remoteName;
            this.id=id;
            this.flags=flags;
            this.invitation=invitation;
            this.connectionInfo=connectionInfo;
        }
    }
}
