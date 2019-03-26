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
    [Table("Hyperlinks")]
    public class Hyperlink
    {
        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        #endregion

        #region Properties
        [Key]
        public int iHyperlinkKey { get; set; }

        public int iProjectKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sLinkName { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [MaxLength(2000, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sUrl { get; set; }

        [Display(Name = "Publiek beschikbaar")]
        public bool bVisible { get; set; }

        [Display(Name = "Beschikbaar voor monteur")]
        public bool VisibleForMechanic { get; set; }

        [Display(Name = "Gedownload")]
        public bool bDownloaded { get; set; }

        public ProjectInfo ProjectInfo { get; set; }
        #endregion

        #region Methods
        
        public FileResult GetFile(int iHyperlinkKey)
        {

            // Find selected hyperlink
            Hyperlink hyperlink = db.Hyperlinks.Find(iHyperlinkKey);
                                    
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hyperlink.sUrl);

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

        public bool SaveFile(int HyperLinkKey)
        {
            string filedir = System.Configuration.ConfigurationManager.AppSettings["FileLocation"].ToString();
            //Definieer huidige datum
            DateTime today = DateTime.Today;
            // Find all hyperlinks
            Hyperlink hyperlink = db.Hyperlinks.Include(i => i.ProjectInfo).FirstOrDefault(w => w.iHyperlinkKey ==HyperLinkKey && w.sUrl.StartsWith("http://dms.eteck.nl/ufc") && !w.bDownloaded);
            if (hyperlink == null)
                return false;

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hyperlink.sUrl);

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
                    string map = string.Format("{0}/{1}", filedir, hyperlink.ProjectInfo.ProjectBase.sProjectCode);
                    //string projectMap = HttpContext.Current.Server.MapPath(map);
                    string projectMap = map;
                    if (!Directory.Exists(projectMap))
                    {
                        Directory.CreateDirectory(projectMap);
                    }

                    string invalid = new string(Path.GetInvalidFileNameChars());
                    foreach (char c in invalid)
                    {
                        hyperlink.sLinkName = hyperlink.sLinkName.Replace(c.ToString(), "_");
                    }
                    string FileName = string.Format("{0}_{1}", today.ToShortDateString(), hyperlink.sLinkName + Path.GetExtension(fileName));

                    FileStream fs = System.IO.File.
                        Create(Path.Combine(projectMap, FileName));
                    byte[] bytesInStream = new byte[responseBytes.Length];
                    fs.Write(responseBytes, 0, responseBytes.Length);

                    hyperlink.bDownloaded = true;
                    db.Entry(hyperlink).State = EntityState.Modified;
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