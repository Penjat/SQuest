using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class MailSender : MonoBehaviour {
    public static void SendEmail(){
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("mixolyvia@gmail.com");
        mail.To.Add("spencer_symington@hotmail.com");
        mail.Subject = "the Path of the Brave";
        mail.Body = "Hello there, you have just signed up for the Path of the Brave a sissy's journey mailing list.";
        // you can use others too.
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("mixolyvia@gmail.com", "Boobmass00") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        smtpServer.Send(mail);
    }
}
