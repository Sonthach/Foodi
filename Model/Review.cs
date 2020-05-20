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
    class Review : Java.Lang.Object
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public float Score { get; set; }
        public DateTime Date { get; set; }
        public string Detail { get; set; }

        public override string ToString()
        {
            return Name + " | " + Detail;
        }
    }
}