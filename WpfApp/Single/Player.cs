using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace WpfApp
{
    /// <summary>
    ///  single player game - player class
    /// </summary>
    class Player
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Position Position { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="p">The p.</param>
        public Player(Position p)
        {
            Position = p;
        }

        /// <summary>
        /// Gets the next location.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public Position GetNextLocation(string s)
        {
            Direction d = ParseDirection(s);
            int row = Position.Row;
            int col = Position.Col;
            switch (d)
            {
                case Direction.Up:
                    --row;
                    break;
                case Direction.Down:
                    ++row;
                    break;
                case Direction.Left:
                    --col;
                    break;
                case Direction.Right:
                    ++col;
                    break;
            }
            return new Position(row, col);
        }

        /// <summary>
        /// Parses the direction.
        /// </summary>
        /// <param name="dirStr">The dir string.</param>
        /// <returns></returns>
        private Direction ParseDirection(string dirStr)
        {
            Direction dir;
            switch (dirStr)
            {
                case "up":
                    dir = Direction.Up;
                    break;
                case "down":
                    dir = Direction.Down;
                    break;
                case "left":
                    dir = Direction.Left;
                    break;
                case "right":
                    dir = Direction.Right;
                    break;
                default:
                    dir = Direction.Unknown;
                    break;
            }
            return dir;
        }
    }
}