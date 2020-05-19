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
using Android.Support.V7.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
namespace Foodi
{
    [Activity(Label = "FoodiPlaces", Theme ="@style/MyTheme.Base")]
    public class FoodiPlaces : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar toolbar;
        string LatLon = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_foodi_places);

            LatLon = Intent.GetStringExtra(Constants.LatLon);
            InitViews();
        }

        void InitViews()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.TbMain);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Foodi Places";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    {
                        Intent i = new Intent(this, typeof(MainActivity));
                        StartActivity(i);
                        return true;
                    }
            }
            return base.OnOptionsItemSelected(item);

        }
    }
}