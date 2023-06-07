using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;

namespace TicketEase
{
    [Activity(Label = "ViewCinema")]
    public class ViewCinema : Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        Button btn1, btn2, btn3, btn4;
        string cinema = "", seats = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewCinema);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            // Create your application here
            btn1 = FindViewById<Button>(Resource.Id.button1);
            btn2 = FindViewById<Button>(Resource.Id.button2);
            btn3 = FindViewById<Button>(Resource.Id.button3);
            btn4 = FindViewById<Button>(Resource.Id.button4);

            btn1.Click += this.Cinema1;
            //btn2.Click += this.Cinema2;
            //btn3.Click += this.Cinema3;
            //btn4.Click += this.Cinema4;

        }
        public void Cinema1(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(ViewCinemaDetails));
            i.PutExtra("cinema", cinema);
            StartActivity(i);

        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    //textMessage.SetText(Resource.String.title_home);
                    Intent i = new Intent(this, typeof(Homepage));
                    StartActivity(i);
                    return true;
                case Resource.Id.navigation_movies:
                    return true;
                case Resource.Id.navigation_cinema:
                    return true;
                case Resource.Id.navigation_food:
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