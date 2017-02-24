namespace SharpViewCore
{
    public class RemoteApplication
    {
        string name;
        uint flags;
        int id;

        /// <summary>
        /// Gets the flags.
        /// </summary>
        /// <value>The flags.</value>
        public uint Flags
        {
            get
            {
                return flags;
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SharpViewCore.RemoteApplication"/> is shared.
        /// </summary>
        /// <value><c>true</c> if shared; otherwise, <c>false</c>.</value>
        public bool Shared
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.RemoteApplication"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="id">Identifier.</param>
        /// <param name="flags">Flags.</param>
        public RemoteApplication(string name, int id, uint flags)
        {
            this.name=name;
            this.id=id;
            this.flags=flags;
        }
    }
}
