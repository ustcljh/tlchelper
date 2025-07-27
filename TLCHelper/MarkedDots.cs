using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TLCHelper
{
    public partial class MarkedDots : Form
    {
        public MarkedDots(MainWindow p)
        {
            InitializeComponent();

            parent = p;
        }

        private void MarkedDots_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        MainWindow? parent = null;

        public void RefreshMarkedDotsView()
        {
            if (parent == null)
            {
                return;
            }

            listView1.Items.Clear();

            foreach (var item in parent.markingPoints)
            {
                listView1.Items.Add(item.Name);

                var rf = parent.ComputeRF(item.Position);
                if (rf != null)
                {
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add($"{rf:F3}");
                }
            }
        }

        private void MarkedDots_Load(object sender, EventArgs e)
        {
            if (parent == null)
            {
                return;
            }
        }
    }
}
