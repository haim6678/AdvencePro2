using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Server.Model;
using Server.Model.Requests;
using SharedData;

namespace Server.Controller.Commands
{
    /// <summary>
    /// a class that is in charge of executing the
    /// close command
    /// </summary>
    /// <seealso cref="ServerCommand" />
    class CloseCommand : ServerCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseCommand(IModel model) : base(model) { }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public override CommandResult Execute(IClient sender, string[] args)
        {
            // TODO: decide when to keep connection, when to close it.
            if (args.Length != 1)
                return new CommandResult(false, Command.Close, "Usage: close <name>", true);

            CloseRequest request = new CloseRequest(sender, args[0]);
            try
            {
                _model.CloseGame(request);
            }
            catch (Exception e)
            {
                return new CommandResult(false, Command.Close, e.Message, true);
            }

            return new CommandResult(true, Command.Close, "Successfully closed the game.", false);
        }
    }
}
