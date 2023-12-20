namespace MindMap
{
    partial class NodeDetails
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
            txtTitle = new TextBox();
            rtbNodeDescription = new RichTextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(12, 26);
            txtTitle.Name = "txtTitle";
            txtTitle.PlaceholderText = "Node Title";
            txtTitle.Size = new Size(772, 27);
            txtTitle.TabIndex = 0;
            txtTitle.KeyUp += txtTitle_KeyUp;
            // 
            // rtbNodeDescription
            // 
            rtbNodeDescription.Location = new Point(12, 80);
            rtbNodeDescription.Name = "rtbNodeDescription";
            rtbNodeDescription.Size = new Size(345, 323);
            rtbNodeDescription.TabIndex = 1;
            rtbNodeDescription.Text = "";
            rtbNodeDescription.TextChanged += rtbNodeDescription_TextChanged;
            rtbNodeDescription.KeyUp += rtbNodeDescription_KeyUp;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(420, 409);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(364, 29);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(12, 409);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // NodeDetails
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(rtbNodeDescription);
            Controls.Add(txtTitle);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "NodeDetails";
            ShowInTaskbar = false;
            Text = "Node Details";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSave;
        private Button btnCancel;
        public TextBox txtTitle;
        public RichTextBox rtbNodeDescription;
    }
}