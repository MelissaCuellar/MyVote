using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyVote.UIForms.Views;
using MyVote.UIForms.ViewModels;
using MyVote.Common.Helpers;
using Newtonsoft.Json;
using MyVote.Common.Models;

namespace MyVote.UIForms
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        public App()
        {
            InitializeComponent();

            if (Settings.IsRemember)
            {
                var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

                if (token.Expiration > DateTime.Now)
                {
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token = token;
                    mainViewModel.UserEmail = Settings.UserEmail;
                    mainViewModel.UserPassword = Settings.UserPassword;
                    mainViewModel.VotingEvents = new VotingEventsViewModel();
                    this.MainPage = new MasterPage();
                    return;
                }
            }


            MainViewModel.GetInstance().Login = new LoginViewModel();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
