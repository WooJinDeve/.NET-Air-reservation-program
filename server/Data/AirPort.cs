using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class AirPort
    {
        public string DomesticNum { get; set; } // 항공편명
        public string AirlineKorean { get; set; } // 항공사
  
        public string Arrivalcity { get; set; } // 도착공항
        public string Startcity { get; set; } // 출발공항
        public DateTime DomesticArrivalTime { get; set; } // 출발공항
        public DateTime DomesticStartTime { get; set; } // 예정시간        
        public int Economy { get; set; }
        public int Business { get; set; }

        public AirPort() { }

        public AirPort(string domesticNum, string airlineKorean, string arrivalcity, string startcity, DateTime domesticArrivalTime, DateTime domesticStartTime)
        {
            DomesticNum = domesticNum;
            AirlineKorean = Replace(airlineKorean);
            Arrivalcity = arrivalcity;
            Startcity = startcity;
            DomesticArrivalTime = domesticArrivalTime;
            DomesticStartTime = domesticStartTime;
            Price();
        }


        private string Replace(string msg)
        {
            string replace = "'";

            msg = msg.Replace(replace, string.Empty);

            return msg;
        }

        private void Price()
        {
            if (Startcity == "서울/김포")
            {
                Economy = 80000; Business = 160000;
            }
            else if (Startcity == "제주")
            {
                Economy = 68000; Business = 136000;
            }
            else if (Startcity == "울산")
            {
                Economy = 40800; Business = 84000;
            }
            else if (Startcity == "청주")
            {
                Economy = 51000; Business = 102000;
            }
            else if (Startcity == "부산/김해")
            {
                Economy = 55000; Business = 110000;
            }
            else if (Startcity == "광주")
            {
                Economy = 38000; Business = 76000;
            }
            else if (Startcity == "무안")
            {
                Economy = 44000; Business = 88000;
            }
            else if (Startcity == "군산")
            {
                Economy = 54000; Business = 108000;
            }
            else if (Startcity == "여수")
            {
                Economy = 57000; Business = 104000;
            }
            else if (Startcity == "대구")
            {
                Economy = 60000; Business = 120000;
            }
            else if (Startcity == "양양")
            {
                Economy = 40000; Business = 80000;
            }
            else if (Startcity == "진주/사천")
            {
                Economy = 42000; Business = 84000;
            }
            else if (Startcity == "원주")
            {
                Economy = 24000; Business = 48000;
            }
            else if (Startcity == "포항")
            {
                Economy = 32000; Business = 62000;
            }
        }
    }
}
