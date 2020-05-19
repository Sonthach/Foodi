using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Telerik.Widget.Chart.Visualization.Behaviors.Animations;
using Com.Telerik.Widget.List;
using Foodi.Model;

namespace Foodi.Adapter
{
    class FoodiPlaceAdapter : ListViewAdapter
    {
        private FoodiPlacesActivity context;

        public FoodiPlaceAdapter(IList items, FoodiPlacesActivity context)
           : base(items)
        {
            this.context = context;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View view = inflater.Inflate(Resource.Layout.item_foodi_places, parent, false);
            return new FoodiPlaceViewHolder(view);
        }

        public override void OnBindListViewHolder(ListViewHolder holder, int position)
        {
            FoodiPlaceViewHolder viewHolder = (FoodiPlaceViewHolder)holder;
            Results touristPlace = (Results)Items[position];
            viewHolder.txtNameFoodiPlaces.Text = touristPlace.poi.name;
            //Glide.With(context).Load(int.Parse(touristPlace.info)).Into(viewHolder.image);
        }

        public class FoodiPlaceViewHolder : ListViewHolder
        {
            public TextView txtNameFoodiPlaces;
            public ImageView image;

            public FoodiPlaceViewHolder(View itemView)
                : base(itemView)
            {
                txtNameFoodiPlaces = (TextView)itemView.FindViewById(Resource.Id.txtNameFoodiPlaces);
                //image = (ImageView)itemView.FindViewById(Resource.Id.placeImage);
            }
        }

        public class FoodiPlacesClickListenner : Java.Lang.Object, RadListView.IItemClickListener
        {
            private FoodiPlacesActivity context;
            private ListViewAdapter listViewAdapter;

            public FoodiPlacesClickListenner(FoodiPlacesActivity context, ListViewAdapter listViewAdapter)
            {
                this.context = context;
                this.listViewAdapter = listViewAdapter;
            }
            public void OnItemClick(int postion, MotionEvent motionEvent)
            {
                var items = listViewAdapter.GetItem(postion).ToString().Split("|");
                context.ShowPlaceInfo(context,items);
                context.GetPlaceInfo(items[1], items[3].Replace(",", "."), items[4].Replace(",", "."));
                context.FocusToCity(BitmapDescriptorFactory.HueCyan, 15);
            }

            public void OnItemLongClick(int postion, MotionEvent motionEvent)
            {
                var items = listViewAdapter.GetItem(postion).ToString().Split("|");
                context.ShowPlaceInfo(context, items);
                context.GetPlaceInfo(items[1], items[3].Replace(",", "."), items[4].Replace(",", "."));
                context.FocusToCity(BitmapDescriptorFactory.HueCyan, 15);
            }
        }
    }
}