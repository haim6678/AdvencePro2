using MazeLib;
using SearchAlgorithmsLib;
using Server.Model.Game;
using Server.Model.Requests;
using SharedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    /// <summary>
    /// define the functionalty of a model.
    /// </summary>
    interface IModel
    {

        Maze GenerateMaze(GenerateRequest request);

        MazeSolution SolveMaze(SolveRequest request);

        void CreateGame(StartRequest request);

        List<string> GetAvailableGames();

        void JoinGame(JoinRequest request);

        void Play(PlayRequest request);

        void CloseGame(CloseRequest request);

        bool IsInGame(IClient c);
    }
}
