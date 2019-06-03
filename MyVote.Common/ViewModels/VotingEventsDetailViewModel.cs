using System;
using System.Collections.Generic;
using System.Text;

namespace MyVote.Common.ViewModels
{
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

    public class VotingEventsDetailViewModel : MvxViewModel<NavigationArgs>
    {
        private List<Candidate> candidates;

        private MvxCommand<Candidate> voteCommand;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private VotingEvent votingEvent;
        private bool isLoading;
        private MvxCommand updateCommand;
        private MvxCommand deleteCommand;

        public VotingEventsDetailViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.IsLoading = false;

        }

        public ICommand VoteCommand
        {
            get
            {
                this.voteCommand = this.voteCommand ?? new MvxCommand<Candidate>(this.ComfirmVoteCommand);
                return this.voteCommand;
            }
        }

        

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        
        public VotingEvent VotingEvent
        {
            get => this.votingEvent;
            set => this.SetProperty(ref this.votingEvent, value);
        }

        public List<Candidate> Candidates
        {
            get => this.candidates;
            set => this.SetProperty(ref this.candidates, value);
        }

       

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            this.LoadCandidates();
        }

        private async void LoadCandidates()
        {
            var eve = votingEvent.Candidates.OrderByDescending(e => e.NumVotes);
            this.Candidates = eve.ToList();
        }


        public override void Prepare(NavigationArgs parameter)
        {
            this.votingEvent = parameter.VotingEvent;
        }

        private void ComfirmVoteCommand(Candidate candidate)
        {
            
                
                    this.dialogService.Confirm(
               "Confirm",
               "Are you sure to vote for " + candidate.Name + "?",
               "Yes",
               "No",
               () => { this.ConfirmVoteCandidate(candidate); },
               null);
                
            
               

        }

        private async void ConfirmVoteCandidate(Candidate candidate)
        {
            var request = new VoteRequest
            {
                Email = Settings.UserEmail,
                CandidateId = candidate.Id
            };

            this.IsLoading = true;

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await this.apiService.AddVoteAsync(
                "https://myvotesystem.azurewebsites.net",
                "/api",
                "/VotingEvents/AddVote",
                request,
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }
            else
            {
                this.dialogService.Alert("Confirmation", response.Message, "Accept");
            }

            await this.navigationService.Close(this);

        }
       
    }

}

