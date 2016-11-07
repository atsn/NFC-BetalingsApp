using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StatueApp.Common;

namespace NFC_BetalingsApp
{
    class LoginWievmodel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private RelayCommand _logincommand;
        private string _response;
        private bool _isactive;

        public bool isactive
        {
            get { return _isactive; }
            set
            {
                _isactive = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand Logincommand
        {
            get { return _logincommand; }
            set
            {
                _logincommand = value;
                OnPropertyChanged();
            }
        }

        public string response
        {
            get { return _response; }
            set
            {
                _response = value;
                OnPropertyChanged();
            }
        }

        public LoginWievmodel()
        {
            Logincommand = new RelayCommand(login);
        }

        public async void login()
        {
            try
            {
                isactive = true;
                response = await Facade.login(Username, Password);
                isactive = false;
                if (response.Contains("Success:"))
                {
                    NavigationHelper.navigate(typeof(MainPage));
                }
               
            }
            catch (Exception e)
            {
                isactive = false;
                var messageBox = new MessageDialog("Login failed " + e.Message);
                await messageBox.ShowAsync();

            }



        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
