using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal interface StateIF
    {
        public void removeTask(Task task);
        public void addTask(Task task);
        public void execute();
        public void Build();
    }
}
