using System.Data;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
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

            TextView mobilePhone = FindViewById<TextView>(Resource.Id.txtMobileTitle);
            TextView personalPhone = FindViewById<TextView>(Resource.Id.txtPersonalPhoneTitle);
            TextView workPhone = FindViewById<TextView>(Resource.Id.txtWorkPhoneTitle);

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
            mobilePhone.Text = contactInfo.Tables[0].Rows[0]["tel_movil"].ToString();
            FindViewById<TextView>(Resource.Id.txtSMSTitle).Text = contactInfo.Tables[0].Rows[0]["tel_movil"].ToString();
            personalPhone.Text = contactInfo.Tables[0].Rows[0]["tel_particular"].ToString();
            workPhone.Text = contactInfo.Tables[0].Rows[0]["tel_trabajo"].ToString();
            FindViewById<TextView>(Resource.Id.txtEmailTitle).Text = contactInfo.Tables[0].Rows[0]["email"].ToString();
            FindViewById<TextView>(Resource.Id.txtIMTitle).Text = contactInfo.Tables[0].Rows[0]["direccion_im"].ToString();

            /**
             * Call contact
             */
            mobilePhone.Click += delegate {
                callToContactPhone(mobilePhone.Text);
            };
            personalPhone.Click += delegate {
                callToContactPhone(personalPhone.Text);
            };
            workPhone.Click += delegate {
                callToContactPhone(workPhone.Text);
            };

            void callToContactPhone(string phone) {
                Intent call = new Intent(Intent.ActionDial, Uri.Parse("tel:" + phone));
                StartActivity(call);
            }

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
                toEditIntent.PutExtra("name", contactInfo.Tables[0].Rows[0]["nombre"].ToString());
                StartActivity(toEditIntent);
            };
        }
    }
}
