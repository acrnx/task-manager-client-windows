using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskManager.ViewModels
{
    class LoginPageViewModel : INotifyPropertyChanged
    {
        private string _status;
        private string _signText;
        private bool _registerBox;
        private Pages.LoginPage parent;

        public LoginPageViewModel(Pages.LoginPage Parent)
        {
            parent = Parent;
            Login = new Commands.LoginCommand(this);
            Register = new Commands.RegisterCommand(this);
            SignText = "Sign In";
            parent.SignButton.Command = Login;

        }
        public string Email { set; get; }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
        public string SignText
        {
            get => _signText;
            set
            {
                _signText = value;
                OnPropertyChanged();
            }
        }

        public bool RegisterBox
        {
            get => _registerBox;
            set
            {
                _registerBox = value;
                if (_registerBox == true)
                {
                    SignText = "Sign Up";
                    parent.SignButton.Command = Register;

                }
                else
                {
                    SignText = "Sign In";
                    parent.SignButton.Command = Login;
                }
                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Login { set; get; }
        public ICommand Register { set; get; }

        public void OnPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
