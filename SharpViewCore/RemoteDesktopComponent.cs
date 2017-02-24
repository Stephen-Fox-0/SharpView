using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public class RemoteDesktopComponent : Component
    {
        /// <summary>
        /// Gets or sets the desktop control.
        /// </summary>
        /// <value>The desktop control.</value>
        public RemoteDesktop DesktopControl
        {
            get;
            set;
        }

        /// <summary>
        /// Disconnect this instance.
        /// </summary>
        public void Disconnect()
        {
            if ( DesktopControl!=null )
                DesktopControl.Disconnect ();
        }

        /// <summary>
        /// Connect the specified info.
        /// </summary>
        /// <param name="info">Info.</param>
        public void Connect(RemoteDesktopInfo info)
        {
            if ( DesktopControl!=null )
                DesktopControl.Connect (info);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.RemoteDesktopComponent"/> class.
        /// </summary>
        public RemoteDesktopComponent() { }
    }
}
