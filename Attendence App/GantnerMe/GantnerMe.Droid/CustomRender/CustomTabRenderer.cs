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
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using Android.Graphics.Drawables;
using GantnerMe.Droid.CustomRender;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabRenderer))]
namespace GantnerMe.Droid.CustomRender
{
   public class CustomTabRenderer : TabbedRenderer
    {
        private Activity activity;
        private TabbedPage _tabbedPage;
        private const string COLOR = "#316888";

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            activity = this.Context as Activity;
            _tabbedPage = e.NewElement as TabbedPage;
        }

        protected override void DispatchDraw(Canvas canvas)
        {

            ActionBar actionBar = activity.ActionBar;
            if (actionBar.TabCount > 0)
            {
                ColorDrawable colorDrawable = new ColorDrawable(global::Android.Graphics.Color.ParseColor(COLOR));
                actionBar.SetStackedBackgroundDrawable(colorDrawable);
                ActionBarTabsSetup(actionBar);

            }

            base.DispatchDraw(canvas);
        }


        private void ActionBarTabsSetup(ActionBar actionBar)
        {
            try
            {
                if (actionBar.TabCount == 0) return;
                //if (this.Element.GetType() == typeof(ConferencePage))
                //{
                //    ActionBar.Tab Keynote = actionBar.GetTabAt(0);
                //    Keynote.SetIcon(Resource.Drawable.keynote_presentation);
                //    ActionBar.Tab General = actionBar.GetTabAt(1);
                //    General.SetIcon(Resource.Drawable.general_prsenatation);
                //    isFirstDesign = false;
                //}
                //if (this.Element.GetType() == typeof(ChatListingUsers))
                //{
                //    ActionBar.Tab Chat = actionBar.GetTabAt(0);
                //    Chat.SetIcon(Resource.Drawable.ChatIcon);
                //    ActionBar.Tab Participant = actionBar.GetTabAt(1);
                //    Participant.SetIcon(Resource.Drawable.participantlist_icon);
                //    isFirstDesign = false;
                //}
                //_tabbedPage.Children[0].IC
                //for (int i = 0; i < actionBar.TabCount; i++)
                //{
                //    Android.App.ActionBar.Tab dashboardTab = actionBar.GetTabAt(i);
                //    if (TabIsEmpty(dashboardTab))
                //    {

                //        int id = Resources.GetIdentifier(_tabbedPage.Children[i].Icon.File, "drawable", Context.PackageName);
                //        TabSetup(dashboardTab, id);
                //    }

                //}

            }
            catch (Exception)
            {

            }

        }

        private bool TabIsEmpty(ActionBar.Tab tab)
        {
            if (tab != null)
                if (tab.CustomView == null)
                    return true;
            return false;
        }

        private void TabSetup(ActionBar.Tab tab, int resourceID)
        {
            ImageView iv = new ImageView(activity);
            iv.SetImageResource(resourceID);
            iv.SetPadding(0, 10, 0, 0);

            tab.SetCustomView(iv);
        }



    }
}