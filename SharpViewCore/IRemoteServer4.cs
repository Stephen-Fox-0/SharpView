using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public interface IRemoteServer4
    {
        /// <summary>
        /// Open this instance.
        /// </summary>
        void Open();
        /// <summary>
        /// Pause this instance.
        /// </summary>
        void Pause();
        /// <summary>
        /// Resume this instance.
        /// </summary>
        void Resume();
    }
}
