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
using Android.Webkit;
using GeeseDisturberProject.Resources;
using GeeseDisturberProject.Resources.DataHelper;
using Android.Util;

namespace GeeseDisturberProject.Camera
{
    [Activity(Label = "Camera Menu")]
    public class CameraActivity : Activity
    {
        WebView web_view;
        DataBase db;
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        private Button buttonStreamSetting;
        private Button BackButton;

        static string url = "http://proxy7.remote-iot.com:";
        static string port = "29444";

        static string stream = url + port + "/stream";
        static string StreamSetting = url + port + "/panel";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.CameraActivity);
            CameraView();
            FindViews();
            HandleEvents();
        }

        public void CameraView()
        {
            web_view = FindViewById<WebView>(Resource.Id.WebView);
            web_view.Settings.JavaScriptEnabled = true;
            web_view.SetWebViewClient(new HelloWebViewClient());

            int default_zoom_level = 50;
            web_view.SetInitialScale(default_zoom_level);

            var width = web_view.Width;
            var height = web_view.Height;

            //var metrics = Resources.DisplayMetrics;
            //var width = metrics.WidthPixels;
            //var height = metrics.HeightPixels;

            SetViewSettings(web_view);

            web_view.LoadUrl(stream + "?width=" + width + "&height=" + height);
        }

        private void HandleEvents()
        {
            buttonStreamSetting.Click += StreamSettings_Click;
            BackButton.Click += BackButton_Click;
        }

        private void StreamSettings_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CameraActivitySettings));
            intent.PutExtra("key", StreamSetting);
            StartActivity(intent);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainMenu));
            StartActivity(intent);
        }

        private void FindViews()
        {
            buttonStreamSetting = FindViewById<Button>(Resource.Id.StreamSettings);
            BackButton = FindViewById<Button>(Resource.Id.Back);
        }

        private void GetData()
        {
            db = new DataBase();
            db.createDataBase();
            
            Log.Info("DB_PATH", folder);
            var table = db.selectQueryTableHistory(1);


        }

        private void SetViewSettings(WebView view)
        {
            view.Settings.LoadWithOverviewMode = true;
            view.Settings.UseWideViewPort = true;
        }

        public class HelloWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);
                return false;
            }
        }

        public override bool OnKeyDown(Android.Views.Keycode keyCode, Android.Views.KeyEvent e)
        {
            if (keyCode == Keycode.Back && web_view.CanGoBack())
            {
                web_view.GoBack();
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }
    }
}
