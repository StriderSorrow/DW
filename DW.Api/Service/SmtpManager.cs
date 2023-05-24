using System.Security.Cryptography;
using System.Text;
using DW.Data.Database;
using DW.Data.Database.Entities;
using MailKit.Net.Smtp;
using MimeKit;

namespace DW.Api.Service
{
    public static class SmtpManager
    {
        private static readonly SmtpClient _mail;
        /* A constant string that is used to send emails. */
        private const string NoReplyMail = "dw_noreply@sbeusilent.space";

        /* A static constructor. It is called once when the class is first loaded. */
        static SmtpManager()
        {
            _mail = new SmtpClient();

        }

        /// <summary>
        /// It sends a confirmation email to the user with a confirmation code
        /// </summary>
        /// <param name="XIdentityUser">The user model</param>
        /// <param name="ApiDbContext">The database context</param>
        /// <returns>
        /// The confirmation.Id is being returned.
        /// </returns>
        public static async Task<string> SendConfirmationEmail(this DwUser user, DwDbContext context)
        {
            if (!_mail.IsConnected || !_mail.IsAuthenticated)
            {
                _mail.Connect("sbeusilent.space", 587, true);
                _mail.AuthenticationMechanisms.Remove("XOAUTH2");
                _mail.Authenticate(NoReplyMail, "CapybaraRulesTheWorld");
            }
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new Exception("Email not found.");
            }

            var toEmail = user.Email;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("No-Reply", NoReplyMail));
            message.To.Add(new MailboxAddress(user.UserName, toEmail));
            message.Subject = "Confirmation message";
            var confirmation = new DwUserConfirmation()
            {
                Id = Guid.NewGuid().ToString(),
                User = user,
                Code = GenerateEncryptedString()
            };
            await context.Confirmations.AddAsync(confirmation);
            var code = confirmation.MailCode;
            message.Body = new TextPart("plain") { Text = $"Confirmation code: \n\n {code}" };
            await _mail.SendAsync(message);
            await context.SaveChangesAsync();
            await _mail.DisconnectAsync(true);
            return confirmation.Code;
        }

        private static string GenerateEncryptedString()
        {
            var secret = Guid.NewGuid().ToString() + Guid.NewGuid();
            var salt = Guid.NewGuid().ToString().Substring(0, 16);
            var sha = Aes.Create();
            var preHash = Encoding.UTF32.GetBytes(secret + salt);
            var hash = sha.EncryptCbc(preHash, Encoding.UTF8.GetBytes(salt));
            var result = Convert.ToBase64String(hash);
            return result;
        }
    }
}
