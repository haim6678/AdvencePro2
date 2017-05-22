using SharedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Communication;
using WpfApp.Multi.Game;
using WpfApp.Multi.Menu;

namespace WpfApp.Multi
{
    class MultiManager
    {
        private string ip;
        private int port;
        // operation: open menu. get needed operation.
        // then 

        public MultiManager(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void Start()
        {
            string cmd = GetCommandFromMenu();
            // if user did not start or join a game
            if (cmd == null)
                return;

            MessageBox.Show("Command to send " + cmd);
            return;

            // then we have a command to send.
            // create async communicator. send the command, get response for join or start.
            // if unsuccessfull, prompt the user with a message, and return
            // if successfull, unlink the event of asyncComm, send the communicator to
            // the GameModel, and show it.

            Communicator com = new Communicator(ip, port);
            com.SendMessage(cmd);
            cmd = com.ReadMessage();
            Message m = Message.FromJSON(cmd);

            if (m.MessageType != MessageType.CommandResult)
            {
                // received notification when not expecting it.
                com.Dispose();
                return;
            }

            CommandResult res = CommandResult.FromJSON(m.Data);
            // filter unwanted commands
            switch (res.Command)
            {
                case Command.Start:
                case Command.Join:
                    break;
                default:
                    // received different response for some reason
                    com.Dispose();
                    return;
            }

            if (!res.Success)
            {
                // failde to start / join a game
                com.Dispose();
                MessageBox.Show(res.Data);
                return;
            }

            // successfully created a game. pass the handle to the GameModel and start.
            GameModel gmod = new GameModel(com);
            GameVM gvm = new GameVM(gmod);
            GameView gv = new GameView(gvm);

            gv.ShowDialog();
        }

        private string GetCommandFromMenu()
        {
            // open multiplayer window
            MultiMenuModel model = new MultiMenuModel(ip, port);
            MultiMenuVM vm = new MultiMenuVM(model);
            MultiMenu mnu = new MultiMenu(vm);

            string cmd;
            mnu.ShowDialog(out cmd);
            return cmd;
        }
    }
}
