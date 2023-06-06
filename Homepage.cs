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
    [Activity(Label = "NextActivity")]
    public class Homepage : Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Homepage);

            textMessage = FindViewById<TextView>(Resource.Id.message);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);


            // Create your application here
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