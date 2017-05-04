using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Wpf
{
    class Player
    {
        public Position Position { get; set; }

        public Player(Position p)
        {
            Position = p;
        }

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