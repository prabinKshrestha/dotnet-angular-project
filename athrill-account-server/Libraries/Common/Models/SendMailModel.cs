using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Models
{
    public class SendMailModel
    {
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public bool IsHtml { get; set; }
        public string TextBody { get; set; }
        public string HtmlBody { get; set; }
        public SendMailContentModel Content { get; set; }
        public SendMailButtonModel Button { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }

    public class SendMailButtonModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
    public class SendMailContentModel
    {
        public string Title { get; set; }
        public List<string> Bodies { get; set; }
    }
}
