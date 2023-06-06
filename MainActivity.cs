using System;
using System.IO;
using System.Net;
using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using Java.Security;

namespace TicketEase
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView textMessage;
        TextView text1;
        EditText edit1, edit2;
        Button btn1, btn2;
        HttpWebResponse response;
        HttpWebRequest request;
        string res = "", str = "", user_email = "", user_pass = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.LoginUI);

            text1 = FindViewById<TextView>(Resource.Id.textView1);
            edit1 = FindViewById<EditText>(Resource.Id.edituser);
            edit2 = FindViewById<EditText>(Resource.Id.editpass);
            btn1 = FindViewById<Button>(Resource.Id.btnlogin);
            btn2 = FindViewById<Button>(Resource.Id.btnsignup);

            btn1.Click += this.Login;
            btn2.Click += this.Signup;
        }
        public void Login(object sender, EventArgs e)
        {
            user_email = edit1.Text;
            user_pass = edit2.Text;
            //192.168.1.32
            //192.168.1.50
            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.32:8080/ticketease/rest/admin_login.php?user_email=" + user_email + "&user_pass=" + user_pass);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();
            Toast.MakeText(this, res, ToastLength.Long).Show();

            if (res.Contains("Login Successfuly"))
            {
                Intent i = new Intent(this, typeof(Homepage));
                i.PutExtra("Name", user_email);
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

//pullmotojustin