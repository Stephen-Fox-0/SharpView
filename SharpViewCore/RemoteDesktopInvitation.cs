using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore
{
    public class RemoteDesktopInvitation
    {
        string group, password, connectionstring;

        /// <summary>
        /// Gets or sets the attendee limit.
        /// </summary>
        /// <value>The attendee limit.</value>
        public int AttendeeLimit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString
        {
            get
            {
                return connectionstring;
            }
        }

        /// <summary>
        /// Gets the name of the group.
        /// </summary>
        /// <value>The name of the group.</value>
        public string GroupName
        {
            get
            {
                return group;
            }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get
            {
                return password;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktopInvitation"/> is revoked.
        /// </summary>
        /// <value><c>true</c> if revoked; otherwise, <c>false</c>.</value>
        public bool Revoked
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.RemoteDesktopInvitation"/> class.
        /// </summary>
        /// <param name="connectString">Connect string.</param>
        /// <param name="groupName">Group name.</param>
        /// <param name="password">Password.</param>
        public RemoteDesktopInvitation(string connectString, string groupName, string password)
        {
            this.connectionstring=connectString;
            this.group=groupName;
            this.password=password;
        }
    }
}
