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
    public partial class CompoundEdit : Form
    {
        public CompoundEdit()
        {
            InitializeComponent();
        }

        public MainWindow.TLCMarkingPoint? editingPoint = null;
        public MainWindow? parent = null;

        private void CompoundEdit_Load(object sender, EventArgs e)
        {
            if (editingPoint == null)
            {
                return;
            }

            textBoxName.Text = editingPoint.Name;
            textBoxName.SelectAll();

            if (parent != null)
            {
                var rf = parent.ComputeRF(editingPoint.Position);
                if (rf != null)
                {
                    textBoxRf.Text = rf.ToString();
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (editingPoint != null)
            {
                editingPoint.Name = textBoxName.Text;
            }

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == '\r')
            {
                buttonOK_Click(sender, e);
            }
        }
    }
}
