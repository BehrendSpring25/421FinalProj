using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class SmsTask : AbsTask
    {
        private Form1? _ui => Application.OpenForms.OfType<Form1>().FirstOrDefault();
        public override void Run()
        {
            _ui.Log("Starting SMS sending task");
            SendGmail sendGmail = new SendGmail();
            _ui.Log($"Sending SMS to {getRecipient()}@tmomail.net");
            sendGmail.SendMessage($"{getRecipient()}@tmomail.net", "", getContent());
            _ui.Log("SMS sent, task finished");
        }
    }
}
