using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace August19
{
    public partial class Form1 : Form
    {
        //BFS
        Queue<Node> frontier;
        HashSet<Node> visited;
        List<Node> origin;
        Node current;


        public Form1()
        {
            InitializeComponent();
        }

        public void runBFS(Node start, Node goal)
        {
            richTextBox1.Text = "running BFS...";
            frontier = new Queue<Node>();
            visited = new HashSet<Node>();
            origin = new List<Node>();

            // add to queue the starting node
            frontier.Enqueue(start);
            visited.Add(start);
            origin.Add(start);

            // explore now

        while(frontier.Count > 0)
            {
                current = frontier.Dequeue();
                richTextBox1.Text += "\n current node: " + current.ToString();

                // cehck if goal is reached
                if(current.Row == goal.Row && current.Col == goal.Col)
                {
                    richTextBox1.Text += "\nGOAL REACHED";
                    this.Refresh();
                    return;
                }

                // else we check neighbors in this order: up down left right
                foreach(Node neighbor in MazeSolver.GetNeighbors(current))
                {
   
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor); // is a set, so does not add a repeating node
                        frontier.Enqueue(neighbor); // adds the queue to the fringe
                        origin.Add(current); // see the origin of the neighbor (next node)
                    }
                }
                richTextBox3.Text = string.Join("\n", frontier.ToArray());
                richTextBox4.Text = string.Join("\n", visited.ToArray());
                richTextBox5.Text = string.Join("\n", origin.ToArray());

                this.Refresh();
                try
                {
                    Thread.Sleep(1000);
                } catch
                {

                }
                
            }
            richTextBox1.Text += "\nGOAL NOT REACHED";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            runBFS(new Node(0, 0), new Node(4, 4));
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int[,] grid = MazeSolver.GetGrid();
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            int cellWidth = Math.Max(1, pictureBox1.ClientSize.Width / cols); 
            int cellHeight = Math.Max(1, pictureBox1.ClientSize.Height / rows); // cell size may be less than 1 and return float so use max

            for (int r = 0; r < rows; r++)
            {
                for(int c = 0; c < cols; c++)
                {
                    Rectangle rect = new Rectangle(c * cellWidth, r * cellHeight, cellWidth, cellHeight);
                    if (grid[r, c] == 1)
                    {
                        e.Graphics.FillRectangle(Brushes.Black, rect); // obstacles or walls
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.White, rect); // path
                    }
                    e.Graphics.DrawRectangle(Pens.Gray, rect); // outline for cells
                }
                Rectangle rect1 = new Rectangle(current.Col * cellWidth, current.Row * cellHeight, cellWidth, cellHeight);
                e.Graphics.FillRectangle(Brushes.Red, rect1);
                e.Graphics.DrawString(current.ToString(), new Font("Arial", 8), Brushes.Black, rect1);
            }
        }
    }
}
