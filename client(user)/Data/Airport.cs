using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 과제Client
{
    class Airport
    {
        public string DomesticNum { get; set; } // 항공편명
        public string AirlineKorean { get; set; } // 항공사

        public string Arrivalcity { get; set; } // 도착공항
        public string Startcity { get; set; } // 출발공항
        public DateTime DomesticArrivalTime { get; set; } // 출발공항
        public DateTime DomesticStartTime { get; set; } // 예정시간        
        public int Economy { get; set; }
        public int Business { get; set; }

        public Airport() { }

        public Airport(string domesticNum, string airlineKorean, string arrivalcity, string startcity, DateTime domesticArrivalTime, DateTime domesticStartTime, int economy, int business)
        {
            DomesticNum = domesticNum;
            AirlineKorean = airlineKorean;
            Arrivalcity = arrivalcity;
            Startcity = startcity;
            DomesticArrivalTime = domesticArrivalTime;
            DomesticStartTime = domesticStartTime;
            Economy = economy;
            Business = business;
        }
    }

}
