using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 과제Client
{
    enum LogFlag { NONE, CONNECT, DISCONNECT, ERROR };

    delegate void LogMessage(LogFlag flag, string msg);
    delegate void RecvMessage(string msg);

    class WbClient : IDisposable
    {
        private LogMessage LogMessage = null;
        private RecvMessage RecvMessage = null;

        private Socket sock = null;

        private const int BUFF_SIZE = 1024;

        private Thread t = null;

        public WbClient()
        {
            CreateSocket();
        }

        private void CreateSocket()
        {
            sock = new Socket(AddressFamily.InterNetwork,
                                    SocketType.Stream, ProtocolType.Tcp);

        }

        public bool Start(string server_ip, int server_port, LogMessage log, RecvMessage rmsg)
        {
            LogMessage = log;
            RecvMessage = rmsg;

            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(server_ip), server_port);
                sock.Connect(ipep);

                LogMessage(LogFlag.CONNECT, "서버에 접속...");

                t = new Thread(WorkThread);
                t.IsBackground = true;
                t.Start();

                return true;
            }
            catch (Exception)
            {
                LogMessage(LogFlag.ERROR, "연결오류");
                return false;
            }
        }

        #region  통신(송수신)

        public int SendData(string msg)
        {
            byte[] arr = Encoding.Default.GetBytes(msg);
            int ret = SendData(sock, arr, arr.Length);
            return ret;
        }

        //Recv만 처리
        private void WorkThread()
        {
            try
            {
                while (true)
                {
                    byte[] data = null;
                    if (ReceiveData(sock, ref data) == 0)
                        throw new Exception("오류");
                    // data = new byte[BUFF_SIZE];
                    //  sock.Receive(data, data.Length, SocketFlags.None);
                    RecvMessage(Encoding.Default.GetString(data).Trim('\0'));
                }
            }
            catch (Exception)
            {
                IPEndPoint ip = (IPEndPoint)sock.RemoteEndPoint;
                LogMessage(LogFlag.DISCONNECT, "연결해제");

                sock.Close();
            }
        }

        private int SendData(Socket client, byte[] data, int length)
        {
            try
            {
                int total = 0;
                int size = length; //보낼크기
                int left_data = size;
                int send_data = 0;

                // 전송할 데이터의 크기 전달
                byte[] data_size = new byte[4];
                data_size = BitConverter.GetBytes(size);
                send_data = client.Send(data_size);

                // 실제 데이터 전송
                while (total < size)
                {
                    send_data = client.Send(data, total, left_data, SocketFlags.None);
                    total += send_data;
                    left_data -= send_data;
                }
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        private int ReceiveData(Socket client, ref byte[] data)
        {
            try
            {
                int total = 0;  //실제 받은 크기
                int size = 0;   //수신할 크기
                int left_data = 0;
                int recv_data = 0;

                // 수신할 데이터 크기 알아내기 
                byte[] data_size = new byte[4];
                recv_data = client.Receive(data_size, 0, 4, SocketFlags.None);
                size = BitConverter.ToInt32(data_size, 0);
                left_data = size;

                data = new byte[size];

                // 실제 데이터 수신
                while (total < size)
                {
                    recv_data = client.Receive(data, total, left_data, 0);
                    if (recv_data == 0) break;
                    total += recv_data;
                    left_data -= recv_data;
                }
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        #endregion

        public void Dispose()
        {
            LogMessage(LogFlag.DISCONNECT, "클라이언트 종료......");
            //tr.Abort();
            t.Interrupt();
            if (sock.Connected == true)
                sock.Close();
        }
    }
}
