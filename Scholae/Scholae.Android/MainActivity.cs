using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Scholae.Droid
{
    [Activity(Label = "Scholae", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //var loggingConfig = AWSConfigs.LoggingConfig;
            //loggingConfig.LogMetrics = true;
            //loggingConfig.LogResponses = ResponseLoggingOption.Always;
            //loggingConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
            //loggingConfig.LogTo = LoggingOptions.SystemDiagnostics;
            //
            //AWSConfigs.AWSRegion = "";

            //s3Client
            //IAmazonS3 s3Client = new AmazonS3Client(credentials,RegionEndpoint.USEast1);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#3b5998"));
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}