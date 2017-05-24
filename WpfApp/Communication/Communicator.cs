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
    /// <summary>
    /// in charge of talking to the server
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class Communicator : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">The data.</param>
        public delegate void DataReceivedEventHandler(string data);
        /// <summary>
        /// Occurs when [data received].
        /// </summary>
        public event DataReceivedEventHandler DataReceived;

        /// <summary>
        /// The client
        /// </summary>
        private TcpClient client;
        /// <summary>
        /// The reader
        /// </summary>
        private BinaryReader reader;
        /// <summary>
        /// The writer
        /// </summary>
        private BinaryWriter writer;
        /// <summary>
        /// The listening
        /// </summary>
        private volatile bool listening;

        /// <summary>
        /// Initializes a new instance of the <see cref="Communicator"/> class.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public Communicator(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            client.Connect(ep);

            reader = new BinaryReader(client.GetStream());
            writer = new BinaryWriter(client.GetStream());

            listening = false;
        }

        /// <summary>
        /// Reads the message.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Can not read message while listening to messages</exception>
        public string ReadMessage()
        {
            if (listening)
                throw new InvalidOperationException("Can not read message while listening to messages");
            return reader.ReadString();
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public void SendMessage(string msg)
        {
            writer.Write(msg);
        }

        /// <summary>
        /// Starts the listening.
        /// </summary>
        public void StartListening()
        {
            if (listening)
                return;

            listening = true;
            new Task(Listen).Start();
        }

        /// <summary>
        /// Stops the listening.
        /// </summary>
        public void StopListening()
        {
            listening = false;
        }

        /// <summary>
        /// Listens this instance.
        /// </summary>
        private void Listen()
        {
            while (listening)
            {
                try
                {
                    string msg = reader.ReadString();
                    DataReceived?.Invoke(msg);
                }
                catch (Exception e) when (e is IOException || e is EndOfStreamException || e is ObjectDisposedException)
                {
                    listening = false;
                    Console.WriteLine(e.Message);
                    break;
                }
            }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        #region IDisposable Support
        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
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
