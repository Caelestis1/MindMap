using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMap
{
    internal class MapMarkdownGenerator
    {
        public String generateMarkDown(Node rootNode)
        {
            StringBuilder markdown = new StringBuilder();
            return generateMarkDown(rootNode, 1);
        }

        private String generateMarkDown(Node rootNode, int level)
        {
            StringBuilder markdown = new StringBuilder();
            markdown.Append(generateHeader(rootNode.title, level));
            markdown.Append("  \n");
            markdown.Append(rootNode.description);
            
            if (rootNode.children != null)
            {
                foreach (Node childNode in rootNode.children)
                {
                    markdown.Append(generateMarkDown(childNode, level + 1));
                }
            }

            return markdown.ToString();
        }

        private String generateHeader(String text, int level)
        {
            String header = "".PadLeft(level, '#');
            return String.Format("{0} {1}", header, text);
        }
    }
}
