using GantnerMe;
using GantnerMe.Class;
using GantnerMe.iOS.CustomRenders;
using MapKit;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using System.Linq;
using Newtonsoft.Json;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace GantnerMe.iOS.CustomRenders
{
    public class CustomMapRenderer : MapRenderer
    {
        public AsignedLocationDB AsignedDB = new AsignedLocationDB();
        MKCircleRenderer circleRenderer;
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                nativeMap.OverlayRenderer = null;
            }

            if (e.NewElement != null)
            {
                //var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                //var circle = formsMap.Circle;
                nativeMap.OverlayRenderer = GetOverlayRenderer;
                try
                {
                    var getAsignedLoc = AsignedDB.GetAsignedLocation().ToList();
                    if (getAsignedLoc != null && getAsignedLoc.Count > 0)
                    {
                        foreach (var item in getAsignedLoc)
                        {
                            string Location = item.mapLocation;
                            var AsignedLocation = JsonConvert.DeserializeObject<App.Location1>(Location);
                            var circleOverlay = MKCircle.Circle(new
                    CoreLocation.CLLocationCoordinate2D(AsignedLocation.lat, AsignedLocation.lng), 600);
                            nativeMap.AddOverlay(circleOverlay);
                        }
                    }

                }
                catch (Exception ex)
                {
                    var msg = ex.Message.ToString();

                }

            }
        }

        MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlay)
        {
            try
            {
                if (circleRenderer == null)
                {
                    circleRenderer = new MKCircleRenderer(overlay as MKCircle);
                    circleRenderer.FillColor = UIColor.Red;
                    circleRenderer.Alpha = 0.4f;
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
            }
            return circleRenderer;
        }
    }
}
