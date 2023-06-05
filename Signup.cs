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
        EditText newfname, newlname, newbdate, newnumber, newemail, newpass;
        RadioGroup gender;
        Button signup;
        HttpWebResponse response;
        HttpWebRequest request;
        String user_fname = "", user_lname = "", user_bdate = "", user_number = "", user_email = "", user_pass = "", selectedGender = "", res = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignupUI);

            // Create your application here

            //instantiate widgets
            newfname = FindViewById<EditText>(Resource.Id.editText1);
            newlname = FindViewById<EditText>(Resource.Id.editText2);
            newbdate = FindViewById<EditText>(Resource.Id.editText3);
            gender = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            gender.CheckedChange += myRadioGroup_CheckedChange;
            newnumber = FindViewById<EditText>(Resource.Id.editText4);
            newemail = FindViewById<EditText>(Resource.Id.editText5);
            newpass = FindViewById<EditText>(Resource.Id.editText6);
            signup = FindViewById<Button>(Resource.Id.button1);


            signup.Click += this.AddUser;



        }
        void myRadioGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            int checkedItemId = gender.CheckedRadioButtonId;
            RadioButton checkedRadioButton = FindViewById<RadioButton>(checkedItemId);
            selectedGender = checkedItemId.ToString();
            gender.Check(checkedItemId);


        }
        public void AddUser(object sender, EventArgs e)
        {
            user_fname = newfname.Text;
            user_lname = newlname.Text;
            user_bdate = newbdate.Text;
            user_number = newnumber.Text;
            user_email = newemail.Text;
            user_pass = newpass.Text;

            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.50:8080/ticketease/rest/add_record.php?user_fname=" + user_fname + "&user_lname=" + user_lname + "&user_gender=" + gender + "&user_bdate=" + user_bdate + "&user_number=" + user_number + "&user_email=" + user_email + "&user_pass=" + user_pass);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();
            Toast.MakeText(this, res, ToastLength.Long).Show();

        }
    }
}