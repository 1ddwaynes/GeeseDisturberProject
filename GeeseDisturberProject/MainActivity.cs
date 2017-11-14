using Android.App;
using Android.Widget;
using Android.OS;

using Android.Views;
using System;
using Android.Content;
using GeeseDisturberProject.Settings;
using GeeseDisturberProject.Camera;
using GeeseDisturberProject.Resources.DataHelper;
using GeeseDisturberProject.Model;
using Android.Util;
using System.Collections.Generic;

namespace GeeseDisturberProject
{

    [Activity(Label = "GeeseDisturberProject", MainLauncher = true, Icon = "@drawable/remotecontrol2e")]
    public class MainMenu : Activity
    {
        private Button CameraButton;
        private Button SettingsButton;
        private Button ControlButton;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainMenu);

            FindViews();
            HandleEvents();
            insertDefualtvalues();
        }

        private void HandleEvents()
        {
            CameraButton.Click += CammeraButton_Click;
            SettingsButton.Click += SettingsButton_Click;
            ControlButton.Click += ControlsButton_Click;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SettingsActivity));
            StartActivity(intent);
        }

        private void CammeraButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CameraActivity));
            StartActivity(intent);
        }

        private void ControlsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CameraActivity));
            StartActivity(intent);
        }

        private void FindViews()
        {
            CameraButton = FindViewById<Button>(Resource.Id.Camera);
            SettingsButton = FindViewById<Button>(Resource.Id.Settings);
            ControlButton = FindViewById<Button>(Resource.Id.Control);
        }

       
        private void insertDefualtvalues()
        {
            List<History> lstSource = new List<History>();
            DataBase db;

            db = new DataBase();
            db.createDataBase();

            //db.deleteAllTableHistory();


            History history = new History()
            {
                Address = "proxy7.remote-iot",
                //address = edtAddress.Text,
                Port = 22598,
                //Use = true,
                //Port = int.Parse(edtPort.Text),
                // Email = edtEmail.Text
            };
            db.insertIntoTableHistory(history);
        }
    }
}

