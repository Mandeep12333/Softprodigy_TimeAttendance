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
using Android.Graphics;
using Xamarin.Forms;
using GantnerMe.Droid.CustomRender;
using Android.Text;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]
namespace GantnerMe.Droid.CustomRender
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                EditText textField = (EditText)Control;
                //// Control.SetBackgroundResource(Resource.Drawable.PRATextBox);
                textField.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                // textField.SetHintTextColor(global::Android.Graphics.Color.Black);
                //textField.SetTextColor(global::Android.Graphics.Color.Black);
                //Set font
                textField.SetTypeface(Typeface.Create("Helvetica", TypefaceStyle.Normal), TypefaceStyle.Normal);
                textField.SetTextSize(Android.Util.ComplexUnitType.Sp, 16);
                // Misc
                //textField.SetSingleLine(true);
                //textField.SetHorizontallyScrolling(true);
                //textField.SetSelectAllOnFocus(false);


               // Commneted start//
                //textField.Ellipsize = TextUtils.TruncateAt.End;
                //GradientDrawable gd = new GradientDrawable();
                //gd.SetColor(Android.Graphics.Color.White);
                //gd.SetCornerRadius(20);
                //gd.SetStroke(2, Android.Graphics.Color.LightGray);
                //this.Control.SetBackgroundDrawable(gd);
                // End commented//

                //textField.SetBackgroundDrawable(gd);

            }
            else
            {

            }

        }
    }
}