using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TicketEase
{
    [Activity(Label = "Signup")]
    public class Signup : Activity
    {
        EditText newfname, newlname, newmonth, newday, newyear, newnumber, newemail, newuser, newpass;
        RadioGroup gender;
        Button signup;
        HttpWebResponse response;
        HttpWebRequest request;
        String user_fname = "", user_lname = "", user_gender ="", user_month = "", user_day = "", user_year = "", user_number = "", user_username="", user_email = "", user_pass = "", selectedGender = "", res = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignupUI);

            // Create your application here

            //instantiate widgets
            newfname = FindViewById<EditText>(Resource.Id.editText1);
            newlname = FindViewById<EditText>(Resource.Id.editText2);
            gender = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            gender.CheckedChange += myRadioGroup_CheckedChange;
            newmonth = FindViewById<EditText>(Resource.Id.editText3);
            newday = FindViewById<EditText>(Resource.Id.editText4);
            newyear = FindViewById<EditText>(Resource.Id.editText5);
            newnumber = FindViewById<EditText>(Resource.Id.editText6);
            newemail = FindViewById<EditText>(Resource.Id.editText7);
            newuser = FindViewById<EditText>(Resource.Id.editText8);
            newpass = FindViewById<EditText>(Resource.Id.editText9);

            signup = FindViewById<Button>(Resource.Id.button1);


            signup.Click += this.AddUser;



        }
        void myRadioGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            int checkedItemId = gender.CheckedRadioButtonId;
            RadioButton checkedRadioButton = FindViewById<RadioButton>(checkedItemId);
            if (checkedRadioButton != null)
            {
                // Assign the appropriate value to selectedGender based on the selected RadioButton
                if (checkedRadioButton.Text == "Male")
                {
                    selectedGender = "Male";
                }
                else if (checkedRadioButton.Text == "Female")
                {
                    selectedGender = "Female";
                }
            }
            //selectedGender = checkedItemId.ToString();
            gender.Check(checkedItemId);


        }
        public void AddUser(object sender, EventArgs e)
        {
            user_fname = newfname.Text;
            user_lname = newlname.Text;
            user_gender = selectedGender;
            user_month = newmonth.Text;
            user_day = newday.Text;
            user_year = newyear.Text;
            user_number = newnumber.Text;
            user_email = newemail.Text;
            user_username = newuser.Text;
            user_pass = newpass.Text;

            //input validation, not null
            if (string.IsNullOrEmpty(user_fname) || string.IsNullOrEmpty(user_lname) ||
                string.IsNullOrEmpty(user_gender) || string.IsNullOrEmpty(user_month) ||
                string.IsNullOrEmpty(user_day) || string.IsNullOrEmpty(user_year) ||
                string.IsNullOrEmpty(user_number) || string.IsNullOrEmpty(user_email) ||
                string.IsNullOrEmpty(user_username) || string.IsNullOrEmpty(user_pass))
            {
                Toast.MakeText(this, "Please fill in all fields", ToastLength.Long).Show();
                return;
            }
            //month validation
            int month, day, year;
            if (!int.TryParse(user_month, out month) || !int.TryParse(user_day, out day) || !int.TryParse(user_year, out year))
            {
                Toast.MakeText(this, "Invalid date format", ToastLength.Long).Show();
                return;
            }

            //month validation
            if (month < 1 || month > 12 || 
                day < 1 || day > 31 || 
                year < 1900 || year > DateTime.Now.Year)
            {
                Toast.MakeText(this, "Invalid date input", ToastLength.Long).Show();
                return;
            }
            //email validation
            if (!user_email.EndsWith("@gmail.com") && !user_email.EndsWith("@yahoo.com"))
            {
                Toast.MakeText(this, "Invalid email address", ToastLength.Long).Show();
                return;
            }

            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.50:8080/ticketease/rest/signup.php?user_fname=" + user_fname + "&user_lname=" + user_lname + "&user_gender=" + user_gender + "&user_month=" + user_month + "&user_day=" + user_day + "&user_year=" + user_year + "&user_number=" + user_number + "&user_email=" + user_email + "&user_username=" +user_username + "&user_pass=" + user_pass);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();
            Toast.MakeText(this, res, ToastLength.Long).Show();
            SetContentView(Resource.Layout.LoginUI);

        }
    }
}