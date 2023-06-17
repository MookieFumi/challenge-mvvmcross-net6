using System;
using Android.App;
using Android.Runtime;
using Challenge.Core;
using MvvmCross.Platforms.Android.Views;

namespace Challenge.UI.Droid
{
    [Application]
    public class MainApplication : MvxAndroidApplication<Setup, App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}
