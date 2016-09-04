using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace LetsGoToTalk.Server
{
    /// <summary>
    /// Client manageament logic
    /// </summary>
    public class ServiceClientManager
    {
        public int ClientCount { get { return serviceClientBag.Count; } }

        #region Public Constructors

        public ServiceClientManager()
        {
            serviceClientBag = new ConcurrentDictionary<int, ServiceClient>();
        }

        #endregion Public Constructors

        #region Private Properties

        /// <summary>
        /// Bag o clients connected on service.
        /// </summary>
        private ConcurrentDictionary<int, ServiceClient> serviceClientBag { get; set; }

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
                throw new ArgumentNullException(nameof(client));
            }

            serviceClientBag.TryAdd(client.Id, client);
        }

        /// <summary>
        /// Remove tcp client on the service.
        /// </summary>
        /// <param name="Id">Id of the client to remove.</param>
        public void Remove(int Id)
        {
            ServiceClient serviceClient;
            serviceClientBag.TryRemove(Id, out serviceClient);
        }

        /// <summary>
        /// Return client enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ServiceClient> GetEnumerator()
        {
            foreach (var client in this.serviceClientBag)
            {
                yield return client.Value;
            }
        }

        #endregion Public Methods
    }
}