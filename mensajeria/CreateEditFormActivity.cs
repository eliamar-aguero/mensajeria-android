using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Data;
using System;
using Android.Provider;

namespace mensajeria {
    [Activity(Label = "CreateEditFormActivity")]
    public class CreateEditFormActivity : Activity {

        Intent homeIntent;

        EditText etNombre;
        EditText etOrganizacion;
        EditText etPuesto;
        EditText etArchivarComo;
        EditText etCorreo;
        EditText etMostrarComo;
        EditText etPaginaWeb;
        EditText etDireccionIM;
        EditText etTelefonoTrabajo;
        EditText etTelefonoCasa;
        EditText etFax;
        EditText etCelular;
        EditText etDireccionTrabajo;
        EditText etNotas;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_create_edit_form);

            homeIntent = new Intent(this, typeof(MainActivity));

            /**
             * Go to home screen
             */
            // TODO: validate if is comming from the contact detail to edit the contact or is the create form
            // TODO: the back route should be either the home screen or the details screen base on the condition above
            FindViewById<Button>(Resource.Id.toMainScreenBtn).Click += delegate {
                StartActivity(homeIntent);
            };
            FindViewById<Button>(Resource.Id.btnBack).Click += delegate {
                StartActivity(homeIntent);
            };

            /**
             * Open the camera
             */
            FindViewById<Button>(Resource.Id.FotoBtn).Click += delegate {
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);
            };

            /**
             * Get all text field from the layout
             */
            etNombre = FindViewById<EditText>(Resource.Id.editTextNombre);
            etOrganizacion = FindViewById<EditText>(Resource.Id.editTextOrganizacion);
            etPuesto = FindViewById<EditText>(Resource.Id.editTextPuesto);
            etArchivarComo = FindViewById<EditText>(Resource.Id.editTextArchivarComo);
            etCorreo = FindViewById<EditText>(Resource.Id.editTextCorreo);
            etMostrarComo = FindViewById<EditText>(Resource.Id.editTextMostrarComo);
            etPaginaWeb = FindViewById<EditText>(Resource.Id.editTextPaginaWeb);
            etDireccionIM = FindViewById<EditText>(Resource.Id.editTextDireccionIM);
            etTelefonoTrabajo = FindViewById<EditText>(Resource.Id.editTextTelefonoTrabajo);
            etTelefonoCasa = FindViewById<EditText>(Resource.Id.editTextTelefonoCasa);
            etFax = FindViewById<EditText>(Resource.Id.editTextFax);
            etCelular = FindViewById<EditText>(Resource.Id.editTextCelular);
            etDireccionTrabajo = FindViewById<EditText>(Resource.Id.editTextDireccionTrabajo);
            etNotas = FindViewById<EditText>(Resource.Id.editTextNotas);

            /**
             * Action to save new contact
             */
            FindViewById<Button>(Resource.Id.GuardarBtn).Click += delegate {
                if (string.IsNullOrWhiteSpace(etNombre.Text) || string.IsNullOrWhiteSpace(etCelular.Text) || string.IsNullOrWhiteSpace(etCorreo.Text)) {
                    Toast.MakeText(Application, "Ingresar campos obligatorios(Nombre, Celular y correo)", ToastLength.Long).Show();
                    return;
                } else {
                    if (!IsUniqueName(etNombre.Text)) {
                        Toast.MakeText(Application, "El nombre del contacto ya existe", ToastLength.Long).Show();
                        return;
                    }  else {
                        SaveContact();
                    }
                }
            };
        }

        /**
         * Validate against the DB if the name already exists
         */
        private bool IsUniqueName(string name) {
            ws_mensajeria.somee.com.WebService1 ws = new ws_mensajeria.somee.com.WebService1();
            try {
                DataSet ds = ws.GetSingleContact(name);
                ds.Tables[0].Rows[0]["nombre"].ToString();
                return false;
            } catch (Exception e) {
                return true;
            }
        }

        /**
         * After passing all validations, sends the data to the DB to persist and redirect to the home activity
         */
        private void SaveContact() {
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
        }
    }
}
