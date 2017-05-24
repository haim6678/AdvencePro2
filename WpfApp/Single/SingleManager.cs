using MazeLib;
using SharedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using WpfApp.Communication;
using WpfApp.Other;
using WpfApp.Single.Menu;

namespace WpfApp.Single
{
    /// <summary>
    ///  single player managing class
    /// </summary>
    class SingleManager
    {
        /// <summary>
        /// The ip
        /// </summary>
        private string ip;
        /// <summary>
        /// The port
        /// </summary>
        private int port;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleManager"/> class.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public SingleManager(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            string cmd = GetCommandFromMenu();
            if (cmd == null)
                return;
            //MessageBox.Show(string.Format("Command to send: '{0}'", cmd));

            try
            {

                Communicator com = new Communicator(ip, port);
                com.SendMessage(cmd);
                cmd = com.ReadMessage();
                com.Dispose();

                Message m = Message.FromJSON(cmd);
                if (m.MessageType != MessageType.CommandResult)
                {
                    // received notification when not expecting it.
                    return;
                }

                CommandResult res = CommandResult.FromJSON(m.Data);
                // filter unwanted commands
                if (res.Command != Command.Generate)
                    return;

                if (!res.Success)
                {
                    MessageBox.Show(res.Data);
                    return;
                }

                Maze maze = Maze.FromJSON(res.Data);

                // successfully created a game. pass the handle to the GameModel and start.
                SinglePlayerModel model = new SinglePlayerModel(maze);
                SinglePlayerVM vm = new SinglePlayerVM(model);
                SinglePlayerView view = new SinglePlayerView(vm);

                view.ShowDialog();
            }catch(Exception e)
            {
                MessageBox.Show("Error connecting to the server!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Gets the command from menu.
        /// </summary>
        /// <returns></returns>
        private string GetCommandFromMenu()
        {
            try
            {
                // open multiplayer window
                SingleMenuModel model = new SingleMenuModel();
                SingleMenuVM vm = new SingleMenuVM(model);
                SingleMenu mnu = new SingleMenu(vm);

                string cmd;
                mnu.ShowDialog(out cmd);
                return cmd;
            }catch(Exception e)
            {
                MessageBox.Show("Error connecting to the server!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            
        }
    }
}