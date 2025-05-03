using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace _421FinalProj
{
    internal class Scheduler
    {
        private Thread? runningThread;
        private ArrayList waitingThreads = new ArrayList();
        private ArrayList waitingRequests = new ArrayList();


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

                waitingThreads.Add(thisThread);
                waitingRequests.Add(t);
            }
            lock (thisThread)
            {
                while (thisThread != runningThread)
                {
                    Monitor.Wait(thisThread); // wait until this thread is signaled
                }
            }

            lock (this)
            {
                int i = waitingThreads.IndexOf(thisThread);
                waitingThreads.Remove(i);
                waitingRequests.RemoveAt(i);
            }
        }

        public void Exit()
        {
            int waitCount = waitingThreads.Count;
            if (waitCount <= 0)
            {
                runningThread = null;
            }
            else if (waitCount == 1)
            {
                runningThread = (Thread)waitingThreads[0];
            }
            else
            {
                int next = waitCount - 1;
                TasksIF nextTask = (TasksIF)waitingRequests[next];

                for (int i = waitCount - 2; i >= 0; --i)
                {
                    TasksIF r;
                    r = (TasksIF)waitingRequests[i];

                    runningThread = (Thread)waitingThreads[i];

                    lock (runningThread)
                    {
                        Monitor.PulseAll(runningThread); // signal the next thread
                    }
                }
            }
        }
    }
}