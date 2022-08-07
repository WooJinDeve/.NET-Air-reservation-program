using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class WbDocument
    {
        #region 싱글톤 패턴
        private WbDocument()
        {
        }
        public static WbDocument Instance { get; private set; }
        static WbDocument()
        {
            Instance = new WbDocument();
        }
        #endregion

        #region DB연결정보
        private const string server = "";
        private const string database = "";
        private const string id = "";
        private const string pw = "";
        #endregion

        private WbDB db = new WbDB();

        #region DB연결(WbDocument의 생성자) 및 해제(WbDocument의 Dispose)

        public bool Open()
        {
            return db.Open(server, database, id, pw);
        }

        public void Close()
        {
            db.Close();
        }

        #endregion

        private AirPortPasing ap = new AirPortPasing();
        List<AirPort> airports = new List<AirPort>();

        #region 클라이언트 요청 작업
        public List<AirPort> GetAirPlaneAllListAll()
        {
            List<AirPort> airport = new List<AirPort>();
            string sql = WbQuery.GetAirPlaneAllListAll();
            string msg = db.CommandQuery(sql);

            string[] fp = msg.Split('$');
            foreach(string fp1 in fp)
            {
                if (fp1 == "")
                    return airport;
                string[] fp2 = fp1.Split('#');

                AirPort ar = new AirPort(fp2[1], fp2[2], fp2[3], fp2[4], DateTime.Parse(fp2[5]),DateTime.Parse(fp2[6]));
                airport.Add(ar);
            }
            
            return airport;
        }

        public List<Member> GetMemberAllList()
        {
            List<Member> members = new List<Member>();
            string sql = WbQuery.GetMemberAllList();
            string msg = db.CommandQuery(sql);

            string[] fp = msg.Split('$');
            foreach (string fp1 in fp)
            {
                if (fp1 == "")
                    return members;
                string[] fp2 = fp1.Split('#');

                Member ar = new Member(fp2[0],fp2[1], fp2[2], fp2[3], fp2[4], fp2[5],fp2[6]);
                members.Add(ar);
            }

            return members;
        }

        public string StartSelectAirPort(string msg)
        {
            string[] sp = msg.Split('#');
            string sql = WbQuery.StartSelectAirPort(sp[0], sp[1], sp[3], sp[4]);
            string str = db.CommandQuery(sql);

            return str;
        }
        
        public string EndSelectAirPort(string msg)
        {
            string[] sp = msg.Split('#');
            string sql = WbQuery.StartSelectAirPort(sp[1], sp[0], sp[2], sp[5]);
            string str = db.CommandQuery(sql);

            return str;
        }

        public void OnReservation(string msg)
        {         
            string[] sp = msg.Split('#');
            string sql = WbQuery.OnReservation(sp);
            db.CommandNonQuery(sql);
        }

        public string OnSelectConfirm(string name, string phone)
        {
            string sql = WbQuery.OnSelectConfirm(name, phone);
            string msg = db.CommandQuery(sql);
            return msg;

        }
        #endregion

        #region 서버작업
        public void Parsing()
        {
            try
            {
                Console.Write("데이터 파싱 날짜(EX:20220501) : ");
                string date = Console.ReadLine();
                airports = ap.URLPasing(date);
                Console.WriteLine("파싱성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AirPortSave()
        {
            foreach (var airport in airports)
            {
                string sql = WbQuery.AirPortSave(airport);
                db.CommandNonQuery(sql);
            }
            Console.WriteLine("저장성공");
        }

        public void AirPortDelete()
        {
            string sql = WbQuery.AirPortDelete();
            db.CommandNonQuery(sql);
            Console.WriteLine("삭제성공");
        }

        public void PrintAll()
        {

            List<AirPort> airPorts = GetAirPlaneAllListAll();

            foreach (AirPort airPort in airPorts)
            {
                Console.Write("항공편 :{0}", airPort.DomesticNum);
                Console.Write("  항공사 :{0}", airPort.AirlineKorean);
                Console.Write("  출발공항 :{0}", airPort.Startcity);
                Console.Write("  도착공항 :{0}", airPort.Arrivalcity);
                Console.WriteLine(" 도착시간 :{0}", airPort.DomesticArrivalTime);
                Console.WriteLine("  예정시간 :{0}", airPort.DomesticStartTime);
            }

        }
        #endregion

    }
}
