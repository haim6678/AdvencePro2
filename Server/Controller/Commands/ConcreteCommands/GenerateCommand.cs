using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using MazeLib;
using Server.Model.Requests;
using Server.Model;
using SharedData;

namespace Server.Controller.Commands
{
    /// <summary>
    /// a class that is in charge of executing the
    /// generate command
    /// </summary>
    /// <seealso cref="ServerCommand" />
    class GenerateCommand : ServerCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateCommand(IModel model) : base(model)
        {
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public override CommandResult Execute(IClient sender, string[] args)
        {
            // generate command expects exactly 3 parameters
            // TODO: decide when to keep connection, when to close it.
            if (args.Length != 3)
                return new CommandResult(false, Command.Generate, "Usage: generate <name> <rows> <cols>", true);

            string name;
            int rows, cols;

            name = args[0];
            if (!int.TryParse(args[1], out rows))
                return new CommandResult(false, Command.Generate, "Bad \"rows\" field.", true);

            if (!int.TryParse(args[2], out cols))
                return new CommandResult(false, Command.Generate, "Bad \"cols\" field.", true);

            try
            {
                Maze m = _model.GenerateMaze(new GenerateRequest(name, rows, cols));
                bool keepCom = _model.IsInGame(sender);
                return new CommandResult(true, Command.Generate, m.ToJSON(), keepCom);
            }
            catch (Exception e)
            {
                return new CommandResult(false, Command.Generate, e.Message, true);
            }
        }
    }
}
