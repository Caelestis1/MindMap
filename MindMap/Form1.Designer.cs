namespace MindMap
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pnlCanvas = new Panel();
            dlgSaveFile = new SaveFileDialog();
            pbSave = new PictureBox();
            pbOpen = new PictureBox();
            pictureBox1 = new PictureBox();
            pbHelp = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbSave).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbOpen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbHelp).BeginInit();
            SuspendLayout();
            // 
            // pnlCanvas
            // 
            pnlCanvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlCanvas.BackColor = Color.Transparent;
            pnlCanvas.Location = new Point(12, 72);
            pnlCanvas.Name = "pnlCanvas";
            pnlCanvas.Size = new Size(1139, 606);
            pnlCanvas.TabIndex = 0;
            pnlCanvas.Scroll += pnlCanvas_Scroll;
            pnlCanvas.DoubleClick += pnlCanvas_DoubleClick;
            pnlCanvas.MouseClick += pnlCanvas_MouseClick;
            pnlCanvas.MouseDown += pnlCanvas_MouseDown;
            pnlCanvas.MouseMove += pnlCanvas_MouseMove;
            pnlCanvas.MouseUp += pnlCanvas_MouseUp;
            pnlCanvas.Resize += pnlCanvas_Resize;
            // 
            // pbSave
            // 
            pbSave.BackColor = Color.Transparent;
            pbSave.Image = (Image)resources.GetObject("pbSave.Image");
            pbSave.Location = new Point(12, 2);
            pbSave.Name = "pbSave";
            pbSave.Size = new Size(64, 64);
            pbSave.TabIndex = 1;
            pbSave.TabStop = false;
            pbSave.Click += pbSave_Click;
            // 
            // pbOpen
            // 
            pbOpen.Image = (Image)resources.GetObject("pbOpen.Image");
            pbOpen.Location = new Point(80, 2);
            pbOpen.Name = "pbOpen";
            pbOpen.Size = new Size(64, 64);
            pbOpen.TabIndex = 2;
            pbOpen.TabStop = false;
            pbOpen.Click += pbOpen_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1079, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(64, 64);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pbHelp
            // 
            pbHelp.Image = (Image)resources.GetObject("pbHelp.Image");
            pbHelp.Location = new Point(1009, 2);
            pbHelp.Name = "pbHelp";
            pbHelp.Size = new Size(64, 64);
            pbHelp.TabIndex = 4;
            pbHelp.TabStop = false;
            pbHelp.Click += pbHelp_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 682);
            ControlBox = false;
            Controls.Add(pbHelp);
            Controls.Add(pictureBox1);
            Controls.Add(pbOpen);
            Controls.Add(pnlCanvas);
            Controls.Add(pbSave);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "MindMap";
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pbSave).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbOpen).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbHelp).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCanvas;
        private SaveFileDialog dlgSaveFile;
        private PictureBox pbSave;
        private PictureBox pbOpen;
        private PictureBox pictureBox1;
        private PictureBox pbHelp;
    }
}