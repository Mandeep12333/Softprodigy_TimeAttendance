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
using GantnerMe.Droid.CustomRender;
using Android.Graphics.Drawables;
using Android.Graphics;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Frame), typeof(MyFrameRenderer))]
namespace GantnerMe.Droid.CustomRender
{
    public class MyFrameRenderer : FrameRenderer
    {
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            DrawOutline(canvas, canvas.Width, canvas.Height,8f);//set corner radius
        }


        void DrawOutline(Canvas canvas, int width, int height, float cornerRadius)
        {
            try
            {
                using (var paint = new Paint { AntiAlias = true })
                using (var path = new Path())
                using (Path.Direction direction = Path.Direction.Cw)
                using (Paint.Style style = Paint.Style.Stroke)
                using (var rect = new RectF(0, 0, width, height))
                {
                    float rx = Xamarin.Forms.Forms.Context.ToPixels(cornerRadius);
                    float ry = Xamarin.Forms.Forms.Context.ToPixels(cornerRadius);
                    path.AddRoundRect(rect, rx, ry, direction);

                    //  paint.StrokeWidth = 2f;  //set outline stroke
                    paint.SetStyle(style);
                    paint.Color = Color.ParseColor("#D1DFE4");//set outline color //_frame.OutlineColor.ToAndroid(); 
                    canvas.DrawPath(path, paint);
                }
            }
            catch(Exception ex)
            {

            }
        }

    }
}