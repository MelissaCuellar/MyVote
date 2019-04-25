using MyVote.Common.Models;
using MyVote.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVote.UIForms.ViewModels
{
    public class CandidatesViewModel:BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private ObservableCollection<Candidate> candidates;
        private readonly ApiService apiService;
        
        public VotingEvent votingEvent { get; set; }

        public ObservableCollection<Candidate> Candidates
        {
            get => this.candidates;
            set => this.SetValue(ref this.candidates, value);
        }

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

        public CandidatesViewModel(VotingEvent votingEvent)
        {
            this.votingEvent = votingEvent;
           
            //this.apiService = new ApiService();
            //this.LoadCandidates();
        }

      
    }
}
