using System.Threading.Tasks;

namespace PmsEteck
{
    public interface IIdentityMessageService
    {
        //
        // Summary:
        //     This method should send the message
        //
        // Parameters:
        //   message:
        Task SendAsync(IdentityMessage message);
    }
}