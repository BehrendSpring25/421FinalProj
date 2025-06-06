﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal interface TasksIF
    {
        public string getRecipient();
        public string getSubject();
        public string getContent();
        public CommonContents getCommonContent();
        public void setContent(CommonContents contents);
        public void setRecipient(string recipient);
        public void setSubject(CommonContents subject);
        public void Run();
    }
}
