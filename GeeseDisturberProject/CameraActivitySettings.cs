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
using GeeseDisturberProject.Camera;
using GeeseDisturberProject.Resources.DataHelper;
using GeeseDisturberProject.Model;
using Android.Util;

namespace GeeseDisturberProject
{
    [Activity(Label = "CameraActivitySettings")]
    public class CameraActivitySettings : Activity
    {
        WebView web_view;
        private Button BackButton;
        public string streamSettings = null;

        static string url = "http://proxy7.remote-iot.com:";
        static string port = "22598";
        static string StreamSetting = url + port + "/panel";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Bundle extras = Intent.Extras;
            if (extras != null)
            {
                String streamSettings = extras.GetString("key");
            }

            SetContentView(Resource.Layout.CameraActivitySettings);
            CameraView();
            FindViews();
            HandleEvents();
        }

        

        public void CameraView()
        {
            web_view = FindViewById<WebView>(Resource.Id.WebView);
            web_view.Settings.JavaScriptEnabled = true;
            web_view.SetWebViewClient(new HelloWebViewClient());

            int default_zoom_level = 100;
            web_view.SetInitialScale(default_zoom_level);

            var metrics = Resources.DisplayMetrics;
            var width = metrics.WidthPixels;
            var height = metrics.HeightPixels;

            SetViewSettings(web_view);

            web_view.LoadUrl(StreamSetting + "?width=" + width + "&height=" + height);
        }

        private void HandleEvents()
        {
            BackButton.Click += BackButton_Click;
        }

        private void FindViews()
        {
            BackButton = FindViewById<Button>(Resource.Id.Back);
        }

        private void SetViewSettings(WebView view)
        {
            view.Settings.LoadWithOverviewMode = true;
            view.Settings.UseWideViewPort = true;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CameraActivity));
            StartActivity(intent);
        }

        public class HelloWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);
                return false;
            }
        }
    }
}