using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Xamarin.Essentials;
using Uri = Android.Net.Uri;

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
            TextView smsPhone = FindViewById<TextView>(Resource.Id.txtSMSTitle);
            TextView email = FindViewById<TextView>(Resource.Id.txtEmailTitle);

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
            smsPhone.Text = contactInfo.Tables[0].Rows[0]["tel_movil"].ToString();
            personalPhone.Text = contactInfo.Tables[0].Rows[0]["tel_particular"].ToString();
            workPhone.Text = contactInfo.Tables[0].Rows[0]["tel_trabajo"].ToString();
            email.Text = contactInfo.Tables[0].Rows[0]["email"].ToString();
            FindViewById<TextView>(Resource.Id.txtIMTitle).Text = contactInfo.Tables[0].Rows[0]["direccion_im"].ToString();

            /**
             * Call contact
             */
            mobilePhone.Click += (sender, e) => callToContactPhone(mobilePhone.Text);
            personalPhone.Click += (sender, e) => callToContactPhone(personalPhone.Text);
            workPhone.Click += (sender, e) => callToContactPhone(workPhone.Text);

            void callToContactPhone(string phone) {
                Intent call = new Intent(Intent.ActionDial, Uri.Parse("tel:" + phone));
                StartActivity(call);
            }

            /**
             * Send SMS
             */
            smsPhone.Click += async (sender, e) => await SendSMS(smsPhone.Text);

            /**
             * Send email
             */
            email.Click += async (sender, e) => await SendEmail(new List<string>() { email.Text });


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

        /**
         * Open the SMS app
         */
        private async Task SendSMS(string to) {
            try {
                string msj = "";
                var sms = new SmsMessage(msj, new string[] { to });
                await Sms.ComposeAsync(sms);
            } catch (Exception) { }
        }

        /**
         * Open the email client app
         */
        private async Task SendEmail(List<string> to) {
            try {
                var message = new EmailMessage {
                    Subject = "Email de contacto",
                    Body = "Email de prueba",
                    To = to
                };
                await Email.ComposeAsync(message);
            } catch (Exception) { }
        }
    }
}
