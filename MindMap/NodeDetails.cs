using Markdig;
using Microsoft.Web.WebView2.Core;
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

        //WebBrowser wb = new WebBrowser();

        //WebView2 wv2 = new WebView2();

        MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

        public NodeDetails()
        {
            InitializeComponent();

            wv2.Source = null;
       
            //this.Controls.Add(wb);

            //wb.Height = rtbNodeDescription.Height;
            //wb.Left = rtbNodeDescription.Right + 10;
            //wb.Width = (this.Width - wb.Left) - 30;
            //wb.Top = rtbNodeDescription.Top;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveRoutine();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancelRoutine();
        }

        private void rtbNodeDescription_TextChanged(object sender, EventArgs e)
        {
            generateMarkdown();
        }

        async public void generateMarkdown()
        {
            //.DocumentText = Markdown.ToHtml(rtbNodeDescription.Text, pipeline);
            //String html = Markdown.ToHtml(rtbNodeDescription.Text, pipeline);
            String theDescription = "";
            if (rtbNodeDescription.Text != null)
            {
                theDescription = rtbNodeDescription.Text;
            }
            String html = Markdown.ToHtml(theDescription, pipeline);
            if (rtbNodeDescription.Text.Length == 0 && wv2.Source != null)
            {
                wv2.NavigateToString("");
            } else if (html != null && html.Length > 0)
            {
                html = html + "<script type=\"module\">import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.esm.min.mjs';</script>";
                await wv2.EnsureCoreWebView2Async();
                wv2.NavigateToString(html);
                
            }
            
        }

        private void handleKeys(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Oem8)
            {
                return;
            }
            if ((e.KeyData & Keys.Enter) == Keys.Enter && (e.KeyData & Keys.Control) == Keys.Control)
            {
                saveRoutine();
            }
            else if ((e.KeyData & Keys.Escape) == Keys.Escape)
            {
                cancelRoutine();
            }
        }

        private void rtbNodeDescription_KeyUp(object sender, KeyEventArgs e)
        {
            handleKeys(sender, e);
        }

        private void txtTitle_KeyUp(object sender, KeyEventArgs e)
        {
            handleKeys(sender, e);
        }

        private void saveRoutine()
        {
            btnPressed = BTN_SAVE;
            //wb.Dispose();
            Hide();
        }

        private void cancelRoutine()
        {
            btnPressed = BTN_CANCEL;
            //wb.Dispose();
            Hide();
        }

    }
}
