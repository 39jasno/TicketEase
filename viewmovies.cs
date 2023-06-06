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
    [Activity(Label = "viewmovies")]
    public class viewmovies : Activity
    {
        ListView listView;
        HttpWebRequest request;
        HttpWebResponse response;
        string movie_name = "", res = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.movie_layout);

            listView = FindViewById<ListView>(Resource.Id.listView);
            listView.ItemClick += MovieItemClick;

            movie_name = Intent.GetStringExtra("movie_name");
            SearchMovies(movie_name);
        }

        private void SearchMovies(string movieName)
        {
            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.50/ticketease/rest/search_record.php?movie_name=" + movieName);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;

            List<string> movieNames = new List<string>();
            foreach (JsonElement element in root.EnumerateArray())
            {
                movieName = element.GetProperty("movie_name").ToString();
                movieNames.Add(movieName);
            }

            listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, movieNames);
        }

        private void MovieItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var movieName = listView.GetItemAtPosition(e.Position).ToString();
            // Pass the selected movie name to the respective activity based on the movie name
            Intent intent;
            if (movieName == "Spider-Man")
            {
                intent = new Intent(this, typeof(firstmovie));
            }
            else if (movieName == "The Monster")
            {
                intent = new Intent(this, typeof(secondmovie));
            }
            else
            {
                // Handle other movies or show an error message
                Toast.MakeText(this, "No activity found for this movie", ToastLength.Short).Show();
                return;
            }

            intent.PutExtra("selected_movie", movieName);
            StartActivity(intent);
        }
        
        
    }
}
