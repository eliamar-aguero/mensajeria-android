using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Data;
using System;
using Android.Provider;
using Android.Runtime;
using Android.Graphics;

namespace mensajeria {
    [Activity(Label = "CreateEditFormActivity")]
    public class CreateEditFormActivity : Activity {

        Intent homeIntent;
        Intent detailIntent;

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
        CheckBox checkCorrespondencia;
        EditText etNotas;
        ImageView ivFoto;

        DataSet contactToEdit;
        string nameFromEditActivity;
        bool isEditMode = false;
        readonly ws_mensajeria.somee.com.WebService1 ws = new ws_mensajeria.somee.com.WebService1();

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_create_edit_form);

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
            checkCorrespondencia = FindViewById<CheckBox>(Resource.Id.checkCorrespondencia);
            etNotas = FindViewById<EditText>(Resource.Id.editTextNotas);
            ivFoto = FindViewById<ImageView>(Resource.Id.imageViewFoto);



      

            // check if edit and update, otherwise, create
            // on edit => check custom fields like photo and google maps to preload the data correctly

            try {
                nameFromEditActivity = Intent.GetStringExtra("name");
                contactToEdit = ws.GetSingleContact(nameFromEditActivity);
                PreloadFormToEdit(contactToEdit);
                isEditMode = true;
            } catch (Exception) { }

            /**
             * Go to back to previous screen
             */
            homeIntent = new Intent(this, typeof(MainActivity));
            detailIntent = new Intent(this, typeof(ContactDetailActivity));
            FindViewById<Button>(Resource.Id.toMainScreenBtn).Click += delegate {
                if (isEditMode) {
                    detailIntent.PutExtra("name", contactToEdit.Tables[0].Rows[0]["nombre"].ToString());
                    StartActivity(detailIntent);
                    return;
                }
                StartActivity(homeIntent);
            };
            FindViewById<Button>(Resource.Id.btnBack).Click += delegate {
                if (isEditMode) {
                    detailIntent.PutExtra("name", contactToEdit.Tables[0].Rows[0]["nombre"].ToString());
                    StartActivity(detailIntent);
                    return;
                }
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
             * Action to save new contact
             */
            FindViewById<Button>(Resource.Id.GuardarBtn).Click += delegate {
                if (string.IsNullOrWhiteSpace(etNombre.Text) || string.IsNullOrWhiteSpace(etCelular.Text) || string.IsNullOrWhiteSpace(etCorreo.Text)) {
                    Toast.MakeText(Application, "Ingresar campos obligatorios(Nombre, Celular y correo)", ToastLength.Long).Show();
                    return;
                } else if (!IsValidEmail(etCorreo.Text)) {
                    Toast.MakeText(Application, "Email no tiene el formato correcto", ToastLength.Long).Show();
                    return;
                } else {
                    if (isEditMode) {
                        UpdateContact();
                        return;
                    }  else if (!IsUniqueName(etNombre.Text) && !isEditMode) {
                        Toast.MakeText(Application, "El nombre del contacto ya existe", ToastLength.Long).Show();
                        return;
                    } else {
                        SaveContact();
                    }
                }
            };
        }

        /**
         * Validate against the DB if the name already exists
         */
        private bool IsUniqueName(string name) {
            try {
                DataSet ds = ws.GetSingleContact(name);
                ds.Tables[0].Rows[0]["nombre"].ToString();
                return false;
            } catch (Exception) {
                return true;
            }
        }

        /**
         * After passing all validations, sends the data to the DB to persist and redirect to the home activity
         */
        private void SaveContact() {
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
                checkCorrespondencia.Checked.ToString() == "True" ? 1 : 0,
                etNotas.Text
            );         

            /**
            * After saving the contacto redirect to the home activity
            */
            StartActivity(homeIntent);
        }

        /**
         * In case the user is coming from the details screen after pressing edit contact
         * The contact will be updated instead of created
         */
        private void UpdateContact() {
            int id = int.Parse(contactToEdit.Tables[0].Rows[0]["id"].ToString());
            ws.UpdateContact(
                id,
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
                checkCorrespondencia.Checked.ToString() == "True" ? 1 : 0,
                etNotas.Text
            );

            /**
            * After updating the contacto redirect back to the detail activity
            */
            detailIntent.PutExtra("name", etNombre.Text);
            StartActivity(detailIntent);
        }

        /**
         * In case the user comes from the details activity and selected edit
         * Populate the form with the contact data to edit
         */
        private void PreloadFormToEdit(DataSet data) {
            etNombre.Text = data.Tables[0].Rows[0]["nombre"].ToString();
            etOrganizacion.Text = data.Tables[0].Rows[0]["organizacion"].ToString();
            etPuesto.Text = data.Tables[0].Rows[0]["puesto"].ToString();
            etArchivarComo.Text = data.Tables[0].Rows[0]["archivar_como_a"].ToString();
            etCorreo.Text = data.Tables[0].Rows[0]["email"].ToString();
            etMostrarComo.Text = data.Tables[0].Rows[0]["mostrar_como"].ToString();
            etPaginaWeb.Text = data.Tables[0].Rows[0]["pagina_web"].ToString();
            etDireccionIM.Text = data.Tables[0].Rows[0]["direccion_im"].ToString();
            etTelefonoTrabajo.Text = data.Tables[0].Rows[0]["tel_trabajo"].ToString();
            etTelefonoCasa.Text = data.Tables[0].Rows[0]["tel_particular"].ToString();
            etFax.Text = data.Tables[0].Rows[0]["fax_trabajo"].ToString();
            etCelular.Text = data.Tables[0].Rows[0]["tel_movil"].ToString();
            etDireccionTrabajo.Text = data.Tables[0].Rows[0]["direccion_trabajo"].ToString();
            checkCorrespondencia.Checked = !!(data.Tables[0].Rows[0]["direccion_correspondencia"].ToString() == "1");
            etNotas.Text = data.Tables[0].Rows[0]["notas"].ToString();
        }

        /**
         * Validate if email has the correct format
         */
        private bool IsValidEmail(string email) {
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data) {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            ivFoto.SetImageBitmap(bitmap);
        }
    }
}
