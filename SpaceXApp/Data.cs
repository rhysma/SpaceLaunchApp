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

namespace SpaceXApp
{

    public class SpaceLaunchData
    {
        public int total { get; set; }
        public Launch[] launches { get; set; }
        public int offset { get; set; }
        public int count { get; set; }

        public override string ToString()
        {
            return launches.ToString();
        }
    }

    public class Launch
    {
        public int id { get; set; }
        public string name { get; set; }
        public string windowstart { get; set; }
        public string windowend { get; set; }
        public string net { get; set; }
        public int wsstamp { get; set; }
        public int westamp { get; set; }
        public int netstamp { get; set; }
        public string isostart { get; set; }
        public string isoend { get; set; }
        public string isonet { get; set; }
        public int status { get; set; }
        public int inhold { get; set; }
        public int tbdtime { get; set; }
        public string[] vidURLs { get; set; }
        public object vidURL { get; set; }
        public object[] infoURLs { get; set; }
        public object infoURL { get; set; }
        public object holdreason { get; set; }
        public object failreason { get; set; }
        public int tbddate { get; set; }
        public int probability { get; set; }
        public object hashtag { get; set; }
        public Location location { get; set; }
        public Rocket rocket { get; set; }
        public Mission[] missions { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    public class Location
    {
        public Pad[] pads { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string infoURL { get; set; }
        public string wikiURL { get; set; }
        public string countryCode { get; set; }
    }

    public class Pad
    {
        public int id { get; set; }
        public string name { get; set; }
        public string infoURL { get; set; }
        public string wikiURL { get; set; }
        public string mapURL { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public Agency[] agencies { get; set; }
    }

    public class Agency
    {
        public int id { get; set; }
        public string name { get; set; }
        public string abbrev { get; set; }
        public string countryCode { get; set; }
        public int type { get; set; }
        public string infoURL { get; set; }
        public string wikiURL { get; set; }
        public string[] infoURLs { get; set; }
    }

    public class Rocket
    {
        public int id { get; set; }
        public string name { get; set; }
        public string configuration { get; set; }
        public string familyname { get; set; }
        public Agency1[] agencies { get; set; }
        public string wikiURL { get; set; }
        public string[] infoURLs { get; set; }
        public string infoURL { get; set; }
        public int[] imageSizes { get; set; }
        public string imageURL { get; set; }
    }

    public class Agency1
    {
        public int id { get; set; }
        public string name { get; set; }
        public string abbrev { get; set; }
        public string countryCode { get; set; }
        public int type { get; set; }
        public string infoURL { get; set; }
        public string wikiURL { get; set; }
        public string[] infoURLs { get; set; }
    }

    public class Mission
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int type { get; set; }
        public string typeName { get; set; }
    }

}