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
using Google.Android.Material.BottomNavigation;


namespace TicketEase
{
    [Activity(Label = "viewfood")]
    public class viewfood : Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        ListView listView;
        HttpWebRequest request;
        HttpWebResponse response;
        string food_name = "", res = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.movie_layout);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            listView = FindViewById<ListView>(Resource.Id.listView);
            listView.ItemClick += MovieItemClick;

            food_name = Intent.GetStringExtra("food_name");
            SearchFoods(food_name);
        }

        private void SearchFoods(string foodName)
        {
            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.50:8080/ticketease/rest/search_food.php?food_name=" + foodName);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;

            List<string> foodData = new List<string>();
            foreach (JsonElement element in root.EnumerateArray())
            {

                string foodMovieName = element.GetProperty("food_name").ToString();
                string foodInfo = foodMovieName;
                foodData.Add(foodInfo);
            }

            listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, foodData);
        }

        private void MovieItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string foodMoName = listView.GetItemAtPosition(e.Position).ToString();

            Intent intent = new Intent(this, typeof(foods));
            intent.PutExtra("food_name", foodMoName);
            StartActivity(intent);


        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    //textMessage.SetText(Resource.String.title_home);
                    SetContentView(Resource.Layout.Homepage);
                    return true;
                case Resource.Id.navigation_movies:
                    Intent i = new Intent(this, typeof(viewmovies));
                    StartActivity(i);
                    return true;
                case Resource.Id.navigation_cinema:
                    return true;
                case Resource.Id.navigation_food:
                    i = new Intent(this, typeof(viewfood));
                    StartActivity(i);
                    return true;
                case Resource.Id.navigation_signout:
                    SetContentView(Resource.Layout.LoginUI);
                    return true;
            }
            return false;
        }
    }
}