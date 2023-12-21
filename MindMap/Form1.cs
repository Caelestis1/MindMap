using Markdig;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MindMap
{
    public partial class Form1 : Form
    {

        int panelSpace = 110;


        int centerX = 0;
        int centerY = 0;

        int standardNodeWidth = 110;
        int standardNodeHeight = 110;

        int standardNodeHalfWidth;

        Boolean dragging = false;
        Node draggedNode = null;
        int draggingOffsetX = 0;
        int draggingOffsetY = 0;

        Rectangle mainCircle;
        int mainCircleX = 0;
        int mainCircleY = 0;
        int mainCircleRadius = 0;

        int maxNodes = 0;

        Boolean VerticalScrollAndDisplay = false;

        Font textFont = new Font("Aerial", 12);

        String theresMore = "...";
        int theresMoreSize = 0;

        Image sourceImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"/images/bin.png");
        Image sourceImageOpen = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"/images/binopen.png");
        Rectangle binRect;
        Boolean overBin = false;

        Node masterRootNode;
        Rectangle masterNodeBounds;

        Boolean dataChanged = false;

        Boolean enterPressedOnForm = false;

        /* The Pen is used to draw the circle that the nodes are placed on and the 
         * line that they are placed on when there are too many to shop on the circle. 
         * to change the colour of line width use the penColor and penWidth variables.
         * The pen is created in the constructor so that it isn't on every draw.*/
        Pen myPen;
        Color penColor = Color.Gray;
        int penWidth = 2;

        // Define the colour of the nodes
        Color mainColor = Color.FromArgb(255, 183, 82, 227);

        // Define the coloured dot that highlights when a node has subnodes
        Color subColor = Color.FromArgb(255, 255, 82, 227);
        
        // Declare the brush that is used for drawing the sub element dot / circle
        Brush subElementBrush;
 
        // Declare the brush that is used for the main node drawing
        Brush brush;

        // Declare and instantiate the main brush used for drawing text
        Brush textBrush = new SolidBrush(Color.White);


        public Form1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            pbSave.BackColor = Color.Transparent;

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            pnlCanvas, new object[] { true });

            standardNodeHalfWidth = standardNodeWidth / 2;

            centerX = pnlCanvas.Width / 2;
            centerY = pnlCanvas.Height / 2;

            // Take the smaller of the panel width or height and use that to create a bounding box
            // for the central circle
            int centerSquare = pnlCanvas.Width;

            if (pnlCanvas.Height < pnlCanvas.Width)
            {
                centerSquare = pnlCanvas.Height;
            }

            mainCircle = new Rectangle(centerX - ((centerSquare - panelSpace) / 2), centerY - ((centerSquare - panelSpace) / 2), centerSquare - panelSpace, centerSquare - panelSpace);
            mainCircleX = centerX;
            mainCircleY = centerY;
            mainCircleRadius = (centerSquare - panelSpace) / 2;


            Node.centerX = centerX;
            Node.centerY = centerY;
            Node.centerRadius = mainCircleRadius;

            masterNodeBounds = new Rectangle(centerX - (standardNodeWidth / 2), centerY - (standardNodeHeight / 2), standardNodeWidth, standardNodeHeight);

             // Create Primary / Root node
            Node rootNode = new Node(standardNodeWidth, standardNodeHeight);
            rootNode.setPositionX(centerX - (standardNodeWidth / 2));
            rootNode.setPositionY(centerY - (standardNodeHeight / 2));
            rootNode.setWidth(standardNodeWidth);
            rootNode.setHeight(standardNodeHeight);
            rootNode.title = "Start";
            rootNode.children = new List<Node>();

            masterRootNode = rootNode;

            binRect = new Rectangle(0, 0, 64, 64);

            double circumferenceOfCircle = 2 * Math.PI * mainCircleRadius;
            maxNodes = (int)circumferenceOfCircle / (standardNodeHeight + (standardNodeHeight / 2));

            Debug.WriteLine(String.Format("MAX Nodes [{0}]", maxNodes));

            theresMore = "...";
            theresMoreSize = 0;

            Graphics g = pnlCanvas.CreateGraphics();
            SizeF textSize = g.MeasureString(theresMore, textFont);
            theresMoreSize = (int)textSize.Width;


            textSize = g.MeasureString(rootNode.title, textFont);

            if (textSize.Width > standardNodeWidth)
            {
                int widthPerCharAverage = (int)textSize.Width / rootNode.title.Length;
                int numberToTake = ((standardNodeWidth - theresMoreSize) - 4) / widthPerCharAverage;
                rootNode.shortTitle = rootNode.title.Substring(0, numberToTake) + theresMore;
            }
            else
            {
                rootNode.shortTitle = rootNode.title;
            }

            g = null;


            myPen = new Pen(penColor);
            myPen.Width = penWidth;

            subElementBrush = new SolidBrush(subColor);
            brush = new SolidBrush(mainColor);
            textBrush = new SolidBrush(Color.White);

            pnlCanvas.Paint += new PaintEventHandler(panel1_Paint);
            this.pnlCanvas.MouseWheel += my_MouseWheel;


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //var p = sender as Panel;
            var g = e.Graphics;

            SizeF stringLength;
            float remainder;
            float ypos;

            if (masterRootNode.parent != null)
            {
                Node parentNode = masterRootNode.parent;
                g.FillEllipse(brush, parentNode.Bounds);
                stringLength = g.MeasureString(parentNode.shortTitle, textFont);
                remainder = (parentNode.width - stringLength.Width) / 2;
                ypos = (parentNode.height / 2) - (stringLength.Height / 2);

                g.DrawString(parentNode.shortTitle, textFont, textBrush, parentNode.positionX + remainder, parentNode.positionY + ypos);

            }

            g.FillEllipse(brush, masterNodeBounds);


            stringLength = g.MeasureString(masterRootNode.shortTitle, textFont);
            remainder = (masterRootNode.width - stringLength.Width) / 2;
            ypos = (masterRootNode.height / 2) - (stringLength.Height / 2);

            g.DrawString(masterRootNode.shortTitle, textFont, textBrush, masterNodeBounds.X + remainder, masterNodeBounds.Y + ypos);


            // Draw a portion of the source image.
            if (overBin)
            {
                g.DrawImage(sourceImageOpen, 0, pnlCanvas.Height - 110, binRect, GraphicsUnit.Pixel);
            }
            else
            {
                g.DrawImage(sourceImage, 0, pnlCanvas.Height - 110, binRect, GraphicsUnit.Pixel);
            }


            if (VerticalScrollAndDisplay)
            {

                Point startPoint = new Point(mainCircleX + mainCircleRadius, 0);
                Point endPoint = new Point(mainCircleX + mainCircleRadius, pnlCanvas.Height);
                g.DrawLine(myPen, startPoint, endPoint);

                if (masterRootNode.children != null)
                {

                    foreach (Node node in masterRootNode.children)
                    {
                        if (draggedNode == null || draggedNode != node)
                        {
                            drawNode(sender, e, node, g);
                        }
                        //g.FillEllipse(brush, node.Bounds);
                        //if (node.children != null && node.children.Count() > 0)
                        //{
                        //    g.FillEllipse(subElementBrush, node.smallBounds);
                        //}

                        //stringLength = g.MeasureString(node.shortTitle, textFont);
                        //remainder = (node.width - stringLength.Width) / 2;
                        //ypos = (node.height / 2) - (stringLength.Height / 2);

                        //g.DrawString(node.shortTitle, textFont, textBrush, node.positionX + remainder, node.positionY + ypos);

                    }

                    if (draggedNode != null)
                    {
                        drawNode(sender, e, draggedNode, g);
                    }
                }
            }
            else
            {

                g.DrawEllipse(myPen, mainCircle);

                if (masterRootNode.children != null)
                {

                    foreach (Node node in masterRootNode.children)
                    {
                        drawNode(sender, e, node, g);
                        //g.FillEllipse(brush, node.Bounds);
                        //if (node.children != null && node.children.Count() > 0)
                        //{
                        //    g.FillEllipse(subElementBrush, node.smallBounds);
                        //}

                        //stringLength = g.MeasureString(node.shortTitle, textFont);
                        //remainder = (node.width - stringLength.Width) / 2;
                        //ypos = (node.height / 2) - (stringLength.Height / 2);

                        //g.DrawString(node.shortTitle, textFont, textBrush, node.positionX + remainder, node.positionY + ypos);
                    }
                }
            }
        }

        private void drawNode(object sender, PaintEventArgs e, Node node, Graphics g)
        {
            SizeF stringLength;
            float remainder;
            float ypos;

            g.FillEllipse(brush, node.Bounds);
            if (node.children != null && node.children.Count() > 0)
            {
                g.FillEllipse(subElementBrush, node.smallBounds);
            }

            stringLength = g.MeasureString(node.shortTitle, textFont);
            remainder = (node.width - stringLength.Width) / 2;
            ypos = (node.height / 2) - (stringLength.Height / 2);

            g.DrawString(node.shortTitle, textFont, textBrush, node.positionX + remainder, node.positionY + ypos);
        }

        private Node findRootNode(int X, int Y)
        {
            Node returnValue = null;

            if (X >= masterRootNode.positionX && X <= masterRootNode.positionX + masterRootNode.width
                    && Y >= masterRootNode.positionY && Y <= masterRootNode.positionY + masterRootNode.height)
            {
                returnValue = masterRootNode;
            }

            return returnValue;
        }

        private Node findNodeExcluding(int X, int Y, Node nodeToExclude)
        {
            Node returnValue = null;

            //foreach (Node node in nodes)
            if (masterRootNode.children != null)
            {
                foreach (Node node in masterRootNode.children)
                {
                    if (X >= node.positionX && X <= node.positionX + node.width
                        && Y >= node.positionY && Y <= node.positionY + node.height
                        && node != nodeToExclude)
                    {
                        returnValue = node;
                        break;
                    }
                }
            }

            return returnValue;
        }

        private Node findNode(int X, int Y)
        {
            Node returnValue = null;

            //foreach (Node node in nodes)
            if (masterRootNode.children != null)
            {
                foreach (Node node in masterRootNode.children)
                {
                    if (X >= node.positionX && X <= node.positionX + node.width
                        && Y >= node.positionY && Y <= node.positionY + node.height)
                    {
                        returnValue = node;
                        break;
                    }
                }
            }



            return returnValue;
        }

        private void pnlCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            int mouseX = e.X;
            int mouseY = e.Y;

            if (e.Button == MouseButtons.Left)
            {
                draggedNode = findNode(e.X, e.Y);
                if (draggedNode != null)
                {
                    dragging = true;
                    draggingOffsetX = mouseX - draggedNode.positionX;
                    draggingOffsetY = mouseY - draggedNode.positionY;
                }
            }
        }

        private void pullNodeToVerticalLine(Node nodeToMove)
        {
            nodeToMove.setPositionX((mainCircleX + mainCircleRadius) - standardNodeHalfWidth);
        }

        private void pullNodeToCircle(Node nodeToMove)
        {
            Vector2 centerPoint = new Vector2(centerX - standardNodeHalfWidth, centerY - standardNodeHalfWidth);
            Vector2 nodeVectorPoint = new Vector2(nodeToMove.positionX, nodeToMove.positionY);


            Vector2 delta = centerPoint - nodeVectorPoint;

            float distance = delta.Length();
            Vector2 direction = delta / distance;

            var otherPoint = nodeVectorPoint + direction * (distance - mainCircleRadius);


            nodeToMove.setPositionX((int)otherPoint.X);
            nodeToMove.setPositionY((int)otherPoint.Y);

            double radianValue = Math.Atan2((nodeToMove.positionY + standardNodeHalfWidth) - (mainCircleY), (nodeToMove.positionX + standardNodeHalfWidth) - (mainCircleX));
            double degrees = (180 / Math.PI) * radianValue;
            nodeToMove.angle = (int)degrees;

            return;
        }

        private void pnlCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            Boolean processed = false;
            if (draggedNode != null && overBin)
            {
                var confirmResult = MessageBox.Show("Delete Node?",
                                     "Node Deletion",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    masterRootNode.children.Remove(draggedNode);

                    if (masterRootNode.children.Count() < maxNodes)
                    {
                        VerticalScrollAndDisplay = false;

                        formatAsCircle();

                    }
                    dataChanged = true;
                    processed = true;
                }
            }

            if (draggedNode != null)
            {
                Node node = findNodeExcluding(e.X, e.Y, draggedNode);

                if (node != null)
                {
                    Debug.WriteLine(String.Format("Got [{0}]", node.title));
                    node.addChildNode(draggedNode);
                    draggedNode.parent = node;
                    masterRootNode.children.Remove(draggedNode);
                    if (masterRootNode.children.Count() < maxNodes)
                    {
                        VerticalScrollAndDisplay = false;

                        formatAsCircle();

                    }
                    dataChanged = true;
                    processed = true;
                }

                if (node == null && masterRootNode.parent != null)
                {
                    if (e.X <= masterRootNode.parent.positionX + masterRootNode.parent.width
                        && e.X >= masterRootNode.parent.positionX
                        && e.Y >= masterRootNode.parent.positionY
                        && e.Y <= masterRootNode.parent.positionX + masterRootNode.parent.height)
                    {
                        draggedNode.parent = masterRootNode.parent;
                        masterRootNode.parent.addChildNode(draggedNode);
                        masterRootNode.children.Remove(draggedNode);

                        dataChanged = true;
                        processed = true;
                    }
                }
            }

            if (!processed)
            {
                if (draggedNode != null && !VerticalScrollAndDisplay)
                {
                    pullNodeToCircle(draggedNode);

                }
                else if (draggedNode != null && VerticalScrollAndDisplay)
                {
                    pullNodeToVerticalLine(draggedNode);
                }
            }

            this.pnlCanvas.Invalidate();

            dragging = false;
            draggedNode = null;
            overBin = false;
        }

        private void pnlCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                draggedNode.setPositionX(e.X - draggingOffsetX);
                draggedNode.setPositionY(e.Y - draggingOffsetY);
                this.pnlCanvas.Invalidate();

                if (draggedNode.positionX < 65 && draggedNode.positionY > pnlCanvas.Height - 105)
                {
                    Debug.WriteLine("Over trash");
                    overBin = true;
                }
                else
                {
                    overBin = false;
                }
            }
        }

        private void pnlCanvas_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                MouseEventArgs mea = (System.Windows.Forms.MouseEventArgs)e;
                if (mea.Button == MouseButtons.Left)
                {
                    Node node = findNode(mea.X, mea.Y);

                    if (node == null)
                    {
                        node = findRootNode(mea.X, mea.Y);
                    }

                    if (node != null)
                    {
                        showDetailDialog(node);
                    }
                    else
                    {
                        if (masterRootNode.parent != null)
                        {
                            masterRootNode = masterRootNode.parent;
                            masterRootNode.setPositionX(masterNodeBounds.X);
                            masterRootNode.setPositionY(masterNodeBounds.Y);
                            if (masterRootNode.children != null && masterRootNode.children.Count() >= maxNodes)
                            {
                                formatAsLine();
                                VerticalScrollAndDisplay = true;
                            }
                            else
                            {
                                formatAsCircle();
                                VerticalScrollAndDisplay = false;
                            }

                            pnlCanvas.Invalidate();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                // Ignore
            }

        }

        private void showDetailDialog(Node node)
        {
            NodeDetails nd = new NodeDetails();
            nd.txtTitle.Text = node.title;
            nd.rtbNodeDescription.Text = node.description;
            nd.generateMarkdown();
            nd.ShowDialog();

            if (nd.btnPressed == NodeDetails.BTN_SAVE)
            {
                node.title = nd.txtTitle.Text;
                node.description = nd.rtbNodeDescription.Text;
                dataChanged = true;
            }

            nd.Dispose();
            nd = null;

            Graphics g = pnlCanvas.CreateGraphics();
            SizeF textSize = g.MeasureString(node.title, textFont);

            if (textSize.Width > standardNodeWidth)
            {
                int widthPerCharAverage = (int)textSize.Width / node.title.Length;
                int numberToTake = ((standardNodeWidth - theresMoreSize) - 4) / widthPerCharAverage;
                node.shortTitle = node.title.Substring(0, numberToTake) + theresMore;
            }
            else
            {
                node.shortTitle = node.title;
            }
        }

        private void pnlCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Node clickedNode = findRootNode(e.X, e.Y);

                if (clickedNode != null)
                {
                    addNode(clickedNode);
                    dataChanged = true;
                }
                else
                {
                    clickedNode = findNode(e.X, e.Y);

                    if (clickedNode != null)
                    {
                        // This now becomes its own rootNode
                        // Move "start" node to the 
                        masterRootNode = clickedNode;
                        masterRootNode.setPositionX(masterNodeBounds.X);
                        masterRootNode.setPositionY(masterNodeBounds.Y);

                        masterRootNode.parent.setPositionX(10);
                        masterRootNode.parent.setPositionY(10);

                        if (masterRootNode.children != null && masterRootNode.children.Count() >= maxNodes)
                        {
                            VerticalScrollAndDisplay = true;
                            formatAsLine();
                        }
                        else
                        {
                            VerticalScrollAndDisplay = false;
                            formatAsCircle();
                        }

                        pnlCanvas.Invalidate();
                    }
                }
            }
        }

        private void pnlCanvas_Scroll(object sender, ScrollEventArgs e)
        {
            Debug.WriteLine("Scrolling");
        }

        private void generateNewDegree(Node node, Boolean clockwise = true)
        {
            int currentAngle = node.angle;
            if (clockwise)
            {
                currentAngle += 2;
            }
            else
            {
                currentAngle -= 2;
            }

            int x = (int)(mainCircleX + mainCircleRadius * Math.Cos((currentAngle * (Math.PI / 180))));
            int y = (int)(mainCircleY + mainCircleRadius * Math.Sin((currentAngle * (Math.PI / 180))));
            node.setPositionX(x - standardNodeHalfWidth);
            node.setPositionY(y - standardNodeHalfWidth);
            node.angle = currentAngle;
        }

        private void my_MouseWheel(object sender, MouseEventArgs e)
        {
            if (masterRootNode.children == null || masterRootNode.children.Count() == 0)
            {
                return;
            }
            if (e.Delta > 0)
            {
                Node lastNode = masterRootNode.children.Last();
                if (VerticalScrollAndDisplay && lastNode != null && (lastNode.positionY + lastNode.height) < pnlCanvas.Height)
                {
                    // stop
                }
                else
                {

                    foreach (Node node in masterRootNode.children)
                    {
                        if (VerticalScrollAndDisplay)
                        {
                            // Clockwise scrolls the vertical list down
                            node.setPositionY(node.positionY - 10);
                        }
                        else
                        {
                            generateNewDegree(node);
                        }

                    }
                }
                this.pnlCanvas.Invalidate();
            }
            else
            {
                Node firstNode = masterRootNode.children.First();

                if (VerticalScrollAndDisplay && firstNode != null && firstNode.positionY > 0)
                {
                    // stop
                }
                else
                {

                    foreach (Node node in masterRootNode.children)
                    {
                        if (VerticalScrollAndDisplay)
                        {
                            node.setPositionY(node.positionY + 10);
                        }
                        else
                        {
                            generateNewDegree(node, false);
                        }

                    }
                }
                this.pnlCanvas.Invalidate();
            }

        }

        private void pnlCanvas_Resize(object sender, EventArgs e)
        {
            centerX = pnlCanvas.Width / 2;
            centerY = pnlCanvas.Height / 2;

            int pnlW = pnlCanvas.Width;
            int pnlH = pnlCanvas.Height;

            if (pnlW < pnlH)
            {
                pnlH = pnlW;
            }
            else
            {
                pnlW = pnlH;
            }

            mainCircle = new Rectangle(centerX - ((pnlW - panelSpace) / 2), centerY - ((pnlH - panelSpace) / 2), pnlW - panelSpace, pnlH - panelSpace);
            //mainCircle = new Rectangle(centerX - ((pnlW - 50) / 2), centerY - ((pnlH - 50) / 2), pnlW - 50, pnlH - 50);
            mainCircleX = centerX;
            mainCircleY = centerY;
            mainCircleRadius = (pnlW - panelSpace) / 2;
            //mainCircleRadius = (pnlW - 50) / 2;

            Node.centerX = centerX;
            Node.centerY = centerY;
            Node.centerRadius = mainCircleRadius;

            double circumferenceOfCircle = 2 * Math.PI * mainCircleRadius;
            maxNodes = (int)circumferenceOfCircle / 80;

            Debug.WriteLine(String.Format("MAX Nodes [{0}]", maxNodes));

            //Node rootNode = nodes.First();
            masterRootNode.setPositionX(centerX - (standardNodeWidth / 2));
            masterRootNode.setPositionY(centerY - (standardNodeHeight / 2));
            masterRootNode.setWidth(standardNodeWidth);
            masterRootNode.setHeight(standardNodeHeight);

            foreach (Node node in masterRootNode.children)
            {
                pullNodeToCircle(node);
                this.pnlCanvas.Invalidate();
            }

            this.pnlCanvas.Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F)
            {
                Debug.WriteLine("Format");
                // Get the root node

                if (!VerticalScrollAndDisplay)
                {
                    formatAsCircle();
                }
                else
                {
                    formatAsLine();
                }

                this.pnlCanvas.Invalidate();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                addNode(masterRootNode);
                dataChanged = true;
            }
            else if (e.KeyCode == Keys.S)
            {
                saveFile();
            }
            else if (e.KeyCode == Keys.O)
            {
                loadFile();
                return;
            }
            else if (e.KeyCode == Keys.E)
            {
                Document document = new Document();
                MapMarkdownGenerator gen = new MapMarkdownGenerator();
                String html = gen.generateMarkDown(masterRootNode);
                MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                html = Markdown.ToHtml(html, pipeline);
                html = html + "<script type=\"module\">import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.esm.min.mjs';</script>";

                document.setSource(html);

                document.ShowDialog();
                return;
            }
            else if (e.KeyCode == Keys.Enter && enterPressedOnForm)
            {
                e.Handled = true;
                if (masterRootNode.children != null)
                {
                    showDetailDialog(masterRootNode.children.Last());
                    pnlCanvas.Invalidate();
                }
            }
            else if ((e.KeyData & Keys.Control) == Keys.Control && (e.KeyData & Keys.X) == Keys.X)
            {
                exitApplication();
            }

            enterPressedOnForm = false;
        }

        private void saveFile()
        {
            DialogResult dialofresult = dlgSaveFile.ShowDialog();

            if (dialofresult == DialogResult.OK)
            {
                Debug.WriteLine(String.Format("{0}", dlgSaveFile.FileName));
                // find the top top most parentNode..
                Node tNode = masterRootNode;

                while (tNode.parent != null)
                {
                    tNode = tNode.parent;
                }

                string json = JsonConvert.SerializeObject(tNode, Newtonsoft.Json.Formatting.Indented);
                Debug.WriteLine(json);
                File.WriteAllText(dlgSaveFile.FileName, json);
                dataChanged = false;
            }

            return;
        }

        private void loadFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON files (*.json)|*.json";
            DialogResult dialofresult = ofd.ShowDialog();

            if (dialofresult == DialogResult.OK)
            {
                dataChanged = false;
                // find the top top most parentNode..
                Node tNode = masterRootNode;

                while (tNode.parent != null)
                {
                    tNode = tNode.parent;
                }

                string json = File.ReadAllText(ofd.FileName);

                masterRootNode = JsonConvert.DeserializeObject<Node>(json);

                // Fix recursive parent nodes
                if (masterRootNode.children != null)
                {
                    fixRecursiveParentNodes(masterRootNode);

                    if (masterRootNode.children.Count >= maxNodes)
                    {
                        VerticalScrollAndDisplay = true;
                        formatAsLine();
                    }
                    else
                    {
                        formatAsCircle();
                    }

                }

                pnlCanvas.Invalidate();
            }
        }

        private void exitApplication()
        {
            if (dataChanged)
            {
                DialogResult dlg = MessageBox.Show("Exit and lose changes?", "Confirm", MessageBoxButtons.OKCancel);

                if (dlg.Equals(DialogResult.OK))
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
        private void fixRecursiveParentNodes(Node node)
        {
            foreach (Node child in node.children)
            {
                child.parent = node;
                if (child.children != null)
                {
                    fixRecursiveParentNodes(child);
                }
            }
        }

        private void formatAsLine()
        {
            int lineXPosition = (mainCircleX + mainCircleRadius) - standardNodeHalfWidth;
            int yPosition = standardNodeHalfWidth;
            Point startPoint = new Point(mainCircleX + mainCircleRadius, 0);

            foreach (Node processingNode in masterRootNode.children)
            {
                processingNode.setPositionX(lineXPosition);
                processingNode.setPositionY(yPosition);

                yPosition += processingNode.height + standardNodeHalfWidth;
            }
        }

        private void formatAsCircle()
        {
            //Node rootNode = nodes.First();

            if (masterRootNode.children == null || masterRootNode.children.Count() == 0)
            {
                return;
            }

            int totalNumberofChildren = masterRootNode.children.Count();
            int degrees = 360 / totalNumberofChildren;
            int startDegree = 0;

            foreach (Node node in masterRootNode.children)
            {
                int x = (int)(mainCircleX + mainCircleRadius * Math.Cos((startDegree * (Math.PI / 180))));
                int y = (int)(mainCircleY + mainCircleRadius * Math.Sin((startDegree * (Math.PI / 180))));
                node.setPositionX(x - standardNodeHalfWidth);
                node.setPositionY(y - standardNodeHalfWidth);
                node.angle = startDegree;

                startDegree += degrees;
            }
        }

        private void addNode(Node clickedNode)
        {


            if (clickedNode == masterRootNode)
            {
                Node node = new Node(standardNodeWidth, standardNodeHeight);
                node.parent = masterRootNode;
                pullNodeToCircle(node);
                masterRootNode.addChildNode(node);

                if (masterRootNode.children.Count() >= maxNodes)
                {
                    VerticalScrollAndDisplay = true;
                    Debug.WriteLine("Swap to vertical display");

                    // Recalculate locations based on the line location
                    int lineXPosition = (mainCircleX + mainCircleRadius) - standardNodeHalfWidth;
                    int yPosition = standardNodeHalfWidth;
                    Point startPoint = new Point(mainCircleX + mainCircleRadius, 0);

                    foreach (Node processingNode in masterRootNode.children)
                    {
                        processingNode.setPositionX(lineXPosition);
                        processingNode.setPositionY(yPosition);

                        yPosition += processingNode.height + standardNodeHalfWidth;
                    }


                }


                this.pnlCanvas.Invalidate();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enterPressedOnForm = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            exitApplication();
        }

        private void pbSave_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void pbOpen_Click(object sender, EventArgs e)
        {
            loadFile();
        }

        private void pbHelp_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }
    }
}