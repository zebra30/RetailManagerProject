using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPWPFUserInterface.Models;

namespace TMPWPFUserInterface.Helpers
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient apiClient;

        public APIHelper()
        {
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
    }
}
