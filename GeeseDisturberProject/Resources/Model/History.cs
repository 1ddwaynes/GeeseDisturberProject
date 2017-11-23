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
using SQLite;

namespace GeeseDisturberProject.Model
{
    public class History
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }

        public int Port { get; set;}

        public string Address { get; set; }

        //public bool Use { get; set; }
    }

    public class Setting
    {
        public string Port_n { get; set; }

        public string Address_n { get; set; }

        public string GetUrl(bool settings)
        {
            if (settings)
                return Address_n + Port_n + "/stream";

            else
                return Address_n + Port_n + "/panel";
        }

        public void EditUrl (string port, string Address)
        {
            Port_n = port;

            Address_n = Address;
        }

        public void InitUrl()
        {
            if (Address_n == null)
                Address_n = "http://proxy7.remote-iot.com:";

            if (Port_n == null)
                Port_n = "12356";
        }
    }
}