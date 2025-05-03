using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class EmailTask : AbsTask
    {
        public override void Run()
        {
            Thread t = new Thread(() =>
            {
                Console.WriteLine("Sending Email to: " + getRecipient());
                Console.WriteLine("Subject: " + getSubject());
                Console.WriteLine("Content: " + getContent());
                Console.WriteLine("Email sent successfully!");
            });

            t.Start();
        }
    }
}
