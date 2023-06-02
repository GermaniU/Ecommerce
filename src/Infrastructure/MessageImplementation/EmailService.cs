using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ecommerce.Infrastructure.MessageImplementation;

public class EmailService : IEmailService
{

    private readonly EmailSettings _emailSettings;

    public ILogger<EmailService> Logger { get; }

    public EmailService(IOptions<EmailSettings> emailsettings, ILogger<EmailService> logger)
    {
        Logger = logger;
        _emailSettings = emailsettings.Value;
    }

    public async Task<bool> SendEmail(EmailMessage email, string Token)
    {
        try
        {
            var client = new SendGridClient(_emailSettings.Key);
            var from = new EmailAddress(_emailSettings.Email);
            var subject = email.Subject;
            var to = new EmailAddress(email.To, email.To);

            var plainTextContent = email.Body;
            var htmlContent = $"{email.Body} {_emailSettings.BaseUrlClient}/password/reset/{Token}";

            var msg = MailHelper.CreateSingleEmail(from, to,subject, plainTextContent, htmlContent);

            var response =  await client.SendEmailAsync(msg);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message, null, "El email no pudo ser enviado");
           return false;
        }
    }
}