using SharedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller.Commands
{
    /// <summary>
    /// define the command interface
    /// </summary>
    interface ICommand
    {
        CommandResult Execute(IClient sender, string[] args);
    }
}
