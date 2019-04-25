
namespace MyVote.UIForms.ViewModels
{
    using MyVote.Common.Models;
    using MyVote.Common.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class VotingEventsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private List<VotingEvent> myVotingEvents;
        private ObservableCollection<VotingEventItemViewModel> votingEvents;
        private ObservableCollection<Candidate> candidates;
        private VotingEvent votingEvent;
        private Candidate candidate;
        private bool isRefreshing;

        public ObservableCollection<VotingEventItemViewModel> VotingEvents
        {
            get => this.votingEvents;
            set => this.SetValue(ref this.votingEvents, value);
        }

        public ObservableCollection<Candidate> Candidates
        {
            get => this.candidates;
            set => this.SetValue(ref this.candidates, value);
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

            this.myVotingEvents = (List<VotingEvent>)response.Result;
            this.RefresVotingEventsList();
        }

        private void RefresVotingEventsList()
        {
            this.VotingEvents = new ObservableCollection<VotingEventItemViewModel>(
                this.myVotingEvents.Select(p => new VotingEventItemViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    ImageFullPath = p.ImageFullPath,
                    Description=p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Name = p.Name,
                    User = p.User
                })
                .OrderBy(p => p.Name)
                .ToList());

        }
        public VotingEvent VotingEvent
        {
            get => this.votingEvent;
            set
            {
                this.SetValue(ref this.votingEvent, value);
                this.candidates = new ObservableCollection<Candidate>(this.VotingEvent.Candidates.OrderBy(c => c.Name));
            }
        }
        public Candidate Candidate
        {
            get => this.candidate;
            set => this.SetValue(ref this.candidate, value);
        }
    }
}
