using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace mensajeria {
    [Activity(Label = "CreateEditFormActivity")]
    public class CreateEditFormActivity : Activity {

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_create_edit_form);

            Intent homeIntent = new Intent(this, typeof(MainActivity));

            /**
             * Go to home screen
             */
            FindViewById<Button>(Resource.Id.toMainScreenBtn).Click += delegate {
                StartActivity(homeIntent);
            };
            /**
             * Get all text field from the layout
             */
            EditText etNombre = FindViewById<EditText>(Resource.Id.editTextNombre);
            EditText etOrganizacion = FindViewById<EditText>(Resource.Id.editTextOrganizacion);
            EditText etPuesto = FindViewById<EditText>(Resource.Id.editTextPuesto);
            EditText etArchivarComo= FindViewById<EditText>(Resource.Id.editTextArchivarComo);
            EditText etCorreo = FindViewById<EditText>(Resource.Id.editTextCorreo);
            EditText etMostrarComo = FindViewById<EditText>(Resource.Id.editTextMostrarComo);
            EditText etPaginaWeb = FindViewById<EditText>(Resource.Id.editTextPaginaWeb);
            EditText etDireccionIM = FindViewById<EditText>(Resource.Id.editTextDireccionIM);
            EditText etTelefonoTrabajo = FindViewById<EditText>(Resource.Id.editTextTelefonoTrabajo);
            EditText etTelefonoCasa = FindViewById<EditText>(Resource.Id.editTextTelefonoCasa);
            EditText etFax = FindViewById<EditText>(Resource.Id.editTextFax);
            EditText etCelular = FindViewById<EditText>(Resource.Id.editTextCelular);
            EditText etDireccionTrabajo = FindViewById<EditText>(Resource.Id.editTextDireccionTrabajo);
            EditText etNotas = FindViewById<EditText>(Resource.Id.editTextNotas);

            /**
             * Action to sabe new contact
             */
            FindViewById<Button>(Resource.Id.GuardarBtn).Click += delegate {
                ws_mensajeria.somee.com.WebService1 ws = new ws_mensajeria.somee.com.WebService1();
                ws.CreateContact(
                    etNombre.Text,
                    etOrganizacion.Text,
                    etPuesto.Text,
                    etArchivarComo.Text,
                    etCorreo.Text,
                    etMostrarComo.Text,
                    etPaginaWeb.Text,
                    etDireccionIM.Text,
                    etTelefonoTrabajo.Text,
                    etTelefonoCasa.Text,
                    etFax.Text,
                    etCelular.Text,
                    etDireccionTrabajo.Text,
                    FindViewById<CheckBox>(Resource.Id.checkCorrespondencia).Checked.ToString() == "True" ? 1 : 0,
                    etNotas.Text
                );
                /**
                 * After saving the contacto redirect to the home activity
                 */
                StartActivity(homeIntent);
            };
        }
    }
}
