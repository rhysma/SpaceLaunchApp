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
using Android.Provider;
using Java.Util;

namespace SpaceXApp
{
    [Activity(Label = "LaunchDetailActivity", Theme = "@style/MyTheme")]
    public class LaunchDetailActivity : Activity
    {
        //id for calendar event
        int calID;

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
            TextView url = (TextView)FindViewById(Resource.Id.watchLive);
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
            location.Text += Intent.GetStringExtra("location") ?? "Data not available";
            url.Text += Intent.GetStringExtra("url") ?? "No URL Available";
            var image = GetImageBitmapFromUrl(Intent.GetStringExtra("rocketImg"));
            rocketImg.SetImageBitmap(image);

            //get the button for creating the calendar event
            Button calendarEvent = (Button)FindViewById(Resource.Id.calendarEvent);

            //click action to perform when the button is clicked
            //currently a work in progress
            calendarEvent.Click += (object sender, EventArgs e) =>
            {
                ContentValues eventValues = new ContentValues();
                eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, calID);
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, nameText.Text);
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "Launch Event");
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(2017, 12, 15, 10, 0));
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(2017, 12, 15, 11, 0));

                eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
                eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");
                var uri = ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
                Console.WriteLine("Uri for new event: {0}", uri);
            };

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

        long GetDateTimeMS(int yr, int month, int day, int hr, int min)
        {
            Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);

            c.Set(Calendar.DayOfMonth, 15);
            c.Set(Calendar.HourOfDay, hr);
            c.Set(Calendar.Minute, min);
            c.Set(Calendar.Month, Calendar.December);
            c.Set(Calendar.Year, 2011);

            return c.TimeInMillis;
        }

    }
}