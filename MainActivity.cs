﻿using System;
using System.IO;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace TicketEase
{
    [Activity(Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText edit1, edit2;
        Button btn1, btn2;
        HttpWebResponse response;
        HttpWebRequest request;
        string res = "", user_username="", user_email = "", user_pass = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.LoginUI);

            edit1 = FindViewById<EditText>(Resource.Id.editUser);
            edit2 = FindViewById<EditText>(Resource.Id.editPass);
            btn1 = FindViewById<Button>(Resource.Id.btnLogin);
            btn2 = FindViewById<Button>(Resource.Id.btnSignup);

            btn1.Click += this.Login;
            btn2.Click += this.Signup;
        }
        public void Login(object sender, EventArgs e)
        {
            user_username = edit1.Text;
            user_pass = edit2.Text;

            if (string.IsNullOrEmpty(user_username) || string.IsNullOrEmpty(user_pass))
            {
                Toast.MakeText(this, "Please enter both username and password", ToastLength.Short).Show();
                return;
            }

            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.50:8080/ticketease/rest/login.php?user_username=" + user_username + "&user_pass=" + user_pass);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();
            Toast.MakeText(this, res, ToastLength.Short).Show();

            if (res.Contains("Login Successfuly"))
            {
                Intent i = new Intent(this, typeof(Homepage));
                i.PutExtra("Name", user_username);
                StartActivity(i);
            }
        }

        public void Signup(object sender, EventArgs eventArgs)
        {
            Intent i = new Intent(this, typeof(Signup));
            StartActivity(i);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}