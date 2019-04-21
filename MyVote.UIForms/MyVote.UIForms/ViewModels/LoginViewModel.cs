

namespace MyVote.UIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using MyVote.UIForms.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
            this.Email = "meli.cuellar0117@gmail.com";
            this.Password = "888888";
        }

        private async void Login()
        {
            if(string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password",
                    "Accept");
                return;
            }
            if(!this.Email.Equals("meli.cuellar0117@gmail.com") || !this.Password.Equals("888888"))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "User or  password wrong",
                    "Accept");
                return;
            }

            MainViewModel.GetInstance().VotingEvents = new VotingEventsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new VotingEventsPage());
        }
    }
}
