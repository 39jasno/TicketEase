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
    [Activity(Label = "viewmovies")]
    public class viewmovies : Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        ListView listView;
        HttpWebRequest request;
        HttpWebResponse response;
        string movie_name = "", res = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.movie_layout);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            listView = FindViewById<ListView>(Resource.Id.listView);
            listView.ItemClick += MovieItemClick;

            movie_name = Intent.GetStringExtra("movie_name");
            SearchMovies(movie_name);
        }
        private void SearchMovies(string movieName)
        {
            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.50:8080/ticketease/rest/search_record.php?movie_name=" + movieName);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;

            List<string> movieData = new List<string>();
            foreach (JsonElement element in root.EnumerateArray())
            {

                string currentMovieName = element.GetProperty("movie_name").ToString();
                string movieInfo = currentMovieName;
                movieData.Add(movieInfo);
            }

            listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, movieData);
        }

        private void MovieItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string selectedMovieName = listView.GetItemAtPosition(e.Position).ToString();

            Intent intent = new Intent(this, typeof(movies));
            intent.PutExtra("movie_name", selectedMovieName);
            StartActivity(intent);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    //textMessage.SetText(Resource.String.title_home);
                    Intent i = new Intent(this, typeof(Homepage));
                    StartActivity(i);
                    SetContentView(Resource.Layout.Homepage);
                    return true;
                case Resource.Id.navigation_movies:
                    i = new Intent(this, typeof(viewmovies));
                    StartActivity(i);
                    return true;
                case Resource.Id.navigation_cinema:
                    return true;
                case Resource.Id.navigation_food:
                    i = new Intent(this, typeof(viewfood));
                    StartActivity(i);
                    return true;
                case Resource.Id.navigation_signout:
                    i = new Intent(this, typeof(MainActivity));
                    StartActivity(i);
                    return true;
            }
            return false;
        }

    }
}