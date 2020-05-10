using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Http;
using System.Net;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;


namespace TaskManager.Commands
{
    class LoginCommand : ICommand
    {

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private ViewModels.LoginPageViewModel parent;
        private bool IsRequsting;
        private string token = null;

        public LoginCommand(ViewModels.LoginPageViewModel Parent)
        {
            parent = Parent;
        }

        public bool CanExecute(object parameter) => !IsRequsting;

        public void Execute(object parameter)
        {
            var loginWindow = parameter as IPasswordSupplier;
            doRequest(loginWindow.GetPassword());
        }

        private async void doRequest(string pass)
        {
            IsRequsting = true;
            token = null;
            parent.Status = "";
            try
            {
                var result = await ("http://localhost:8080/login")
                    .PutJsonAsync(new { email = parent.Email ?? " ", password = pass })
                    .ReceiveJson();
                token = result?.accessToken;
            }
            catch { }
            finally
            {
                IsRequsting = false;
                if (string.IsNullOrEmpty(token))
                    parent.Status = "Email or password is incorrect!";
                else
                    parent.Status = token;
            }

        }
    }
}
