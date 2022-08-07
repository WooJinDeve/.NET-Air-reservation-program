using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Packet
    {
        private const string GETAIRPLANEALLLIST = "GetAirPlaneAllList";        //@모드
        private const string GETMEMBERALLLIST = "GetMemberAllList";
        //사용자
        private const string SELECTAIRPORT1 = "SelectAirPort1";    //@중복체크
        private const string SELECTAIRPORT2 = "SelectAirPort2";    //@중복체크
        private const string RESERVATION = "Reservation";
        private const string SELECTCONFIRM = "SelectConfirm";
        public static string GetAirPlaneAllList_Ack(List<AirPort> airports)
        {
            string msg = "";

            airports.ForEach((elem) => {
                msg += elem.DomesticNum +  "#" + elem.AirlineKorean + "#" + elem.Arrivalcity + "#" + elem.Startcity + "#" + elem.DomesticStartTime.ToString() +
                "#" + elem.DomesticArrivalTime.ToString() + "#" + elem.Economy + "#" + elem.Business + "$";
            });

            string packet = string.Format("{0}@{1}", GETAIRPLANEALLLIST, msg);
            return packet;
        }
        public static string GetMemberAllList(List<Member> members)
        {
            string msg = "";

            members.ForEach((elem) => {
                msg += elem.Name + "#" + elem.Phone + "#" + elem.AirPortName + "#" + elem.AirPlaneNum + "#" + elem.Airline + "#" + elem.Date + "#" + elem.Price+"$";
            });

            string packet = string.Format("{0}@{1}", GETMEMBERALLLIST, msg);
            return packet;
        }


        public static string StartSelectAirPort(string msg)
        {
            string packet = string.Format("{0}@{1}", SELECTAIRPORT1, msg);
            return packet;
        }

        public static string EndSelectAirPort(string msg)
        {
            string packet = string.Format("{0}@{1}", SELECTAIRPORT2, msg);
            return packet;
        }
        public static string OnReservation(bool b)
        {
            string packet = string.Format("{0}@{1}", RESERVATION, b);
            return packet;
        }

        public static string OnSelectConfirm(string msg)
        {
            string packet = string.Format("{0}@{1}", SELECTCONFIRM, msg);
            return packet;
        }

    }
}
