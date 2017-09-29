using System.Collections.Generic;
using System.Linq;

namespace ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions
{
    public class EmailMail
    {
        public string From { get; set; }

        public IList<string> To { get; set; }

        public IList<string> Cc { get; set; }

        public IList<string> Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool? IsHtml { get; set; }

        public byte[] ItemAttach { get; set; }

        public IEnumerable<string> UrlItemAttach { get; set; }

        public string AttachmentName { get; set; }

        public bool IsCc(EmailMail entity)
        {
            return entity.Cc != null && entity.Cc.Any();
        }

        public bool IsBcc(EmailMail entity)
        {
            return entity.Bcc != null && entity.Bcc.Any();
        }
    }
}




