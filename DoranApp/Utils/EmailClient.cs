using System;
using System.Net;
using System.Net.Mail;

namespace DoranApp.Utils;

public static class EmailClient
{
    private static string senderEmail = Constants.EMAIL_PENGIRIM_PROG; // Replace with your email
    private static string senderPassword = Constants.PASS_EMAIL_PENGIRIM_PROG; // Replace with your password
    private static string senderName = Constants.NAMA_PENGIRIM; // Replace with your password
    private static SmtpClient smtpClient;

    static EmailClient()
    {
        smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.Port = 587; // Gmail SMTP port
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
    }

    public static bool SendEmail(string recipientEmail, string subject, string body)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail, senderName);
            mailMessage.To.Add(new MailAddress(recipientEmail));
            mailMessage.Subject = subject;

            mailMessage.Body = body;

            smtpClient.Send(mailMessage);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send email: " + ex.Message);
            return false;
        }
    }
}