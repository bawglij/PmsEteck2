using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace PmsEteck.Data.Models
{
    [Table("CustomerDocuments")]
    public class CustomerDocument
    {
        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        #endregion

        #region Properties
        [Key]
        public int iCustomerDocumentKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Entiteit")]
        public int iCustomerKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Categorie")]
        public int iDocumentCategoryKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Documentnaam")]
        [StringLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        public string sDocumentName { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [StringLength(2000, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sUrl { get; set; }

        [Display(Name = "Document gedownload?")]
        public bool bDownloaded { get; set; }
        
        [Display(Name = "Entiteit")]
        public CustomerInfo Customer { get; set; }

        [Display(Name = "Categorie")]
        public DocumentCategory DocumentCategory { get; set; }
        #endregion

        #region Methods
        public FileResult DownloadFile(int iCustomerDocumentKey)
        {
            //Find selected hyperlink
            CustomerDocument document = db.CustomerDocuments.Find(iCustomerDocumentKey);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(document.sUrl);

            //execute the request
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            byte[] responseBytes = new byte[0];
            string contentType = string.Empty;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // we will read data via de response stream
                Stream responseStream = response.GetResponseStream();
                contentType = response.ContentType;
                string fileName = new ContentDisposition(response.Headers["content-disposition"]).FileName;
                string tempString = string.Empty;
                int count = 0;
                byte[] buffer = new byte[16 * 1024];
                do
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        responseBytes = ms.ToArray();
                    }
                } while (count > 0);
            }
            return new FileContentResult(responseBytes, contentType);
        }

        public bool SaveFile(int iCustomerDocumentKey)
        {
            string filedir = System.Configuration.ConfigurationManager.AppSettings["FileLocation"].ToString();
            //Definieer huidige datum
            DateTime today = DateTime.Today;
            // Find all documents
            CustomerDocument customerDocument = db.CustomerDocuments.Include(i => i.Customer).Include(i => i.Customer.CustomerBase).FirstOrDefault(f => f.iCustomerDocumentKey == iCustomerDocumentKey && f.sUrl.StartsWith("http://dms.eteck.nl/ufc") && !f.bDownloaded);
            if (customerDocument == null)
                return false;

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(customerDocument.sUrl);

                //execute the request
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                byte[] responseBytes = new byte[0];
                string contentType = string.Empty;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // we will read data via de response stream
                    Stream responseStream = response.GetResponseStream();
                    contentType = response.ContentType;

                    string fileName = new ContentDisposition(response.Headers["content-disposition"]).FileName;
                    string tempString = string.Empty;
                    int count = 0;
                    byte[] buffer = new byte[16 * 1024];

                    do
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int read;

                            while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, read);
                            }

                            responseBytes = ms.ToArray();
                        }
                    } while (count > 0);
                    string map = string.Format("{0}/{1}", filedir, customerDocument.Customer.CustomerBase.sName);
                    //string projectMap = HttpContext.Current.Server.MapPath(map);
                    string projectMap = map;
                    if (!Directory.Exists(projectMap))
                    {
                        Directory.CreateDirectory(projectMap);
                    }

                    string invalid = new string(Path.GetInvalidFileNameChars());
                    foreach (char c in invalid)
                    {
                        customerDocument.sDocumentName = customerDocument.sDocumentName.Replace(c.ToString(), "_");
                    }
                    string FileName = string.Format("{0}_{1}", today.ToShortDateString(), customerDocument.sDocumentName + Path.GetExtension(fileName));

                    FileStream fs = System.IO.File.Create(Path.Combine(projectMap, FileName));
                    byte[] bytesInStream = new byte[responseBytes.Length];
                    fs.Write(responseBytes, 0, responseBytes.Length);

                    customerDocument.bDownloaded = true;
                    db.Entry(customerDocument).State = EntityState.Modified;
                }

                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}