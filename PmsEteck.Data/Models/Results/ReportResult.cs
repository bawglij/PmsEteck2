using System.Collections.Generic;

namespace PmsEteck.Data.Models.Results
{
    public class ReportResult
    {
        public ReportResult(params string[] errors) : this((IEnumerable<string>)errors) { }

        public ReportResult(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                errors = new string[0];
            }
            Succeeded = false;
            Errors = errors;
        }

        private ReportResult(bool success)
        {
            Succeeded = success;
            Errors = new string[0];
        }
        public bool Succeeded { get; private set; }

        public IEnumerable<string> Errors { get; private set; }

        public static ReportResult Failed(params string[] errors)
        {
            return new ReportResult(errors);
        }

        public static ReportResult Success
        {
            get
            {
                return _success;
            }
        }
        private static readonly ReportResult _success = new ReportResult(true);
    }
}