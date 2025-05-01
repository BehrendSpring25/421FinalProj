using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class Canvas
    {
        private static Canvas c;
        private List<TasksIF> tasks;
        private StateIF state;

        private Canvas()
        {
            c = new Canvas();
        }

        public static Canvas getInstance()
        {
            if (c == null)
            {
                c = new Canvas();
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
    }
}
