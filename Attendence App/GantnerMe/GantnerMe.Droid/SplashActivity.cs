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
using Android.Support.V7.App;
using System.Threading.Tasks;
using Android.Webkit;
using Java.IO;

namespace GantnerMe.Droid
{
    [Activity(Theme = "@style/Theme.Splash", Icon = "@drawable/appicon", //Indicates the theme to use for this activity
             MainLauncher = true, //Set it as boot activity
             NoHistory = true)]
    public class SplashActivity : Activity
    {
        ImageView imageView;
        protected  override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // this.SetContentView(Resource.Layout.SpalshLayout);
           // await Task.Delay(1000);
            //SetContentView(Resource.Layout.SpalshLayout);
            // imageView = FindViewById<ImageView>(Resource.Id.animated_loading);
            // Create your application here
            // System.Threading.ThreadPool.QueueUserWorkItem(o => LoadActivity());
        }

        //public override void OnWindowFocusChanged(bool hasFocus)
        //{

        //    this.SetContentView(Resource.Layout.SpalshLayout);
        //    imageView = FindViewById<ImageView>(Resource.Id.animated_loading);
        //    Android.Graphics.Drawables.AnimationDrawable animation = (Android.Graphics.Drawables.AnimationDrawable)imageView.Drawable;
        //    animation.Start();

        //}
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() =>
            {
                Task.Delay(5000);  // Simulate a bit of startup work.
            });

            startupWork.ContinueWith(t =>
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
                Finish();
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }

    }
}