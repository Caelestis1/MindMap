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
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();

            StringBuilder message = new StringBuilder();
            message.Append(String.Format("F - Format current nodes {0}", Environment.NewLine));
            message.Append(String.Format("O - Open/Load existing map {0}", Environment.NewLine));
            message.Append(String.Format("S - Save current map {0}", Environment.NewLine));
            message.Append(String.Format("E - Export to a html page with the mind map and details {0}", Environment.NewLine));
            message.Append(String.Format("Insert - Add a new node {0}", Environment.NewLine));
            message.Append(String.Format("Enter - Edit the last inserted node {0}", Environment.NewLine));
            message.Append(String.Format("Ctrl+X - Exit application {0}", Environment.NewLine));
            message.Append(String.Format("When editing a nodes title/description pressing Ctrl+Enter will save the changes and close the editor {0}", Environment.NewLine));

            txtKeyPresses.Text = message.ToString();

            message.Clear();

            message.Append(String.Format("Right click the central node to add a new node {0}", Environment.NewLine));
            message.Append(String.Format("Doublt left click the central node to add/change title and description {0}", Environment.NewLine));
            message.Append(String.Format("Double left click and node around the central node to add/change the title and description {0}", Environment.NewLine));
            message.Append(String.Format("Right click a node other than the central node to navigate to it and add children. This will make the node appear in the top left corner. To renavigate up a level double click the node in the top left corner. {0}", Environment.NewLine));
            message.Append(String.Format("Left click and drag a node over the bin to remove it {0}", Environment.NewLine));

            txtClicks.Text = message.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
