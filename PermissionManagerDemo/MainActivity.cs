using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Karan.Churi;
using System.Collections.Generic;
using System.Linq;
using R = PermissionManagerDemo.Resource;
using Android.Widget;

namespace PermissionManagerDemo
{
	[Activity(Label = "PermissionManagerDemo", MainLauncher = true, Icon = "@mipmap/ic_launcher", Theme = "@style/AppTheme")]
	public class MainActivity : AppCompatActivity
	{
		private PermissionManager _permission;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(R.Layout.activity_main);

			_permission = new PermissionManager();

			// ONLY use this Event Handler when you prefer to check your own permission instead from AndroidManifest.xml
			//_permission.SetPermissionEvent += delegate
			//{
			//    //var customPermission = new List<string>();
			//    //customPermission.Add(Manifest.Permission.AccessFineLocation);
			//    //return customPermission;
			//};

			// To initiate checking permission
			_permission.CheckAndRequestPermissions(this);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			_permission.CheckResult(requestCode, permissions, grantResults);

			// To get Granted Permission and Denied Permission
			var granted = _permission.Status[0].Granted;
			var denied = _permission.Status[0].Denied;
#if DEBUG
			Log.Debug("ROFIQ", $"GRANTED: {string.Join(", ", granted.ToArray())}");
			Log.Debug("ROFIQ", $"DENIED: {string.Join(", ", denied.ToArray())}");
#endif
			var message = "";

			// If all permissions are granted
			if (denied.Count == 0)
			{
				message = "All permissions GRANTED :)";
			}
			else
			{
				message = $"Please grants these permission(s): {string.Join(",", denied.ToArray())}";
			}

			Toast.MakeText(this, message, ToastLength.Long).Show();

		}
	}
}

