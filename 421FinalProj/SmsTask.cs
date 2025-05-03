using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class SmsTask : AbsTask
    {
        public override void Run()
        {
            Thread t = new Thread(() =>
            {
                Console.WriteLine("Sending SMS to: " + getRecipient());
                Console.WriteLine("Content: " + getContent());
                Console.WriteLine("SMS sent successfully!");
            });

            t.Start();
        }
    }
}
