using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Member
    {
        public string Name { get; set; } // 이름
        public string Phone { get; set; } // 전화번호
        public string AirPortName { get; set; } // 출발 -> 도착 공항
        public string AirPlaneNum { get; set; }     //항공편명
        public string Airline { get; set; } // 항공사
        public string Date { get; set; }
        public string Price { get; set; } // 가격

        public Member() { }

        public Member(string name, string phone, string airportname, string airplanenum, string airline,string date, string price)
        {
            Name = name;
            Phone = phone;
            AirPortName = airportname;
            AirPlaneNum = airplanenum;
            Airline = airline;
            Date = date;
            Price = price;
        }

    }
}
