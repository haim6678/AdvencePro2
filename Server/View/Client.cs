using SharedData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    /// <summary>
    /// a class that handles the communication with
    /// a client.
    /// </summary>
    class Client : IClient
    {
        private TcpClient c;
        private BinaryReader reader;
        private BinaryWriter writer;

        public bool Connected
        {
            get { return c.Connected; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="c">The client tcp.</param>
        public Client(TcpClient c)
        {
            this.c = c;
            NetworkStream stream = c.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
        }


        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            if (Connected)
            {
                try
                {
                    c.Close();
                }
                catch (Exception e)
                {
                }
            }
        }

        /// <summary>
        /// Reads the message.
        /// </summary>
        /// <returns>the massage</returns>
        public string ReadMessage()
        {
            if (!c.Connected)
                throw new InvalidOperationException("Client is not connected.");
            string msg = reader.ReadString();
            return msg;
        }

        /// <summary>
        /// Serializes and sends the given message object
        /// </summary>
        /// <param name="m">The Message to be sent.</param>
        public void WriteMessage(Message m)
        {
            if (!c.Connected)
                throw new InvalidOperationException("Client is not connected.");
            writer.Write(m.ToJSON());
            writer.Flush();
        }

        /// <summary>
        /// Sends the result of a command execution.
        /// </summary>
        /// <param name="result">The result.</param>
        public void SendCommandResult(CommandResult result)
        {
            Message msg = new Message(MessageType.CommandResult, result.ToJSON());
            WriteMessage(msg);
        }

        /// <summary>
        /// Sends a notification to the client.
        /// </summary>
        /// <param name="notif"></param>
        public void SendNotification(Notification notif)
        {
            Message msg = new Message(MessageType.Notification, notif.ToJSON());
            WriteMessage(msg);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> 
        /// to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> 
        /// is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Client);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(Client other)
        {
            if (other == null)
                return false;
            return this.c.Equals(other.c);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing 
        /// algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.c.GetHashCode();
        }

        /// <summary>
        /// returns string representation of the client
        /// </summary>
        /// <returns>The clients IP address</returns>
        public override string ToString()
        {
            return c.Client.RemoteEndPoint.ToString();
        }
    }
}