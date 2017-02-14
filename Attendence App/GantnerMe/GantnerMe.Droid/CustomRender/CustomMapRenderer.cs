using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using GantnerMe.Model;
using Xamarin.Forms;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using System.Collections;
using GantnerMe;
using GantnerMe.Droid.CustomRender;
using Plugin.Geolocator;
using GantnerMe.Class;
using Newtonsoft.Json;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace GantnerMe.Droid.CustomRender
{

    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        public AsignedLocationDB AsignedDB = new AsignedLocationDB();
        GoogleMap map;
        CustomCircle circle;


        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                circle = formsMap.Circle;

                ((MapView)Control).GetMapAsync(this);
            }
        }

        public  void OnMapReady(GoogleMap googleMap)
        {
            this.map = googleMap;
            map.UiSettings.ZoomControlsEnabled = true;
            try
            {
                List<CircleOptions> list = new List<CircleOptions>();
                //List<App.MultipleCircle> latlonglist = new List<App.MultipleCircle>();
                //App.MultipleCircle objlist1 = new App.MultipleCircle();
                //objlist1.Lat = 30.714478;
                //objlist1.Long = 76.714893;
                //latlonglist.Add(objlist1);
                //objlist1 = new App.MultipleCircle();
                //objlist1.Lat = 30.716588;
                //objlist1.Long = 76.701648;
                //latlonglist.Add(objlist1);
                //objlist1 = new App.MultipleCircle();
                //objlist1.Lat = 30.727019;
                //objlist1.Long = 76.719901;
                //latlonglist.Add(objlist1);
                //objlist1 = new App.MultipleCircle();
                //objlist1.Lat = 30.719059;
                //objlist1.Long = 76.748704;
                //latlonglist.Add(objlist1);

                var getAsignedLoc = AsignedDB.GetAsignedLocation().ToList();
                if (getAsignedLoc != null && getAsignedLoc.Count > 0)
                {
                    foreach (var item in getAsignedLoc)
                    {
                        string Location = item.mapLocation;
                        var AsignedLocation = JsonConvert.DeserializeObject<App.Location1>(Location);
                        CircleOptions objlist = new CircleOptions();
                        objlist.InvokeCenter(new LatLng(AsignedLocation.lat, AsignedLocation.lng));
                        objlist.InvokeRadius(600);
                        objlist.InvokeFillColor(0X66FF0000);
                        objlist.InvokeStrokeColor(0X66FF0000);
                        objlist.InvokeStrokeWidth(4);
                        list.Add(objlist);
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        map.AddCircle(list[i]);
                    }
                }


                //var locator = CrossGeolocator.Current;
                //locator.DesiredAccuracy = 100;
                //var position = await locator.GetPositionAsync(timeoutMilliseconds: 20000);
                //if (position != null)
                //{
                //Circle circle;
                //var getAsignedLoc = AsignedDB.GetAsignedLocation().ToList();
                //if (getAsignedLoc.Count() > 0)
                //{
                //    foreach (var item in getAsignedLoc)
                //    {
                //        string Location = item.mapLocation;
                //        var AsignedLocation = JsonConvert.DeserializeObject<App.Location1>(Location);
                //        map = googleMap;
                //        CircleOptions circleOptions = new CircleOptions();
                //        var latlong = new LatLng(30.714478, 76.714893);
                //        circleOptions.InvokeCenter(new LatLng(30.714478, 76.714893));
                //        circleOptions.InvokeRadius(800);
                //        circleOptions.InvokeFillColor(0X66FF0000);
                //        circleOptions.InvokeStrokeColor(0X66FF0000);
                //        circleOptions.InvokeStrokeWidth(4);
                //        CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlong, 15);
                //        // map.AddCircle(circleOptions);
                //        circle = map.AddCircle(circleOptions);


            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
            }
        }

    }
}