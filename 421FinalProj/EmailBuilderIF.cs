using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class EmailBuilderIF : TaskBuilderIF
    {
        private TasksIF email = new EmailTask();

        public void Body(string content)
        {
            CommonContents c = new Content(content);
            email.setContent(c);
        }

        public TasksIF Build()
        {
            return email;
        }

        public void Subject(string subject)
        {
            CommonContents sub = new Subject(subject);
            email.setSubject(sub);
        }

        public void To(string recipient)
        {
            email.setRecipient(recipient);
        }

        public void addFeature(AdditionalFeatures feat)
        {
            email = new Decorator(email, feat.ToString());
        }
    }
}
