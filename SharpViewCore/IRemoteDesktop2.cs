using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public interface IRemoteDesktop2
    {
        /// <summary>
        /// Gets the status text.
        /// </summary>
        /// <returns>The status text.</returns>
        /// <param name="statusCode">Status code.</param>
        string GetStatusText( uint statusCode );
        /// <summary>
        /// Gets the error description.
        /// </summary>
        /// <returns>The error description.</returns>
        /// <param name="disconnectReason">Disconnect reason.</param>
        /// <param name="extendedDisconnectReason">Extended disconnect reason.</param>
        string GetErrorDescription( uint disconnectReason , uint extendedDisconnectReason );
        /// <summary>
        /// Reconnect the specified ulWidth and ulHeight.
        /// </summary>
        /// <param name="ulWidth">Ul width.</param>
        /// <param name="ulHeight">Ul height.</param>
        RemoteReconnectState Reconnect( uint ulWidth , uint ulHeight );
        /// <summary>
        /// Attachs the event.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        /// <param name="callback">Callback.</param>
        void AttachEvent( string eventName , object callback );
        /// <summary>
        /// Detachs the event.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        /// <param name="callback">Callback.</param>
        void DetachEvent( string eventName , object callback );
        /// <summary>
        /// Syncs the session display settings.
        /// </summary>
        void SyncSessionDisplaySettings();
        /// <summary>
        /// Sends the remote action.
        /// </summary>
        /// <param name="actionType">Action type.</param>
        void SendRemoteAction( RemoteSessionActionType actionType );
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SharpViewCore.IRemoteDesktop2"/> is connected.
        /// </summary>
        /// <value><c>true</c> if is connected; otherwise, <c>false</c>.</value>
        bool IsConnected { get; }
    }
}
