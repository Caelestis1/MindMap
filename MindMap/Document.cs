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
    public partial class Document : Form
    {
        public Document()
        {
            InitializeComponent();
            wbvDocumentDisplay.Source = null;
            this.WindowState = FormWindowState.Maximized;

        }

        async public void setSource(String html)
        {
            await wbvDocumentDisplay.EnsureCoreWebView2Async();
            wbvDocumentDisplay.NavigateToString(html);
        }

    }
}
