using Microsoft.AspNetCore.Mvc;
using Mobile_WebApplication.Models;
using System.Diagnostics;
using System.Net.Mail;

namespace Mobile_WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult e44mailtst(string emailaddress, string subject, string message)
        {


            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("az5700864@gmail.com");
            mail.To.Add(emailaddress); // receiver email address
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = message;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("az5700864@gmail.com", "soft@4321");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            ViewData["Message"] = "Email sent.";
            return View();
        }
        public IActionResult emailtst(string emailaddress, string subject, string message)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("az5700864@gmail.com");
            mail.To.Add("ali@gmail.com"); // receiver email address
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = message;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("az5700864@gmail.com", "soft@4321");
            SmtpServer.EnableSsl = true;
            //SmtpServer.Send(mail);
            ViewData["Message"] = "Email sent.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
