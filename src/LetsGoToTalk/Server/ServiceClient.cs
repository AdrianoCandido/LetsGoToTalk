using System;
using System.Net.Sockets;
using System.Threading;

namespace LetsGoToTalk.Server
{
    /// <summary>
    /// Encapsulate the service  client
    /// </summary>
    public class ServiceClient
    {
        #region Private Fields

        private static long connectionCount = 0;

        #endregion Private Fields

        #region Public Constructors

        public ServiceClient(TcpClient client)
        {
            this.TcpClient = client;
            this.Id = Interlocked.Increment(ref connectionCount);
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Identification of the client on server.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Instance o client
        /// </summary>
        public TcpClient TcpClient { get; private set; }

        #endregion Public Properties
    }
}