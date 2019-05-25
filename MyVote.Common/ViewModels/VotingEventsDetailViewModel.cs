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
            this.Candidates = this.votingEvent.Candidates.OrderBy(p => p.Name).ToList();
        }


        public override void Prepare(NavigationArgs parameter)
        {
            this.votingEvent = parameter.VotingEvent;
        }
    }

}

