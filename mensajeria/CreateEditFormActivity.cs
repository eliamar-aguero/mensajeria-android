
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

namespace mensajeria
{
    [Activity(Label = "CreateEditFormActivity")]
    public class CreateEditFormActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_create_edit_form);

            /**
             * Go to home screen
             */
            Button toMainScreenBtn = FindViewById<Button>(Resource.Id.toMainScreenBtn);

            toMainScreenBtn.Click += delegate
            {
                Intent homeIntent = new Intent(this, typeof(MainActivity));
                StartActivity(homeIntent);
            };
        }
    }
}
