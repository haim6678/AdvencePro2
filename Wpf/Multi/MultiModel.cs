using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedData;
using Message = System.Windows.Forms.Message;

namespace Wpf.Multi
{
    public class MultiModel
    {
        public delegate void Finish();

        public event Finish NotifyFinish;

        public delegate void Notify();

        public event Notify NotifyMessege;

        public bool KeepCom;
        private Communicator com;
        public string FinishMessage { get; private set; }
        public string MessageData { get; private set; }

        public MultiModel(Communicator c)
        {
            KeepCom = true;
            com = c;
            com.Received += HandleServerMassege;
        }

        public void HandleMyMovement(string s)
        {
            com.Send("play" + " " + s);
        }

        public void StartListening()
        {
            Task t = new Task(() =>
            {
                while (KeepCom)
                {
                    com.Listen();
                }
            });
            t.Start();
        }

        public void StartGame(string s)
        {
            com.Send(s);
            StartListening();
        }

        public void JoinGame(string s)
        {
            com.Send("join" + " " + s);
            StartListening();
        }

        private void HandleServerMassege(string s)
        {
            SharedData.Message msg = SharedData.Message.FromJSON(s);
            if (msg.MessageType == MessageType.CommandResult)
            {
                CommandResult result = CommandResult.FromJSON(msg.Data);
                HandleCommandRes(result);
            }
            else if (msg.MessageType == MessageType.Notification)
            {
                Notification notif = Notification.FromJSON(msg.Data);
                switch (notif.NotificationType)
                {
                    case Notification.Type.GameOver:
                        HandleClose(notif.Data);
                        break;
                    case Notification.Type.PlayerMoved:
                        HandleOtherMovement(notif.Data);
                        break;
                    case Notification.Type.GameStarted:
                        HandleStart(notif.Data);
                        break;
                    default:
                        break;
                }
            }
        }

        private void HandleStart(string s)
        {
            this.MessageData = s;
            NotifyMessege?.Invoke();
        }

        private void HandleClose(string data)
        {
            KeepCom = false;
            NotifyFinish?.Invoke();
        }

        private void HandleCommandRes(CommandResult res)
        {
            KeepCom = res.KeepConnection;
            if (res.Command == Command.Close)
            {
                FinishMessage = res.Data; //todo what is the data in this case
                //todo need to check if play command failed?
                NotifyFinish?.Invoke();
            }
        }

        private void HandleOtherMovement(string s)
        {
           NotifyMessege?.Invoke();
        }
    }
}