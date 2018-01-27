using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHackerNews
{
    static class Program
    {
        private static Form1 f;
        private static List<HackerNews> list;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            f = new Form1();
            HackerNewsManager m = new HackerNewsManager();
            list = m.DoWork();

            foreach (HackerNews news in list)
            {
                ListViewItem listItem = new ListViewItem(news.title);
                f.listView1.Items.Add(listItem);
            }

            f.listView1.SelectedIndexChanged += _listBox1_SelectedIndexChanged;

            Application.Run(f);
        }

        private static void _listBox1_SelectedIndexChanged(object pSender, EventArgs pArgs)
        {
            ListView.SelectedListViewItemCollection breakfast =
                f.listView1.SelectedItems;
            foreach (ListViewItem item in breakfast)
            {
                Process.Start(list[item.Index].url);
            }
        }
    }
}
