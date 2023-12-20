namespace MindMap
{
    partial class Help
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
            groupBox1 = new GroupBox();
            txtKeyPresses = new TextBox();
            groupBox2 = new GroupBox();
            txtClicks = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtKeyPresses);
            groupBox1.Location = new Point(12, 23);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(354, 415);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Key Presses";
            // 
            // txtKeyPresses
            // 
            txtKeyPresses.Location = new Point(6, 38);
            txtKeyPresses.Multiline = true;
            txtKeyPresses.Name = "txtKeyPresses";
            txtKeyPresses.ReadOnly = true;
            txtKeyPresses.ScrollBars = ScrollBars.Vertical;
            txtKeyPresses.Size = new Size(342, 371);
            txtKeyPresses.TabIndex = 0;
            txtKeyPresses.TextChanged += textBox1_TextChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtClicks);
            groupBox2.Location = new Point(401, 23);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(387, 415);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Clicks";
            // 
            // txtClicks
            // 
            txtClicks.Location = new Point(6, 29);
            txtClicks.Multiline = true;
            txtClicks.Name = "txtClicks";
            txtClicks.ReadOnly = true;
            txtClicks.ScrollBars = ScrollBars.Vertical;
            txtClicks.Size = new Size(375, 371);
            txtClicks.TabIndex = 0;
            // 
            // Help
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Help";
            Text = "Help";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtKeyPresses;
        private GroupBox groupBox2;
        private TextBox txtClicks;
    }
}