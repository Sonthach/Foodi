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
using SQLite;

namespace Foodi.Model
{
    [Table("Data")]
    public class DistrictItem
    {
        public int Type { set; get; }
        public string SolrID { set; get; }
        [PrimaryKey]
        public int ID { set; get; }
        public string Title { set; get; }
        public int STT { set; get; }
        public string TinhThanhID { get; set; }
        public string TinhThanhTitle { get; set; }
        public string TinhThanhTitleAscii { get; set; }
        public string Created { set; get; }
        public string Updated { set; get; }
        public int TotalDoanhNghiep { set; get; }
    }
}