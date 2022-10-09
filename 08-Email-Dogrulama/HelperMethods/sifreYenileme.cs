using System.Net.Mail;

namespace rentid.HelperMethods
{
    public class sifreYenileme
    {
        public static void passwordResetSendMail(string link ,string email){

            MailMessage mail = new MailMessage();

            SmtpClient smptClient = new SmtpClient("rd-prime-win.guzelhosting.com");

            mail.From = new MailAddress("info@kutluhanyegit.com");
            mail.To.Add(email);
            mail.Subject = $"rentid.com.tr::Şifre Sıfırlama";
            mail.Body="<h2>Şifreyi yenilemeke için aşağıya tıklayın</h2>";
            mail.Body= $"<a href='{link}'>Şifre Yenileme linki</a>";
            mail.IsBodyHtml = true;
            smptClient.Port = 587;
            smptClient.Credentials = new System.Net.NetworkCredential("info@kutluhanyegit.com","Airtes1991.");
            smptClient.Send(mail);


        }
    }
}