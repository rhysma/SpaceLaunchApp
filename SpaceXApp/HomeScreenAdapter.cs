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
    public class HomeScreenAdapter : BaseAdapter<string>
    {
        List<Launch> items;
        //SpaceLaunchData[] items;
        Activity context;

        public HomeScreenAdapter(Activity context, List<Launch> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override string this[int position]
        {
            get { return items[position].ToString(); }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null)
            { // otherwise create a new one
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position].ToString();
            return view;
        }
    }
}