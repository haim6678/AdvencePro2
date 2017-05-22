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
    class SingleManager
    {
        private string ip;
        private int port;

        public SingleManager(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void Start()
        {
            string cmd = GetCommandFromMenu();
            if (cmd == null)
                return;
            MessageBox.Show(string.Format("Command to send: '{0}'", cmd));

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
        }

        private string GetCommandFromMenu()
        {
            // open multiplayer window
            SingleMenuModel model = new SingleMenuModel();
            SingleMenuVM vm = new SingleMenuVM(model);
            SingleMenu mnu = new SingleMenu(vm);

            string cmd;
            mnu.ShowDialog(out cmd);
            return cmd;
        }
    }
}