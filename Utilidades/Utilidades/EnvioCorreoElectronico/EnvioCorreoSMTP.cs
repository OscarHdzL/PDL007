using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Utilidades.EnvioCorreoElectronico
{
    /// <summary>
    /// Envio de correos
    /// </summary>
    public class EnvioCorreoSMTP
    {
        Log4Net.LoggerManager log = new Log4Net.LoggerManager();
        string respuesta;

        private static readonly string templatesPath = Directory.GetCurrentDirectory() + "/Templates/";
        /// <summary>
        /// Metodo envio de correo
        /// </summary>
        /// <param name="emailMessage"></param>
        /// <returns></returns>
        
        //public string Send(String destinatario, String subject, String body, string emailto, string emailpassword, string smtp, string puerto, string usuario)
        //{

        //        List<String> to = new List<String>();
        //        to.Add(destinatario);

        //        NetworkCredential credentials = new NetworkCredential(emailto, emailpassword);
        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress(emailto, emailpassword)
        //    };
        //    //MailMessage mailMessage = new MailMessage();
        //      //  mailMessage.From = new MailAddress(emailto, credentials.UserName);
        //        if (to != null) foreach (String addr in to) mailMessage.To.Add(new MailAddress(addr, "Test"));
        //        mailMessage.Subject = subject;
        //        mailMessage.IsBodyHtml = true;
        //        mailMessage.Body = body;
        //        mailMessage.Priority = MailPriority.Normal;
        //    mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
        //    mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;


        //    //SmtpClient client = new SmtpClient
        //    //{
        //    //    Host = smtp,
        //    //    Port = Int32.Parse(puerto),
        //    //    EnableSsl = false,
        //    //    DeliveryMethod = SmtpDeliveryMethod.Network,
        //    //    UseDefaultCredentials = false,
        //    //    Credentials = credentials
        //    //};

        //    var client = new SmtpClient(smtp, int.Parse(puerto))
        //    {
        //        EnableSsl = false,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(emailto, emailpassword)
        //    };

        //    try
        //    {
        //        client.Send(mailMessage);
        //        respuesta = "Correcto";
        //    }
        //    catch (Exception ex)
        //    {
        //        log.LogError("No se pudo envíar el correo", ex);
        //        respuesta = "Error";
        //    }
        //        return respuesta;
        //}

        public string Send(String destinatario, String subject, String body, string emailto, string emailpassword, string smtp, string puerto, string usuario)
        {

            List<String> to = new List<String>();
            to.Add(destinatario);

            NetworkCredential credentials = new NetworkCredential(emailto, emailpassword);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailto, credentials.UserName);
            if (to != null) foreach (String addr in to) mailMessage.To.Add(new MailAddress(addr, "Test"));
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;
            mailMessage.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient
            {
                Host = smtp,
                Port = Int32.Parse(puerto),
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = credentials
            };
            try
            {
                client.Send(mailMessage);
                respuesta = "Correcto";
            }
            catch (Exception ex)
            {
                log.LogError("No se pudo envíar el correo", ex);
                respuesta = "Error";
            }
            return respuesta;
        }

        public string SendAttachment(String destinatario, String subject, String body, string emailto, string emailpassword, string smtp, string puerto, string usuario, string file)
        {

            List<String> to = new List<String>();
            to.Add(destinatario);

            Attachment fileAttachment = new Attachment(file, MediaTypeNames.Application.Octet);

            NetworkCredential credentials = new NetworkCredential(emailto, emailpassword);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailto, credentials.UserName);
            if (to != null) foreach (String addr in to) mailMessage.To.Add(new MailAddress(addr, "Test"));
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.Attachments.Add(fileAttachment);

            SmtpClient client = new SmtpClient
            {
                Host = smtp,
                Port = Int32.Parse(puerto),
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = credentials
            };
            try
            {
                client.Send(mailMessage);
                respuesta = "Correcto";
            }
            catch (Exception ex)
            {
                log.LogError("No se pudo envíar el correo", ex);
                respuesta = "Error";
            }
            return respuesta;
        }


        //public string SendResetPass(String destinatario, String subject, String body, string emailto, string emailpassword, string smtp,
        //    bool htmlEmail = true)
        //{
        //    List<String> to = new List<String>();
        //    to.Add(destinatario);

        //    NetworkCredential credentials = new NetworkCredential(emailpassword, emailpassword);
        //    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
        //    mailMessage.From = new System.Net.Mail.MailAddress(emailto, credentials.UserName);
        //    if (to != null) foreach (String addr in to) mailMessage.To.Add(new System.Net.Mail.MailAddress(addr, "Test"));
        //    mailMessage.Subject = subject;
        //    mailMessage.IsBodyHtml = htmlEmail;
        //    mailMessage.Body = body;
        //    mailMessage.Priority = System.Net.Mail.MailPriority.Normal;

        //    var fromAddres = new System.Net.Mail.MailAddress(emailto);
        //    var fromPassword = emailpassword;

        //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient
        //    {
        //        Host = smtp,
        //        Port = 25,
        //        EnableSsl = false,

        //        //Variables de prueba para envio de correo
        //        //Host = "smtp.gmail.com",
        //        //Port = 587,
        //        //EnableSsl = true,

        //        DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        //Credentials = credentials
        //        Credentials = new NetworkCredential(fromAddres.Address, fromPassword)
        //};

        //    client.Send(mailMessage);
        //    return "Correcto";
        //}


        #region Generacion de plantillas
        /// <summary>
        /// GenerarPlantillaBasica
        /// </summary>
        /// <param name="asunto"></param>
        /// <param name="encabezado"></param>
        /// <param name="contenido"></param>
        /// <returns></returns>
        public static string GenerarPlantillaBasica(string asunto, string encabezado, string contenido)
        {
            string plantilla = System.IO.File.ReadAllText(templatesPath + "notificacion-basica.html");

            plantilla = plantilla.Replace("ASUNTO", asunto);
            plantilla = plantilla.Replace("PREVIEW", contenido.Substring(0, 20) + "...");
            plantilla = plantilla.Replace("ENCABEZADO", encabezado);
            plantilla = plantilla.Replace("CONTENIDO", contenido);

            return plantilla;
        }
        /// <summary>
        /// GenerarPlantillaBoton
        /// </summary>
        /// <param name="asunto"></param>
        /// <param name="encabezado"></param>
        /// <param name="contenido"></param>
        /// <param name="textoBoton"></param>
        /// <param name="enlaceBoton"></param>
        /// <returns></returns>
        public static string GenerarPlantillaBoton(string asunto, string encabezado, string contenido, string textoBoton, string enlaceBoton)
        {
            string plantilla = System.IO.File.ReadAllText(templatesPath + "notificacion-boton.html");

            plantilla = plantilla.Replace("ASUNTO", asunto);
            plantilla = plantilla.Replace("PREVIEW", contenido.Substring(0, 20) + "...");
            plantilla = plantilla.Replace("ENCABEZADO", encabezado);
            plantilla = plantilla.Replace("CONTENIDO", contenido);
            plantilla = plantilla.Replace("TEXTO-BOTON", textoBoton);
            plantilla = plantilla.Replace("href=''", "href='" + enlaceBoton + "'");

            return plantilla;
        }

        #endregion
    }
}
