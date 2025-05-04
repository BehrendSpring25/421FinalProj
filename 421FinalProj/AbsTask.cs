using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal abstract class AbsTask : TasksIF
    {
        private string recipient;
        private CommonContents contents;
        private CommonContents subject;

        public string getContent()
        {
            return contents.getText();
        }

        public CommonContents getCommonContent()
        {
            return contents;
        }

        public string getRecipient()
        {
            return recipient;
        }

        public string getSubject()
        {
            return subject.getText();
        }

        public void setRecipient(string recipient)
        {
            this.recipient = recipient;
        }

        public void setContent(CommonContents contents)
        {
            this.contents = contents;
        }

        public void setSubject(CommonContents subject)
        {
            this.subject = subject;
        }

        public abstract void Run();
    }
}
