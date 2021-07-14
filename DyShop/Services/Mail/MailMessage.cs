using System.Collections.Generic;
using System.Net.Mail;
using MimeKit;

namespace DyShop.Services.Mail
{
    public class MailMessage<TModel>
    {
        public bool FillDefaultEmailFrom { get; set; } = true;
        
        public bool FillDefaultEmailTo { get; set; }
        
        public List<MailboxAddress> To { get; set; } = new List<MailboxAddress>();

        public List<MailboxAddress> From { get; set; } = new List<MailboxAddress>();
        
        public string Subject { get; set; }
        
        public string TemplateName { get; set; }
        
        public TModel Model { get; set; }
    }
}