using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 과제Client
{
    public partial class Reservation : Form
    {
        public bool IsCheck { get; set; }
        //공항
        public string AirPortStartName { get; set; }
        public string AirPortEndName { get; set; }
        //항공편
        public string AirPlaneNumStart { get; set; }
        public string AirPlaneNumEnd { get; set; }
        //항공사
        public string AirlineStart { get; set; }
        public string AirlineEnd { get; set; }
        //기간
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        //가격
        public string PriceStart { get; set; }
        public string PriceEnd { get; set; }


        public Reservation()
        {
            InitializeComponent();
        }

        private void Reservation_Load(object sender, EventArgs e)
        {
            label1.Text = AirPortStartName;
            textBox1.Text = AirPlaneNumStart;
            textBox3.Text = AirlineStart;
            textBox5.Text = DateStart;
            textBox7.Text = PriceStart;
            label2.Text = AirPortEndName;

            if (IsCheck == true)
            {
                textBox2.Enabled = true;
                textBox4.Enabled = true;
                textBox6.Enabled = true;
                textBox8.Enabled = true;

                textBox2.Text = AirPlaneNumEnd;
                textBox4.Text = AirlineEnd;
                textBox6.Text = DateEnd;
                textBox8.Text = PriceEnd;
                //       
            }
            else
            {
                textBox2.Enabled = false;
                textBox4.Enabled = false;
                textBox6.Enabled = false;
                textBox8.Enabled = false;
            }
        }


        //가는편 항공권

        //가격 선택
        private void metroListView1_Click(object sender, EventArgs e)
        {

        }

        //가격 선택
        private void metroListView2_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == "" && textBox10.Text == "")
                return;
            CautionForm cautionForm = new CautionForm();
            if(cautionForm.ShowDialog() == DialogResult.OK)
            {
                WbControl.Instance.Reservation(textBox9.Text, textBox10.Text,AirPortStartName, AirPlaneNumStart, AirlineStart, DateStart, PriceStart); 
                if(IsCheck == true)
                    WbControl.Instance.Reservation(textBox9.Text, textBox10.Text, AirPortEndName, AirPlaneNumEnd, AirlineEnd, DateEnd, PriceEnd);
                this.Close();
            }
       
        }
    }
}
