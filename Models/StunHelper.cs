using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Do_an.Services
{
    public static class StunHelper
    {
        // Hỏi Google STUN Server để lấy IP và Port công khai
        public static IPEndPoint QueryPublicEndPoint(int localPort)
        {
            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    // [QUAN TRỌNG] Buộc Socket phải dùng cổng 11000 để Router ánh xạ đúng cổng đó
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    socket.Bind(new IPEndPoint(IPAddress.Any, localPort));

                    string stunServer = "stun.l.google.com";
                    int stunPort = 19302;

                    // Phân giải IP của Google STUN
                    IPAddress[] ipAddresses = Dns.GetHostAddresses(stunServer);
                    IPEndPoint stunEndPoint = new IPEndPoint(ipAddresses[0], stunPort);

                    // Tạo gói tin STUN Binding Request đơn giản
                    byte[] request = new byte[] {
                        0x00, 0x01, 0x00, 0x00,
                        0x21, 0x12, 0xA4, 0x42,
                        0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B
                    };

                    socket.SendTo(request, stunEndPoint);

                    byte[] buffer = new byte[1024];
                    EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

                    // Đợi phản hồi trong 1 giây
                    socket.ReceiveTimeout = 1000;
                    int length = socket.ReceiveFrom(buffer, ref remoteEP);

                    // Giải mã phản hồi để lấy IP/Port (XOR-MAPPED-ADDRESS)
                    int i = 20;
                    while (i < length)
                    {
                        int attrType = (buffer[i] << 8) | buffer[i + 1];
                        int attrLen = (buffer[i + 2] << 8) | buffer[i + 3];

                        if (attrType == 0x0020) // 0x0020 là XOR-MAPPED-ADDRESS
                        {
                            int port = (buffer[i + 6] << 8) | buffer[i + 7];
                            port ^= 0x2112 >> 16; // Giải mã Port

                            byte[] ipBytes = new byte[4];
                            ipBytes[0] = (byte)(buffer[i + 8] ^ 0x21);
                            ipBytes[1] = (byte)(buffer[i + 9] ^ 0x12);
                            ipBytes[2] = (byte)(buffer[i + 10] ^ 0xA4);
                            ipBytes[3] = (byte)(buffer[i + 11] ^ 0x42); // Giải mã IP

                            return new IPEndPoint(new IPAddress(ipBytes), port);
                        }
                        i += 4 + attrLen;
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}