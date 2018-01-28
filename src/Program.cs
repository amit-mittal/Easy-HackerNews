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
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            f = new Form1();
            HackerNewsManager m = new HackerNewsManager();
            list = m.DoWork();

            ColumnHeader header1 = new ColumnHeader();
            header1.Name = "col1";
            header1.Text = "Title";
            header1.Width = 317;
            ColumnHeader header2 = new ColumnHeader();
            header2.Name = "col2";
            header2.Text = "Points";
            header2.Width = 50;
            header2.TextAlign = HorizontalAlignment.Center;
            ColumnHeader header3 = new ColumnHeader();
            header3.Name = "col3";
            header3.Text = "Comments";
            header3.Width = 70;
            header3.TextAlign = HorizontalAlignment.Center;

            SetHeight(f.listView1, 20);

            f.listView1.Columns.Add(header1);
            f.listView1.Columns.Add(header2);
            f.listView1.Columns.Add(header3);

            foreach (HackerNews news in list)
            {
                ListViewItem listItem = new ListViewItem();
                f.listView1.Items.Add(listItem);
                listItem.Text = news.title;
                listItem.SubItems.Add(news.points.ToString());
                listItem.SubItems.Add(news.comments_count.ToString());
            }

            f.listView1.SelectedIndexChanged += _listBox1_SelectedIndexChanged;
            f.listView1.ColumnWidthChanging += listView1_ColumnWidthChanging;

            Application.Run(f);
        }

        private static void SetHeight(ListView listView, int height)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);
            listView.SmallImageList = imgList;
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

        private static void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = f.listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }
    }
}
