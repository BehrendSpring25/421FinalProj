using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class EmailTask : AbsTask
    {
        private Form1? _ui => Application.OpenForms.OfType<Form1>().FirstOrDefault();
        public override void Run()
        {
            _ui.Log("Starting Email sending task");
            SendGmail sendGmail = new SendGmail();
            _ui.Log($"Sending Email to {getRecipient()}");
            sendGmail.SendMessage(getRecipient(), getSubject(), getContent());
            _ui.Log("Email sent, task finished");
        }
    }
}
