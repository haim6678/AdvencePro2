using Server.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.GameManager
{
    /// <summary>
    /// an interface that define a game manager.
    /// </summary>
    /// <seealso cref="IGameContainer" />
    /// <seealso cref="IGameEventHandler" />
    interface IGameManager : IGameContainer, IGameEventHandler
    {
    }
}
