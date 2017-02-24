using SharpViewCore.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SharpViewCore.Service
{
    public class ServiceConsoleManager
    {
        /// <summary>
        /// Gets the commands.
        /// </summary>
        /// <value>The commands.</value>
        public List<IServiceCommand> Commands
        {
            get;
            private set;
        }

        /// <summary>
        /// Occurs when before init.
        /// </summary>
        public event ServiceConsoleEventHandler<ServiceEventArgs> BeforeInit;

        /// <summary>
        /// Inits the console.
        /// </summary>
        /// <param name="commandLineText">Command line text.</param>
        /// <param name="helpCommandString">Help command string.</param>
        public ServiceConsoleManager()
        {
            this.Commands=new List<Service.IServiceCommand> ();
        }

        /// <summary>
        /// Inits the console.
        /// </summary>
        /// <param name="commandLineText">Command line text.</param>
        /// <param name="helpCommandString">Help command string.</param>
        public void InitConsole(string commandLineText, string helpCommandString)
        {
            ServiceConsoleEventHandler<ServiceEventArgs> beforeInitHandler = this.BeforeInit;
            if ( beforeInitHandler!=null )
            {
                this.BeforeInit (new EventArgs.ServiceEventArgs ());
            }

            for ( ; true; )
            {
                Console.Write (commandLineText);
                var entryLine = Console.ReadLine ();

                foreach ( IServiceCommand command in this.Commands )
                {
                    if ( entryLine==command.Name )
                    {
                        command.DoEntered ();
                    }
                }

                if(entryLine == helpCommandString)
                {
                    foreach ( IServiceCommand command in this.Commands )
                    {
                        Console.WriteLine (command.Name+" - "+command.Description);
                    }
                }
            }
        }

        /// <summary>
        /// Execute the specified command.
        /// </summary>
        /// <param name="commandName">Command name.</param>
        public void Execute(string commandName)
        {
            GetServiceCommandByName (commandName).DoEntered ();
        }

        /// <summary>
        /// Gets a command by its name.
        /// </summary>
        /// <returns>The service command by name.</returns>
        /// <param name="commandName">Command name.</param>
        public IServiceCommand GetServiceCommandByName( string commandName )
        {
            foreach ( IServiceCommand command in this.Commands )
            {
                if ( command.Name==commandName )
                    return command;
            }
            return null;
        }
    }
}

namespace SharpViewCore.EventArgs
{
    public class ServiceEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.EventArgs.ServiceEventArgs"/> class.
        /// </summary>
        public ServiceEventArgs() { }
    }
}
