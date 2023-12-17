using System.Net;
using System.Net.Mail;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using TemplateDDD.Core.Services.Interfaces;
using TemplateDDD.Data.Dtos.Common;
using TemplateDDD.Shared.Models;
using TemplateDDD.Shared.Responses;

namespace TemplateDDD.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly Appsettings _appsettings;

        public EmailService(ILogger<EmailService> logger, IOptions<Appsettings> options)
        {
            _logger = logger;
            _appsettings = options.Value;
        }

        public async Task<ServiceResponse<bool>> SendEmail(EmailDto email)
        {
            var response = new ServiceResponse<bool>();
            SmtpServerSettings serverSettings = new SmtpServerSettings();

            try
            {
                MailMessage mailMessage = new();
                foreach (var mail in email.EmailTo)
                {
                    mailMessage.To.Add(mail);
                }
                
                mailMessage.From = new MailAddress(email.EmailFrom);
                mailMessage.Subject = email.EmailSubject;
                mailMessage.Body = email.EmailBody;
                mailMessage.IsBodyHtml = true;

                // Log email here...
                _logger.LogInformation($"Template DDD service Exception: Sending email to {email.EmailTo}. Date: {DateTime.Now}");

                var server = _appsettings.EmailServerType;
                if (server == 0)
                {
                    serverSettings = new SmtpServerSettings
                    {
                        Host = _appsettings.MailSettings.Host,
                        Port = _appsettings.MailSettings.Port,
                        DisplayName = _appsettings.MailSettings.DisplayName,
                        UseDefaultCredentials = _appsettings.MailSettings.UseDefaultCredentials,
                        UseSSl = _appsettings.MailSettings.UseSSl,
                        Mail = _appsettings.MailSettings.Mail,
                        Password = _appsettings.MailSettings.Password
                    };
                }
                else if (server == 1)
                {
                    serverSettings = new SmtpServerSettings
                    {
                        Host = _appsettings.EtherealMailSettings.Host,
                        Port = _appsettings.EtherealMailSettings.Port,
                        DisplayName = _appsettings.EtherealMailSettings.DisplayName,
                        UseDefaultCredentials = _appsettings.EtherealMailSettings.UseDefaultCredentials,
                        UseSSl = _appsettings.EtherealMailSettings.UseSSl,
                        Mail = _appsettings.EtherealMailSettings.Mail,
                        Password = _appsettings.EtherealMailSettings.Password
                    };
                }
                else
                {
                    serverSettings = new SmtpServerSettings
                    {
                        Host = _appsettings.EmailConfigSetting.Host,
                        Port = _appsettings.EmailConfigSetting.Port,
                        DisplayName = _appsettings.EtherealMailSettings.DisplayName,
                        UseDefaultCredentials = _appsettings.EtherealMailSettings.UseDefaultCredentials,
                        UseSSl = _appsettings.EmailConfigSetting.UseSSL,
                        Mail = _appsettings.EmailConfigSetting.EmailId,
                        Password = _appsettings.EmailConfigSetting.Password
                    };
                }

                using var smtp = new SmtpClient(serverSettings.Host, serverSettings.Port);

                if (server == 0 && serverSettings.UseDefaultCredentials)
                {
                    smtp.UseDefaultCredentials = true;
                    smtp.Send(mailMessage);
                }

                if (server == 0 && !serverSettings.UseDefaultCredentials)
                {
                    smtp.Credentials = new NetworkCredential(serverSettings.Mail, serverSettings.Password);
                    smtp.Send(mailMessage);
                }

                if (server == 1)
                {
                    var mailkitemail = new MimeMessage();

                    //mailkitemail.To.Add(MailboxAddress.Parse(email.EmailTo));
                    foreach (var mail in email.EmailTo)
                    {
                        mailkitemail.To.Add(MailboxAddress.Parse(mail));
                    }
                    mailkitemail.Sender = MailboxAddress.Parse(serverSettings.Mail);
                    mailkitemail.Subject = email.EmailSubject;
                    var builder = new BodyBuilder();
                    builder.HtmlBody = email.EmailBody;
                    mailkitemail.Body = builder.ToMessageBody();

                    var mailKitSmtp = new MailKit.Net.Smtp.SmtpClient();
                    await mailKitSmtp.ConnectAsync(smtp.Host, smtp.Port, SecureSocketOptions.StartTls);
                    mailKitSmtp.Authenticate(serverSettings.Mail, serverSettings.Password);

                    _logger.LogInformation($"Sending email to {email.EmailTo}");
                    await mailKitSmtp.SendAsync(mailkitemail);
                    mailKitSmtp.Disconnect(true);
                }

                if (server == 2)
                {
                    smtp.Credentials = new NetworkCredential(serverSettings.Mail, serverSettings.Password);
                    smtp.EnableSsl = serverSettings.UseSSl;

                    smtp.Send(mailMessage);
                }

                // Dispose smtp here...
                smtp.Dispose();

                // Log email here...
                _logger.LogInformation($"Exception email sent to {email.EmailTo}. Date: {DateTime.Now}");

                response.Data = true;
                response.Message = $"Email sent to {email.EmailTo}";

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.AddError(ex.Message);
                response.AddError($"We are unable to send {email.EmailTo} an email. please try again");

                _logger.LogInformation($"We are unable to send {email.EmailTo} an email. please try again");
                _logger.LogError(ex.Message, ex.ToString());

                return await Task.FromResult(response);
            }
        }
    }
}