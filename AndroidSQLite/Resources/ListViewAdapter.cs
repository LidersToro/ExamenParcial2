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
using AndroidSQLite.Resources.Model;
using Java.Lang;

namespace AndroidSQLite.Resources
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtFecha { get; set; }
        public TextView txtCantidad { get; set; }
        public TextView txtId_cliente { get; set; }
        public TextView txtId_producto { get; set; }
    }
    public class ListViewAdapter:BaseAdapter
    {
        private Activity activity;
        private List<Notes> lstPerson;
        public ListViewAdapter(Activity activity, List<Notes> lstPerson)
        {
            this.activity = activity;
            this.lstPerson = lstPerson;
        }

        public override int Count
        {
            get
            {
                return lstPerson.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstPerson[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view_dataTemplate, parent, false);

            var txtNota = view.FindViewById<TextView>(Resource.Id.textView1);
            

            txtNota.Text = lstPerson[position].Nota;
            

            return view;
        }
    }
}