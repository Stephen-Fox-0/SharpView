using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public interface IRemoteServerSocket
    {
        /// <summary>
        /// Bind the specified hostNameOrIpAddress and port.
        /// </summary>
        /// <param name="hostNameOrIpAddress">Host name or ip address.</param>
        /// <param name="port">Port.</param>////
        void Bind( string hostNameOrIpAddress , int port = 8080 );
        /// <summary>
        /// Bind the specified hostNameOrIpaddress.
        /// </summary>
        /// <param name="hostNameOrIpaddress">Host name or ipaddress.</param>
        void Bind( string hostNameOrIpaddress );
        /// <summary>
        /// Connect the specified hostNameOrIpAddress and port.
        /// </summary>
        /// <param name="hostNameOrIpAddress">Host name or ip address.</param>
        /// <param name="port">Port.</param>
        void Connect( string hostNameOrIpAddress , int port );

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SharpViewCore.IRemoteServerSocket"/> is connected.
        /// </summary>
        /// <value><c>true</c> if is connected; otherwise, <c>false</c>.</value>
        bool IsConnected { get; }

        /// <summary>
        /// Occurs when connected value chaned.
        /// </summary>
        event EventArgs.AxEventHandler<EventArgs.RemoteServerSocketConnectedValueChanged> ConnectedValueChaned;
    }

    namespace EventArgs
    {
        public class RemoteServerSocketConnectedValueChanged : BoolValueEventArgs
        {
            /// <summary>
            /// Gets a value indicating whether this
            /// <see cref="T:SharpViewCore.EventArgs.RemoteServerSocketConnectedValueChanged"/> new value.
            /// </summary>
            /// <value><c>true</c> if new value; otherwise, <c>false</c>.</value>
            public bool NewValue { get; private set; }
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="T:SharpViewCore.EventArgs.RemoteServerSocketConnectedValueChanged"/> class.
            /// </summary>
            /// <param name="newValue">If set to <c>true</c> new value.</param>
            public RemoteServerSocketConnectedValueChanged(bool newValue)
            {
                this.NewValue=newValue;
            }
        }

        public class BoolValueEventArgs
        {
            /// <summary>
            /// Gets a value indicating whether this <see cref="T:SharpViewCore.EventArgs.BoolValueEventArgs"/> false value.
            /// </summary>
            /// <value><c>true</c> if false value; otherwise, <c>false</c>.</value>
            public bool FalseValue { get { return false; } }
            /// <summary>
            /// Gets a value indicating whether this <see cref="T:SharpViewCore.EventArgs.BoolValueEventArgs"/> true value.
            /// </summary>
            /// <value><c>true</c> if true value; otherwise, <c>false</c>.</value>
            public bool TrueValue { get { return true; } }
        }
    }
}
