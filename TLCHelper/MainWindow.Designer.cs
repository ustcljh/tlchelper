namespace TLCHelper
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            loadPhotoToolStripMenuItem = new ToolStripMenuItem();
            markTLCPlaneToolStripMenuItem = new ToolStripMenuItem();
            markBaselineToolStripMenuItem = new ToolStripMenuItem();
            markSolventFrontToolStripMenuItem = new ToolStripMenuItem();
            markToolStripMenuItem = new ToolStripMenuItem();
            markSpeciesPointToolStripMenuItem = new ToolStripMenuItem();
            drawBaselineToolStripMenuItem = new ToolStripMenuItem();
            drawSolventFrontToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exportImageToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            pictureBox1 = new PictureBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelTip = new ToolStripStatusLabel();
            toolStripStatusLabelRF = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { loadPhotoToolStripMenuItem, markTLCPlaneToolStripMenuItem, markBaselineToolStripMenuItem, markSolventFrontToolStripMenuItem, markToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1338, 40);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // loadPhotoToolStripMenuItem
            // 
            loadPhotoToolStripMenuItem.Name = "loadPhotoToolStripMenuItem";
            loadPhotoToolStripMenuItem.Size = new Size(195, 36);
            loadPhotoToolStripMenuItem.Text = "[O] Load photo";
            loadPhotoToolStripMenuItem.Click += loadPhotoToolStripMenuItem_Click;
            // 
            // markTLCPlaneToolStripMenuItem
            // 
            markTLCPlaneToolStripMenuItem.Name = "markTLCPlaneToolStripMenuItem";
            markTLCPlaneToolStripMenuItem.Size = new Size(233, 36);
            markTLCPlaneToolStripMenuItem.Text = "[P] Mark TLC plane";
            markTLCPlaneToolStripMenuItem.Click += markTLCPlaneToolStripMenuItem_Click;
            // 
            // markBaselineToolStripMenuItem
            // 
            markBaselineToolStripMenuItem.BackColor = Color.Silver;
            markBaselineToolStripMenuItem.Name = "markBaselineToolStripMenuItem";
            markBaselineToolStripMenuItem.Size = new Size(218, 36);
            markBaselineToolStripMenuItem.Text = "[B] Mark baseline";
            markBaselineToolStripMenuItem.Click += markBaselineToolStripMenuItem_Click;
            // 
            // markSolventFrontToolStripMenuItem
            // 
            markSolventFrontToolStripMenuItem.BackColor = Color.Silver;
            markSolventFrontToolStripMenuItem.Name = "markSolventFrontToolStripMenuItem";
            markSolventFrontToolStripMenuItem.Size = new Size(264, 36);
            markSolventFrontToolStripMenuItem.Text = "[F] Mark solvent front";
            markSolventFrontToolStripMenuItem.Click += markSolventFrontToolStripMenuItem_Click;
            // 
            // markToolStripMenuItem
            // 
            markToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { markSpeciesPointToolStripMenuItem, drawBaselineToolStripMenuItem, drawSolventFrontToolStripMenuItem, toolStripSeparator1, exportImageToolStripMenuItem });
            markToolStripMenuItem.Name = "markToolStripMenuItem";
            markToolStripMenuItem.Size = new Size(116, 36);
            markToolStripMenuItem.Text = "Analyse";
            // 
            // markSpeciesPointToolStripMenuItem
            // 
            markSpeciesPointToolStripMenuItem.Name = "markSpeciesPointToolStripMenuItem";
            markSpeciesPointToolStripMenuItem.ShortcutKeyDisplayString = "S";
            markSpeciesPointToolStripMenuItem.Size = new Size(375, 44);
            markSpeciesPointToolStripMenuItem.Text = "Mark species point";
            markSpeciesPointToolStripMenuItem.Click += markSpeciesPointToolStripMenuItem_Click;
            // 
            // drawBaselineToolStripMenuItem
            // 
            drawBaselineToolStripMenuItem.Name = "drawBaselineToolStripMenuItem";
            drawBaselineToolStripMenuItem.Size = new Size(375, 44);
            drawBaselineToolStripMenuItem.Text = "Draw baseline";
            // 
            // drawSolventFrontToolStripMenuItem
            // 
            drawSolventFrontToolStripMenuItem.Name = "drawSolventFrontToolStripMenuItem";
            drawSolventFrontToolStripMenuItem.Size = new Size(375, 44);
            drawSolventFrontToolStripMenuItem.Text = "Draw solvent front";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(372, 6);
            // 
            // exportImageToolStripMenuItem
            // 
            exportImageToolStripMenuItem.Name = "exportImageToolStripMenuItem";
            exportImageToolStripMenuItem.ShortcutKeyDisplayString = "X";
            exportImageToolStripMenuItem.Size = new Size(375, 44);
            exportImageToolStripMenuItem.Text = "Export image";
            exportImageToolStripMenuItem.Click += exportImageToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(99, 36);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ControlDark;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 40);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1338, 625);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.MouseClick += pictureBox1_MouseClick;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(32, 32);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelTip, toolStripStatusLabelRF });
            statusStrip1.Location = new Point(0, 623);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1338, 42);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelTip
            // 
            toolStripStatusLabelTip.Name = "toolStripStatusLabelTip";
            toolStripStatusLabelTip.Size = new Size(78, 32);
            toolStripStatusLabelTip.Text = "Ready";
            toolStripStatusLabelTip.Click += toolStripStatusLabel1_Click;
            // 
            // toolStripStatusLabelRF
            // 
            toolStripStatusLabelRF.Name = "toolStripStatusLabelRF";
            toolStripStatusLabelRF.Size = new Size(400, 32);
            toolStripStatusLabelRF.Text = "Mark baseline and solvent front first";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1338, 665);
            Controls.Add(statusStrip1);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainWindow";
            Text = "TLC Helper";
            Resize += MainWindow_Resize;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem loadPhotoToolStripMenuItem;
        private ToolStripMenuItem markBaselineToolStripMenuItem;
        private ToolStripMenuItem markSolventFrontToolStripMenuItem;
        private ToolStripMenuItem markToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem markTLCPlaneToolStripMenuItem;
        private PictureBox pictureBox1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabelTip;
        private ToolStripStatusLabel toolStripStatusLabelRF;
        private ToolStripMenuItem markSpeciesPointToolStripMenuItem;
        private ToolStripMenuItem drawBaselineToolStripMenuItem;
        private ToolStripMenuItem drawSolventFrontToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exportImageToolStripMenuItem;
    }
}