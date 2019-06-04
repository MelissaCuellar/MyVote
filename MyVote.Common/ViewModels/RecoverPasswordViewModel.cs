
using MvvmCross.Navigation;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Windows.Input;
using System;
using MyVote.Common.Models;
using MyVote.Common.Services;
using MyVote.Common.Interfaces;

namespace MyVote.Common.ViewModels
{
    

    public class RecoverPasswordViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private MvxCommand recoverPasswordCommand;
        private string email;
        private bool isLoading;

        public RecoverPasswordViewModel(
            IMvxNavigationService navigationService,
            IApiService apiService,
            IDialogService dialogService)
        {
            this.navigationService = navigationService;
            this.apiService = apiService;
            this.dialogService = dialogService;
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
                this.recoverPasswordCommand = this.recoverPasswordCommand ?? new MvxCommand(this.RecoverPassword);
                return this.recoverPasswordCommand;
            }
        }

        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value);
        }

        private async void RecoverPassword()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                this.dialogService.Alert("Error", "You must enter a email.", "Accept");
                return;
            }
            this.IsLoading = true;

            var request = new RecoverPasswordRequest
            {
                Email = this.Email
            };

            var response = await this.apiService.RecoverPasswordAsync(
                "https://myvotesystem.azurewebsites.net/",
                "/api",
                "/Account/RecoverPassword",
                request);

            this.IsLoading = false;

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.dialogService.Alert("Ok", response.Message, "Accept", () => { this.ReturnLogin(); });
        }

        private async void ReturnLogin()
        {
            await this.navigationService.Close(this);
        }
    }
}
