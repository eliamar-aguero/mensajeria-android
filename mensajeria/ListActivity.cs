
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
    [Activity(Label = "ListActivity")]
    public class ListActivity : Activity
    {
        private readonly string[] contactsList = { "person A", "Person B", "Person C", "Person D" };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_list);

            /**
             * Go to home screen
             */
            Button toMainScreenBtn = FindViewById<Button>(Resource.Id.toMainScreenBtn);

            toMainScreenBtn.Click += delegate
            {
                Intent homeIntent = new Intent(this, typeof(MainActivity));
                StartActivity(homeIntent);
            };

            /**
             * Fill List View
             */
            ListView lv = FindViewById<ListView>(Resource.Id.contactListView);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, contactsList);
            lv.Adapter = adapter;

            /**
             * List item click action
             */
            lv.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                string selected = lv.GetItemAtPosition(e.Position).ToString();
                Intent toDetailIntent = new Intent(this, typeof(ContactDetailActivity));
                toDetailIntent.PutExtra("id", selected);
                StartActivity(toDetailIntent);
            };
        }
    }
}
