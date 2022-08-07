using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class WbQuery
    {

        public static string GetAirPlaneAllListAll()
        {
            string sql = string.Format("select * from AirPlane");
            return sql;
        }
        public static string GetMemberAllList()
        {
            string sql = string.Format("select * from Member");
            return sql;
        }


        public static string AirPortSave(AirPort airport)
        {

            string sql = string.Format("insert into AirPlane(DomesticNum, AirlineKorean, Arrivalcity,Startcity,DomesticArrivalTime,DomesticStartTime,Economy,Business) " +
                "values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7})", airport.DomesticNum, airport.AirlineKorean, airport.Arrivalcity, airport.Startcity,
                airport.DomesticArrivalTime.ToString("MM/dd/yyyy HH:mm:ss"), airport.DomesticStartTime.ToString("MM/dd/yyyy HH:mm:ss"), airport.Economy, airport.Business);
            return sql;
        }

        public static string AirPortDelete()
        {
            string sql = string.Format(" DELETE FROM AirPlane");
            return sql;
        }

        public static string StartSelectAirPort(string arrivalcity, string startcity, string DomesticStarttime, string starttime)
        {
            string sql = string.Format("SELECT * FROM AirPlane WHERE(Arrivalcity = '{0}' And Startcity = '{1}') And " +
                 "(DomesticStartTime >= '{2} {3}:00' And DomesticStartTime <= '{4} 23:59:00')", arrivalcity, startcity, DomesticStarttime, starttime, DomesticStarttime);

            return sql;

        }

        public static string OnReservation(string[] Member)
        {
            string sql = string.Format("insert into Member values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",Member[0],Member[1], Member[2], Member[3], Member[4], Member[5], Member[6]);

            return sql;
        }

        public static string OnSelectConfirm(string name, string phone)
        {
            string sql = string.Format("Select * from Member where name = '{0}' And phone = '{1}'", name, phone);
            return sql;
        }
    }
}
