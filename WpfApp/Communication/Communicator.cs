using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Communication
{
    public class Communicator : IDisposable
    {
        public delegate void DataReceivedEventHandler(string data);
        public event DataReceivedEventHandler DataReceived;

        private TcpClient client;
        private BinaryReader reader;
        private BinaryWriter writer;
        private volatile bool listening;

        public Communicator(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            client.Connect(ep);

            reader = new BinaryReader(client.GetStream());
            writer = new BinaryWriter(client.GetStream());

            listening = false;
        }

        public string ReadMessage()
        {
            if (listening)
                throw new InvalidOperationException("Can not read message while listening to messages");
            return reader.ReadString();
        }

        public void SendMessage(string msg)
        {
            writer.Write(msg);
        }

        public void StartListening()
        {
            if (listening)
                return;

            listening = true;
            new Task(Listen).Start();
        }

        public void StopListening()
        {
            listening = false;
        }

        private void Listen()
        {
            while (listening)
            {
                try
                {
                    string msg = reader.ReadString();
                    DataReceived?.Invoke(msg);
                }
                catch (Exception e)
                {
                    listening = false;
                    Console.WriteLine(e.Message);
                    break;
                }
            }
        }

        public void Close()
        {
            Dispose();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    reader.Dispose();
                    writer.Dispose();
                    client.Close();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Communicator() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
