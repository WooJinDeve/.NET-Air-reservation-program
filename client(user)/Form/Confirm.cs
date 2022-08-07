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
    public partial class Confirm : Form
    {
        public Confirm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Confirm_Load(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();

            WbControl.Instance.GetConfirm(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" && textBox2.Text == "")
                    throw new Exception("정보를 입력해주세요.");

                string name = textBox1.Text;
                string phone = textBox2.Text;

                WbControl.Instance.SelectConfirm(name, phone);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ListView2PrintAll(List<Member> members)
        {
            listView2.Items.Clear();
            foreach (Member mem in members)
            {

                ListViewItem item = new ListViewItem(
                new string[] {mem.Name,mem.Phone ,mem.AirPortName ,mem.AirPlaneNum
                   ,mem.Airline,mem.Date ,mem.Price }
                   );
                listView2.Items.Add(item);
            }

        }
    }
}
