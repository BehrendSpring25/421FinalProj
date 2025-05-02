using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class CanvasManager
    {
        private static CanvasManager c;
        private List<TasksIF> tasks;
        private StateIF state;

        private CanvasManager()
        {
            
        }

        public static CanvasManager getInstance()
        {
            if (c == null)
            {
                c = new CanvasManager();
            }
            return c;
        }

        public void addTask(TasksIF task)
        {
            tasks.Add(task);
        }

        public void removeTask(TasksIF task)
        {
            tasks.Remove(task);
        }

        public void setState(StateIF state)
        {
            this.state = state;
        }

        public List<TasksIF> getTasks() {
            return tasks;
        }

        public StateIF getState()
        {
            return state;
        }
    }
}
