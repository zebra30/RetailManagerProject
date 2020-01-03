using System.Threading.Tasks;
using TMPWPFUserInterface.Models;

namespace TRMDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string userName, string password);

        Task GetLoggedInUserInfo(string token);
    }
}