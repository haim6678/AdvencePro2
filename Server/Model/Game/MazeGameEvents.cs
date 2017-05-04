using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Game
{
    delegate void GameStarted(MazeGame sender);
    delegate void GameOver(MazeGame sender, GameOverEventArgs args);
    delegate void PlayerMoved(MazeGame game, IClient player, Direction d);

    /// <summary>
    /// an event class extend the EventArgs class
    /// but with parameters to the constructor.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    class GameOverEventArgs : EventArgs
    {
        public IClient Winner { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverEventArgs"/> class.
        /// </summary>
        public GameOverEventArgs()
        {
            Winner = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverEventArgs"/> class.
        /// </summary>
        /// <param name="winner">The winner.</param>
        public GameOverEventArgs(IClient winner)
        {
            this.Winner = winner;
        }
    }
}
