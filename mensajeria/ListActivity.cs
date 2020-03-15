using System.Collections.Generic;
using System.Data;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace mensajeria {
    [Activity(Label = "ListActivity")]
    public class ListActivity : Activity {

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_list);

            /**
             * Go to home screen
             */
            Button toMainScreenBtn = FindViewById<Button>(Resource.Id.toMainScreenBtn);
            toMainScreenBtn.Click += delegate {
                Intent homeIntent = new Intent(this, typeof(MainActivity));
                StartActivity(homeIntent);
            };

            /**
             * Fill List View
             */
            ws_mensajeria.somee.com.WebService1 ws = new ws_mensajeria.somee.com.WebService1();
            DataSet ds = ws.GetAllContacts();

            ListView lv = FindViewById<ListView>(Resource.Id.contactListView);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                adapter.Add(ds.Tables[0].Rows[i]["nombre"].ToString());
            }
            
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

            /**
             * Filter contact list
             */
            EditText searchField = FindViewById<EditText>(Resource.Id.searchField);
            searchField.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => adapter.Filter.InvokeFilter(searchField.Text);
        }
    }
}
