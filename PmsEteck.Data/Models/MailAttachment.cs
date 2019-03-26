using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("MailAttachments", Schema = "service")]
    public class MailAttachment
    {
        [Key]
        public int iMailAttachmentID { get; set; }

        public string sFileName { get; set; }

        public string sContentType { get; set; }

        public byte[] bByteArray { get; set; }

        public int iResponseID { get; set; }

        public Response Response { get; set; }
    }
}