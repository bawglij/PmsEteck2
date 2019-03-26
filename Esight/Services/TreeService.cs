//using Esight.Esight.Tree;
using Eisight.Tree;
using System.Collections.Generic;
using System.Net;

namespace Esight.Services
{
    class TreeService
    {
        
        public CookieContainer CookieContainer { get; private set; }
        public List<object> Companies { get; private set; }
        private clsTreeEntity Tree { get; set; }
        public TreeService(CookieContainer cookieContainer)
        {
            CookieContainer = cookieContainer;
            Tree = new clsTreeEntity();
        }

        public void GetCompanies()
        {
            //Tree.CookieContainer = CookieContainer;
        }
        
    }
    
}
