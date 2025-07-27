using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace TLCHelper
{
    public partial class MainWindow : Form
    {
        public class TLCMarkingPoint
        {
            public PointF Position { get; set; }
            public string Name { get; set; } = "";
        }

        Bitmap? TLCimage = null;
        List<TLCMarkingPoint> markingPoints = new List<TLCMarkingPoint>();

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.O)
            {
                loadPhotoToolStripMenuItem_Click("ShortcutKey", new EventArgs());
                return true; // handled
            }
            if (keyData == Keys.P)
            {
                markTLCPlaneToolStripMenuItem_Click("ShortcutKey", new EventArgs());
                return true; // handled
            }
            if (keyData == Keys.B)
            {
                markBaselineToolStripMenuItem_Click("ShortcutKey", new EventArgs());
                return true; // handled
            }
            if (keyData == Keys.F)
            {
                markSolventFrontToolStripMenuItem_Click("ShortcutKey", new EventArgs());
                return true; // handled
            }
            if (keyData == Keys.S)
            {
                markSpeciesPointToolStripMenuItem_Click("ShortcutKey", new EventArgs());
                return true; // handled
            }
            if (keyData == Keys.X)
            {
                exportImageToolStripMenuItem_Click("ShortcutKey", new EventArgs());
                return true; // handled
            }
            if (keyData == Keys.Delete)
            {
                toolStripMenuItemDelete_Click("ShortcutKey", new EventArgs());
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void loadPhotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new();
            dialog.Filter = "JPG image (*.jpg)|*.jpg|PNG image (*.png)|*.png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(dialog.FileName);

                TLCimage = bitmap;
                RefreshImageView();

                ClickPointList.Clear();
                BaselinePoints.Clear();
                SolventFrontPoints.Clear();

                markBaselineToolStripMenuItem.BackColor = Color.Silver;
                markSolventFrontToolStripMenuItem.BackColor = Color.Silver;

                toolStripStatusLabelRF.Text = "Mark baseline and solvent front first";
            }
        }

        private void RefreshImageView()
        {
            if (TLCimage == null)
            {
                pictureBox1.Image = null;
                return;
            }

            Bitmap TLCdraw = ImageProcess.ResizeImage(TLCimage, pictureBox1.Width, pictureBox1.Height);
            Graphics graphics = Graphics.FromImage(TLCdraw);

            float radius = 20, penWidth = 3;
            var font = new System.Drawing.Font("Arial", 20);

            foreach (var mark in markingPoints)
            {
                using (Pen pen = new Pen(Color.White, penWidth))
                using (Brush brush = new SolidBrush(Color.White))
                {
                    string markText = $"{mark.Name}";
                    var RF = ComputeRF(mark.Position);
                    if (RF != null)
                    {
                        markText += $"\r\n{RF:F3}";
                    }

                    SizeF textSize = graphics.MeasureString(markText, font);

                    // Calculate top-left point so that text is bottom-centered at anchorPoint
                    float drawX = mark.Position.X * TLCdraw.Width - textSize.Width / 2;
                    float drawY = mark.Position.Y * TLCdraw.Height - textSize.Height - font.Size;

                    graphics.DrawString(markText, font, brush, new PointF(drawX, drawY));

                    // Draw ellipse on the mask
                    float diameter = radius * 2;
                    graphics.DrawEllipse(pen, mark.Position.X * TLCdraw.Width - radius, mark.Position.Y * TLCdraw.Height - radius, diameter, diameter);
                }
            }

            penWidth = 5;

            if (drawBaselineToolStripMenuItem.Checked && BaselinePoints.Count == 2)
            {
                using (Pen pen = new Pen(Color.Blue, penWidth))
                {
                    PointF p1 = new(BaselinePoints[0].X * TLCdraw.Width, BaselinePoints[0].Y * TLCdraw.Height);
                    PointF p2 = new(BaselinePoints[1].X * TLCdraw.Width, BaselinePoints[1].Y * TLCdraw.Height);

                    graphics.DrawLine(pen, p1, p2);
                }
            }

            if (drawSolventFrontToolStripMenuItem.Checked && SolventFrontPoints.Count == 2)
            {
                using (Pen pen = new Pen(Color.Black, penWidth))
                {
                    PointF p1 = new(SolventFrontPoints[0].X * TLCdraw.Width, SolventFrontPoints[0].Y * TLCdraw.Height);
                    PointF p2 = new(SolventFrontPoints[1].X * TLCdraw.Width, SolventFrontPoints[1].Y * TLCdraw.Height);

                    graphics.DrawLine(pen, p1, p2);
                }
            }

            pictureBox1.Image = TLCdraw;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TLC Helper: A tiny tool for easily compute RF values from photo of TLC plates.\nWritten by Jiahong Luo @ Dept. of Chemical Physics, USTC", "About TLCHelper");
        }

        List<PointF> ClickPointList = new();
        List<int> nearPointIndexList = new();
        bool MarkingTLCPlate = false;
        bool MarkingBaseline = false;
        bool MarkingSolventFront = false;
        bool MarkingResultPoint = false;

        List<PointF> BaselinePoints = new(), SolventFrontPoints = new();

        private void markTLCPlaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TLCimage == null)
            {
                MessageBox.Show("You need to load a photo of the TLC plate first.");
                return;
            }

            if (sender.ToString() == "ShortcutKey" || MessageBox.Show("Please click on the four edge points of the TLC plate in the photo.\nThe mouse pointer will tell you which point should be clicked.\nThe dot on the mouse pointer indicates the chosen point.\nDo you want to begin?", "Mark TLC Plate", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MarkingTLCPlate = true;
                pictureBox1.Cursor = Cursors.PanNW;
                toolStripStatusLabelTip.Text = "Please click on the NorthWest point of the plate on the screen.";

                ClickPointList.Clear();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (TLCimage == null)
            {
                return;
            }

            float x = (float)e.X / pictureBox1.Width;
            float y = (float)e.Y / pictureBox1.Height;

            if (MarkingResultPoint)
            {
                markingPoints.Add(new TLCMarkingPoint() { Name = "Compound", Position = new(x, y) });
                MarkingResultPoint = false;
                pictureBox1.Cursor = Cursors.Cross;

                RefreshImageView();
            }
            else if (MarkingBaseline)
            {
                ClickPointList.Add(new PointF(x, y));

                switch (ClickPointList.Count)
                {
                    case 0:
                        break;
                    case 1:
                        pictureBox1.Cursor = Cursors.PanSouth;
                        toolStripStatusLabelTip.Text = "Please click on another point of the plate on the screen.";
                        break;
                    case 2:
                        MarkingBaseline = false;
                        toolStripStatusLabelTip.Text = "Ready";
                        pictureBox1.Cursor = Cursors.Cross;

                        BaselinePoints = ClickPointList.ToList();
                        ClickPointList.Clear();

                        MessageBox.Show("The baseline has been marked!");
                        markBaselineToolStripMenuItem.BackColor = Color.LightGreen;

                        if (TLCimage != null && BaselinePoints.Count() == 2 && SolventFrontPoints.Count() == 2)
                        {
                            pictureBox1.Cursor = Cursors.Cross;
                            RefreshImageView();
                        }

                        break;
                }
            }
            else if (MarkingSolventFront)
            {
                ClickPointList.Add(new PointF(x, y));

                switch (ClickPointList.Count)
                {
                    case 0:
                        break;
                    case 1:
                        pictureBox1.Cursor = Cursors.PanSouth;
                        toolStripStatusLabelTip.Text = "Please click on another point of the plate on the screen.";
                        break;
                    case 2:
                        MarkingSolventFront = false;
                        toolStripStatusLabelTip.Text = "Ready";
                        pictureBox1.Cursor = Cursors.Cross;

                        SolventFrontPoints = ClickPointList.ToList();
                        ClickPointList.Clear();

                        MessageBox.Show("The solvent front has been marked!");
                        markSolventFrontToolStripMenuItem.BackColor = Color.LightGreen;

                        if (TLCimage != null && BaselinePoints.Count() == 2 && SolventFrontPoints.Count() == 2)
                        {
                            pictureBox1.Cursor = Cursors.Cross;
                            RefreshImageView();
                        }

                        break;
                }
            }
            else if (MarkingTLCPlate)
            {
                ClickPointList.Add(new PointF(x, y));

                switch (ClickPointList.Count)
                {
                    case 0:
                        break;
                    case 1:
                        toolStripStatusLabelTip.Text = "Please click on the NorthEast point of the plate on the screen.";
                        pictureBox1.Cursor = Cursors.PanNE;
                        break;
                    case 2:
                        toolStripStatusLabelTip.Text = "Please click on the SouthEast point of the plate on the screen.";
                        pictureBox1.Cursor = Cursors.PanSE;
                        break;
                    case 3:
                        toolStripStatusLabelTip.Text = "Please click on the SouthWest point of the plate on the screen.";
                        pictureBox1.Cursor = Cursors.PanSW;
                        break;
                    case 4:
                        List<PointF> EdgePointList2 = new List<PointF>();

                        for (int i = 0; i < ClickPointList.Count; i++)
                        {
                            PointF point = new(ClickPointList[i].X * TLCimage.Width, ClickPointList[i].Y * TLCimage.Height);
                            EdgePointList2.Add(point);
                        }

                        MarkingTLCPlate = false;
                        toolStripStatusLabelTip.Text = "Ready";
                        pictureBox1.Cursor = Cursors.Cross;

                        TLCimage = ImageProcess.Transform(new Bitmap(TLCimage), EdgePointList2.ToArray(), 1000, 500);
                        RefreshImageView();

                        MessageBox.Show("The edge has been marked!");

                        break;
                    default:
                        break;
                }
            }
            else if (MarkingPointToDelete)
            {
                if (nearPointIndexList.Count > 0)
                {
                    markingPoints.RemoveAt(nearPointIndexList[0]);
                    MarkingPointToDelete = false;

                    RefreshImageView();
                }
            }
            else if (pictureBox1.Cursor == Cursors.Hand)
            {
                if (nearPointIndexList.Count == 0)
                {
                    pictureBox1.Cursor = Cursors.Cross;
                    return;
                }

                CompoundEdit editor = new();
                editor.editingPoint = markingPoints[nearPointIndexList[0]];
                editor.parent = this;

                editor.ShowDialog();
                RefreshImageView();
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void markBaselineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TLCimage == null)
            {
                MessageBox.Show("You need to load a photo of the TLC plate first.");
                return;
            }

            if (sender.ToString() == "ShortcutKey" || MessageBox.Show("Please click on two points on the TLC baseline.\nThe mouse pointer will tell you which point should be clicked.\nThe dot on the mouse pointer indicates the chosen point.\nDo you want to begin?", "Mark TLC Plate", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MarkingBaseline = true;
                pictureBox1.Cursor = Cursors.PanNorth;
                toolStripStatusLabelTip.Text = "Please click on one point of the baseline on the screen.";

                ClickPointList.Clear();
            }
        }

        private void markSolventFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TLCimage == null)
            {
                MessageBox.Show("You need to load a photo of the TLC plate first.");
                return;
            }

            if (sender.ToString() == "ShortcutKey" || MessageBox.Show("Please click on two points on the TLC solvent front.\nThe mouse pointer will tell you which point should be clicked.\nThe dot on the mouse pointer indicates the chosen point.\nDo you want to begin?", "Mark TLC Plate", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MarkingSolventFront = true;
                pictureBox1.Cursor = Cursors.PanNorth;
                toolStripStatusLabelTip.Text = "Please click on one point of the solvent front on the screen.";

                ClickPointList.Clear();
            }
        }

        public double? ComputeRF(PointF point)
        {
            if (BaselinePoints.Count() != 2 || SolventFrontPoints.Count() != 2)
            {
                return null;
            }

            var distBaseline = ImageProcess.DistancePointToLine(point, BaselinePoints[0], BaselinePoints[1]);
            var distSolventFront = ImageProcess.DistancePointToLine(point, SolventFrontPoints[0], SolventFrontPoints[1]);

            var totalHeight = distBaseline + distSolventFront;
            return distBaseline / totalHeight;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            float x = (float)e.X / pictureBox1.Width;
            float y = (float)e.Y / pictureBox1.Height;

            double threshold = 20.0 / Math.Max(pictureBox1.Width, pictureBox1.Height);

            nearPointIndexList.Clear();
            for (int i = 0; i < markingPoints.Count; ++i)
            {
                var p = markingPoints[i];
                var distance = Math.Sqrt((x - p.Position.X) * (x - p.Position.X) + (y - p.Position.Y) * (y - p.Position.Y));

                // Debug.WriteLine(distance);
                if (distance < threshold)
                {
                    nearPointIndexList.Add(i);
                }
            }

            if (nearPointIndexList.Count > 0)
            {
                var index = nearPointIndexList[0];
                string statusBarText = $"Click to edit: {markingPoints[index].Name}";

                if (nearPointIndexList.Count > 1)
                {
                    statusBarText += $" ({nearPointIndexList.Count - 1} more)";
                }

                toolStripStatusLabelNearPoint.Text = statusBarText;
            }
            else
            {
                toolStripStatusLabelNearPoint.Text = $"No point";
            }

            if (pictureBox1.Cursor == Cursors.Cross && nearPointIndexList.Count != 0)
            {
                pictureBox1.Cursor = Cursors.Hand;
            }
            if (pictureBox1.Cursor == Cursors.Hand && nearPointIndexList.Count == 0)
            {
                pictureBox1.Cursor = Cursors.Cross;
            }

            if (TLCimage == null || BaselinePoints.Count() != 2 || SolventFrontPoints.Count() != 2)
            {
                toolStripStatusLabelRF.Text = "Mark baseline and solvent front first";
                return;
            }

            var RF = ComputeRF(new PointF(x, y));

            toolStripStatusLabelRF.Text = $"Lane RF value = {RF:F3}";
        }

        private void markSpeciesPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TLCimage == null)
            {
                MessageBox.Show("You need to load a photo of the TLC plate first.");
                return;
            }

            MarkingResultPoint = true;
            pictureBox1.Cursor = Cursors.Cross;
        }

        private void exportImageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            RefreshImageView();
        }

        private void drawBaselineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawBaselineToolStripMenuItem.Checked = !drawBaselineToolStripMenuItem.Checked;
            RefreshImageView();
        }

        private void drawSolventFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawSolventFrontToolStripMenuItem.Checked = !drawSolventFrontToolStripMenuItem.Checked;
            RefreshImageView();
        }

        bool MarkingPointToDelete = false;

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelTip.Text = "Click on the marked point to delete.";
            MarkingPointToDelete = true;
        }
    }
}
