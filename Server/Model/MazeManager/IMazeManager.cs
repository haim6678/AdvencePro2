using MazeLib;
using Server.Model.Requests;
using SharedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.MazeManager
{
    /// <summary>
    /// define the single player manager.
    /// </summary>
    interface IMazeManager
    {
        /// <summary>
        /// Generates the specified maze by the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Maze GenerateMaze(GenerateRequest request);

        /// <summary>
        /// returns the solution for the requeseted maze.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MazeSolution SolveMaze(SolveRequest request);
    }
}
