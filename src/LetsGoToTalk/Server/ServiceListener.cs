using System;
using System.Net;
using System.Threading;

namespace LetsGoToTalk.Server
{
    /// <summary>
    /// Logic implementation for TcpListenner.
    /// </summary>
    public class ServiceListener
    {
        #region Public Fields

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

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// Listenner for acept clients.
        /// </summary>
        private TcpListennerWrapper listenner;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// ServiceListener listenner default constructor.
        /// </summary>
        public ServiceListener(IPEndPoint endPoint)
        {
            int workerThreads;
            int completionPortThreads;
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            ThreadPool.SetMinThreads(workerThreads, completionPortThreads);

            listenner = new TcpListennerWrapper(endPoint);
            // listenner.
            ClientManager = new ServiceClientManager();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Manager for connected clients.
        /// </summary>
        public ServiceClientManager ClientManager { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Start tcp client listenner.
        /// </summary>
        public void Start()
        {
            if (listenner.IsActive == false)
            {
                listenner.Start(1000);
                AcceptClients();
            }
        }

        /// <summary>
        /// Stop listenner to accept clients
        /// </summary>
        public void Stop()
        {
            if (listenner.IsActive)
            {
                listenner.Stop();
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Start process to accept clients.
        /// </summary>
        private void AcceptClients()
        {
            while (listenner.IsActive)
            {
                // Wait for client to accept.
                ServiceClient serviceClient = new ServiceClient(listenner.AcceptTcpClient());

                // Add client do client list.
                ClientManager.Add(serviceClient);

                // Notify client connected.
                this.RiseClientConnected(serviceClient);
            }
        }

        /// <summary>
        /// Invoke Client connected event.
        /// </summary>
        /// <param name="serviceClient">Client that conect on the service.</param>
        private void RiseClientConnected(ServiceClient serviceClient)
        {
            if (this.ClientConnected != null)
            {
                this.ClientConnected.Invoke(this, serviceClient);
            }
        }

        /// <summary>
        /// Invoke Client connected event.
        /// </summary>
        /// <param name="serviceClient">Client that conect on the service.</param>
        private void RiseClientDisconnected(ServiceClient serviceClient)
        {
            if (this.ClientConnected != null)
            {
                this.ClientConnected.Invoke(this, serviceClient);
            }
        }

        /// <summary>
        /// Invoke Client connected event.
        /// </summary>
        /// <param name="serviceClient">Client that conect on the service.</param>
        private void RiseReceivedData(ServiceClient serviceClient, byte[] data, int size)
        {
            if (this.ClientConnected != null)
            {
                this.ClientConnected.Invoke(this, serviceClient);
            }
        }

        #endregion Private Methods
    }
}