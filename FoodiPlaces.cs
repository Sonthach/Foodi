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

namespace Foodi
{
    [Activity(Label = "FoodiPlaces")]
    public class FoodiPlaces : Activity
    {
        string LatLon = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LatLon = Intent.GetStringExtra(Constants.LatLon);
        }
    }
}