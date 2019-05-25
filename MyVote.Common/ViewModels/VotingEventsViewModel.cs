namespace MyVote.Common.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;

    public class VotingEventsViewModel : MvxViewModel
    {
        private List<VotingEvent> votingEvents;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;

        public List<VotingEvent> VotingEvents
        {
            get => this.votingEvents;
            set => this.SetProperty(ref this.votingEvents, value);
        }

        public VotingEventsViewModel(
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.LoadVotingEvents();
        }

        private async void LoadVotingEvents()
        {
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.GetListAsync<VotingEvent>(
                "https://myvotesystem.azurewebsites.net",
                "/api",
                "/VotingEvents",
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.VotingEvents = (List<VotingEvent>)response.Result;
            this.VotingEvents = this.VotingEvents.OrderBy(p=>p.Name).ToList();
        }

    }
}
