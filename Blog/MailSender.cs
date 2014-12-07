using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Blog
{
    public static class MailSender
    {
        private static string myemail = "alexeybelweb@gmail.com";
        private static string password = "sharingan";
        private static string sub = "Спасибо за регистрацию на моем блоге!";

        public static void send(string email, string text)
        {
            var loginInfo = new NetworkCredential(myemail, password);
            var message = new MailMessage(); // формируем письмо
            var smtp = new SmtpClient("smtp.gmail.com", 587); // айпи нашего SMTP клиента
            
            message.From = new MailAddress(myemail); // отправитель
            message.To.Add(new MailAddress(email)); // адрес регистрирующегося
            message.IsBodyHtml = true; // тело письма - html
            message.Subject = sub; // заголовок письма
            message.Body = text; // текст письма

            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = loginInfo;
            
            smtp.Send(message); //отправляем письмо
            

        }

    }
}