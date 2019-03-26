using System.Collections.Generic;

namespace Brunata.Models
{
    public class Result
    {
        public Result(params string[] errors) : this((IEnumerable<string>)errors) { }

        public Result(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                errors = new string[0];
            }
            Succeeded = false;
            Errors = errors;
        }

        private Result(bool success)
        {
            Succeeded = success;
            Errors = new string[0];
        }
        public bool Succeeded { get; private set; }

        public IEnumerable<string> Errors { get; private set; }

        public static Result Failed(params string[] errors)
        {
            return new Result(errors);
        }

        public static Result Success
        {
            get
            {
                return _success;
            }
        }
        private static readonly Result _success = new Result(true);
    }
}