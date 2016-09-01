using System;
using System.Net;
using System.Net.Sockets;

namespace LetsGoToTalk.Server
{
    /// <summary>
    /// Wrapper to encapsulate <see cfref="System.Net.Sockets.TcpListener" />
    /// </summary>
    public class TcpListennerWrapper : TcpListener
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener"/> class with the specified local endpoint.
        /// </summary>
        /// <param name="localEP">An <see cref="T:System.Net.IPEndPoint"/> that represents the local endpoint to which to bind the listener <see cref="T:System.Net.Sockets.Socket"/>. </param><exception cref="T:System.ArgumentNullException"><paramref name="localEP"/> is null. </exception>
        public TcpListennerWrapper(IPEndPoint localEP) : base(localEP) { }

        /// <summary>
        /// Gets a value that indicates whether TcpListener is actively listening for client connections.
        /// </summary>
        public bool IsActive
        {
            get { return base.Active; }
        }
    }
}