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
using Com.Telerik.Widget.List;
using Foodi.Adapter;
using Android.Gms.Maps;
using Com.Syncfusion.Rating;
using Foodi.Model;
using System.Net;
using Newtonsoft.Json;
using Android.Gms.Tasks;
using System.Threading.Tasks;
using Android.Gms.Maps.Model;
using static Foodi.Adapter.FoodiPlaceAdapter;

namespace Foodi
{
    [Activity(Label = "FoodiPlaces", Theme ="@style/MyTheme.Base")]
    public class FoodiPlacesActivity : AppCompatActivity, IOnMapReadyCallback
    {
        private Android.Support.V7.Widget.Toolbar toolbar;
        private string LatLon = "";
        private string Name = "";
        private RadListView radLvFoodiPlaces;
        private FoodiPlaceAdapter placeAdapter;
        private List<Results> foodiPlaces;
        private TextView txtAddressFoodiPlaces;
         


        private static SfRating ratingBar;

        private GoogleMap googleMap;
        private MapFragment mapFragment;
        private static string namePlaces = "";
        private static double latP = 0;
        private static double lonP = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_foodi_places);

            LatLon = Intent.GetStringExtra(Constants.LatLon);
            Name = Intent.GetStringExtra(Constants.Name);
            InitViews();
        }

        void InitViews()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.TbMain);
            radLvFoodiPlaces = FindViewById<RadListView>(Resource.Id.radLvFoodiPlaces);
            txtAddressFoodiPlaces = FindViewById<TextView>(Resource.Id.txtAddressFoodiPlaces);
            
            foodiPlaces = new List<Results>();

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Foodi Places";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            LoadMapLayout();
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


        public async void LoadMapLayout()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                this.RunOnUiThread(() =>
            {
                if (CheckConnection.IsInternet())
                {
                    GetPlaceInfo(Name, LatLon.Split(",")[0], LatLon.Split(",")[1]);
                    foodiPlaces = GetPlaces();

                    placeAdapter = new FoodiPlaceAdapter(foodiPlaces, this);
                    LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this,LinearLayoutManager.Horizontal,false);
                    radLvFoodiPlaces.SetLayoutManager(linearLayoutManager);
                    radLvFoodiPlaces.SetAdapter(placeAdapter);

                    FoodiPlacesClickListenner foodiPlacesClickListenner = new FoodiPlacesClickListenner(this, placeAdapter);
                    radLvFoodiPlaces.AddItemClickListener(foodiPlacesClickListenner);
                    mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
                    mapFragment.GetMapAsync(this);
                }
            });
            });
              
        }
        public void GetPlaceInfo(string name, string lat, string lon)
        {
            namePlaces = name;
            latP = double.Parse(lat, System.Globalization.CultureInfo.InvariantCulture);
            lonP = double.Parse(lon, System.Globalization.CultureInfo.InvariantCulture);
        }

        public List<Results> GetPlaces()
        {
            try
            {
                List<Results> places = new List<Results>();
                string jsonPlaces;
                string url = APIs.FoodiPlaces + latP.ToString().Replace(",", ".") + "&lon=" + lonP.ToString().Replace(",", ".") + "&limit=5";
                using (WebClient client = new WebClient())
                {
                    jsonPlaces = client.DownloadString(url);
                };

                FoodiPlace foodiPlace = JsonConvert.DeserializeObject<FoodiPlace>(jsonPlaces);
                foreach(Results r in foodiPlace.results)
                {
                    places.Add(r);
                }
                return places;
            }
            catch
            {
                return null;
            }
        }

        public void OnMapReady(GoogleMap map)
        {
            googleMap = map;
            googleMap.MapType = GoogleMap.MapTypeTerrain;
            googleMap.Clear();
            FocusToCity(BitmapDescriptorFactory.HueRed, 12);
            foreach (Results r in foodiPlaces)
            {
                GetPlaceInfo(r.poi.name, r.position.lat.ToString().Replace(",", "."), r.position.lon.ToString().Replace(",", "."));
                googleMap.AddMarker(MarkCity(BitmapDescriptorFactory.HueCyan));
            }
            googleMap.MarkerClick += GoogleMap_MarkerClick;
        }

        public void FocusToCity(float color, float zoom)
        {
            var markerOpt1 = MarkCity(color);
            googleMap.AddMarker(markerOpt1).ShowInfoWindow();
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(markerOpt1.Position);
            builder.Bearing(155);
            builder.Tilt(65);
            CameraPosition cameraPosition = builder.Zoom(zoom).Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            googleMap.MoveCamera(cameraUpdate);
        }

        private void GoogleMap_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            e.Handled = true;
            var marker = e.Marker;
            marker.ShowInfoWindow();
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(marker.Position);
            builder.Bearing(155);
            builder.Tilt(65);
            CameraPosition cameraPosition = builder.Zoom(17).Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            googleMap.MoveCamera(cameraUpdate);
        }

        private bool IsListGone(LinearLayout layout)
        {
            if (layout.Visibility == ViewStates.Invisible || layout.Visibility == ViewStates.Gone)
            {
                return true;
            }
            return false;
        }

        public static MarkerOptions MarkCity(float color)
        {
            MarkerOptions markerOpt1 = new MarkerOptions();
            markerOpt1.SetPosition(new LatLng(latP, lonP));
            markerOpt1.SetTitle(namePlaces);
            var bmDescriptor = BitmapDescriptorFactory.DefaultMarker(color);
            markerOpt1.SetIcon(bmDescriptor);
            return markerOpt1;
        }

        public void ShowPlaceInfo(Context context, string[] info)
        {

        }
    }
}