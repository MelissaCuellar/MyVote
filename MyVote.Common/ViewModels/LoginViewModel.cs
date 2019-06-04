using System;
using System.Collections.Generic;
using System.Text;

namespace MyVote.Common.ViewModels
{
    using System.Windows.Input;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using MyVote.Common.Helpers;
    using Newtonsoft.Json;
    using Services;

    public class LoginViewModel : MvxViewModel
    {
        private string email;
        private string password;
        private MvxCommand recoverPasswordCommand;
        private MvxCommand loginCommand;
        private MvxCommand registerCommand;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private readonly INetworkProvider networkProvider;
        private bool isLoading;


        public LoginViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService,
            INetworkProvider networkProvider)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.networkProvider = networkProvider;

            this.Email = "meli.cuellar0117@gmail.com";
            this.Password = "888888";
            this.IsLoading = false;
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public ICommand RecoverPasswordCommand
        {
            get
            {
                this.recoverPasswordCommand = this.recoverPasswordCommand ?? new MvxCommand(this.DoRecoverPasswordCommand);
                return this.recoverPasswordCommand;
            }
        }

        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value);
        }

        public string Password
        {
            get => this.password;
            set => this.SetProperty(ref this.password, value);
        }

        public ICommand LoginCommand
        {
            get
            {
                this.loginCommand = this.loginCommand ?? new MvxCommand(this.DoLoginCommand);
                return this.loginCommand;
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                this.registerCommand = this.registerCommand ?? new MvxCommand(this.DoRegisterCommand);
                return this.registerCommand;
            }
        }

        private async void DoLoginCommand()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                this.dialogService.Alert("Error", "You must enter an email.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.dialogService.Alert("Error", "You must enter a password.", "Accept");
                return;
            }

            if (!this.networkProvider.IsConnectedToWifi())
            {
                this.dialogService.Alert("Error", "You need internet connection to enter to the App.", "Accept");
                return;
            }

            this.IsLoading = true;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

            var response = await this.apiService.GetTokenAsync(
                "https://myvotesystem.azurewebsites.net",
                "/Account",
                "/CreateToken",
                request);

            if (!response.IsSuccess)
            {
                this.IsLoading = false;
                this.dialogService.Alert("Error", "User or password incorrect.", "Accept");
                return;
            }

            var token = (TokenResponse)response.Result;
            var response2 = await this.apiService.GetUserByEmailAsync(
                "https://myvotesystem.azurewebsites.net",
                "/api",
                "/Account/GetUserByEmail",
                this.Email,
                "bearer",
                token.Token);

            var user = (User)response2.Result;
            Settings.UserPassword = this.Password;
            Settings.User = JsonConvert.SerializeObject(user);
            Settings.UserEmail = this.Email;
            Settings.Token = JsonConvert.SerializeObject(token);

            this.IsLoading = false;

            await this.navigationService.Navigate<VotingEventsViewModel>();

        }

        private async void DoRecoverPasswordCommand()
        {
            await this.navigationService.Navigate<RecoverPasswordViewModel>();
        }

        private async void DoRegisterCommand()
        {
            await this.navigationService.Navigate<RegisterViewModel>();
        }

    }

}
