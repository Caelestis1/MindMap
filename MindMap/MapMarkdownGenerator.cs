using Markdig.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MindMap
{
    internal class MapMarkdownGenerator
    {
        public String generateMarkDown(Node rootNode)
        {
            StringBuilder markdown = new StringBuilder();

            markdown.AppendLine(generateMermaidDiagram(rootNode, 1));
            markdown.AppendLine(generateMarkDown(rootNode, 1));
            return markdown.ToString();
        }

        private String generateMermaidDiagram(Node rootNode, int level)
        {
            StringBuilder markdown = new StringBuilder();

            if (level == 1)
            {
                markdown.Append(String.Format("```mermaid{0}", "\n"));
                markdown.Append(String.Format("mindmap{0}", "\n"));
            }

            String padding = "".PadLeft(level, ' ');

            if (level == 1)
            {
                
                markdown.Append(String.Format("{0}(({1})){2}", padding, rootNode.title, "\n"));
            }
            else
            {
                markdown.Append(String.Format("{0}({1}){2}", padding, rootNode.title, "\n"));
            }

            if (rootNode.children != null)
            {
                foreach (Node child in rootNode.children) {
                    markdown.AppendLine(generateMermaidDiagram(child, level + 1));
                }
            }


            if (level == 1)
            {
                markdown.Append(String.Format("```{0}", "\n"));
            }
            return markdown.ToString();
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
