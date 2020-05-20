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
using Android.Graphics;
using Android.Text;
using Android.Text.Method;
using Android.Text.Style;

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
        private TextView txtNameFoodiPlaces;
        private readonly List<Review> reviews = new List<Review>
        {
            new Review { Name = "Quang Minh", Date = DateTime.Now.AddDays(-4), Score = (float)3.5, Detail="Bình thường" },
            new Review { Name = "Hùng Minh", Date = DateTime.Now.AddDays(-4), Score = (float)1.5, Detail="Quá Tệ" },
            new Review { Name = "Nặc Danh", Date = DateTime.Now.AddDays(-2), Score = (float)2.5, Detail="Tệ" },
            new Review { Name = "Người dùng", Date = DateTime.Now.AddDays(-2), Score = (float)4.5, Detail="Tuyệt Vời" },
            new Review { Name = "Account", Date = DateTime.Now, Score = (float)4.5, Detail="Quá Tuyệt Vời" },
        };
        private TextView txtScore;
        private static SfRating ratingBar;
        private FrameLayout frReviews;
        private TextView txtTopReviewsDetail;
        private SfRating sfRatingTopReview;
        private TextView txtTopReviewsDate;
        private TextView txtMore;
        private LinearLayout lnReviewsTop;
        private LinearLayout lnMoreReviews;
        private ReviewAdapter reviewAdapter;
        private RadListView radReviews;

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

            txtNameFoodiPlaces.Visibility = ViewStates.Gone;
            txtAddressFoodiPlaces.Visibility = ViewStates.Gone;
        }

        void InitViews()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.TbMain);
            radLvFoodiPlaces = FindViewById<RadListView>(Resource.Id.radLvFoodiPlaces);
            txtAddressFoodiPlaces = FindViewById<TextView>(Resource.Id.txtAddressFoodiPlaces);
            txtNameFoodiPlaces = FindViewById<TextView>(Resource.Id.txtNameFoodiPlaces);
            ratingBar = FindViewById<SfRating>(Resource.Id.sfRatingTotal);
            foodiPlaces = new List<Results>();
            txtScore = FindViewById<TextView>(Resource.Id.txtScore);
            frReviews = FindViewById<FrameLayout>(Resource.Id.frReviews);
            txtTopReviewsDate = FindViewById<TextView>(Resource.Id.txtTopReviewsDate);
            txtTopReviewsDetail = FindViewById<TextView>(Resource.Id.txtTopReviewsDetail);
            txtMore = FindViewById<TextView>(Resource.Id.txtMore);
            sfRatingTopReview = FindViewById<SfRating>(Resource.Id.sfRatingTopReview);
            lnReviewsTop = FindViewById<LinearLayout>(Resource.Id.lnReviewsTop);
            lnMoreReviews = FindViewById<LinearLayout>(Resource.Id.lnMoreReviews);
            radReviews = FindViewById<RadListView>(Resource.Id.radReviews);


            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Foodi Places";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            toolbar.SetTitleTextColor(Color.White);
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

                    DisplayCustomRatingBar(ratingBar);
                }
                else
                {
                    Toast.MakeText(this, "No Internet Connected!", ToastLength.Long).Show();
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
                int i = 0;
                FoodiPlace foodiPlace = JsonConvert.DeserializeObject<FoodiPlace>(jsonPlaces);
                foreach(Results r in foodiPlace.results)
                {
                    r.info = Resources.GetIdentifier("p" + i, "drawable", PackageName).ToString();
                    places.Add(r);
                    i++;
                }
                return places;
            }
            catch
            {
                return null;
            }
        }

        static void DisplayCustomRatingBar(SfRating ratingBar)
        {
            ratingBar.Precision = Precision.Exact;
            ratingBar.ReadOnly = true;
            ratingBar.ItemSize = 10;
            //ratingBar.ItemCount = 4;
            ratingBar.ItemSpacing = 4;
            SfRatingSettings ratingSettings = new SfRatingSettings
            {
                RatedFill = Color.Yellow,
                UnRatedFill = Color.Gray,
                RatedStroke = Color.DarkOrange,
                UnRatedStroke = Color.DarkGray
            };
            ratingBar.RatingSettings = ratingSettings;
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
            txtAddressFoodiPlaces.Visibility = ViewStates.Visible;
            txtNameFoodiPlaces.Visibility = ViewStates.Visible;
            frReviews.Visibility = ViewStates.Visible;
            lnReviewsTop.Visibility = ViewStates.Visible;
            txtTopReviewsDetail.Visibility = ViewStates.Visible;
            lnMoreReviews.Visibility = ViewStates.Gone;
            txtMore.Visibility = ViewStates.Visible;
            var score = (float)Math.Round(float.Parse(info[0], System.Globalization.CultureInfo.InvariantCulture) / 2, 2);
            ratingBar.Value = score;
            txtScore.Text = score.ToString("0.00");
            txtNameFoodiPlaces.Text = info[1];
            txtAddressFoodiPlaces.Text = "Địa chỉ: " + info[2] + ".";

            Random random = new Random();
            int randomPosition = random.Next(1, reviews.Count - 1);
            Review review = reviews[randomPosition];
            sfRatingTopReview.Value = review.Score;
            DisplayCustomRatingBar(sfRatingTopReview);
            txtTopReviewsDetail.Text = review.Detail;
            txtTopReviewsDate.Text = review.Date.ToShortDateString() + "\n" + review.Name;
            SpannableString ss = new SpannableString("Xem Thêm");
            var clickableSpan = new MyClickableSpan();
            clickableSpan.Click += v => LoadReviews();
            ss.SetSpan(clickableSpan, 0, 8, SpanTypes.ExclusiveExclusive);
            txtMore.TextFormatted = ss;
            txtMore.MovementMethod = new LinkMovementMethod();
        }

        private class MyClickableSpan : ClickableSpan
        {
            public Action<View> Click;

            public override void OnClick(View widget)
            {
                Click?.Invoke(widget);//check if not null
            }
        }


        public async void LoadReviews()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                this.RunOnUiThread(() =>
                {
                    if (reviewAdapter == null)
                    {
                        reviewAdapter = new ReviewAdapter(reviews, this);
                        radReviews.SetAdapter(reviewAdapter);
                    }
                });
            });
            lnMoreReviews.Visibility = ViewStates.Visible;
            frReviews.Visibility = ViewStates.Gone;
            lnReviewsTop.Visibility = ViewStates.Gone;
            txtTopReviewsDetail.Visibility = ViewStates.Gone;
            txtMore.Visibility = ViewStates.Gone;
        }
    }
}