using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class SMSBuilderIF : TaskBuilderIF
    {
        private TasksIF sms = new SmsTask();

        public void Body(string content)
        {
            CommonContents body = new Content(content);
            sms.setContent(body);
        }

        public TasksIF Build()
        {
            return sms;
        }

        public void Subject(string subject)
        {
            CommonContents body = new Subject(subject);
            sms.setSubject(body);
        }

        public void To(string recipient)
        {
            sms.setRecipient(recipient);
        }

        public void AddCarrierGateway(string carrierGateway)
        {
            sms = new Decorator(sms, carrierGateway);
        }
    }
}
