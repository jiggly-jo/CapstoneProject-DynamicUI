using System;
using Smartroom.Models;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ExternalImplementation))]
namespace Smartroom.Models
{
    public class ExternalImplementation : IExternalIntents
    {
        public ExternalImplementation()
        {

        }

        public void OpenExternalApp()
        {

        
        
            NSUrl request = new NSUrl("nflx://www.netflix.com/WiPlayer?shopperId=G-510cf375-e838-41c3-b3bd-1ab5ae98b667-1&movieid=1179468&trkid=1462433&episodeid=0&returnUrl=http%3A%2F%2Fwww.netflix.com%2FWiPlayer%3Fmovieid%3D1179468%26trkid%3D1462433%26pbc%3Dtrue");

            try
            {
                bool isOpened = UIApplication.SharedApplication.OpenUrl(request);

                if (isOpened == false)
                    UIApplication.SharedApplication.OpenUrl(new NSUrl("yourappurl"));
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);

            }

        }
    }
}