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

namespace TicketEase
{
    [Activity(Label = "ViewCinemaDetails")]
    public class ViewCinemaDetails : Activity
    {
        ImageView imgView;
        TextView textView1, textView2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Cinema);

            // Create your application here

            imgView = FindViewById<ImageView>(Resource.Id.imageView1);
            textView1 = FindViewById<TextView>(Resource.Id.textView1);
            textView2 = FindViewById<TextView>(Resource.Id.textView2);

        }
    }
}