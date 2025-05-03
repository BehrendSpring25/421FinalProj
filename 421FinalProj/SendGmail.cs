using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;
using System;
using System.IO;
using System.Threading;
using File = System.IO.File;

namespace _421FinalProj
{
    internal class SendGmail
    {
        public GmailService GetService()
        {
            using var stream = File.OpenRead("GmailAPIKey\\client_secret.json");

            var credPath = "token.json";
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                new[] { GmailService.Scope.GmailSend },
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)
            ).Result;

            return new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Gmail-API-SendDemo"
            });
        }

        public MimeMessage BuildMessage()
        {
            var msg = new MimeMessage();
            msg.From.Add(MailboxAddress.Parse("me"));                 // “me” = authenticated user
            msg.To.Add(MailboxAddress.Parse("ckm5913@psu.edu"));
            msg.Subject = "Hello from the Gmail API";

            msg.Body = new TextPart("plain") { Text = "your mother" };
            return msg;
        }

        public void SendMessage()
        {
            var service = GetService();
            var mime = BuildMessage();

            using var ms = new MemoryStream();
            mime.WriteTo(ms);
            var rawBytes = ms.ToArray();

            var gMsg = new Google.Apis.Gmail.v1.Data.Message
            {
                Raw = Convert.ToBase64String(rawBytes)
                            .Replace('+', '-')
                            .Replace('/', '_')
                            .Replace("=", "")
            };

            var result = service.Users.Messages.Send(gMsg, "me").Execute();
            Console.WriteLine($"Sent!   Gmail Id: {result.Id}");
        }
    }
}
