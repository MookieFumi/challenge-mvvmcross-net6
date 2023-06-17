using Android.App;
using Android.OS;
using Challenge.Core.ViewModels;
using MvvmCross.Platforms.Android.Views;

namespace Challenge.UI.Droid.Views
{
    [Activity(Label = "Comic", Theme = "@style/AppTheme")]
    public class ComicDetailView : MvxActivity<ComicDetailViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Comic);
        }
    }
}