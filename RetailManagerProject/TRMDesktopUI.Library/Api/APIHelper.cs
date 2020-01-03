using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPWPFUserInterface.Models;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.Library.Api
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient apiClient;
        private ILoggedInUserModel _loggedInUser;

        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            _loggedInUser = loggedInUser;
            initializeClient();
        }

        private void initializeClient()
        {
            apiClient = new HttpClient();
            //Clear Headers
            apiClient.DefaultRequestHeaders.Accept.Clear();
            //Add our Headers
            apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string userName, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
            });

            using (HttpResponseMessage response = await apiClient.PostAsync("https://localhost:44342/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw (new Exception(response.ReasonPhrase));
                }
            }
        }

        public async Task GetLoggedInUserInfo(string token)
        {
            apiClient.DefaultRequestHeaders.Clear();
            //Clear Headers
            apiClient.DefaultRequestHeaders.Accept.Clear();
            //Add our Headers
            apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //Add Token
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await apiClient.GetAsync("https://localhost:44342/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    _loggedInUser.CreatedDate = result.CreatedDate;
                    _loggedInUser.EmailAddress = result.EmailAddress;
                    _loggedInUser.FirstName = result.FirstName;
                    _loggedInUser.Id = result.Id;
                    _loggedInUser.LastName = result.LastName;
                    _loggedInUser.Token = token;
                }
                else
                {
                    throw (new Exception(response.ReasonPhrase));
                }
            }
        }
    }
}
