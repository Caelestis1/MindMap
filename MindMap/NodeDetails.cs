using Markdig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindMap
{
    public partial class NodeDetails : Form
    {
        public static int BTN_SAVE = 1;
        public static int BTN_CANCEL = 2;
        public static int BTN_UNDEFINED = -1;

        public int btnPressed = -1;

        WebBrowser wb = new WebBrowser();

        MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

        public NodeDetails()
        {
            InitializeComponent();


            this.Controls.Add(wb);


            wb.Height = rtbNodeDescription.Height;
            wb.Left = rtbNodeDescription.Right + 10;
            wb.Width = (this.Width - wb.Left) - 30;
            wb.Top = rtbNodeDescription.Top;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnPressed = BTN_SAVE;
            Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnPressed = BTN_CANCEL;
            Hide();
        }

        private void rtbNodeDescription_TextChanged(object sender, EventArgs e)
        {
            generateMarkdown();
        }

        public void generateMarkdown()
        {
            wb.DocumentText = Markdown.ToHtml(rtbNodeDescription.Text, pipeline);
        }

        private void rtbNodeDescription_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyData & Keys.Enter) == Keys.Enter && (e.KeyData & Keys.Control) == Keys.Control)
            {
                btnPressed = BTN_SAVE;
                Hide();
            } else if ((e.KeyData & Keys.Escape) == Keys.Escape)
            {
                btnPressed = BTN_CANCEL;
                Hide();
            }
        }

        private void txtTitle_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyData & Keys.Enter) == Keys.Enter && (e.KeyData & Keys.Control) == Keys.Control)
            {
                btnPressed = BTN_SAVE;
                Hide();
            }
            else if ((e.KeyData & Keys.Escape) == Keys.Escape)
            {
                btnPressed = BTN_CANCEL;
                Hide();
            }
        }
    }
}
