using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpViewCore.EventArgs;

namespace SharpViewCore.Service
{
    public class ServiceCommand: IServiceCommand
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Occurs when on entered.
        /// </summary>
        public event ServiceConsoleEventHandler<ServiceConsoleCommandEventArgs> OnEntered;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.Service.ServiceCommand"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="description">Description.</param>
        public ServiceCommand(string name, string description)
        {
            this.Name=name;
            this.Description=description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.Service.ServiceCommand"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        public ServiceCommand( string name ) :
            this (name , "")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.Service.ServiceCommand"/> class.
        /// </summary>
        public ServiceCommand() { }

        /// <summary>
        /// Dos the entered.
        /// </summary>
        public void DoEntered()
        {
            ServiceConsoleEventHandler<ServiceConsoleCommandEventArgs> handler = this.OnEntered;
            if ( handler!=null ) handler (new ServiceConsoleCommandEventArgs (this));
        }
    }
}
