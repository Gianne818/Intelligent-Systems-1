using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Activity1.VacuummEnvironment;

namespace Activity1
{
    public partial class Form1 : Form
    {
        VacuummEnvironment env = new VacuummEnvironment();
        Agent agent = new SimpleReflexAgent();
        public Form1()
        {
            InitializeComponent();
        }

        private void refreshOrDraw(int x, int y)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.DrawArc(Pens.RoyalBlue, x, y, 20, 20, 0, 360);
            }

            xz += 10;
            yz += 10;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text = "Creating 2x2 World\n";
            //richTextBox1.Text += env;
            refreshOrDraw(0, 0);

            for (int step = 0; step < 10; step++)
            {
                var percept = env.Percept(agent);

                var action = agent.Program(percept) as string;

                env.ExecuteAction(agent, action);

                var tup = percept as Tuple<int, int, bool>;
                string locationText = "(? ,?)";
                if (tup != null)
                {
                    locationText = $"{tup.Item1}, {tup.Item2})";
                }

                richTextBox1.AppendText($"Step {step + 1}: Action = {action} | Location = {locationText} | askScore = {agent.Performance}\r\n");

                this.Refresh();
                refreshOrDraw((pictureBox1.Width/4)*tup.Item1, (pictureBox1.Width/4)*tup.Item2);
                try
                {
                    Thread.Sleep(500);
                    //pictureBox1.Invalidate();
                }
                catch (Exception)
                {
                    richTextBox1.Text = "error tabang error:-(";
                    throw;
                }
            }

            richTextBox1.AppendText("\r\n--- Final 2x2 World ---\r\n");
            richTextBox1.AppendText(env.ToString());
            richTextBox1.AppendText($"Final Performance Score: {agent.Performance}\r\n");


        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawArc(Pens.RoyalBlue, 0, 0, 20, 20, 0, 360);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        int xz = 0;
        int yz = 0;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //var percept = env.Percept(agent);
            //var tup = percept as Tuple<int, int, bool>;
            //pictureBox1_Paint(sender, e);
            //pictureBox1.Invalidate();
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.DrawArc(Pens.RoyalBlue, xz, yz, 20, 20, 0, 360);
            }

            xz += 10;
            yz += 10;

        }


    }
}
