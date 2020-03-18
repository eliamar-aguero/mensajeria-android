
using System;
using System.Collections.Generic;
using System.Data;
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
             * Get back to the list activity
             */
            Intent toContactListIntent = new Intent(this, typeof(ListActivity));
            FindViewById<Button>(Resource.Id.backToContactListBtn).Click += delegate {
                StartActivity(toContactListIntent);
            };

            /**
             * Get data from the selected contact on the activity list and pass it to the web service
             * Get the selected contact info by name from the DB to display data in the contact details view
             */
            string selectedContact = Intent.GetStringExtra("name");
            ws_mensajeria.somee.com.WebService1 ws = new ws_mensajeria.somee.com.WebService1();
            DataSet contactInfo = new DataSet();

            contactInfo = ws.GetSingleContact(selectedContact);
            FindViewById<TextView>(Resource.Id.txtName).Text = contactInfo.Tables[0].Rows[0]["nombre"].ToString();
            FindViewById<TextView>(Resource.Id.txtMobileTitle).Text = contactInfo.Tables[0].Rows[0]["tel_movil"].ToString();
            FindViewById<TextView>(Resource.Id.txtSMSTitle).Text = contactInfo.Tables[0].Rows[0]["tel_movil"].ToString();
            FindViewById<TextView>(Resource.Id.txtPersonalPhoneTitle).Text = contactInfo.Tables[0].Rows[0]["tel_particular"].ToString();
            FindViewById<TextView>(Resource.Id.txtWorkPhoneTitle).Text = contactInfo.Tables[0].Rows[0]["tel_trabajo"].ToString();
            FindViewById<TextView>(Resource.Id.txtEmailTitle).Text = contactInfo.Tables[0].Rows[0]["email"].ToString();
            FindViewById<TextView>(Resource.Id.txtIMTitle).Text = contactInfo.Tables[0].Rows[0]["direccion_im"].ToString();

            /**
             * Attempt to delete the contact
             */
            Button deleteBtn = FindViewById<Button>(Resource.Id.deleteContactBtn);
            deleteBtn.Click += delegate {
                var deleteDialog = new AlertDialog.Builder(this);
                deleteDialog.SetMessage("Eliminar " + selectedContact + "?");
                deleteDialog.SetNegativeButton("Cancelar", delegate { });
                deleteDialog.SetPositiveButton("Eliminar", delegate {
                    ws_mensajeria.somee.com.WebService1 ws = new ws_mensajeria.somee.com.WebService1();
                    int id = int.Parse(contactInfo.Tables[0].Rows[0]["id"].ToString());
                    ws.DeleteContact(id);
                    StartActivity(toContactListIntent);
                });
                deleteDialog.Show();
        	};

            /**
             * Action to go to the Edit screen
             */
            FindViewById<Button>(Resource.Id.btnEdit).Click += delegate {
                Intent toEditIntent = new Intent(this, typeof(CreateEditFormActivity));
                StartActivity(toEditIntent);
            };
        }
    }
}
