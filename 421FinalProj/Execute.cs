using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    
    internal class Execute : StateIF
    {
        private Scheduler s = new Scheduler();
        public void addTask(Task task)
        {
            throw new NotImplementedException();
        }

        public void execute()
        {
            foreach(TasksIF task in CanvasManager.getInstance().getTasks())
            {
                try
                {
                    s.Enter(task);
                    try
                    {
                        task.Run(); // Tasks will run sequentially
                    }
                    finally
                    {
                        s.Exit();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error executing task: {e.Message}");
                }
            }
        }

        public void removeTask(Task task)
        {
            throw new NotImplementedException();
        }

        public void Build()
        {
            throw new NotImplementedException();
        }
    }
}
