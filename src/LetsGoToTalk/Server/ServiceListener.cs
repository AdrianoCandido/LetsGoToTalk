using System;
using System.Net;
using System.Net.Sockets;

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
        public event EventHandler<ServiceClient> ClientConnected;

        /// <summary>
        /// Client disconnected on the service.
        /// </summary>
        public event EventHandler<ServiceClient> ClientDesconnected;

        /// <summary>
        /// Event to client send data to service.
        /// </summary>
        public event EventHandler<DataReceivedEventArgs> DataReceived;

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// Listenner for acept clients.
        /// </summary>
        private TcpListennerWrapper listenner;

        /// <summary>
        /// Buffer to read data from clients
        /// </summary>
        private byte[] buffer;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// ServiceListener listenner default constructor.
        /// </summary>
        public ServiceListener(IPEndPoint endPoint)
        {
            this.ReaderBufferSize = 1024;
            this.buffer = new byte[ReaderBufferSize];
            this.listenner = new TcpListennerWrapper(endPoint);
            this.ClientManager = new ServiceClientManager();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Manager for connected clients.
        /// </summary>
        public ServiceClientManager ClientManager { get; set; }

        /// <summary>
        /// Size of buffer to read client data.
        /// </summary>
        public int ReaderBufferSize { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Start tcp client listenner.
        /// </summary>
        public void Start()
        {
            if (listenner.IsActive == false)
            {
                listenner.Start();
                StartServiceProcess();
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
        /// Accept client on the service.
        /// </summary>
        private void AcceptClient()
        {
            // Verify pending connections.
            if (listenner.Pending() == false)
            {
                return;
            }

            // Wait for client to accept.
            ServiceClient serviceClient = new ServiceClient(listenner.AcceptTcpClient());

            // Add client do client list.
            ClientManager.Add(serviceClient);

            // Notify client connected.
            this.RiseClientConnected(serviceClient);
        }

        /// <summary>
        /// Disconnect client from the server.
        /// </summary>
        /// <param name="client"></param>
        private void DisconnectClient(ServiceClient client)
        {
            this.ClientManager.Remove(client.Id);

            try
            {
                client.TcpClient.Close();
            }
            catch
            {
            }

            this.RiseClientDisconnected(client);
        }

        private void ReadClientData(ServiceClient client)
        {
            try
            {
                int read = client.TcpClient.Client.Receive(buffer, 0, buffer.Length, SocketFlags.None);

                if (read > 0)
                {
                    byte[] bufferRead = new byte[read];

                    Array.Copy(buffer, bufferRead, read);

                    // Invoke data reveive event.
                    this.RiseReceivedData(client, bufferRead, read);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Receive data for connected clients.
        /// </summary>
        private void ReceiveCLientsData()
        {
            foreach (ServiceClient client in this.ClientManager)
            {
                bool connected = client.TcpClient.IsConnected();
                bool dataAvaliable = client.TcpClient.Available > 0;

                if (connected && dataAvaliable)
                {
                    this.ReadClientData(client);
                }
                else
                if (connected == false)
                {
                    this.DisconnectClient(client);
                }
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
            if (this.ClientDesconnected != null)
            {
                this.ClientDesconnected.Invoke(this, serviceClient);
            }
        }

        /// <summary>
        /// Invoke Client connected event.
        /// </summary>
        /// <param name="serviceClient">Client that conect on the service.</param>
        private void RiseReceivedData(ServiceClient serviceClient, byte[] data, int size)
        {
            if (this.DataReceived != null)
            {
                this.DataReceived.Invoke(this, new DataReceivedEventArgs(serviceClient, data, size));
            }
        }

        /// <summary>
        /// Start process to accept clients.
        /// </summary>
        private void StartServiceProcess()
        {
            while (listenner.IsActive)
            {
                this.AcceptClient();
                this.ReceiveCLientsData();
            }
        }

        #endregion Private Methods
    }
}