using Android.App;
using Android.OS;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using MyVote.Common.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace MyVote.UICross.Android.Views
{
    [Activity(Label = "@string/candidates")]
    public class VotingEventDetailView : MvxAppCompatActivity<VotingEventsDetailViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.VotingEventDetailPage);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var actionBar = SupportActionBar;
            if (actionBar != null)
            {
                actionBar.SetDisplayHomeAsUpEnabled(true);
            }
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == global::Android.Resource.Id.Home) { OnBackPressed(); }
            return base.OnOptionsItemSelected(item);
        }

    }
}




