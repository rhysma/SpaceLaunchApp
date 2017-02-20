using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace SpaceXApp
{
    [Activity(Label = "SpaceLaunchApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        public List<Launch> Items { get; set; }

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //remove the action bar from the top
            ActionBar.Hide();

            //initialize the list
            Items = new List<Launch>();

            //setup the client that will get our data
            HttpClient client = new HttpClient();
            
            //this is the url to the API for the next 5 launches
            client.BaseAddress = new Uri("https://launchlibrary.net/1.2/launch/next/10");

            //adding default headers for the request
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            client.DefaultRequestHeaders.UserAgent.ParseAdd("SpaceLaunchApp/1.0");

            //get the data from the API
            await getRestData(client);

            //add the list of items to the list adapter to display them
            ListAdapter = new HomeScreenAdapter(this, Items);
        }


        public async Task<List<Launch>> getRestData(HttpClient client)
        {
            try
            {
                var response = await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<SpaceLaunchData>(content);

                    foreach(var item in items.launches)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Items;
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            //when they click on an item in the list
            //pass over the object data and start the Launch Detail Activity to 
            //display the information
            Launch t = Items[position];
            var launchActivity = new Intent(this, typeof(LaunchDetailActivity));
            launchActivity.PutExtra("name", t.name);
            launchActivity.PutExtra("status", t.status);
            launchActivity.PutExtra("start", t.windowstart);
            launchActivity.PutExtra("end", t.windowend);
            launchActivity.PutExtra("location", t.location.name);
            launchActivity.PutExtra("rocketImg", t.rocket.imageURL);
            StartActivity(launchActivity);
        }
    }
}

