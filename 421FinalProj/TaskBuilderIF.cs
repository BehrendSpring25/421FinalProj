using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal interface TaskBuilderIF
    {
        public void To(string recipient);
        public void Subject(string subject);
        public void Body(string content);

        public TasksIF Build();
    }
}
