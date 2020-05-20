using System;
using System.Collections;
using System.Collections.Generic;
using Android.Graphics;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Syncfusion.Rating;
using Com.Telerik.Widget.List;
using Foodi.Model;

namespace Foodi.Adapter
{
    class ReviewAdapter : ListViewAdapter
    {
        private FoodiPlacesActivity activity;

        public ReviewAdapter(IList items, FoodiPlacesActivity activity)
            :base(items)
        {
            this.activity = activity;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View view = inflater.Inflate(Resource.Layout.item_reviews, parent, false);
            return new FoodiPlaceViewHolder(view);
        }

        public override void OnBindListViewHolder(ListViewHolder holder, int position)
        {
            FoodiPlaceViewHolder viewHolder = (FoodiPlaceViewHolder)holder;
            Review review = (Review)Items[position];
            viewHolder.Name.Text = review.Name;
            viewHolder.Date.Text = review.Date.ToShortDateString();
            viewHolder.Detail.Text = review.Detail;

            viewHolder.Score.Precision = Precision.Exact;
            viewHolder.Score.ReadOnly = true;
            viewHolder.Score.ItemSize = 10;
            viewHolder.Score.ItemSpacing = 4;
            viewHolder.Score.Value = review.Score;
            SfRatingSettings ratingSettings = new SfRatingSettings
            {
                RatedFill = Color.Yellow,
                UnRatedFill = Color.Gray,
                RatedStroke = Color.DarkOrange,
                UnRatedStroke = Color.DarkGray
            };
            viewHolder.Score.RatingSettings = ratingSettings;
        }

        public class FoodiPlaceViewHolder : ListViewHolder
        {
            public TextView Name;
            public ImageView Avatar;
            public SfRating Score;
            public TextView Date;
            public TextView Detail;

            public FoodiPlaceViewHolder(View itemView)
                : base(itemView)
            {
                Name = itemView.FindViewById<TextView>(Resource.Id.txtReviewsName);
                Avatar = itemView.FindViewById<ImageView>(Resource.Id.imvReviews);
                Score = itemView.FindViewById<SfRating>(Resource.Id.sfRatingReviews);
                Date = itemView.FindViewById<TextView>(Resource.Id.txtReviewsDate);
                Detail = itemView.FindViewById<TextView>(Resource.Id.txtReviesDetail);
            }
        }

    }
}