using System.Threading.Tasks;
using TMPWPFUserInterface.Models;

namespace TMPWPFUserInterface.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string userName, string password);
    }
}