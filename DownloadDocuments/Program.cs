using PmsEteck.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DownloadDocuments
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PmsEteckContext db = new PmsEteckContext();
                Console.WriteLine("We starten nu met het downloaden van de documenten per entiteit");

                CustomerDocument _customerDocument = new CustomerDocument();
                List<CustomerDocument> documentsForDownload = db.CustomerDocuments.Where(w => !w.bDownloaded && w.sUrl.StartsWith("http://dms.eteck.nl/ufc")).ToList();
                Console.WriteLine(string.Format("De lijst met documenten is opgehaald en bevat {0} items", documentsForDownload.Count));

                foreach (var item in documentsForDownload)
                {
                    Console.WriteLine(string.Format("Gestart met het downloaden van document:  {0}", item.sDocumentName));
                    bool result = _customerDocument.SaveFile(item.iCustomerDocumentKey);
                    if (!result)
                        Console.WriteLine(string.Format("Er heeft zich een fout voorgedaan bij het downloaden van document {0}", item.sDocumentName));
                    else
                        Console.WriteLine(string.Format("Het downloaden van het bestand is voltooid: {0}", item.sDocumentName));
                }

                Console.WriteLine("We starten met het downloaden van alle documenten.");

                Hyperlink _hyperlink = new Hyperlink();
                List<Hyperlink> hyperlinksForDownload = db.Hyperlinks.Where(w => !w.bDownloaded && w.sUrl.StartsWith("http://dms.eteck.nl/ufc")).ToList();
                Console.WriteLine(string.Format("De lijst met hyperlinks is opgehaald. Dit zijn er {0}", hyperlinksForDownload.Count));

                foreach (Hyperlink hyperlink in hyperlinksForDownload)
                {
                    Console.WriteLine(string.Format("Gestart met het downloaden van document:  {0}", hyperlink.sLinkName));
                    bool result = _hyperlink.SaveFile(hyperlink.iHyperlinkKey);
                    if (!result)
                        Console.WriteLine(string.Format("Er heeft zich een fout voorgedaan bij het downloaden van document {0}", hyperlink.sLinkName));
                    else
                        Console.WriteLine(string.Format("Het downloaden van het bestand is voltooid: {0}", hyperlink.sLinkName));

                }
                Console.WriteLine("Alle bestanden zijn gedownload.");


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
