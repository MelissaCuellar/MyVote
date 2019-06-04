using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MyVote.Common.Interfaces;
using MyVote.Common.Models;
using MyVote.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyVote.Common.ViewModels
{
   public class ShowEndViewModel : MvxViewModel<NavigationArgs>
    {
        private List<Candidate> candidates;
        private VotingEvent votingEvent;
       

       

        public List<Candidate> Candidates
        {
            get => this.candidates;
            set => this.SetProperty(ref this.candidates, value);
        }

        public VotingEvent VotingEvent
        {
            get => this.votingEvent;
            set => this.SetProperty(ref this.votingEvent, value);
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
    }
}
