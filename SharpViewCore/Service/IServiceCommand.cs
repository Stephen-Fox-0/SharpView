using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpViewCore.Service
{
    public interface IServiceCommand
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }

        /// <summary>
        /// Occurs when on entered.
        /// </summary>
        event EventArgs.ServiceConsoleEventHandler<EventArgs.ServiceConsoleCommandEventArgs> OnEntered;

        /// <summary>
        /// Dos the entered.
        /// </summary>
        void DoEntered();
    }

}

namespace SharpViewCore.EventArgs
{
    public class ServiceConsoleCommandEventArgs
    {
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        public SharpViewCore.Service.IServiceCommand Command
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.EventArgs.ServiceConsoleCommandEventArgs"/> class.
        /// </summary>
        /// <param name="command">Command.</param>
        public ServiceConsoleCommandEventArgs( SharpViewCore.Service.IServiceCommand command )
        {
            this.Command=command;
        }
    }

    /// <summary>
    /// Service console event handler.
    /// </summary>
    public delegate void ServiceConsoleEventHandler<TEventArgs>( TEventArgs e );
}
