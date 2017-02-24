using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public interface IRemoteDesktop3
    {
        /// <summary>
        /// Gets the invitiations.
        /// </summary>
        /// <value>The invitiations.</value>
        RDPCOMAPILib.IRDPSRAPIInvitationManager Invitiations { get; }
    }
}
