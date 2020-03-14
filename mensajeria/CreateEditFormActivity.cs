using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

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
