namespace MyVote.Common.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;

    public class VotingEventsViewModel : MvxViewModel
    {
        private List<VotingEvent> votingEvents;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private MvxCommand<VotingEvent> itemClickCommand;

        public List<VotingEvent> VotingEvents
        {
            get => this.votingEvents;
            set => this.SetProperty(ref this.votingEvents, value);
        }

        public VotingEventsViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
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
            this.VotingEvents = this.VotingEvents.OrderBy(p => p.Name).ToList();
        }

        public ICommand ItemClickCommand
        {
            get
            {
                this.itemClickCommand = new MvxCommand<VotingEvent>(this.OnItemClickCommand);
                return itemClickCommand;
            }
        }
        private async void OnItemClickCommand(VotingEvent votingEvent)
        {
            await this.navigationService.Navigate<VotingEventsDetailViewModel, NavigationArgs>(new NavigationArgs { VotingEvent = votingEvent });
        }

    }
}
