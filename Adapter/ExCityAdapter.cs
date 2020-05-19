using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Telerik.Widget.List;
using Foodi.Model;
using System.Collections;
using System.Collections.Generic;

namespace Foodi.Adapter
{
    public class ExCityAdapter : ListViewAdapter
    {
        private MainActivity activity;
        public List<District> districts;
       

        public ExCityAdapter(IList items,MainActivity context)
            : base(items)
        {
            this.activity = context;
            this.districts = new List<District>();
            this.districts = items as List<District>;
        }
       
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View view = inflater.Inflate(Resource.Layout.item_district, parent, false);
            return new DistrictViewHolder(view);
        }
        public override void OnBindListViewHolder(ListViewHolder holder, int pos)
        {
            DistrictViewHolder vh = (DistrictViewHolder)holder;
            District district = (District)Items[pos];
            vh.txtDistrictName.Text = district.Name;

            vh.txtDistrictName.Click += (s, e) =>
            {
                Intent i = new Intent(activity, typeof(FoodiPlaces));
                i.PutExtra(Constants.LatLon, districts[pos].LatLon);
                activity.StartActivity(i);
            };

        }
        
        public void RefreshList()
        {
            Intent i = new Intent(activity, typeof(MainActivity));
            activity.StartActivity(i);
        }
    }

    public class DistrictViewHolder : ListViewHolder
    {
        public TextView txtDistrictName;

        public DistrictViewHolder(View itemView) : base(itemView)
        {
            txtDistrictName = itemView.FindViewById<TextView>(Resource.Id.txtDistrictName);
        }
    }
    public class DistrictClickListener : Java.Lang.Object, RadListView.IItemClickListener
    {
        private MainActivity mainAct;
        private ListViewAdapter listViewAdapter;

        public DistrictClickListener(MainActivity mainAct, ListViewAdapter adapter)
        {
            this.mainAct = mainAct;
            listViewAdapter = adapter;
        }

        public void OnItemClick(int postion, MotionEvent motionEvent)
        {
            
            //var item = listViewAdapter.GetItem(postion).ToString().Split("|");
        }

        public void OnItemLongClick(int postion, MotionEvent motionEvent)
        {
        }
    }
}