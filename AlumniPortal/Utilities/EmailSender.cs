using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using AlumniPortal.Entities;
using AlumniPortal.Models;

namespace AlumniPortal.Utilities
{
    public static class EmailSender
    {
        const string deloitteEmail = "info@deloittesa.co.za";

        public static bool SendEmail(EmailDto email, IEnumerable<ApplicationUser> users = null)
        {
            SmtpClient smtp = new SmtpClient
            {
                Host = "in.mailjet.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("7b67f6bbbeacdb75c51b36a32d4ee7f2", "992fa90fe63fd28648e1eda3fead616f"),
                Timeout = 10000
            };

            if (email.IncomingEmail)
            {
                MailMessage message = new MailMessage(email.Sender, deloitteEmail, email.Subject, email.Body)
                {
                    IsBodyHtml = true
                };

                try
                {
                    smtp.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in SendEmail(): {0}", ex);
                    return false;
                }
            }
            else
            {
                foreach (var user in users)
                {
                    MailMessage message = new MailMessage(deloitteEmail, user.Email, email.Subject, email.Body)
                    {
                        IsBodyHtml = true
                    };

                    try
                    {
                        smtp.Send(message);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught in SendEmail(): {0}",
                              ex);
                        return false;
                    }
                }
            }
           
            return false;
        }
    }
}