using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public interface IRemoteServer3
    {
        /// <summary>
       /// Gets the connection identifier.
       /// </summary>
       /// <value>The connection identifier.</value>
        string ConnectionId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the desktop invitation.
        /// </summary>
        /// <value>The desktop invitation.</value>
        RemoteDesktopInvitation DesktopInvitation
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the guests.
        /// </summary>
        /// <value>The guests.</value>
        List<RemoteDesktopGuest> Guests
        {
            get;
            set;
        }
    }
}
