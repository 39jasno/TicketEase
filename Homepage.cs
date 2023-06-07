using System;
using System.IO;
using System.Net;
using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;

namespace TicketEase
{
    [Activity(Label = "NextActivity")]
    public class Homepage : Activity
    {
        Button btn1,btn2,btn3,btn4;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Homepage);

            btn1 = FindViewById<Button>(Resource.Id.button1);
            btn2 = FindViewById<Button>(Resource.Id.button2);
            btn3 = FindViewById<Button>(Resource.Id.button3);
            btn4 = FindViewById<Button>(Resource.Id.button4);

            //btn1.Click += this.ViewMovies;
            btn2.Click += this.ViewCinema;
            //btn3.Click += this.ViewFood;
            //btn4.Click += this.ViewAccount;

        }
        public void ViewCinema(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.ViewCinema);

        }

    }

}