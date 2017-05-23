﻿using SharedData;
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

            //MessageBox.Show("Command to send " + cmd);

            // then we have a command to send.
            // create async communicator. send the command, get response for join or start.
            // if unsuccessfull, prompt the user with a message, and return
            // if successfull, unlink the event of asyncComm, send the communicator to
            // the GameModel, and show it.

            // successfully created a game. pass the handle to the GameModel and start.
            try
            {
                using (GameModel gmod = new GameModel(cmd))
                using (GameVM gvm = new GameVM(gmod))
                {
                    GameView gv = new GameView(gvm);
                    gv.ShowDialog();
                }
            }catch(GameNotStartedException e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetCommandFromMenu()
        {
            // open multiplayer window
            MultiMenuModel model = new MultiMenuModel();
            MultiMenuVM vm = new MultiMenuVM(model);
            MultiMenu mnu = new MultiMenu(vm);

            string cmd;
            mnu.ShowDialog(out cmd);
            return cmd;
        }
    }
}
