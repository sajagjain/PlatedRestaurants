using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBAL
{
    public class EmailBL
    {
        public int SendMail(EmailModel model)
        {
            try
            {
                MailMessage mm = new MailMessage("platedrestaurants@gmail.com", model.To);
                mm.Subject = model.Subject;
                mm.Body = model.Body;
                mm.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential("platedrestaurants@gmail.com", "9826855195");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Send(mm);

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
