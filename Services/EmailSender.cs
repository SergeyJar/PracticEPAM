namespace PracticEPAM.Services
{
    using PracticEPAM.Models;
    using MailKit.Net.Smtp;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Options;
    using MimeKit;

        public class EmailSender : IEmailSender
        {
            private readonly ILogger _logger;
            public AuthMessageSenderOptions Options { get; }


            public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                ILogger<EmailSender> logger)
            {
                Options = optionsAccessor.Value;
                _logger = logger;
            }


            public async Task SendEmailAsync(string email, string subject, string message)
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Администрация сайта", "CommunityPortalService@mail.ru"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.mail.ru", 25, false);
                    await client.AuthenticateAsync("CommunityPortalService@mail.ru", "jkiifJBcTibHcYGkQjzc");
                    var response = await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
            }
        }
    }


//    public async Task SendEmailAsync(string toEmail, string subject, string message)
//    {
//        if (string.IsNullOrEmpty(Options.SendGridKey))
//        {
//            throw new Exception("Null SendGridKey");
//        }
//        await Execute(Options.SendGridKey, subject, message, toEmail);
//    }
//    public async Task Execute(string apiKey, string subject, string message, string toEmail)
//    {
//        var client = new SendGridClient(apiKey);
//        var msg = new SendGridMessage()
//        {
//            From = new EmailAddress("Slavapetlitsky2@gmail.com", "Sladafolw14"),
//            Subject = subject,
//            PlainTextContent = message,
//            HtmlContent = message
//        };
//        msg.AddTo(new EmailAddress(toEmail));

//        // Disable click tracking.
//        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
//        msg.SetClickTracking(false, false);
//        var response = await client.SendEmailAsync(msg);
//        _logger.LogInformation(response.IsSuccessStatusCode
//                               ? $"Email to {toEmail} queued successfully!"
//                               : $"Failure Email to {toEmail}");
//    }
//}

