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
    class FoodiPlace
    {
        public Summary summary { get; set; }
        public List<Results> results { get; set; }
    }
    public class Summary
    {
        public string query { get; set; }
        public string queryType { get; set; }
        public string queryTime { get; set; }
        public string numResults { get; set; }
        public string offset { get; set; }
        public string totalResults { get; set; }
        public string fuzzyLevel { get; set; }
        public Position geoBias { get; set; }

    }
    public class Position
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
    public class Results
    {
        public string type { get; set; }
        public string id { get; set; }
        public string score { get; set; }
        public string dist { get; set; }
        public string info { get; set; }
        public Poi poi { get; set; }
        public Address address { get; set; }
        public Position position { get; set; }
        public ViewPort viewPort { get; set; }
        public List<EntryPoints> entryPoints { get; set; }

        public override string ToString()
        {
            return score + "|" + poi.name + "|" + address.freeformAddress + "|" + position.lat + "|" + position.lon + "|" + info;
        }
    }
    public class Poi
    {
        public string name { get; set; }
        public string phone { get; set; }
        public List<CategorySet> catagorySet { get; set; }
        public string url { get; set; }
        //public List<Categories> categories { get; set; }
        public List<Classifications> classifications { get; set; }
    }
    public class CategorySet
    {
        public string id { get; set; }
    }
    public class Categories
    {
        public string importantTouristAttraction { get; set; }
        public string naturalTouristAttraction { get; set; }
        public string building { get; set; }

    }
    public class Classifications
    {
        public string code { get; set; }
        public List<Names> names { get; set; }
    }
    public class Names
    {
        public string nameLocale { get; set; }
        public string name { get; set; }
    }
    public class Address
    {
        public string streetNumber { get; set; }
        public string streetName { get; set; }
        public string municipalitySubdivision { get; set; }
        public string municipality { get; set; }
        public string countrySecondarySubdivision { get; set; }
        public string countrySubdivision { get; set; }
        public string postalCode { get; set; }
        public string countryCode { get; set; }
        public string country { get; set; }
        public string countryCodeISO3 { get; set; }
        public string freeformAddress { get; set; }
        public string localName { get; set; }
    }
    public class ViewPort
    {
        public Position topLeftPoint { get; set; }
        public Position btmRightPoint { get; set; }
    }
    public class EntryPoints
    {
        public string type { get; set; }
        public Position position { get; set; }
    }
}
