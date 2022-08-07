using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 과제Client
{
    public partial class MainForm : Form
    {
        ProcessForm process = new ProcessForm();
        public string Startday { get; set; }
        public string Arriveday { get; set; }

        WbControl con = WbControl.Instance;

        public MainForm()
        {
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            con.Init();
        }

        public Bitmap ChangeOpacity(Image img, float opacityvalue)

        {

            Bitmap bmp = new Bitmap(img.Width, img.Height);

            Graphics graphics = Graphics.FromImage(bmp);

            ColorMatrix colormatrix = new ColorMatrix();

            colormatrix.Matrix33 = opacityvalue;

            ImageAttributes imgAttribute = new ImageAttributes();

            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);

            graphics.Dispose();

            return bmp;

        }

        #region 왕복 항공
        private void button1_Click_1(object sender, EventArgs e)
        {
           
                WBCrossThread.LogIn_FormShow(this, false);
                process.IsChecked = true;
                WBCrossThread.ShowDialog(process);
                WBCrossThread.LogIn_FormShow(this, true);         
        }
        #endregion

        #region 편도
        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();         
            process.IsChecked = false;
            process.ShowDialog();

            this.Show();
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Confirm confirm = new Confirm();
            confirm.ShowDialog();

            this.Show();
        }
    }
}
