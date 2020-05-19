using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using Foodi.Adapter;
using Newtonsoft.Json;
using System.Collections.Generic;
using Org.Apache.Http.Conn;
using Com.Telerik.Widget.List;
using Foodi.Model;
using System.Net;
using SQLite;
using Android.Util;
using Refit;
using Android.Views;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Animation;

namespace Foodi
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme.Base")]
    public class MainActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar toolbar;
        private RadListView radListView;
        private ExCityAdapter districtAdapter;
        private List<DistrictItem> districts;
        private Android.Support.V7.Widget.SearchView searchView;
        private ExCityAdapter SearchAdapter;
        private List<District> dsSearch;
        private readonly List<string> coords = new List<string>
        {
            "10.771779, 106.697601",
            "10.787089, 106.735830",
            "10.784644, 106.686360",
            "10.758219, 106.703995",
            "10.755081, 106.665874",
            "10.745301, 106.634730",
            "10.740955, 106.713407",
            "10.729290, 106.636965",
            "10.831577, 106.819116",
            "10.771369, 106.667625",
            "10.767632, 106.642119",
            "10.873367, 106.654346",
            "10.800530, 106.679966",
            "10.810122, 106.652929",
            "10.787157, 106.628692",
            "10.759200, 106.597406",
            "10.807412, 106.704279",
            "10.856632, 106.738916",
            "10.838945, 106.664604",
            "10.667052, 106.718715",
            "10.735738, 106.556669",
            "10.547773, 106.866179",
            "11.016590, 106.505081",
            "10.884103, 106.577698",
            "10.805931, 106.599054"
        };
        private List<District> dsList;

        private static bool isfabOpen;
        private FloatingActionButton fabMain;
        private FloatingActionButton fabAccount1;
        private FloatingActionButton fabAccount2;
        private View bgFabMenu;

        private readonly SQLiteConnection db = new SQLiteConnection(DatabaseFilePath);

        public static string DatabaseFilePath
        {
            get
            {
                var sqliteFilename = "district1.db3";
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var path = System.IO.Path.Combine(documentsPath, sqliteFilename);
                return path;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            InitViews();

            fabMain = FindViewById<FloatingActionButton>(Resource.Id.fab_main);
            fabAccount1 = FindViewById<FloatingActionButton>(Resource.Id.fab_account1);
            fabAccount2 = FindViewById<FloatingActionButton>(Resource.Id.fab_account2);
            bgFabMenu = FindViewById<View>(Resource.Id.bg_fab_menu);

            fabMain.Click += (o, e) =>
            {
                if (!isfabOpen)
                    ShowFabMenu();
                else
                    CloseFabMenu();
            };
            fabAccount2.Click += (o, e) =>
            {
                CloseFabMenu();
                GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 2, LinearLayoutManager.Vertical, false);
                radListView.SetLayoutManager(gridLayoutManager);
                //return true;

            };
            fabAccount1.Click += (o, e) =>
            {
                CloseFabMenu();
                //Toast.MakeText(this, "Account1!", ToastLength.Short).Show();
                LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this);
                radListView.SetLayoutManager(linearLayoutManager);
                //return true;
            };
            bgFabMenu.Click += (o, e) =>
            {

            };
        }

        private void CloseFabMenu()
        {
            isfabOpen = false;
            fabAccount1.Visibility = ViewStates.Visible;
            fabAccount2.Visibility = Android.Views.ViewStates.Visible;
            bgFabMenu.Visibility = Android.Views.ViewStates.Visible;

            fabMain.Animate().Rotation(90f);
            bgFabMenu.Animate().Alpha(1f);
            fabAccount1.Animate()
                .TranslationY(0f)
                .Rotation(90f);
            fabAccount2.Animate()
                .TranslationY(0f)
                .Rotation(90f).SetListener(new FabAnimatorListener(bgFabMenu, fabAccount1, fabAccount2));

        }

        private void ShowFabMenu()
        {
            isfabOpen = true;
            fabAccount1.Visibility = ViewStates.Visible;
            fabAccount2.Visibility = Android.Views.ViewStates.Visible;
            bgFabMenu.Visibility = Android.Views.ViewStates.Visible;

            fabMain.Animate().Rotation(135f);
            bgFabMenu.Animate().Alpha(1f);
            fabAccount2.Animate()
                .TranslationY(-Resources.GetDimension(Resource.Dimension.standard_100))
                .Rotation(0f);
            fabAccount1.Animate()
                .TranslationY(-Resources.GetDimension(Resource.Dimension.standard_55))
                .Rotation(0f);

        }

        private class FabAnimatorListener : Java.Lang.Object, Animator.IAnimatorListener
        {
            View[] viewsToHide;
            public FabAnimatorListener(params View[] viewsToHide)
            {
                this.viewsToHide = viewsToHide;
            }
            public void OnAnimationCancel(Animator animation)
            {

            }

            public void OnAnimationEnd(Animator animation)
            {
                if (!isfabOpen)
                    foreach (var view in viewsToHide)
                        view.Visibility = ViewStates.Gone;
            }

            public void OnAnimationRepeat(Animator animation)
            {

            }

            public void OnAnimationStart(Animator animation)
            {

            }
        }
        void InitViews()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.TbMain);
            radListView = FindViewById<RadListView>(Resource.Id.radListView);
            districts = new List<DistrictItem>();
            dsList = new List<District>();
            db.CreateTable<DistrictItem>();
            if (CheckConnection.IsInternet())
            {
                GetData();
            }
            else
            {
                //Toast.MakeText(this, "No Internet Connected!", ToastLength.Long).Show();
                dsList = getDistricts();
                districtAdapter = new ExCityAdapter(dsList, this);
                radListView.SetAdapter(districtAdapter);
            }

            dsList = getDistricts();
            districtAdapter = new ExCityAdapter(dsList, this);
            radListView.SetAdapter(districtAdapter);
            DistrictClickListener districtClickListener = new DistrictClickListener(this, districtAdapter);
            radListView.AddItemClickListener(districtClickListener);
            LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this);
            radListView.SetLayoutManager(linearLayoutManager);

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Foodi";
        }

        

        private bool IsItemExist(int id, SQLiteConnection db)
        {
            try
            {
                var existingItem = db.Get<DistrictItem>(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        void GetData()
        {
            try
            {
                string json;
                using (WebClient client = new WebClient())
                {
                    json = client.DownloadString(APIs.District);
                };
                int i = 0;
                districts = JsonConvert.DeserializeObject<List<DistrictItem>>(json);
                foreach(var ds in districts)
                {
                    if (!IsItemExist(ds.ID, db) && db != null)
                    {
                        if(ds.Title != "Chưa rõ" && (ds.Title == "Thành Phố Hồ Chí Minh" || ds.Title == "Quận 1" || ds.Title == "Quận 2" 
                            || ds.Title == "Quận 3" || ds.Title == "Quận 4" || ds.Title == "Quận 5" || ds.Title == "Quận 6" || ds.Title == "Quận 7" ||
                            ds.Title == "Quận 8" || ds.Title == "Quận 9" || ds.Title == "Quận 10" || ds.Title == "Quận 11" || ds.Title == "Quận 12"
                            || ds.Title == "Huyện Nhà Bè" || ds.Title == "Quận Bình Tân" || ds.Title == "Huyện Bình Chánh" || ds.Title == "Quận Bình Thạnh"
                            || ds.Title == "Quận Tân Bình" || ds.Title == "Huyện Cần Giờ" || ds.Title == "Huyện Củ Chi" || ds.Title == "Quận Thủ Đức" || ds.Title == "Quận Gò Vấp"
                            || ds.Title == "Huyện Hóc Môn" || ds.Title == "Quận Phú Nhuận" || ds.Title == "Quận Tân Phú"))
                        {
                            ds.Created = coords[i];
                            ds.Updated = "Thành Phố Hồ Chí Minh";
                            db.Insert(ds);
                        }
                        i++;
                    }
                }
            }
            catch
            {

            }
        }
        List<District> getDistricts()
        {
            List<District> dsList = new List<District>();
            var districtList = db.Table<DistrictItem>();
            foreach (var dsItem in districtList)
            {
                District d = new District
                {
                    ID = dsItem.ID,
                    Name = dsItem.Title,
                    LatLon = dsItem.Created,
                    Image = dsItem.TotalDoanhNghiep,
                    Area = dsItem.Updated
                };
                dsList.Add(d);
            }
            return dsList;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            var item = menu.FindItem(Resource.Id.action_search);
            var search = MenuItemCompat.GetActionView(item);
            searchView = search.JavaCast<Android.Support.V7.Widget.SearchView>();
            dsSearch = new List<District>();
            
            searchView.QueryTextChange += (s, e) =>
            {
                dsSearch.Clear();
                foreach(var ds in dsList)
                {
                    if(ds.Name.ToLower().Contains(e.NewText.ToLower()))
                    {
                        dsSearch.Add(ds);
                    }
                    SearchAdapter = new ExCityAdapter(dsSearch, this);
                    radListView.SetAdapter(SearchAdapter);
                }
            };

            return true;
        }
    }
}