﻿

namespace MyVote.UIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using MyVote.Common.Models;
    using MyVote.Common.Services;
    using MyVote.UIForms.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class LoginViewModel : BaseViewModel
    {
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.Email = "meli.cuellar0117@gmail.com";
            this.Password = "888888";
            this.isEnabled = true;
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

            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetTokenAsync(
                url,
                "/Account",
                "/CreateToken",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email or password incorrect.", "Accept");
                return;
            }

            var token = (TokenResponse)response.Result;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.VotingEvents = new VotingEventsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new VotingEventsPage());

        }
    }
}
