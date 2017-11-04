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
    [Activity(Label = "Space Launch App", Icon = "@drawable/splash_logo", Theme = "@style/MyTheme")]
    public class MainActivity : ListActivity
    {
        public List<Launch> Items { get; set; }

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Toast toast = Toast.MakeText(this, String.Format("Getting Next 10 Launches..."), ToastLength.Short);
            toast.Show();

            //initialize the lists
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


        /// <summary>
        ///get the list of launch items and agencies from the API and add them to the list
        ///we will use in the UI
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
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

        /// <summary>
        /// called when the user clicks on an item in the listivew
        /// </summary>
        /// <param name="l"></param>
        /// <param name="v"></param>
        /// <param name="position"></param>
        /// <param name="id"></param>
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            //when they click on an item in the list
            //pass over the object data and start the LaunchDetailActivity to 
            //display the information
            Launch t = Items[position];
            var launchActivity = new Intent(this, typeof(LaunchDetailActivity));
            launchActivity.PutExtra("name", t.name);
            launchActivity.PutExtra("status", t.status);
            launchActivity.PutExtra("start", t.windowstart);
            launchActivity.PutExtra("end", t.windowend);
            launchActivity.PutExtra("location", t.location.name);
            launchActivity.PutExtra("url", t.vidURLs[0]);
            launchActivity.PutExtra("rocketImg", t.rocket.imageURL);
            StartActivity(launchActivity);
        }
    }
}

