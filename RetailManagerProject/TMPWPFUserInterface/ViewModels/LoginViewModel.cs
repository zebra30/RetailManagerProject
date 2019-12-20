using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPWPFUserInterface.Helpers;

namespace TMPWPFUserInterface.ViewModels
{
    public class LoginViewModel : Screen
    {
        private IAPIHelper _iAPIHelper;
        public LoginViewModel(IAPIHelper iAPIHelper)
        {
            _iAPIHelper = iAPIHelper;
        }
        
        private string _userName = "";

       

        public string UserName
        {
            get { return _userName; }
            set 
            { 
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }


        private string _password;

        public string Password
        {
            get { return _password; }
            set 
                { 
                    _password = value;

                    NotifyOfPropertyChange(() => Password);
                    
                    NotifyOfPropertyChange(() => CanLogIn);
                }
        }

        public bool CanLogIn
        {
            get
            {
                bool answer = false;

                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    answer = true;
                }

                return answer;
            }
        }

        public async Task LogIn(string userName, string password)
        {
            try
            {
                var model = await _iAPIHelper.Authenticate(UserName, Password);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        
        }

    }
}
