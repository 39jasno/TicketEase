using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using static Android.Graphics.Paint;
using Google.Android.Material.BottomNavigation;


namespace TicketEase
{
    [Activity(Label = "ViewCinema")]
    public class ViewCinema: Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        ListView listView;
        HttpWebRequest request;
        HttpWebResponse response;
        string cinema_name = "", res = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Cinema);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            listView = FindViewById<ListView>(Resource.Id.listView10);
            listView.ItemClick += CinemaItemClick;

            cinema_name = Intent.GetStringExtra("cinema_name");
            SearchCinema(cinema_name);
        }

        private void SearchCinema(string cinemaName)
        {
            request = (HttpWebRequest)WebRequest.Create("http://192.168.100.52/ticketease/rest/view_cinema.php?cinema_name=" + cinemaName);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var check = reader.ReadToEnd();
            using JsonDocument doc = JsonDocument.Parse(check);
            JsonElement root = doc.RootElement;

            List<string> cinemaData = new List<string>();
            foreach (JsonElement element in root.EnumerateArray())
            {
                string currentCinemaName = element.GetProperty("cinema_name").ToString();
                string cinemaInfo = currentCinemaName;
                cinemaData.Add(cinemaInfo);
            }

            listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, cinemaData);
        }

        private void CinemaItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string selectedCinemaName = listView.GetItemAtPosition(e.Position).ToString();

            Intent intent = new Intent(this, typeof(cinema));
            intent.PutExtra("cinema_name", selectedCinemaName);
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
}
