using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace ArchitectureTemplate.Business.Services
{
    public class EmailMailService : IEmailMailService
    {
        #region Fields

        private readonly string _from = ConfigurationManager.AppSettings["EmailFrom"];
        private readonly string _user = ConfigurationManager.AppSettings["SMTPUser"];
        private readonly string _password = ConfigurationManager.AppSettings["SMTPPassword"];
        private readonly string _smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
        private readonly int _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);

        #endregion

        #region Constructors

        #endregion

        #region Methods

        public void SendEmail(EmailMail entity)
        {
            var mail = new MailMessage();

            var smtp = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_user, _password)
            };

            if (entity.ItemAttach != null && entity.ItemAttach.Any())
            {
                Stream fileAtt = new MemoryStream(entity.ItemAttach, true);
                var att = new Attachment(fileAtt, entity.AttachmentName)
                {
                    TransferEncoding = TransferEncoding.Base64
                };
                mail.Attachments.Add(att);
            }

            if (entity.UrlItemAttach != null)
            {
                foreach (var attachmentFilename in entity.UrlItemAttach)
                {
                    Attachment attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = attachment.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(attachmentFilename);
                    disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
                    disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
                    disposition.FileName = Path.GetFileName(attachmentFilename);
                    disposition.Size = new FileInfo(attachmentFilename).Length;
                    disposition.DispositionType = DispositionTypeNames.Attachment;
                    mail.Attachments.Add(attachment);
                }
            }

            mail.From = new MailAddress(_from);

            foreach (var emailTo in entity.To)
                mail.To.Add(emailTo);

            if (entity.IsBcc(entity))
                foreach (var emailCc in entity.Cc)
                    mail.CC.Add(emailCc);

            if (entity.IsCc(entity))
                foreach (var emailCc in entity.Cc)
                    mail.CC.Add(emailCc);

            mail.Priority = MailPriority.Normal;

            mail.IsBodyHtml = !entity.IsHtml.HasValue || entity.IsHtml.Value;

            mail.Subject = entity.Subject;

            mail.Body = entity.Body;

            //if (!string.IsNullOrEmpty(entity.SignatureImage))
            //{
            //    var linkedResource = new LinkedResource(entity.SignatureImage) { ContentId = "Imagem" };

            //    var alternateView = AlternateView.CreateAlternateViewFromString(entity.Body, null, MediaTypeNames.Text.Html);
            //    alternateView.LinkedResources.Add(linkedResource);

            //    mail.AlternateViews.Add(alternateView);
            //}

            mail.SubjectEncoding = Encoding.UTF8;

            mail.BodyEncoding = Encoding.UTF8;

            smtp.Send(mail);
        }

        #endregion
    }
}