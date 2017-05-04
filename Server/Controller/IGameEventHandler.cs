using MazeLib;
using Server.Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller
{
    /// <summary>
    /// a class that handles the event should have thos 3 
    /// functions.
    /// </summary>
    interface IGameEventHandler
    {
        void HandleGameStarted(MazeGame m);
        void HandleGameOver(MazeGame game, GameOverEventArgs args);
        void HandlePlayerMoved(MazeGame game, IClient player, Direction d);
    }
}
