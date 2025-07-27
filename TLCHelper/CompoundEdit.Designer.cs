namespace TLCHelper
{
    partial class CompoundEdit
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
            label1 = new Label();
            textBoxName = new TextBox();
            label2 = new Label();
            textBoxRf = new TextBox();
            buttonOK = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(78, 32);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // textBoxName
            // 
            textBoxName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBoxName.Location = new Point(96, 12);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(692, 39);
            textBoxName.TabIndex = 1;
            textBoxName.KeyPress += textBoxName_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 60);
            label2.Name = "label2";
            label2.Size = new Size(36, 32);
            label2.TabIndex = 2;
            label2.Text = "Rf";
            // 
            // textBoxRf
            // 
            textBoxRf.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBoxRf.Location = new Point(96, 57);
            textBoxRf.Name = "textBoxRf";
            textBoxRf.ReadOnly = true;
            textBoxRf.Size = new Size(200, 39);
            textBoxRf.TabIndex = 3;
            // 
            // buttonOK
            // 
            buttonOK.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonOK.Location = new Point(638, 139);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(150, 46);
            buttonOK.TabIndex = 4;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(482, 139);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(150, 46);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // CompoundEdit
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(textBoxRf);
            Controls.Add(label2);
            Controls.Add(textBoxName);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CompoundEdit";
            Text = "Edit compound";
            Load += CompoundEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxName;
        private Label label2;
        private TextBox textBoxRf;
        private Button buttonOK;
        private Button buttonCancel;
    }
}