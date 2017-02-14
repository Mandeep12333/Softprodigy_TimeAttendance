using CoreGraphics;
using GantnerMe.iOS.CustomRenders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(Frame), typeof(MyFrameRenderer))]
namespace GantnerMe.iOS.CustomRenders
{
    public class MyFrameRenderer : FrameRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {
            try
            {
                base.OnElementChanged(e);
                var elem = (Frame)this.Element;
                if (elem != null)
                {
                    // Border
                    this.Layer.CornerRadius = 10;
                    this.Layer.Bounds.Inset(1, 1);
                    this.Layer.BorderWidth = (float)1.0;
                    this.Layer.MasksToBounds = true;

                    // Shadow
                    this.Layer.ShadowColor = UIColor.DarkGray.CGColor;
                    this.Layer.ShadowOpacity = 0.6f;
                    this.Layer.ShadowRadius = 2.0f;
                    this.Layer.ShadowOffset = new SizeF(0, 0);
                }
            }
            catch (Exception ex)
            {

            }

        }

        //public override void Draw(CGRect rect)
        //{
        //    SetupShadowLayer();
        //    base.Draw(rect);
        //}


        //void SetupShadowLayer()
        //{
        //    try
        //    {
        //        // Border
        //        this.Layer.CornerRadius = (nfloat)20;
        //        this.Layer.Bounds.Inset(1, 1);
        //        Layer.BorderColor = UIColor.FromRGB(209, 223, 228).CGColor;
        //        Layer.BorderWidth = (float)0.5;

        //        // Shadow
        //        this.Layer.ShadowColor = UIColor.FromRGB(209, 223, 228).CGColor;
        //        this.Layer.ShadowOpacity = 0.5f;
        //        this.Layer.ShadowRadius = 2;
        //        this.Layer.ShadowOffset = new SizeF(2, 2);
        //        Layer.RasterizationScale = UIScreen.MainScreen.Scale;
        //        Layer.ShouldRasterize = true;
        //        //this.Layer.MasksToBounds = true;
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}


    }
}
