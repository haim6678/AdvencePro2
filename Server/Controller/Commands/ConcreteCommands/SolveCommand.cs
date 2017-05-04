using Server.Model;
using Server.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using SharedData;

namespace Server.Controller.Commands
{
    /// <summary>
    /// a class that is in charge of executing the
    /// solve command
    /// </summary>
    /// <seealso cref="ServerCommand" />
    class SolveCommand : ServerCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolveCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveCommand(IModel model) : base(model) { }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public override CommandResult Execute(IClient sender, string[] args)
        {
            if (args.Length != 2)
                return new CommandResult(false, Command.Solve, "Usage: solve <name> <algorithm>", true);

            string name = args[0];
            byte algorithm;

            if (!byte.TryParse(args[1], out algorithm))
                return new CommandResult(false, Command.Solve, "Bad <algorithm> field. Expected: 0 - for bfs, 1 - for dfs", true);

            SolveRequest request = new SolveRequest(name, algorithm);

            try
            {
                MazeSolution sol = _model.SolveMaze(request);
                //serialize and send the object
                bool keepCom = _model.IsInGame(sender);
                return new CommandResult(true, Command.Solve, sol.ToJSON(), keepCom);
            }
            catch (Exception e)
            {
                return new CommandResult(false, Command.Solve, e.Message, true);
            }
        }
    }
}
