using MetroFramework.Controls;
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
    public partial class ProcessForm : Form
    {
        private WbControl con = WbControl.Instance;

        public bool IsChecked { get; set; }
        
        string Arrive;
        string ArriveDay;
        string ArriveTime;
        string Departure;
        string DepartureDay;
        string DepartureTime;

        public ProcessForm()
        {
            InitializeComponent();

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "HH:mm";
            dateTimePicker1.ShowUpDown = true;

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "HH:mm";
            dateTimePicker2.ShowUpDown = true;

    
        }

        private void ProcessForm_Load(object sender, EventArgs e)
        {
            con.GetProcessForm(this);

            ControlClear();

            if (IsChecked == true)
            {
                dateTimePicker2.Enabled = true;
                metroDateTime2.Enabled = true;
                metroComboBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox7.Enabled = true;
                textBox9.Enabled = true;
                comboBox2.Enabled = true;
            }
            else
            {
                dateTimePicker2.Enabled = false;
                metroDateTime2.Enabled = false;
                metroComboBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox7.Enabled = false;
                textBox9.Enabled = false;
                comboBox2.Enabled = false;
            }
        }

        private void ControlClear()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
        }

        #region  예약하기
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" && textBox2.Text == "" || textBox10.Text == "" || (IsChecked == true && textBox9.Text == ""))
                    throw new Exception("값이 비어있음.");
                
                Reservation reservationform = new Reservation();
                reservationform.IsCheck = IsChecked;
                reservationform.AirPortStartName = textBox1.Text;
                reservationform.AirPlaneNumStart = textBox8.Text;
                reservationform.AirlineStart = textBox3.Text;
                reservationform.DateStart = textBox6.Text;
                reservationform.PriceStart = textBox10.Text;

                if(IsChecked == true)
                {
                    reservationform.AirPortEndName = textBox2.Text;
                    reservationform.AirPlaneNumEnd = textBox7.Text;
                    reservationform.AirlineEnd = textBox4.Text;
                    reservationform.DateEnd = textBox5.Text;
                    reservationform.PriceEnd = textBox9.Text;
                }
                else if(IsChecked == false)
                {
                    reservationform.AirPortEndName = "-";
                    reservationform.AirPlaneNumEnd = "";
                    reservationform.AirlineEnd = "";
                    reservationform.DateEnd = "";
                    reservationform.PriceEnd = "";

                }

                this.Hide();
                reservationform.ShowDialog();
                this.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ReservationCheck(bool b)
        {
            if (b == true)
                MessageBox.Show("예약성공");
            else
                MessageBox.Show("예약실패");
        }
        #endregion

        #region 검색버튼(클릭) - 리스트뷰 PrintAll

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                Data();
                if (Arrive == "" || Departure == "")
                    return;
                // 왕복에 관한 정보 true
                if (IsChecked == true)
                    con.SelectAirPort(Arrive, Departure, ArriveDay, DepartureDay, ArriveTime, DepartureTime);

                //// 편도에 관한 정보 false
                else
                    con.SelectAirPort(Arrive, Departure, 0, DepartureDay, ArriveTime);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Data()
        {
            //도착공항
            if (metroComboBox3.Text == "")
                throw new Exception("도착지을 선택해주세요");
            Arrive = metroComboBox3.SelectedItem.ToString();

            //출발공항
            Departure = metroComboBox4.SelectedItem.ToString();

            //출발일
            string msg = metroDateTime1.Value.ToString();
            string[] sp = msg.Split(' ');
            DepartureDay = sp[0].ToString();

            //도착일
            msg = metroDateTime2.Value.ToString();
            sp = msg.Split(' ');
            ArriveDay = sp[0].ToString();

            //시간
            ArriveTime = dateTimePicker1.Value.ToString("HH:mm");
            DepartureTime = dateTimePicker2.Value.ToString("HH:mm");
        }


        public void ListViewPrintAll1(string[] strarr)
        {
            listView2.Items.Clear();
            foreach (string str in strarr)
            {
                if (str == "")
                    return;
                string[] data = str.Split('#');
     
                string[] str1 = new string[] { data[1], data[2], data[4], data[3], data[6], data[5], data[7], data[8] };
                
                ListViewItem item = new ListViewItem(str1);

                //listView2.Items.Add(item);
                WBCrossThread.ListViewAdd(listView2, item);
            }
        }

        public void ListViewPrintAll2(string[] strarr)
        {
            listView1.Items.Clear();
            foreach (string str in strarr)
            {
                if (str == "")
                    return;
                string[] data = str.Split('#');
                string[] str1 = new string[] { data[1], data[2], data[4], data[3], data[6], data[5], data[7], data[8] };
                ListViewItem item = new ListViewItem(str1);

                //listView1.Items.Add(item);
                WBCrossThread.ListViewAdd(listView1, item);
            }
        }

        #endregion

        #region 리스트 박스 클릭시 생기는 이벤트 처리
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count != 0)
            {
                int SelectRow = listView2.SelectedItems[0].Index;

                textBox1.Text = string.Format("{0} ~ {1}", listView2.Items[SelectRow].SubItems[3].Text, listView2.Items[SelectRow].SubItems[2].Text);
                textBox8.Text = listView2.Items[SelectRow].SubItems[0].Text;
                textBox3.Text = listView2.Items[SelectRow].SubItems[1].Text;
                textBox6.Text = string.Format("{0} ~ {1}", listView2.Items[SelectRow].SubItems[5].Text, listView2.Items[SelectRow].SubItems[4].Text);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                int SelectRow = listView1.SelectedItems[0].Index;

                textBox2.Text = string.Format("{0} ~ {1}", listView1.Items[SelectRow].SubItems[3].Text, listView1.Items[SelectRow].SubItems[2].Text);
                textBox7.Text = listView1.Items[SelectRow].SubItems[0].Text;
                textBox4.Text = listView1.Items[SelectRow].SubItems[1].Text;
                textBox5.Text = string.Format("{0} ~ {1}", listView1.Items[SelectRow].SubItems[5].Text, listView1.Items[SelectRow].SubItems[4].Text);              
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count != 0)
            {
                int SelectRow = listView2.SelectedItems[0].Index;
                if (comboBox1.Text == "이코노미")
                {
                    textBox10.Text = listView2.Items[SelectRow].SubItems[6].Text;
                }
                if (comboBox1.Text == "비즈니스")
                {
                    textBox10.Text = listView2.Items[SelectRow].SubItems[7].Text;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                int SelectRow = listView1.SelectedItems[0].Index;
                if (comboBox2.Text == "이코노미")
                {
                    textBox9.Text = listView1.Items[SelectRow].SubItems[6].Text;
                }
                if (comboBox2.Text == "비즈니스")
                {
                    textBox9.Text = listView1.Items[SelectRow].SubItems[7].Text;
                }
            }
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
