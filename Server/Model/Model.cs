using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Model.Requests;
using SharedData;
using Server.Model.Game;
using MazeGeneratorLib;
using Server.Model.MazeManager;
using Server.Controller;
using Server.Model.GameManager;

namespace Server.Model
{
    /// <summary>
    /// the model part of mvc.
    /// handle the management,and logic.
    /// </summary>
    /// <seealso cref="IModel" />
    class Model : IModel
    {
        private IMazeGenerator mazeGen;
        private IMazeManager mazeManager;
        private IGameManager gameManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public Model(IGameEventHandler handler)
        {
            mazeGen = new DFSMazeGenerator();
            mazeManager = new MazeManager.MazeManager();
            gameManager = new GameManager.GameManager(new ListGameContainer(), handler);
        }

        /// <summary>
        /// Generates the specified maze by the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Maze GenerateMaze(GenerateRequest request)
        {
            return mazeManager.GenerateMaze(request);
        }

        /// <summary>
        /// Solves the requested maze.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public MazeSolution SolveMaze(SolveRequest request)
        {
            return mazeManager.SolveMaze(request);
        }

        /// <summary>
        /// Creates the game for the start command
        /// for a multiplyer game.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.InvalidOperationException">
        /// A game already exists in that name!
        /// or
        /// You are already in other game!
        /// </exception>
        public void CreateGame(StartRequest request)
        {
            // if there is already a game in this name
            if (gameManager.ContainsGame(request.MazeDetails.MazeName))
                throw new InvalidOperationException("A game already exists in that name!");

            // if the client is already in other game
            if (gameManager.ContainsGame(request.Client))
                throw new InvalidOperationException("You are already in other game!");

            Maze m = mazeGen.Generate(request.MazeDetails.Rows, request.MazeDetails.Columns);
            m.Name = request.MazeDetails.MazeName;
            MazeGame game = new MazeGame(m);

            gameManager.AddGame(game);
            game.AddPlayer(request.Client);
        }

        /// <summary>
        /// Returns a list containing the names of all the available games.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAvailableGames()
        {
            // list all names of the unstarted games.
            List<MazeGame> unstarted = gameManager.GetNonStartedGames();
            List<string> available = new List<string>(unstarted.Select(x => x.Name));
            return available;
        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="request">The join request.</param>
        /// <returns></returns>
        public void JoinGame(JoinRequest request)
        {
            // find the requested game
            MazeGame gameToJoin = gameManager.GetGame(request.GameName);

            // if no game found
            if (gameToJoin == null)
                throw new InvalidOperationException(string.
                    Format("Could not find the game \"{0}\"", request.GameName));

            // if the requested game has already started
            if (gameToJoin.Started)
                throw new InvalidOperationException(string.
                    Format("The game \"{0}\" has already started.",request.GameName));

            // else, the player can join..
            // now check that this player is not in any other game:
            // if the player is already in an unfinished game
            if (gameManager.ContainsGame(request.Client))
            {
                throw new InvalidOperationException("You have already joined other game.");
            }

            // the player is not in any other game.
            // add him to the game.
            gameToJoin.AddPlayer(request.Client);
        }

        /// <summary>
        /// Plays the one move int the multiplyer game.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public void Play(PlayRequest request)
        {
            MazeGame game = gameManager.GetGame(request.Client);
            if (game == null)
                throw new InvalidOperationException("You did not join any game.");

            game.PlayMove(request.Client, request.Move);
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public void CloseGame(CloseRequest request)
        {
            MazeGame game = gameManager.GetGame(request.Client);
            if (game == null)
                throw new InvalidOperationException("You did not join any game.");

            game.RemovePlayer(request.Client);
        }

        /// <summary>
        /// returns whether the client given is in game.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool IsInGame(IClient c)
        {
            return gameManager.ContainsGame(c);
        }
    }
}