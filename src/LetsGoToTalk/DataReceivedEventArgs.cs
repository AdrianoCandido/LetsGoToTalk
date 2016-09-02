using LetsGoToTalk.Server;
using System;

namespace LetsGoToTalk
{
    /// <summary>
    /// Event rise on server receive data.
    /// </summary>
    public class DataReceivedEventArgs
    {
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
        }

        #endregion Public Methods
    }
}