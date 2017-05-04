using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using MazeAdapterLib;

namespace Task1
{
    /// <summary>
    /// this class is create a maze
    /// print it
    /// solving it with bfs and dfs
    /// and printing the num of nodes every algo was checking
    /// </summary>
    class Program
    {
        /// <summary>
        /// the main function
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            CompareSolvers(20, 15);
            CompareSolvers(60, 60);
            CompareSolvers(34, 34);
            CompareSolvers(75, 75);
            CompareSolvers(95, 95);
            CompareSolvers(135, 95);
            Console.WriteLine("Press Any Key To Continue");
            Console.ReadLine();
        }

        /// <summary>
        /// Compares the solvers.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        public static void CompareSolvers(int row, int col)
        {
            DFSMazeGenerator generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(row, col);
            Console.WriteLine(maze.ToString());
            ISearchable<Position> searchable = new MazeAdapter(maze);

            ISearcher<Position> searcher = new BFSSearcher<Position>();
            Solution<Position> solution = searcher.Search(searchable);
            Console.WriteLine("BFS solved the maze with {0} evaluated nodes",
                searcher.GetNumberOfNodesEvaluated());

            searcher = new DFSSearcher<Position>();
            solution = searcher.Search(searchable);
            Console.WriteLine("DFS solved the maze with {0} evaluated nodes",
                searcher.GetNumberOfNodesEvaluated());

            Console.WriteLine("");
        }
    }
}