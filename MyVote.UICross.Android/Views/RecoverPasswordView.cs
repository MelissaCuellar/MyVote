using Android.Views;
using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using MyVote.Common.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace MyVote.UICross.Android.Views
{



    [Activity(Label = "@string/recoverpassword")]
    public class RecoverPasswordView : MvxAppCompatActivity<RecoverPasswordViewModel>       
        {
            protected override void OnCreate(Bundle bundle)
            {
                base.OnCreate(bundle);
                this.SetContentView(Resource.Layout.RecoverPasswordPage);
                var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
                SetSupportActionBar(toolbar);

                var actionBar = SupportActionBar;
                if (actionBar != null)
                {
                    actionBar.SetDisplayHomeAsUpEnabled(true);
                }
        }
        }
    
}