using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class Recipient : AdditionalFeatures
    {
        private string name;

        public Recipient(string name)
        {
            this.name = name;
        }

        public string getText()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }
    }
}
