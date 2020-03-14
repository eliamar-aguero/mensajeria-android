
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
            Button btnSave = FindViewById<Button>(Resource.Id.GuardarBtn);
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
            EditText etCorrespondencia = FindViewById<EditText>(Resource.Id.editTextCorrespondencia);
            EditText etNotas = FindViewById<EditText>(Resource.Id.editTextNotas);



            toMainScreenBtn.Click += delegate
            {
                Intent homeIntent = new Intent(this, typeof(MainActivity));
                StartActivity(homeIntent);
            };

            btnSave.Click += delegate
            {
                com.somee.ws_mensajeria1.WebService1 ws = new com.somee.ws_mensajeria1.WebService1();
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
                    int.Parse(etCorrespondencia.Text), 
                    etNotas.Text
                    );

            };
        }
    }
}
