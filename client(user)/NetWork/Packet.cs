using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 과제Client
{
    class Packet
    {
        #region
        //--[Client --> Server]
        private const string SELECTAIRPORT = "SelectAirPort";
        private const string SELECTAIRPORTONEWAY = "SelectAirPortOneWay";
        private const string RESERVATION = "Reservation";
        private const string SELECTCONFIRM = "SelectConfirm";
        public static string SelectAirPort(string Arrive, string Departure, string ArriveDay,
            string DepartureDay, string ArriveTime, string DepartureTime)
        {
            string packet = string.Format("{0}@{1}#{2}#{3}#{4}#{5}#{6}", SELECTAIRPORT, Arrive, Departure, ArriveDay, DepartureDay, ArriveTime, DepartureTime);

            return packet;
        }

        public static string SelectAirPort(string Arrive, string Departure, int temp, string DepartureDay, string ArriveTime)
        {
            string packet = string.Format("{0}@{1}#{2}#{3}#{4}#{5}", SELECTAIRPORTONEWAY, Arrive, Departure, temp, DepartureDay, ArriveTime);

            return packet;
        }


        public static string Reservation(string name, string phone, string AirPortName, string AirPlaneNum, string Airline, string Date, string Price)
        {
            string packet = string.Format("{0}@{1}#{2}#{3}#{4}#{5}#{6}#{7}", RESERVATION, name, phone, AirPortName, AirPlaneNum, Airline, Date, Price);
            return packet;
        }

        public static string SelectConfirm(string name, string phone)
        {
            string packet = string.Format("{0}@{1}#{2}", SELECTCONFIRM, name, phone);

            return packet;
        }
        #endregion
    }
}
