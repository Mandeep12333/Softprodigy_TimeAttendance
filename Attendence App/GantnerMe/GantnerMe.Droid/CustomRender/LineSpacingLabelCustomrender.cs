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
using Xamarin.Forms;
using GantnerMe;
using GantnerMe.Droid.CustomRender;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LineSpacingLabel), typeof(LineSpacingLabelCustomrender))]
namespace GantnerMe.Droid.CustomRender
{
    public class LineSpacingLabelCustomrender : LabelRenderer
    {
        protected LineSpacingLabel LineSpacingLabel { get; private set; }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                this.LineSpacingLabel = (LineSpacingLabel)this.Element;
            }

            var lineSpacing = this.LineSpacingLabel.LineSpacing;

            this.Control.SetLineSpacing(1f, (float)lineSpacing);

            this.UpdateLayout();
        }
    }
}