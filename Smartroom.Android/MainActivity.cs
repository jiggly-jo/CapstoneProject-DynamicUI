using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using Android.Speech;
using Xamarin.Forms;
using Smartroom.Models;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using System.Collections.Generic;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(Smartroom.Droid.MainActivity))]
namespace Smartroom.Droid
{
   [Activity(Label = "Smartroom", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IRecognitionListener, ISpeechToText
    {
        private SpeechRecognizer speech;
        private Intent recognizerIntent;
       
        protected override void OnCreate(Bundle savedInstanceState) { 
            if (ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.RecordAudio) == (int)Permission.Granted)
            {
                Console.WriteLine("Permission Found");
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.RecordAudio}, 200);
              
            }

            

            speech = SpeechRecognizer.CreateSpeechRecognizer(this);
            speech.SetRecognitionListener(this);

            recognizerIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);

            recognizerIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

            recognizerIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            recognizerIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            recognizerIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            recognizerIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

            recognizerIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);

            speech.StartListening(recognizerIntent);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;


            
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);


            LoadApplication(new App());
        }

        public bool CheckSpeechToText()
        {
            return false;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == 200)
            {
               
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        public void OnBeginningOfSpeech()
        {
            Console.WriteLine("begining");
        }

        public void SetElevFlag(bool setTo)
        {

        }

        public void OnBufferReceived(byte[] buffer)
        {
            Console.WriteLine("buffering");
        }

        public void PlayOKSound()
        {
            Console.WriteLine("buffering");
        }

        public void SetElevCount(int setTo)
        {
           
        }

        public void PlayYesSound()
        {
            Console.WriteLine("buffering");
        }

        public void PlayNoSound()
        {
            Console.WriteLine("buffering");
        }

        public void OnEndOfSpeech()
        {
            Console.WriteLine("ending");
            speech.StartListening(recognizerIntent);

        }

        public void OnError([GeneratedEnum] SpeechRecognizerError error)
        {
            Console.WriteLine(error);
            speech.StartListening(recognizerIntent);
        }

        public void OnEvent(int eventType, Bundle @params)
        {
            Console.WriteLine("event found");
        }

        public void OnPartialResults(Bundle partialResults)
        {
            Console.WriteLine("PArtial found");
        }

        public void PlaySpeech(int SpeechNum)
        {

        }

            
        public void OnReadyForSpeech(Bundle @params)
        {
            Console.WriteLine("Ready");
        }

        public void OnResults(Bundle results)
        {
            IList<string> matches = results.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
            MessagingCenter.Send<ISpeechToText, string>(this, "STT", matches[0]);

            Console.WriteLine("found results");
            speech.StartListening(recognizerIntent);

        }

        public void OnRmsChanged(float rmsdB)
        {

        }

        public void StartSpeechToText()
        {

        }

        public void SetSTTFlag(bool setTo)
        {

        }
        public void StopSpeechToText()
        {
            Console.WriteLine("Stopping");
        }

        public void LongAlert(string message)
        {
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }

        public void PlayYesSpeech()
        {

        }
    }
}