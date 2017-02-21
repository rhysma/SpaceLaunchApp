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
using Android.Graphics;
using System.Net;

namespace SpaceXApp
{
    [Activity(Label = "LaunchDetailActivity", Theme = "@style/MyTheme")]
    public class LaunchDetailActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LaunchDetail);

            //get the controls we want to put information into
            TextView nameText = (TextView)FindViewById(Resource.Id.name);
            TextView statusText = (TextView)FindViewById(Resource.Id.status);
            TextView startText = (TextView)FindViewById(Resource.Id.windowStart);
            TextView endText = (TextView)FindViewById(Resource.Id.windowEnd);
            TextView location = (TextView)FindViewById(Resource.Id.location);
            ImageView rocketImg = (ImageView)FindViewById(Resource.Id.rocketImg);

            //get the information that was passed over
            nameText.Text += Intent.GetStringExtra("name") ?? "Data not available";
            int status = Intent.GetIntExtra("status", 0);
            switch(status)
            {
                case 0:
                statusText.Text = "Data not available";
                    break;
                case 1:
                    statusText.Text += "Green";
                    statusText.SetTextColor(Android.Graphics.Color.Green);
                    break;
                case 2:
                    statusText.Text += "Red";
                    statusText.SetTextColor(Android.Graphics.Color.Red);
                    break;
                case 3:
                    statusText.Text += "Success";
                    statusText.SetTextColor(Android.Graphics.Color.Green);
                    break;
                case 4:
                    statusText.Text += "Failure";
                    statusText.SetTextColor(Android.Graphics.Color.Red);
                    break;
            }
            startText.Text += Intent.GetStringExtra("start") ?? "Data not available";
            endText.Text += Intent.GetStringExtra("end") ?? "Data not available";
            location.Text = Intent.GetStringExtra("location") ?? "Data not available";
            var image = GetImageBitmapFromUrl(Intent.GetStringExtra("rocketImg"));
            rocketImg.SetImageBitmap(image);
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}