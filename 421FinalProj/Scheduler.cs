using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace _421FinalProj
{
    internal class Scheduler
    {
        private Thread? runningThread;
        private readonly Queue<(Thread thread, TasksIF task)> waitingQueue = new();

        public void Enter(TasksIF t)
        {
            Thread thisThread = Thread.CurrentThread;

            lock (this)
            {
                if (runningThread == null)
                {
                    runningThread = thisThread;
                    return;
                }

                waitingQueue.Enqueue((thisThread, t));
            }

            lock (thisThread)
            {
                while (thisThread != runningThread)
                {
                    Monitor.Wait(thisThread); // wait until this thread is signaled
                }
            }
        }

        public void Exit()
        {
            lock (this)
            {
                if (waitingQueue.Count > 0)
                {
                    var (nextThread, _) = waitingQueue.Dequeue();
                    runningThread = nextThread;

                    lock (nextThread)
                    {
                        Monitor.Pulse(nextThread); // signal the next thread
                    }
                }
                else
                {
                    runningThread = null;
                }
            }
        }
    }
}