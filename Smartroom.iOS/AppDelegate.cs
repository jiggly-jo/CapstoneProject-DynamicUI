﻿using System;
using System.Collections.Generic;
using System.Linq;
using Intents;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Foundation;
using UIKit;


namespace Smartroom.iOS
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
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Calabash.Start();
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Forms.FormsMaterial.Init();
            CachedImageRenderer.Init();
            var ignore = typeof(SvgCachedImage);
            Forms9Patch.iOS.Settings.Initialize(this);
            LoadApplication(new App());

         /*   try
            {
                INPreferences.RequestSiriAuthorization((INSiriAuthorizationStatus status) =>
                {
                    // Respond to returned status
                    switch (status)
                    {
                        case INSiriAuthorizationStatus.Authorized:
                            break;
                        case INSiriAuthorizationStatus.Denied:
                            break;
                        case INSiriAuthorizationStatus.NotDetermined:
                            break;
                        case INSiriAuthorizationStatus.Restricted:
                            break;
                    }
                });
            }
            catch (Exception e)
            {
                string Ex = e.ToString();
                Console.WriteLine(e);
            }*/
                      

            return base.FinishedLaunching(app, options);
        }


        

    }
}
