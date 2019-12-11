using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class MailSender : MonoBehaviour {
    public static void SendEmail(){
        //keep the email and password data private
        TextAsset emailData = Resources.Load("HiddenData/EmailData") as TextAsset;
        if(emailData == null){
            Debug.Log("the Anxiomancer Says: you do not have the appropriate credentials to access this function.");
            return;
        }
        Debug.Log("Data found, sending email");
        string password = emailData.ToString();
        TextAsset emailBody = Resources.Load("PageContent/EmailBody") as TextAsset;

        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("mixolyvia@gmail.com");
        mail.To.Add("spencer_symington@hotmail.com");
        mail.Subject = "checking";
        mail.Body = emailBody.ToString();
        // you can use others too.
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("mixolyvia@gmail.com", password) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        smtpServer.Send(mail);
    }
}
