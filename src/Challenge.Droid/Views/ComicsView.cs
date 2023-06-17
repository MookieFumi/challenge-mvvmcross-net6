using System.Diagnostics;
using Android.App;
using Android.OS;
using Challenge.Core.ViewModels;
using MvvmCross.Platforms.Android.Views;

namespace Challenge.UI.Droid.Views
{
    [Activity(Label = "Marvel Challenge. Comics", Theme = "@style/AppTheme")]
    public class ComicsView : MvxActivity<ComicsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Comics);
        }
    }
}