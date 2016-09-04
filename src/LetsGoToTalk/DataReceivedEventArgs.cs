using LetsGoToTalk.Server;
using System;

namespace LetsGoToTalk
{
    /// <summary>
    /// Event rise on server receive data.
    /// </summary>
    public class DataReceivedEventArgs
    {
        /// <summary>
        /// Create data received event instance with especifiend parameters.
        /// </summary>
        /// <param name="client">Client that send the data.</param>
        /// <param name="buffer">Buffer reference used to receive client data.</param>
        /// <param name="receivedLenght">length of data received from client.</param>
        public DataReceivedEventArgs(ServiceClient client, byte[] buffer, int receivedLenght)
        {
            if (buffer == null)
            {
                throw new ArgumentException(nameof(buffer));
            }

            if (client == null)
            {
                throw new ArgumentException(nameof(client));
            }

            this.ServiceClient = client;
            this.Buffer = buffer;
            this.DataLeght = receivedLenght;
        }

        #region Public Properties

        /// <summary>
        /// Array of bytes sent by client.
        /// </summary>
        public byte[] Buffer { get; private set; }

        /// <summary>
        /// Leght of bytes received from client.
        /// </summary>
        public int DataLeght { get; set; }

        /// <summary>
        /// Rerence of client that send the data.
        /// </summary>
        public ServiceClient ServiceClient { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Reply to the client.
        /// </summary>
        /// <param name="data"></param>
        public void Reply(byte[] data)
        {
            try
            {
                ServiceClient.TcpClient.Client.Send(data);
            }
            catch
            {
            }
        }

        #endregion Public Methods
    }
}