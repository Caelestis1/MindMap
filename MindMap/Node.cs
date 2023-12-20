using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MindMap
{
    internal class Node
    {
        public Node(int pWidth, int pHeight)
        {
            Radius = 10;
            positionX = 0;
            positionY = 0;
            width = pWidth;
            height = pHeight;
            title = "";
            shortTitle = "";
            calcRect();
        }

        public Rectangle smallBounds { get; set; }

        public String shortTitle { get; set; }

        public static int centerX {  get; set; }
        public static int centerY { get; set; }

        public static int centerRadius { get; set; }

        public int angle { get; set; }

        public Rectangle Bounds;
        public int Radius = 10;

        private void calcRect()
        {
            Bounds = new Rectangle(positionX, positionY, width, height);
            smallBounds = new Rectangle(positionX + ((width/2)-5), positionY + (height - 10), 10, 10);
        }

        public int positionX { get; set; }

        public void setPositionX(int newX)
        {
            positionX = newX;
            calcRect();

        }

        public int positionY { get; set; }

        public void setPositionY(int y)
        {
            positionY = y;
            calcRect();
        }

        public int width { get; set; }

        public void setWidth(int width)
        {
            this.width = width;
            calcRect();
        }

        public int height { get; set; }

        public void setHeight(int height)
        {
            this.height = height;
            calcRect();
        }

        public string? title { get; set; }

        public string? description { get; set; }

        public List<Node> children { get; set; }

        [JsonIgnore]
        public Node parent { get; set; }

        public void addChildNode(Node node)
        {
            if (children == null)
            {
                children = new List<Node>();
            }

            children.Add(node);
        }

        public String generateJSON()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{");

            sb.Append(String.Format("\"shorttitle\": \"{0}\",",shortTitle));
            sb.Append(String.Format("\"angle\": {0},", angle));
            sb.Append(String.Format("\"radius\": {0},", Radius));
            sb.Append(String.Format("\"positionX\": {0},", positionX));
            sb.Append(String.Format("\"positionY\": {0},", positionY));
            sb.Append(String.Format("\"width\": {0},", width));
            sb.Append(String.Format("\"height\": {0},", height));

            sb.Append(String.Format("\"title\": \"{0}\",", title));
            sb.Append(String.Format("\"description\": \"{0}\",", description));

            sb.Append(String.Format("\"children\": [", shortTitle));

            int i = 0;
            if (children != null)
            {
                foreach (Node node in children)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(node.generateJSON());
                    i++;
                }
            }

            sb.Append("]");
            sb.Append("}");
            return sb.ToString();
        }

        /*
         *          public Rectangle smallBounds { get; set; }
         *          public static int centerX {  get; set; }
                    public static int centerY { get; set; }
                    public static int centerRadius { get; set; }
                    public Rectangle Bounds;
         *         

        public List<Node> children { get; set; }
        public Node parent { get; set; }
        */
    }
}
