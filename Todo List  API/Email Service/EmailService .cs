using System.Net;
using System.Net.Mail;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using var smtpClient = new SmtpClient
        {
            Host = _configuration["EmailSettings:SmtpServer"],
            Port = int.Parse(_configuration["EmailSettings:Port"]),
            Credentials = new NetworkCredential(
                            _configuration["EmailSettings:Username"],
                            _configuration["EmailSettings:Password"]
                        ),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["EmailSettings:SenderEmail"]),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage);
    }
}