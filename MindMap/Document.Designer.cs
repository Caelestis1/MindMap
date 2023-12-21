namespace MindMap
{
    partial class Document
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
            wbvDocumentDisplay = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)wbvDocumentDisplay).BeginInit();
            SuspendLayout();
            // 
            // wbvDocumentDisplay
            // 
            wbvDocumentDisplay.AllowExternalDrop = true;
            wbvDocumentDisplay.CreationProperties = null;
            wbvDocumentDisplay.DefaultBackgroundColor = Color.White;
            wbvDocumentDisplay.Location = new Point(12, 57);
            wbvDocumentDisplay.Name = "wbvDocumentDisplay";
            wbvDocumentDisplay.Size = new Size(776, 381);
            wbvDocumentDisplay.TabIndex = 0;
            wbvDocumentDisplay.ZoomFactor = 1D;
            // 
            // Document
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(wbvDocumentDisplay);
            Name = "Document";
            Text = "Document";
            ((System.ComponentModel.ISupportInitialize)wbvDocumentDisplay).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 wbvDocumentDisplay;
    }
}