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

namespace Foodi.Model
{
    class District : Java.Lang.Object
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string LatLon { set; get; }
        public int Image { set; get; }
        public string Area { set; get; }

        public override string ToString()
        {
            return ID + "|" + Name + "|" + LatLon + "|" + Image;
        }
    }
}