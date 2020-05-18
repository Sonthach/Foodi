using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Foodi.Model;
using Refit;

namespace Foodi
{
    class APIs
    {
        public static string District = "https://thongtindoanhnghiep.co/api/city/4/district";
        public static string City = "https://thongtindoanhnghiep.co/api/city";
    }
}