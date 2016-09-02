using System;
using System.Collections.Concurrent;

namespace LetsGoToTalk.Server
{
    /// <summary>
    /// Client manageament logic
    /// </summary>
    public class ServiceClientManager
    {
        #region Public Constructors

        public ServiceClientManager()
        {
            serviceClientBag = new ConcurrentDictionary<long, ServiceClient>();
        }

        #endregion Public Constructors

        #region Private Properties

        /// <summary>
        /// Bag o clients connected on service.
        /// </summary>
        private ConcurrentDictionary<long, ServiceClient> serviceClientBag { get; set; }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Add tcp client to server.
        /// </summary>
        /// <param name="client">Tcp client to add.</param>
        public void Add(ServiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }

            serviceClientBag.TryAdd(client.Id, client);
        }

        /// <summary>
        /// Remove tcp client on the service.
        /// </summary>
        /// <param name="Id">Id of the client to remove.</param>
        public void Remove(long Id)
        {
            ServiceClient serviceClient;
            serviceClientBag.TryRemove(Id, out serviceClient);
        }

        #endregion Public Methods
    }
}