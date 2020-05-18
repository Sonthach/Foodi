using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Arch.Core.Util;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Telerik.Widget.List;
using Foodi.Model;

namespace Foodi.Adapter
{
    public class ExCityAdapter : ListViewAdapter
    {
        private MainActivity activity;

        public ExCityAdapter(IList items, MainActivity context)
            : base(items)
        {
            this.activity = context;
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
            DistrictItem district = (DistrictItem)Items[pos];
            vh.txtDistrictName.Text = district.Title;
        }
        
        public void RefreshList()
        {
            Intent i = new Intent(activity, typeof(MainActivity));
            activity.StartActivity(i);
        }
    }

    public class CollapseListener : Java.Lang.Object, CollapsibleGroupsBehavior.ICollapseGroupListener
    {
        Context context_listener;
        public CollapseListener(Context context)
        {
            context_listener = context;
        }

        public void OnGroupCollapsed(Java.Lang.Object p0)
        {
        }

        public void OnGroupExpanded(Java.Lang.Object p0)
        {
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
        private CollapsibleGroupsBehavior collapsibleGroupsBehavior;

        public DistrictClickListener(MainActivity mainAct, ListViewAdapter adapter, CollapsibleGroupsBehavior collapsibleGroupsBehavior)
        {
            this.mainAct = mainAct;
            listViewAdapter = adapter;
            this.collapsibleGroupsBehavior = collapsibleGroupsBehavior;
        }

        public void OnItemClick(int postion, MotionEvent motionEvent)
        {
            var item = listViewAdapter.GetItem(postion).ToString().Split("|");
            CollapseListener collapseListener = new CollapseListener(mainAct);
            if (collapsibleGroupsBehavior.IsGroupCollapsed(listViewAdapter.GetItem(postion)) == false)
            {
                collapseListener.OnGroupCollapsed(listViewAdapter.GetItem(postion));
            }
            else
            {
                collapseListener.OnGroupExpanded(listViewAdapter.GetItem(postion));
            }
        }

        public void OnItemLongClick(int postion, MotionEvent motionEvent)
        {
        }
    }


}