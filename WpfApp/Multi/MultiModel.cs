using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SharedData;
using WpfApp.Single.Confirm;


namespace WpfApp.Multi
{
    public class MultiModel
    {
        public delegate void Finish();

        public event Finish NotifyFinish;

        public delegate void Notify();

        public event Notify NotifyMessege;
        public string StartInfo { get; set; }
        public bool KeepCom;
        private Communicator com;
        public string FinishMessage { get; private set; }
        public string MessageData { get; private set; }
        private ConfirmWindow confirm;

        public MultiModel(Communicator c, string s)
        {
            KeepCom = true;
            StartInfo = s;
            com = c;
            com.Received += HandleServerMassege;
        }

        public void HandleMyMovement(Key k)
        {
            string s = Movement(k);
            if (!s.Equals("ignore"))
            {
                com.Send("play " + s);
            }
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

        public void StartGame()
        {
            com.Send(StartInfo);
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

        public void HandleClose(string data)
        {
            KeepCom = false;
            FinishMessage = data;
            NotifyFinish?.Invoke();
        }


        private void HandleCommandRes(CommandResult res)
        {
            KeepCom = res.KeepConnection;
            if (res.Command == Command.Close)
            {
                HandleClose(res.Data);
            }
            if (!res.Success)
            {
                //todo handle command fail
            }
        }

        public string Movement(Key k)
        {
            string s;
            switch (k)
            {
                case Key.Up:
                    s = "up";
                    break;
                case Key.Down:
                    s = "down";
                    break;
                case Key.Left:
                    s = "left";
                    break;
                case Key.Right:
                    s = "right";
                    break;
                default:
                    s = "ignore";
                    break;
            }
            return s;
        }

        private void HandleOtherMovement(string s)
        {
            this.MessageData = s;
            NotifyMessege?.Invoke();
        }

        public void BackToMenu()
        {
            confirm = new ConfirmWindow();
            confirm.NotifCancel += HandleCancel;
            confirm.NotifOk += HandleBack;
            confirm.ShowDialog();
        }

        private void HandleBack()
        {
            confirm.Close();
            HandleClose("you quit");
        }

        private void HandleCancel()
        {
            confirm.Close();
        }
    }
}