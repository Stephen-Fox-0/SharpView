using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public interface IRemoteServer5
    {
        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <returns>The property.</returns>
        /// <param name="propertyName">Property name.</param>
        object GetProperty( string propertyName );
    }
}
