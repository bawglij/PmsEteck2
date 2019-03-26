namespace PmsEteck
{
    public class IdentityMessage
    {
        public IdentityMessage() { }

        //
        // Summary:
        //     Destination, i.e. To email, or SMS phone number
        public virtual string Destination { get; set; }
        //
        // Summary:
        //     Subject
        public virtual string Subject { get; set; }
        //
        // Summary:
        //     Message contents
        public virtual string Body { get; set; }
    }
}