using System.Collections.Generic;

namespace PmsEteck.Data.Models.Results
{
    public class InvoiceResult
    {
        public InvoiceResult(params string[] errors) : this((IEnumerable<string>)errors) { }

        public InvoiceResult(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                errors = new string[0];
            }
            Succeeded = false;
            Errors = errors;
        }

        private InvoiceResult(bool success)
        {
            Succeeded = success;
            Errors = new string[0];
        }
        public bool Succeeded { get; private set; }
        
        public IEnumerable<string> Errors { get; private set; }
        
        public static InvoiceResult Failed(params string[] errors)
        {
            return new InvoiceResult(errors);
        }

        public static InvoiceResult Success
        {
            get
            {
                return _success;
            }
        }
        private static readonly InvoiceResult _success = new InvoiceResult(true);
    }
}