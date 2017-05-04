using MazeLib;
using SharedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeAdapterLib;
using Server.Model.Requests;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace Server.Model.MazeManager
{
    /// <summary>
    ///  implements the IMazeManager,
    /// and hanlde the single player functionality.
    /// </summary>
    /// <seealso cref="IMazeManager" />
    class MazeManager : IMazeManager
    {
        private IMazeGenerator mazeGen;
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, MazeSolution> solutionCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeManager"/> class.
        /// </summary>
        public MazeManager()
        {
            mazeGen = new DFSMazeGenerator();
            mazes = new Dictionary<string, Maze>();
            solutionCache = new Dictionary<string, MazeSolution>();
        }

        /// <summary>
        /// Generates the specified maze by the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public Maze GenerateMaze(GenerateRequest request)
        {
            // if there is a maze with this name
            if (mazes.ContainsKey(request.MazeName))
            {
                throw new InvalidOperationException(string.Format("Maze with the name \"{0}\" already exists!", request.MazeName));
            }
            Maze m = mazeGen.Generate(request.Rows, request.Columns);
            m.Name = request.MazeName;
            mazes[request.MazeName] = m;
            return m;
        }

        /// <summary>
        /// Solves the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public MazeSolution SolveMaze(SolveRequest request)
        {
            // if there is a maze with this name
            if (!mazes.ContainsKey(request.MazeName))
            {
                throw new InvalidOperationException(string.Format("There is no maze with the name \"{0}\"", request.MazeName));
            }

            // if solution exists in the cache
            if (solutionCache.ContainsKey(request.MazeName))
            {
                return solutionCache[request.MazeName];
            }
            // initialize the search components
            MazeAdapter adapter = new MazeAdapter(mazes[request.MazeName]);
            ISearcher<Position> algorithm = GetAlgorithm(request.Algorithm);
            // search for the solution
            Solution<Position> sol = algorithm.Search(adapter);
            // create the mazeSolution object.
            MazeSolution path = new MazeSolution(algorithm.GetNumberOfNodesEvaluated(), request.MazeName, sol);
            // save the solution in the cache
            solutionCache[request.MazeName] = path;
            //return the solution
            return path;
        }

        /// <summary>
        /// Gets the algorithm th solve a maze.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns></returns>
        private static ISearcher<Position> GetAlgorithm(byte algorithm)
        {
            ISearcher<Position> searcher;
            switch (algorithm)
            {
                case 0:
                    searcher = new BFSSearcher<Position>();
                    break;
                case 1:
                    searcher = new DFSSearcher<Position>();
                    break;
                default:
                    throw new ArgumentException(string.Format("Unknown algorithm signifier \"{0}\"", algorithm));
            }
            return searcher;
        }
    }
}
