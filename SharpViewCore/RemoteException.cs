using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public class RemoteException : Exception
    {
        /// <summary>
        /// Initialize a new instance of <see cref="RemoteException"/>
        /// </summary>
        public RemoteException() : base ("There was an unexpected error") { }
        public RemoteException(string message) : base ("There was an unexpected error\nCode Message: " + message) { }
    }
}
