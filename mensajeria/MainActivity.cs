using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace mensajeria
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon ="@drawable/ContactManagement")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            /**
             * Actions for the buttons on the main screen
             */
            Button toAddNewBtn = FindViewById<Button>(Resource.Id.toAddNewBtn);
            Button toContactsListBtn = FindViewById<Button>(Resource.Id.toContactsListBtn);

            toAddNewBtn.Click += delegate {
                Intent createEditFormIntent = new Intent(this, typeof(CreateEditFormActivity));
                StartActivity(createEditFormIntent);
            };
            toContactsListBtn.Click += delegate {
                Intent listIntent = new Intent(this, typeof(ListActivity));
                StartActivity(listIntent);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}