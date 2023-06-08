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
    [Activity(Label = "cinema")]
    public class cinema : Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView name, seat;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.cinema1);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            name = FindViewById<TextView>(Resource.Id.textView2);
            seat = FindViewById<TextView>(Resource.Id.textView4);

            string cinemaName = Intent.GetStringExtra("cinema_name");
            if (!string.IsNullOrEmpty(cinemaName))
            {
                LoadcinemaData(cinemaName);
            }
            else
            {
                Toast.MakeText(this, "No cinema selected", ToastLength.Short).Show();
            }
        }

        private void LoadcinemaData(string cinemaName)
        {
            string url = "http://192.168.100.52/ticketease/rest/view_cinema.php?cinema_name=" + cinemaName;
            using (var webClient = new WebClient())
            {
                try
                {
                    string jsonData = webClient.DownloadString(url);
                    List<Cinema> cinemaData = JsonConvert.DeserializeObject<List<Cinema>>(jsonData);

                    if (cinemaData.Count > 0)
                    {
                        Cinema cinema = cinemaData[0];
                        name.Text = cinema.cinemaName;
                        seat.Text = cinema.cinemaSeat.ToString();
                    }
                    else
                    {
                        Toast.MakeText(this, "No cinema found with the given name", ToastLength.Short).Show();
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
                    i = new Intent(this, typeof(ViewCinema));
                    StartActivity(i);
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

    public class Cinema
    {
        [JsonProperty("cinema_name")]
        public string cinemaName { get; set; }

        [JsonProperty("cinema_seats")]
        public decimal cinemaSeat { get; set; }
    }
}
