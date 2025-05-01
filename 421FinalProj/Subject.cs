using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class Subject : CommonContents
    {
        private string subject;

        public Subject(string subject)
        {
            this.subject = subject;
        }

        public string getText()
        {
            return subject;
        }
    }
}
