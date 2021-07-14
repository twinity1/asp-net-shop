using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Razor.Templating.Core;

namespace DyShop.Services.Mail
{
    public class MailService
    {
        private readonly SmtpConfig _smtpConfig;
        private const string DefaultEmail = "kutekales@gmail.com";

        public MailService(IOptions<SmtpConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        
        public async Task Send<TModel>(MailMessage<TModel> mailMessage)
        {
            using var client = new SmtpClient ();
            
            var connectAsync = client.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port, _smtpConfig.Ssl);
                
            var message = await ConfigureMessage(mailMessage);

            await connectAsync;

            if (_smtpConfig.Username != null && _smtpConfig.Password != null)
            {
                await client.AuthenticateAsync(_smtpConfig.Username, _smtpConfig.Password);
            }

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        private async Task<MimeMessage> ConfigureMessage<TModel>(MailMessage<TModel> mailMessage)
        {
            var message = new MimeMessage();

            message.From.AddRange(mailMessage.From);
            message.To.AddRange(mailMessage.To);

            if (mailMessage.FillDefaultEmailFrom)
            {
                message.From.Add(new MailboxAddress("Admin", DefaultEmail));
            }
            if (mailMessage.FillDefaultEmailTo)
            {
                message.To.Add(new MailboxAddress("Admin", DefaultEmail));
            }

            message.Subject = mailMessage.Subject;

            message.Body = new TextPart("html")
            {
                Text = await RazorTemplateEngine.RenderAsync($"~/Services/Mail/Templates/{mailMessage.TemplateName}.cshtml", mailMessage.Model)
            };
            
            return message;
        }
    }
}