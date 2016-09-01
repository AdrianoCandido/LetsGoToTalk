using LetsGoToTalk.Server;
using System;

namespace LetsGoToTalk
{
    public class DataReceivedEventArgs
    {
        public ServiceClient ServiceClient { get; set; }
        public byte[] Data { get; }

        public void Reply(byte[] data)
        {
        }
    }
}