using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 과제Client
{
    class WbControl
    {
        public ProcessForm processform = null;
        public Confirm confirm = null;
        #region 싱글톤
        private WbControl() { }
        public static WbControl Instance { get; private set; }
        static WbControl()
        {
            Instance = new WbControl();
        }
        #endregion

        #region 네트웤 사용 필드
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 8000;

        private WbClient client = new WbClient();
        #endregion


        #region 네트웤 콜백 메서드
        public void LogMessage(LogFlag flag, string msg)
        {
            /*string temp = string.Format("[{0}] : {1} ({2})", flag, msg, DateTime.Now.ToString());
            if (flag == LogFlag.CONNECT || flag == LogFlag.DISCONNECT);*/

        }

        private void RecvMessage(string msg)
        {
            string[] sp = msg.Split('@');
            switch (sp[0])
            {
                case "SelectAirPort1": SelectsaDay1_Ack(sp[1]); break;
                case "SelectAirPort2": SelectsDay2_Ack(sp[1]); break;
                case "SelectAirPortOneWay": SelectsaDay1_Ack(sp[1]); break;
                case "Reservation": Reservation_Ack(sp[1]); break;
                case "SelectConfirm": SelectConfirm_Ack(sp[1]); break;
            }
        }
        #endregion

        #region Mainform Load시 서버 연결
        public void Init()
        {
           
            if (client.Start(SERVER_IP, SERVER_PORT, LogMessage, RecvMessage) == false)
                return;
        }
        #endregion

        #region Form 연결

        public void GetProcessForm(ProcessForm form)
        {
            processform = form;
        }

        public void GetConfirm(Confirm form)
        {
            confirm = form;
        }
        #endregion


        #region 날짜 검색
        public void SelectAirPort(string Arrive, string Departure, string ArriveDay, string DepartureDay, string ArriveTime, string DepartureTime)
        {
            string packet = Packet.SelectAirPort(Arrive, Departure, ArriveDay, DepartureDay, ArriveTime, DepartureTime);
            client.SendData(packet);
        }
        public void SelectAirPort(string Arrive, string Departure,int temp, string DepartureDay, string ArriveTime)
        {
            string packet = Packet.SelectAirPort(Arrive, Departure, temp,DepartureDay, ArriveTime);
            client.SendData(packet);
        }
        public void SelectsaDay1_Ack(string msg)
        {
            string[] sp = msg.Split('$');         
            processform.ListViewPrintAll1(sp);            
        }

        //왕복시 추가 저장
        public void SelectsDay2_Ack(string msg)
        {
            string[] sp = msg.Split('$');
            processform.ListViewPrintAll2(sp);
        }
        #endregion


        #region 예약
        public void Reservation(string name, string phone, string AirPortName, string AirPlaneNum, string Airline, string Date, string Price)
        {
            string packet = Packet.Reservation(name, phone, AirPortName, AirPlaneNum,  Airline, Date, Price);
            client.SendData(packet);

        }

        public void Reservation_Ack(string msg)
        {
            bool b = bool.Parse(msg);
            processform.ReservationCheck(b);
        }
        #endregion

        #region 예약확인 

        public void SelectConfirm(string name, string phone)
        {
            string packet = Packet.SelectConfirm(name, phone);
            client.SendData(packet);
        }
        
        public void SelectConfirm_Ack(string msg)
        {
            confirm.ListView2PrintAll(GetmemAllList(msg));
        }

        public List<Member> GetmemAllList(string msg)
        {
            string[] sp1 = msg.Split('$');
            List<Member> members = new List<Member>();
            foreach (string str in sp1)
            {
                if (str == "")
                    return members;
                string[] data = str.Split('#');
                Member mem = new Member(data[0], data[1], data[2], data[3], data[4], data[5], data[6]);
                members.Add(mem);
            }

            return members;
        }
        #endregion

    }
}
