﻿using Android.Content;
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
                 activity.lnProgressBar.Visibility = ViewStates.Visible;
                 Intent i = new Intent(activity, typeof(FoodiPlacesActivity));
                 i.PutExtra(Constants.LatLon, districts[pos].LatLon);
                 i.PutExtra(Constants.Name, districts[pos].Name);
                 activity.StartActivity(i);
             };
            activity.lnProgressBar.Visibility = ViewStates.Gone;
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
        private Context context;
        private ListViewAdapter listViewAdapter;

        public DistrictClickListener(Context context, ListViewAdapter adapter)
        {
            this.context = context;
            listViewAdapter = adapter;
        }

        public void OnItemClick(int postion, MotionEvent motionEvent)
        {
            /*var item = listViewAdapter.GetItem(postion).ToString().Split("|");
            if(item.Length > 1)
            {
                Intent i = new Intent(context, typeof(FoodiPlacesActivity));
                //context.LoadMapLayout(item);
                //context.StartActivity(i);
            }*/
        }

        public void OnItemLongClick(int postion, MotionEvent motionEvent)
        {
        }
    }
}