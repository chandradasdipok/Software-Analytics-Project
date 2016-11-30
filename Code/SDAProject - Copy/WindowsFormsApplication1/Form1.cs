using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDAProject;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private List<Node> nodes = new List<Node>();
        private int _heightUnit = 100;
        private int _totalWidth = 1000;
        int rectHeight = 100;
        int rectWidth = 100;

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Size = new Size(this.Size.Width*2, this.Size.Height*2);
            // DrawIt();
            /*nodes.Add(new Node { Text = "node1" });
            nodes.Add(new Node { Text = "node2"});
            nodes.Add(new Node { Text = "node2"});
            nodes.Add(new Node { Text = "node3"});
            nodes.Add(new Node { Text = "node3"});

            nodes.ToArray()[0].ChildNodes.Add(nodes.ToArray()[1]);
            nodes.ToArray()[0].ChildNodes.Add(nodes.ToArray()[2]);
            nodes.ToArray()[1].ChildNodes.Add(nodes.ToArray()[3]);
            nodes.ToArray()[1].ChildNodes.Add(nodes.ToArray()[4]);

            AddLabel(nodes[0]);
*/
            IConceptLattice conceptLattice = new Algorithm();
            string fcaFilePath = @"E:\MSSE Program\MSSE 2nd Semester\Software Analytics\Code\my_input.txt";
            string latticeFilePath = @"E:\MSSE Program\MSSE 2nd Semester\Software Analytics\Code\lattice_input.txt";
            var concepts = conceptLattice.ConstructConceptLattice(fcaFilePath, latticeFilePath);

            nodes = ConvertToNodes(concepts);
            AddLabel(nodes[0]);

            int maxLevel = nodes.Max(node => node.Level);

            for (int i = 0; i <= maxLevel; i++)
            {
                var sameLevelNodes = nodes.Where(node => node.Level.Equals(i)).ToArray();
                for (int j = 0; j < sameLevelNodes.Count(); j++)
                {
                    Node nd = sameLevelNodes[j];
                    nd.X = this.Size.Width / sameLevelNodes.Count() * (j);
                    nd.Y = 150 * i;
                }
            }

            foreach (var node in nodes)
            {
                DrawIt(node);
                foreach (var child in node.ChildNodes)
                {
                    DrawEdge(node, child);
                }
            }

        }

        private List<Node> ConvertToNodes(List<Concept> concepts)
        {
            var map = new Dictionary<Concept, Node>();
            foreach (var concept in concepts)
            {
                String txt = "";
                foreach (var intent in concept.Intents)
                {
                    txt = txt + intent + " ";
                }
                Node node = new Node {Text = txt};
                map[concept] = node;
            }

            foreach (var concept in concepts)
            {
                foreach (var childConcept in concept.ChildList)
                {
                    map[concept].ChildNodes.Add(map[childConcept]);
                }
            }
            return map.Values.ToList();
        }

        private void DrawEdge(Node parent, Node child)
        {
            Graphics graphics = this.CreateGraphics();
            float x1 = parent.X + rectHeight / 2;
            float y1 = parent.Y + rectWidth;
            float x2 = child.X + rectHeight / 2;
            float y2 = child.Y;
            graphics.DrawLine(Pens.Brown, x1, y1, x2, y2);
        }

        private void DrawIt(Node node)
        {
            Graphics graphics = this.CreateGraphics();
            Rectangle rectangle = new Rectangle(node.X, node.Y, rectHeight, rectWidth);
            TextFormatFlags flags = TextFormatFlags.WordBreak;
            TextRenderer.DrawText(graphics, node.Text, new Font("Arial", 10), rectangle, Color.Blue, flags);
            //graphics.DrawString(node.Text,new Font("Arial",10), Brushes.Blue, rectangle);
            // graphics.DrawEllipse(System.Drawing.Pens.Black, rectangle);
            graphics.DrawRectangle(System.Drawing.Pens.Red, rectangle);
        }

        public void AddLabel(Node root)
        {
            root.Level = 0;
            var stack = new Stack<Node>();
            stack.Push(root);

            while (stack.Count != 0)
            {
                var parentNode = stack.Pop();
                foreach (var childNode in parentNode.ChildNodes)
                {
                    childNode.Level = parentNode.Level + 1;
                    stack.Push(childNode);
                }
            }
        }

    }

    public class Node
    {
        public string Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Level { get; set; }
        public List<Node> ChildNodes { get; set; }
        public Node()
        {
            ChildNodes = new List<Node>();
        }
    }



}
