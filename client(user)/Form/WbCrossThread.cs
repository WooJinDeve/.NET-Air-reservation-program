using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 과제Client
{
    static class WBCrossThread
    {
        public static void LogIn_FormShow(MainForm form, bool b)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(delegate ()
                {
                    if (b)
                        form.Show();
                    else
                        form.Hide();
                }));
            }
            else
            {
                if (b)
                    form.Show();
                else
                    form.Hide();
            }
        }
        public static void ShowDialog(ProcessForm form)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(delegate ()
                {
                    form.ShowDialog();
                }));
            }
            else
            {
                form.ShowDialog();
            }
        }


        public static void ListViewAdd(ListView listView, ListViewItem item)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new MethodInvoker(delegate ()
                {
                    listView.Items.Add(item);
                }));
            }
            else
            {
                listView.Items.Add(item);
            }
        }
    }
}
