using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Control
    {
        #region 싱글톤
        public static Control Instance { get; private set; }
        static Control()
        {
            Instance = new Control();
        }
        private Control() { }
        #endregion

        #region 네트웤 사용 필드
        private const int SERVER_PORT = 8000;
        private WbServer server = null;
        #endregion

        private WbDocument wb = WbDocument.Instance;

        #region 시작과 종료시점(데이터 베이스 연결/소켓 및 종료 처리)
        public bool Init()
        {
            if (wb.Open() == false)
                return false;

            server = new WbServer(SERVER_PORT); //소켓생성--> listen
            server.Start(LogMessage, RecvMessage); //ListenThread
            return true;
        }

        public void Exit()
        {
            wb.Close();

            server.Dispose(); //ListenThread를 종료, 대기소켓close
            server = null;
        }
        #endregion 

        #region 네트웤 콜백 메서드
        private void LogMessage(LogFlag flag, string msg)
        {
            Console.WriteLine("[{0}] : {1} ({2})",
                        flag, msg, DateTime.Now.ToString());
        }

        private void RecvMessage(Socket client, string msg)
        {
            string[] sp = msg.Split('@');
            switch (sp[0])
            {
                //서버 -> 클라이언트(관리자)
                case "GetAirPlaneAllList": OnGetAirPlaneAllList(client); break;
                case "GetMemberAllList":  OnGetMemberAllList(client); break;

                //서버 -> 클라이언트(사용자)
                case "SelectAirPort": OnSelectAirPort1(client, sp[1]); OnSelectAirPort2(client, sp[1]); break;
                case "SelectAirPortOneWay": OnSelectAirPort1(client, sp[1]); break;
                case "Reservation": OnReservation(client, sp[1]); break;
                case "SelectConfirm":  OnSelectConfirm(client, sp[1]); break;
            }
        }
        #endregion

        private void OnGetAirPlaneAllList(Socket client)
        {
            try
            {
                string packet = Packet.GetAirPlaneAllList_Ack(wb.GetAirPlaneAllListAll());
                server.SendData(client, packet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void OnGetMemberAllList(Socket client)
        {
            string packet = Packet.GetMemberAllList(wb.GetMemberAllList());
            server.SendData(client, packet);
        }

        private void OnSelectAirPort1(Socket client, string msg)
        {
            //출발 -> 도착
            string str = wb.StartSelectAirPort(msg);
            string packet = Packet.StartSelectAirPort(str);
            server.SendData(client, packet);


        }
        public void OnSelectAirPort2(Socket client, string msg)
        {
            //도착 -> 출발
            string str = wb.EndSelectAirPort(msg);
            string packet = Packet.EndSelectAirPort(str);
            server.SendData(client, packet);
        }

        private void OnReservation(Socket client,string msg)
        {
            try
            {
                bool b = true;
                if (msg == "")
                {
                    b = false;
                }
                wb.OnReservation(msg);
                string packet = Packet.OnReservation(b);
                server.SendData(client, packet);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void OnSelectConfirm(Socket client, string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                string name = sp[0];
                string phone = sp[1];
                msg = wb.OnSelectConfirm(name, phone);


                string packet = Packet.OnSelectConfirm(msg);
                server.SendData(client, packet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region DB 파싱
        public void Parsing()
        {
            try
            {
                wb.Parsing();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AirPortSave()
        {
            try
            {
                wb.AirPortSave();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AirPortDelete()
        {
            try
            {
                wb.AirPortDelete();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void PrintAll()
        {
            try
            {
                wb.PrintAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion
    }
}
