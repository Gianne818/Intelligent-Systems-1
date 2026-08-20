using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Activity1
{
    internal class VacuumCleaning
    {





     



    }

    public abstract class Environment
    {

        public abstract void ExecuteAction(Agent agent, object action);
    }

    public abstract class Agent
    {

        //Tracks the performance measure
        public int Performance { get; set; } = 0;


        // Brain of thea agent - Takes a percept and returns an action
        public abstract object Program(object percept);
    }

    public class VacuummEnvironment : Environment
    {
        // 2X2 grid: 1 = dirty, 0 = clean
        private int[,] grid = new int[2, 2];
        private int agentX = 0;
        private int agentY = 0;
        private Random rand = new Random();

        public VacuummEnvironment()
        {
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    grid[i, j] = rand.Next(0, 2); // 0 or 1 dirty or clean
                }
            }

            agentX = 0; //Starts at top-left
            agentY = 0;
        }

        public int[] AgentLoc()
        {
            int[] loc = new int[2] { agentX, agentY };
            return loc;
        }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Grid State: ");
            for(int i = 0; i<2; i++)
            {
                for(int j =0; j<2; j++)
                {
                    sb.Append(grid[i, j] + " ");
                }

                sb.AppendLine();
            }

            sb.AppendLine($"Agent position: ({agentX}, {agentY})");
            return sb.ToString();
        }


        public object Percept(Agent agent)
        {
            // 
            bool isDirty = grid[agentX, agentY] == 1;
            return Tuple.Create(agentX, agentY, isDirty);
        }


        public override void ExecuteAction(Agent agent, object action)
        {

            string act = action as string;
            
            if(act == null)
            {
                agent.Performance -= 1;
            }
        
            if(act == "Suck")
            {

                if (grid[agentX, agentY] == 1)
                {
                    grid[agentX, agentY] = 0;
                    agent.Performance += 10; // reward for  cleaning
                }
      
            }
            else if(act == "Up" && agentX > 0)
            {
                agentX -= 1;
                agent.Performance -= 1; 

            }
            else if(act == "Down" && agentX < 1)
            {
                agentX += 1;
                agent.Performance -= 1;
            }
            else if(act == "Left" && agentY > 0)
            {
                agentY -= 1;
                agent.Performance -= 1;
            }
            else if(act == "Right" && agentY < 1)
            {
                agentY += 1;
                agent.Performance -= 1;
            }
      
        
        }


        public class SimpleReflexAgent : Agent
        {
            private readonly Random rand = new Random();

            public override object Program(object percept)
            {
                var tup = percept as Tuple<int, int, bool>;
                if (tup == null) return null;

                bool isDirty = tup.Item3;

                if (isDirty) return "Suck";

                string[] choices = { "Up", "Down", "Left", "Right" };
                return choices[rand.Next(choices.Length)];
            }
        }


    }


}
