using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace LetsGoToTalk.Server
{
    /// <summary>
    /// Client manageament logic
    /// </summary>
    public class ServiceClientManager
    {
        /// <summary>
        /// Bag o clients connected on service.
        /// </summary>
        private ConcurrentBag<ServiceClient> serviceClientBag { get; set; }

        public ServiceClientManager()
        {
        }

        public void Add(TcpClient client)
        {

        }

        public void Remove(Guid Id)
        {

        }
    }
}