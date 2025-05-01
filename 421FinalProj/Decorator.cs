using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class Decorator : AbsTask
    {

        public Decorator(TasksIF task, AdditionalFeatures feature)
        {
            string recip = task.getRecipient();
            recip = recip + " ," + feature.getText();
            setRecipient(recip);
        }
    }
}
