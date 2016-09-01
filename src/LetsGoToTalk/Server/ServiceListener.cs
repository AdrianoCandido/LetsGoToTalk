using System;

namespace LetsGoToTalk.Server
{
    /// <summary>
    /// Logic implementation for TcpListenner.
    /// </summary>
    public class ServiceListener
    {
        /// <summary>
        /// Listenner for acept clients.
        /// </summary>
        private TcpListennerWrapper listenner;

        /// <summary>
        /// Service accept client connection.
        /// </summary>
        public EventHandler<ServiceClient> ClientConnected;

        /// <summary>
        /// Client disconnected on the service.
        /// </summary>
        public EventHandler<ServiceClient> ClientDesconnected;

        /// <summary>
        /// Event to client send data to service.
        /// </summary>
        public EventHandler<DataReceivedEventArgs> DataReceived;
    }
}