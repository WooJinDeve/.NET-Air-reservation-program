using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0413_관리자
{
    public partial class MainForm : Form
    {
        private WbControl con = WbControl.Instance;

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
       }


        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                con.Init(this);
                con.GetMemberAllList();
                con.GetAirPlaneListAll();
             
                timer1.Start();
      
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }     


        public void ListViewPrintAll(List<Airport> airports)
        {
            listView1.Items.Clear();
            foreach (Airport airport in airports)
            {

                ListViewItem item = new ListViewItem(
                   new string[] {airport.DomesticNum,airport.AirlineKorean,airport.Arrivalcity,airport.Startcity
                   ,airport.DomesticArrivalTime.ToString(),airport.DomesticStartTime.ToString(),airport.Economy.ToString(),airport.Business.ToString()}
                   );
                listView1.Items.Add(item);
            }
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // con.GetAirPlaneListAll();
        }

        public void ListView2PrintAll(List<Member> members)
        {
            listView2.Items.Clear();
            foreach (Member mem in members)
            {

                ListViewItem item = new ListViewItem(
                new string[] {mem.Name,mem.Phone ,mem.AirPortName ,mem.AirPlaneNum
                   ,mem.Airline ,mem.Price }
                   );
                listView2.Items.Add(item);
            }

        }


    }
}
