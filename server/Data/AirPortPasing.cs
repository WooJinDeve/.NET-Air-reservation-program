using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp2
{
    class AirPortPasing
    {
        public AirPortPasing() { }
        public List<AirPort> URLPasing(string date)
        {
            List<AirPort> airPorts = new List<AirPort>();
            string results = string.Empty;

            for (int i = 0; i < 90; i++)
            {
                string msg = string.Format("{0}", i);
                string url = "http://openapi.airport.co.kr/service/rest/FlightScheduleList/getDflightScheduleList"; // URL
                url += "?ServiceKey=" + ""; // Service Key
                url += "&pageNo=" + msg;
                url += "&schDate=" + date;

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";


                HttpWebResponse response;
                using (response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    results = reader.ReadToEnd();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(results);
                    XmlNode node1 = doc.SelectSingleNode("response");
                    XmlNode node2 = node1.SelectSingleNode("body");
                    XmlNode n = node2.SelectSingleNode("items");
                    AirPort air = null;
                    foreach (XmlNode el in n.SelectNodes("item"))
                    {
                        air = MakeAirPort(el, date);
                        airPorts.Add(air);
                    }
                }
            }
            Console.WriteLine("시작");        

            return airPorts;

        }

        private static string ConvertString(string str)
        {
            int sindex = 0;
            int eindex = 0;
            while (true)
            {
                sindex = str.IndexOf('<');
                if (sindex == -1)
                {
                    break;
                }
                eindex = str.IndexOf('>');
                str = str.Remove(sindex, eindex - sindex + 1);
            }
            return str;
        }

        private AirPort MakeAirPort(XmlNode xn, string date)
        {
            string domesticNum = string.Empty;
            string airlineKorean = string.Empty;
            string arrivalcity = string.Empty;
            string startcity = string.Empty;
            string domesticArrivalTime = string.Empty;
            string domesticStartTime = string.Empty;


            XmlNode domesticNum_node = xn.SelectSingleNode("domesticNum");
            domesticNum = ConvertString(domesticNum_node.InnerText);

            XmlNode airlineKorean_node = xn.SelectSingleNode("airlineKorean");
            airlineKorean = ConvertString(airlineKorean_node.InnerText);

            XmlNode arrivalcity_node = xn.SelectSingleNode("arrivalcity");
            arrivalcity = ConvertString(arrivalcity_node.InnerText);

            XmlNode startcity_node = xn.SelectSingleNode("startcity");
            startcity = ConvertString(startcity_node.InnerText);



            XmlNode domesticArrivalTime_node = xn.SelectSingleNode("domesticArrivalTime");
            domesticArrivalTime = ConvertString(domesticArrivalTime_node.InnerText);

            XmlNode domesticStartTime_node = xn.SelectSingleNode("domesticStartTime");
            domesticStartTime = ConvertString(domesticStartTime_node.InnerText);

            DateTime domesticstarttime = DomesticTime(date, domesticStartTime);
            DateTime domesticarrivaltime = DomesticTime(date, domesticArrivalTime);

            return new AirPort(domesticNum, airlineKorean, arrivalcity, startcity, domesticarrivaltime, domesticstarttime);
        }


        private DateTime DomesticTime(string date, string Time)
        {
            DateTime time = Gettime(Time);
            string year = string.Format("{0}{1}{2}{3}", date[0], date[1], date[2], date[3]);
            string month = date[4] + date[5].ToString();
            string day = date[6] + date[7].ToString();

            DateTime DomesticTime = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), time.Hour, time.Minute, time.Second);

            return DomesticTime;
        }

        private DateTime Gettime(string msg)
        {
            string hour = msg[0].ToString() + msg[1].ToString();
            string min = msg[2].ToString() + msg[3].ToString();
            string time = string.Format("{0}:{1}", hour, min);
            return Convert.ToDateTime(time);
        }
    }
}
