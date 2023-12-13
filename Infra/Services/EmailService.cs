using Application.Common.Configs;
using Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infra.Services;
public class EmailService(IOptions<EmailConfig> emailConfig) : IEmailService
{
    private readonly EmailConfig _emailConfig = emailConfig.Value ?? throw new ArgumentNullException(nameof(emailConfig));

    public async Task<bool> FirstAccess(string email, string name, string password, string link)
    {
        var lk = $"{_emailConfig.UrlFront}{link}";
        EmailAddress fromEmail = new(_emailConfig.FromEmail, _emailConfig.FromName);
        var client = new SendGridClient(_emailConfig.ApiKey);
        var to = new EmailAddress(email);
        var message = new SendGridMessage();
        message.SetFrom(fromEmail);
        message.AddTo(to);
        message.SetTemplateId(_emailConfig.FirstAccessId);
        var data = new { email, lk, name, password };
        message.SetTemplateData(data);

        var result = await client.SendEmailAsync(message);

        return result.IsSuccessStatusCode;
    }

    public async Task<bool> ForgotPassword(string email, string name, string link)
    {
        var lk = $"{_emailConfig.UrlFront}{link}";
        EmailAddress fromEmail = new(_emailConfig.FromEmail, _emailConfig.FromName);
        var client = new SendGridClient(_emailConfig.ApiKey);
        var to = new EmailAddress(email);
        var message = new SendGridMessage();
        message.SetFrom(fromEmail);
        message.AddTo(to);
        message.SetTemplateId(_emailConfig.ForgotPasswordId);
        var data = new { email, link = lk, name };
        message.SetTemplateData(data);

        var result = await client.SendEmailAsync(message);

        return result.IsSuccessStatusCode;
    }
}

