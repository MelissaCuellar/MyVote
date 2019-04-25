using GalaSoft.MvvmLight.Command;
using MyVote.Common.Models;
using MyVote.UIForms.Views;
using System.Windows.Input;

namespace MyVote.UIForms.ViewModels
{
    public class VotingEventItemViewModel : VotingEvent
    {
        public ICommand SelectVotingEventCommand => new RelayCommand(this.SelectVotingEvent);

        private async void SelectVotingEvent()
        {
            MainViewModel.GetInstance().Candidates = new CandidatesViewModel(this);
            await App.Navigator.PushAsync(new CandidatesPage());
        }
    }
}
