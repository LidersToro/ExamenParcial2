using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using AndroidSQLite.Resources.Model;
using AndroidSQLite.Resources.DataHelper;
using AndroidSQLite.Resources;
using Android.Util;

namespace AndroidSQLite
{
    [Activity(Label = "AndroidSQLite", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ListView lstData;
        List<Notes> lstSource = new List<Notes>();
        DataBase db;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Create DataBase
            db = new DataBase();
            db.createDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            lstData = FindViewById<ListView>(Resource.Id.listView);

            var edtNota = FindViewById<EditText>(Resource.Id.edtNota);
            

            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            //LoadData
            LoadData();

            //Event
            btnAdd.Click += delegate
            {
                Notes person = new Notes() {
                    Nota = edtNota.Text,
                   
                };
                db.insertIntoTablePerson(person);
                LoadData();
            };

            btnEdit.Click += delegate {
                Notes person = new Notes()
                {
                    Id = int.Parse(edtNota.Tag.ToString()),
                    Nota = edtNota.Text
                    
                };
                db.updateTablePerson(person);
                LoadData();
            };

            btnDelete.Click += delegate {
                Notes person = new Notes()
                {
                    Id = int.Parse(edtNota.Tag.ToString()),
                    Nota = edtNota.Text
                    
                };
                db.deleteTablePerson(person);
                LoadData();
            };

            lstData.ItemClick += (s,e) =>{
                //Set background for selected item
                for(int i = 0; i < lstData.Count; i++)
                {
                    if (e.Position == i)
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.DarkGray);
                    else
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);

                }

                //Binding Data
                var txtnota = e.View.FindViewById<TextView>(Resource.Id.textView1);

                edtNota.Text = txtnota.Text;
                edtNota.Tag = e.Id;

                
            };

        }

        private void LoadData()
        {
            lstSource = db.selectTablePerson();
            var adapter = new ListViewAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }
    }
}