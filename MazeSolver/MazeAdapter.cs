using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace MazeSolver
{
    public class MazeAdapter : ISearchable<Position>
    {
        private Maze maze;

        public string name
        {
            get { return maze.Name; }
            
        }

        public MazeAdapter(Maze m)
        {
            this.maze = m;
        }

        public State<Position> GetInitialState()
        {
            return new State<Position>(maze.InitialPos);
        }

        public State<Position> GetGoalState()
        {
            return new State<Position>(maze.GoalPos);
        }

        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> list = new List<State<Position>>();
            int row = s.state.Row;
            int col = s.state.Col;
            int mazeRowSize = maze.Rows;
            int mazeColSize = maze.Cols;
            
            if ((row + 1 < mazeRowSize) && (maze[row + 1, col] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row + 1, col)));
            }
            if ((col + 1 < mazeColSize) && (maze[row, col + 1] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row, col + 1)));
            }
            if ((row - 1 >= 0) && (maze[row - 1, col] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row - 1, col)));
            }
            if ((col - 1 >= 0) && (maze[row, col - 1] == CellType.Free))
            {
                list.Add(new State<Position>(new Position(row, col - 1)));
            }
            return list;
        }
    }
}