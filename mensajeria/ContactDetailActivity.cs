
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
    [Activity(Label = "ContactDetailActivity")]
    public class ContactDetailActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_contact_detail);

            /**
             * Get data from the list
             */
            string selectedContact = Intent.GetStringExtra("id");
            TextView tv = FindViewById<TextView>(Resource.Id.textView1);
            tv.Text = selectedContact;

            /**
             * Attempt to delete the contact
             */
            Button deleteBtn = FindViewById<Button>(Resource.Id.deleteContactBtn);
            deleteBtn.Click += delegate {
                var deleteDialog = new AlertDialog.Builder(this);
                deleteDialog.SetMessage("Eliminar " + selectedContact + "?");
                deleteDialog.SetNegativeButton("Cancelar", delegate { });
                deleteDialog.SetPositiveButton("Eliminar", delegate { });
                deleteDialog.Show();
        	};

            /**
             * Get back to the list activity
             */
            Button backToListBtn = FindViewById<Button>(Resource.Id.backToContactListBtn);
            backToListBtn.Click += delegate {
                Intent toContactListIntent = new Intent(this, typeof(ListActivity));
                StartActivity(toContactListIntent);
        	};
        }
    }
}
