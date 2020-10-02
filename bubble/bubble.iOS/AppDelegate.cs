using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace bubble.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
        }
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
                {
            // Get current device token
            byte[] bytes = deviceToken.ToArray<byte>();
            string[] hexArray = bytes.Select(b => b.ToString("x2")).ToArray();
            deviceToken = string.Join(string.Empty, hexArray);
            Console.WriteLine(deviceToken);
            var DeviceToken = deviceToken.Description;
            Console.WriteLine("DESCRIPTION", DeviceToken);
                    if (!string.IsNullOrWhiteSpace(DeviceToken))
                    {
                        DeviceToken = DeviceToken.Trim('<').Trim('>');
                    }

                    // Get previous device token
                    var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");

                    // Has the token changed?
                    if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
                    {
                        //TODO: Put your own logic here to notify your server that the device token has changed/been created!
                    }

                    // Save new device token
                    NSUserDefaults.StandardUserDefaults.SetString(DeviceToken, "PushDeviceToken");
                }
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                    new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }
            return base.FinishedLaunching(app, options);
        }
    }
}
