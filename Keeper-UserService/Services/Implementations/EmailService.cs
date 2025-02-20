using Keeper_UserService.Models.Db;
using Keeper_UserService.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using Keeper_ApiGateWay.Models.Services;

namespace Keeper_UserService.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<ServiceResponse<string?>> SendWelcomeEmailAsync(string email, ActivationPasswords password)
        {
            var message = new MimeMessage();

            Console.WriteLine(_emailSettings.Email);
            Console.WriteLine(_emailSettings.Password);
            try
            {
                message.From.Add(new MailboxAddress("Keeper", _emailSettings.Email));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Welcome2Keeper!";
                message.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = $"Your activation code: {password.Password}"
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.SmtpHost, _emailSettings.SmtpPort, false);
                    await client.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return ServiceResponse<string?>.Success(default);
            }
            catch (Exception ex)
            {
                return ServiceResponse<string?>.Fail(default, 400, $"EmailService: {ex.Message}");
            }
        }
    }

    public class EmailSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 587;
    }
}
