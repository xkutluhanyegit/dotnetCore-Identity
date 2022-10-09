using System.Net.Mail;

namespace rentid.HelperMethods
{
    public static class emailDogrulama
    {
        public static void emailConfirmSendMail(string link ,string email){

            MailMessage mail = new MailMessage();

            SmtpClient smptClient = new SmtpClient("rd-prime-win.guzelhosting.com");

            mail.From = new MailAddress("info@kutluhanyegit.com");
            mail.To.Add(email);
            mail.Subject = $"rentid.com.tr::Email Doğrulama";
            mail.Body="<h2>Email adresinizi doğrulamak için aşağıda ki linke tıklayın</h2>";
            mail.Body= $"<a href='{link}'>Email doğrulama linki</a>";
            mail.IsBodyHtml = true;
            smptClient.Port = 587;
            smptClient.Credentials = new System.Net.NetworkCredential("info@kutluhanyegit.com","Airtes1991.");
            smptClient.Send(mail);


        }
    }
}