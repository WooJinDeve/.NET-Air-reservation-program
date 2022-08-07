using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0413_관리자
{
    class WbControl
    {
        #region 싱글톤 패턴
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

        private WBClient client = new WBClient();
        #endregion

        private MainForm form = null;

        #region Form Load 시 서버 연결
        public void Init(MainForm mainform)
        {
            form = mainform; 
            if (client.Start(SERVER_IP, SERVER_PORT, LogMessage, RecvMessage) == false)
                return;
        }
        #endregion

   
        #region 네트웤 콜백 메서드
        private void LogMessage(LogFlag flag, string msg)
        {
            string temp = string.Format("[{0}] : {1} ({2})", flag, msg, DateTime.Now.ToString());
            if (flag == LogFlag.CONNECT || flag == LogFlag.DISCONNECT)
              
                form.Text = temp;
        }

        private void RecvMessage(string msg)
        {
            string[] sp = msg.Split('@');
            switch (sp[0])
            {
                case "GetAirPlaneAllList": OnGetAirPlaneAllList(sp[1]); break;
                case "GetMemberAllList":    OnGetMemberAllList(sp[1]); break;
            }
        }
        #endregion

        #region 항공기전체정보 요청 - 응답
        public void GetAirPlaneListAll()
        {
            string packet = Packet.GetAirPlaneAllList();
            client.SendData(packet);
        }

        public void OnGetAirPlaneAllList(string msg)
        {      
            List<Airport> airport = GetAirpotAllList(msg);
            form.ListViewPrintAll(airport);
        }
        public List<Airport> GetAirpotAllList(string msg)
        {
            string[] sp1 = msg.Split('$');
            List<Airport> airports = new List<Airport>();
            foreach (string str in sp1)
            {
                if (str == "")
                    return airports;
                string[] data = str.Split('#');
                Airport airport = new Airport(data[0], data[1], data[2], data[3], DateTime.Parse(data[4]), DateTime.Parse(data[5]));
                airports.Add(airport);
            }
          
                return airports;
        }
        #endregion

        #region 회원전체정보 요청 - 응답
        public void GetMemberAllList()
        {
            string packet = Packet.GetMemberAllList();
            client.SendData(packet);
        }

        public void OnGetMemberAllList(string msg)
        {
            List<Member> members = GetmemAllList(msg);
            form.ListView2PrintAll(members);
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
                Member mem = new Member(data[0], data[1], data[2], data[3],data[4], data[5], data[6]);
                members.Add(mem);
            }

            return members;
        }
        #endregion
    }
}
