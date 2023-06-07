using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text.Json.Serialization;

namespace TicketEase
{
    [Activity(Label = "movies")]
    public class movies : Activity
    {
        TextView name, description, price;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.movie1);

            name = FindViewById<TextView>(Resource.Id.textView1);
            description = FindViewById<TextView>(Resource.Id.textView2);
            price = FindViewById<TextView>(Resource.Id.textView3);

            string movieName = Intent.GetStringExtra("movie_name");
            if (!string.IsNullOrEmpty(movieName))
            {
                LoadMovieData(movieName);
            }
            else
            {
                // No movie name provided
                Toast.MakeText(this, "No movie selected", ToastLength.Short).Show();
            }
        }

        private void LoadMovieData(string movieName)
        {
            string url = "http://192.168.100.52/ticketease/rest/Search.php?movie_name=" + movieName;
            using (var webClient = new WebClient())
            {
                try
                {
                    string jsonData = webClient.DownloadString(url);
                    List<Movie> movieData = JsonConvert.DeserializeObject<List<Movie>>(jsonData);

                    if (movieData.Count > 0)
                    {
                        Movie movie = movieData[0]; 
                        name.Text = movie.MovieName;
                        description.Text = movie.MovieDescription;
                        price.Text = movie.MoviePrice.ToString();
                    }
                    else
                    {
                        
                        Toast.MakeText(this, "No movie found with the given name", ToastLength.Short).Show();
                    }
                }
                catch (WebException ex)
                {
                
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }

    public class Movie
    {
        [JsonProperty("movie_name")]
        public string MovieName { get; set; }

        [JsonProperty("movie_description")]
        public string MovieDescription { get; set; }

        [JsonProperty("movie_price")]
        public decimal MoviePrice { get; set; }
    }
}
