using System;
using System.Threading;

namespace _421FinalProj
{
    internal class Decorator : AbsTask
    {
        private readonly TasksIF _task;

        public Decorator(TasksIF task, string carrierGateway)
        {
            _task = task;
            string recip = task.getRecipient();
            setRecipient($"{recip}@{carrierGateway}");
            setContent(task.getCommonContent());
        }

        public override void Run()
        {
            _task.Run();
        }
    }
}
