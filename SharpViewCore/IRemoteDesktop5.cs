using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public interface IRemoteDesktop5
    {
        /// <summary>
        /// Sets the disconnected text.
        /// </summary>
        /// <param name="disconnectedText">Disconnected text.</param>
        void SetDisconnectedText( string disconnectedText );
        /// <summary>
        /// Sets the connected text.
        /// </summary>
        /// <param name="connectedText">Connected text.</param>
        void SetConnectedText( string connectedText );
    }
}
