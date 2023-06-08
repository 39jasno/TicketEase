using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Google.Android.Material.BottomNavigation;


namespace TicketEase
{
    [Activity(Label = "foods")]
    public class foods : Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView name, price;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.food1);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            name = FindViewById<TextView>(Resource.Id.textView1);
            price = FindViewById<TextView>(Resource.Id.textView2);

            string foodName = Intent.GetStringExtra("food_name");
            if (!string.IsNullOrEmpty(foodName))
            {
                LoadFoodData(foodName);
            }
            else
            {
                Toast.MakeText(this, "No food selected", ToastLength.Short).Show();
            }
        }

        private void LoadFoodData(string foodName)
        {
            string url = "http://192.168.1.50:8080/ticketease/rest/search_food.php?food_name=" + foodName;
            using (var webClient = new WebClient())
            {
                try
                {
                    string jsonData = webClient.DownloadString(url);
                    List<Food> foodData = JsonConvert.DeserializeObject<List<Food>>(jsonData);

                    if (foodData.Count > 0)
                    {
                        Food food = foodData[0];
                        name.Text = food.FoodName;
                        price.Text = food.FoodPrice.ToString();
                    }
                    else
                    {
                        Toast.MakeText(this, "No food found with the given name", ToastLength.Short).Show();
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
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

    public class Food
    {
        [JsonProperty("food_name")]
        public string FoodName { get; set; }

        [JsonProperty("food_price")]
        public decimal FoodPrice { get; set; }
    }
}