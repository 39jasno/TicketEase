using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
namespace TicketEase
{
    [Activity(Label = "viewfood")]
    public class viewfood : Activity
    {
        ListView listView;
        HttpWebRequest request;
        HttpWebResponse response;
        string food_name = "", res = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.movie_layout);

            listView = FindViewById<ListView>(Resource.Id.listView);
            listView.ItemClick += MovieItemClick;

            food_name = Intent.GetStringExtra("food_name");
            SearchFoods(food_name);
        }

        private void SearchFoods(string foodName)
        {
            request = (HttpWebRequest)WebRequest.Create("http://192.168.100.52/ticketease/rest/search_food.php?food_name=" + foodName);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;

            List<string> foodNames = new List<string>();
            foreach (JsonElement element in root.EnumerateArray())
            {
                foodName = element.GetProperty("food_name").ToString();
                foodNames.Add(foodName);
            }

            listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, foodNames);
        }

        private void MovieItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var foodName = listView.GetItemAtPosition(e.Position).ToString();
            // Pass the selected movie name to the respective activity based on the movie name
            Intent intent;
            if (foodName == "Pop-Corn")
            {
                intent = new Intent(this, typeof(firstfood));
            }
            else if (foodName == "Coca-Cola")
            {
                intent = new Intent(this, typeof(secondfood));
            }
            else
            {
                // Handle other movies or show an error message
                Toast.MakeText(this, "No activity found for this movie", ToastLength.Short).Show();
                return;
            }

            intent.PutExtra("selected_movie", foodName);
            StartActivity(intent);
        }
    }
}