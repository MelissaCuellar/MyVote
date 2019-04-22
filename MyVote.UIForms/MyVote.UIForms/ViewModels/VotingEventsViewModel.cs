
namespace MyVote.UIForms.ViewModels
{
    using MyVote.Common.Models;
    using MyVote.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class VotingEventsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<VotingEvent> votingEvents;
        private bool isRefreshing;

        public ObservableCollection<VotingEvent> VotingEvents
        {
            get => this.votingEvents;
            set => this.SetValue(ref this.votingEvents, value);
        }

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }

        public VotingEventsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadVotingEvents();
        }

        private async void LoadVotingEvents()
        {
            this.IsRefreshing = true;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<VotingEvent>(
                url,
                "/api",
                "/VotingEvents",
                "bearer",
                MainViewModel.GetInstance().Token.Token);


            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var myVotingEvents = (List<VotingEvent>)response.Result;
            this.VotingEvents = new ObservableCollection<VotingEvent>(myVotingEvents);
        }
    }
}
