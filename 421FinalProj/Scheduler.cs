using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace _421FinalProj
{
    internal class Scheduler
    {
        private Thread? runningThread;
        private readonly object threadLock = new object();
        private readonly Queue<Thread> scheduled = new Queue<Thread>(); 

        public void Enter()
        {
            Thread thisThread = Thread.CurrentThread;

            lock (threadLock)
            {
                if (runningThread == null)
                {
                    runningThread = thisThread;
                    return;
                }

                scheduled.Enqueue(thisThread);

                while (runningThread != thisThread)
                {
                    Monitor.Wait(threadLock); 
                }
            }
        }

        public void Exit()
        {
            lock (threadLock)
            {
                if (scheduled.Count > 0)
                {
                    runningThread = scheduled.Dequeue();
                    Monitor.PulseAll(threadLock); 
                }
                else
                {
                    runningThread = null;
                }
            }
        }
    }
}