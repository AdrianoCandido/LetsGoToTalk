using System.Net.Sockets;

namespace LetsGoToTalk
{
    /// <summary>
    /// Extension utils for tcp client,
    /// </summary>
    public static class TcpClientExtensions
    {
        /// <summary>
        /// Checks whether a given TCP client is connected.
        /// </summary>
        /// <param name="client">Instance of the TCP client.</param>
        /// <returns>Whether the TCP client is connected.</returns>
        static public bool IsConnected(this TcpClient client)
        {
            // We consider 'null' to be disconnected.
            if (client == null)
            {
                return false;
            }

            try
            {
                // If the socket is 'null', the client was already disposed.
                if (client.Client == null)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return IsConnected(client.Client);
        }

        /// <summary>
        /// Checks whether a given socket is connected.
        /// </summary>
        /// <param name="client">Instance of the socket.</param>
        /// <returns>Whether the socket is connected.</returns>
        static public bool IsConnected(this Socket client)
        {
            // We consider 'null' to be disconnected.
            if (client == null)
            {
                return false;
            }

            try
            {
                // Check the client state.
                if (client.Connected == false)
                {
                    return false;
                }

                // If we try to send empty data, an exception is thrown if the socket is not connected.
                client.Send(new byte[0] { });
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}