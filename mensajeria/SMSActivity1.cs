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
using Plugin.Messaging;

namespace mensajeria
{
    [Activity(Label = "SMSActivity1")]
    public class SMSActivity1 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_SMS);
            // Create your application here

            string selectedContact = Intent.GetStringExtra("name");
            ws_mensajeria.somee.com.WebService1 ws = new ws_mensajeria.somee.com.WebService1();
            DataSet contactInfo = new DataSet();
            contactInfo = ws.GetSingleContact(selectedContact);


            Button SendSMS = FindViewById<Button>(Resource.Id.SMSBtn);

            FindViewById<TextView>(Resource.Id.ContactSendSMS).Text = contactInfo.Tables[0].Rows[0]["nombre"].ToString();
            FindViewById<TextView>(Resource.Id.NumberSendSMS).Text = contactInfo.Tables[0].Rows[0]["tel_movil"].ToString();

           
            SendSMS.Click += SendSMS_Click;
        }

        private void SendSMS_Click(object sender, EventArgs e)
        {
            var smsMessenger =
                CrossMessaging.Current.SmsMessenger;
            if (smsMessenger.CanSendSms)
                smsMessenger.SendSms("+50687360481", "Prueba SMS");
            
        }
    }
}