using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyVote.UICross.Android.Views
{
    using Common.ViewModels;
    using global::Android.App;
    using global::Android.OS;
    using MvvmCross.Platforms.Android.Views;

    [Activity(Label = "@string/app_name")]
    public class LoginView : MvxActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.LoginPage);
        }
    }

}