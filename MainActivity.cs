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

namespace Foodi
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme.Base")]
    public class MainActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar toolbar;
        private RadListView radListView;
        private CollapsibleGroupsBehavior collapsibleGroupsBehavior;
        private ExCityAdapter districtAdapter;
        private List<DistrictItem> districts;
        
        private readonly SQLiteConnection db = new SQLiteConnection(DatabaseFilePath);

        public static string DatabaseFilePath
        {
            get
            {
                var sqliteFilename = "district.db3";
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


        }

        void InitViews()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.TbMain);
            radListView = FindViewById<RadListView>(Resource.Id.radListView);
            districts = new List<DistrictItem>();
            //db.CreateTable<DistrictItem>();

            if (IsInternet())
            {
                GetData();
            }
            else
            {
                Toast.MakeText(this, "No Internet Connected!", ToastLength.Long).Show();
            }

            collapsibleGroupsBehavior = new CollapsibleGroupsBehavior();
            radListView.AddBehavior(collapsibleGroupsBehavior);
            LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this);
            radListView.SetLayoutManager(linearLayoutManager);

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Foodi";
        }

        public static bool IsInternet()
        {
            try
            {
                Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
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

                districts = JsonConvert.DeserializeObject<List<DistrictItem>>(json);
                districtAdapter = new ExCityAdapter(districts, this);
                radListView.SetAdapter(districtAdapter);
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

    }
}