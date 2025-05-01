using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class Content : CommonContents
    {

        private string content;

        public Content(string content)
        {
            this.content = content;
        }

        public string getText()
        {
            return content;
        }
    }
}
