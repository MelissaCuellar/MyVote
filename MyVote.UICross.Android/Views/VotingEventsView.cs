using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using MyVote.Common.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace MyVote.UICross.Android.Views
{
    [Activity(Label = "@string/votingevents")]
    public class VotingEventsView : MvxAppCompatActivity<VotingEventsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.VotingEventsPage);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }
    }
}