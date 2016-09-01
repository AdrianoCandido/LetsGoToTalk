using System;
using System.Net.Sockets;

namespace LetsGoToTalk.Server
{
    /// <summary>
    /// Encapsulate the service  client
    /// </summary>
    public class ServiceClient
    {
        /// <summary>
        /// Instance o client
        /// </summary>
        public TcpClient TcpClient { get; private set; }

        /// <summary>
        /// Identification of the client on server.
        /// </summary>
        public Guid Id { get; private set; }
    }
}