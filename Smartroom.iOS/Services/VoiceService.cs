using AVFoundation;
using Foundation;
using Speech;
using System;
using Xamarin.Forms;
using Smartroom.Models;
using Smartroom.Helpers;
using AudioToolbox;
using Mono;
using System.Threading;

[assembly: Xamarin.Forms.Dependency(typeof(SpeechToTextImplementation))]
namespace Smartroom.Models
{
    public class SpeechToTextImplementation : ISpeechToText
    {
        private AVAudioEngine _audioEngine = new AVAudioEngine();
        private SFSpeechRecognizer _speechRecognizer = new SFSpeechRecognizer();
        private SFSpeechAudioBufferRecognitionRequest _recognitionRequest;
        private SFSpeechRecognitionTask _recognitionTask;
        private string _recognizedString;
        private bool _isAuthorized;
        private NSTimer _AfterSpeechTimer;
        private NSTimer _MaxTimeoutTimer;
        private NSTimer _SilenceTimeoutTimer;
        private int ElevCount = 0;
        public bool elevFlag = false;
        private bool STTFlag = false;
        private int invalidate = 5;
        private AVAudioPlayer _ringtoneAudioPlayer;

        public SpeechToTextImplementation()
        {
            AskForSpeechPermission();

        }

        public void StartSpeechToText()
        {

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": Listener Started");

            if (_audioEngine.Running)
            {
                StopRecordingAndRecognition();

            }
            StartRecordingAndRecognizing();
        }

        public void PlayOKSound()
        {

            SystemSound systemSound = new SystemSound(1109);
            systemSound.PlayAlertSound();
        }


        public void SetElevFlag(bool setTo)
        {
            elevFlag = setTo;
        }
        public void PlayYesSound()
        {


            SystemSound systemSound = new SystemSound(1004);
            systemSound.PlayAlertSound();
        }

        public void PlayElevatorConfirm()
        {

        }

        public void SetSTTFlag(bool setTo)
        {
            STTFlag = setTo;
        }
        public void PlayYesSpeech()
        {
            STTFlag = true;
            var soundname = NSBundle.MainBundle.PathForResource("questionsound", "wav");
            _ringtoneAudioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename(soundname));
            _ringtoneAudioPlayer.NumberOfLoops = 0;
            _ringtoneAudioPlayer.Play();
        }

        public void PlayNoSound()
        {
            SystemSound systemSound = new SystemSound(1003);
            systemSound.PlayAlertSound();
        }

        public void PlaySpeech(int speechNum)
        {
            var soundname = NSBundle.MainBundle.PathForResource("questionsound", "wav");
            switch (speechNum)
            {
                case 1: soundname = NSBundle.MainBundle.PathForResource("Elevator", "mp3");
                    break;
                case 2: soundname = NSBundle.MainBundle.PathForResource("Origin", "mp3");
                    break;
                case 3: soundname = NSBundle.MainBundle.PathForResource("Destination", "mp3");
                    break;
                case 4: soundname = NSBundle.MainBundle.PathForResource("Error1", "mp3");
                    break;
                case 5: soundname = NSBundle.MainBundle.PathForResource("Error2", "mp3");
                    break;
            }
            _ringtoneAudioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename(soundname));
            _ringtoneAudioPlayer.NumberOfLoops = 0;
            _ringtoneAudioPlayer.Play();
        }

        public void StopSpeechToText()              //stop recording
        {

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": Listener Stopped");
            StopRecordingAndRecognition();
        }

        private void AskForSpeechPermission()
        {
            SFSpeechRecognizer.RequestAuthorization((SFSpeechRecognizerAuthorizationStatus status) =>
            {
                switch (status)
                {
                    case SFSpeechRecognizerAuthorizationStatus.Authorized:
                        _isAuthorized = true;
                        break;
                    case SFSpeechRecognizerAuthorizationStatus.Denied:
                        break;
                    case SFSpeechRecognizerAuthorizationStatus.NotDetermined:
                        break;
                    case SFSpeechRecognizerAuthorizationStatus.Restricted:
                        break;
                }
            });
        }

        public bool CheckSpeechToText()
        {
            if (_audioEngine.Running)
                return true;
            else
                return false;
        }

        private void TimeOut()   //call when timers expire
        {


            sendResult(_recognizedString);

        }

        private void StartRecordingAndRecognizing()      //Start a new recording session
        {

            _recognizedString = "_";

            ResetTimeoutTimer();


            _MaxTimeoutTimer = NSTimer.CreateRepeatingScheduledTimer(30, delegate         //Stop listening after 30 seconds 
            {
                STTFlag = false;
                sendResult("_");
            });


            Console.WriteLine("Starting To Listen");

            _recognitionTask?.Cancel();
            _recognitionTask = null;

            var audioSession = AVAudioSession.SharedInstance();
            NSError nsError;
            nsError = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            audioSession.SetMode(AVAudioSession.ModeDefault, out nsError);
            nsError = audioSession.SetActive(true, AVAudioSessionSetActiveOptions.NotifyOthersOnDeactivation);
            audioSession.OverrideOutputAudioPort(AVAudioSessionPortOverride.Speaker, out nsError);

            audioSession.SetCategory("AVAudioSessionCategoryPlay", AVAudioSessionCategoryOptions.MixWithOthers, out nsError);
            _recognitionRequest = new SFSpeechAudioBufferRecognitionRequest();

            var inputNode = _audioEngine.InputNode;
            if (inputNode == null)
            {
                throw new Exception();
            }

            var recordingFormat = inputNode.GetBusOutputFormat(0);

            inputNode.InstallTapOnBus(0, 1024, recordingFormat, (buffer, when) =>
             {
                 _recognitionRequest?.Append(buffer);
             });

            _audioEngine.Prepare();
            _audioEngine.StartAndReturnError(out nsError);


            _recognitionTask = _speechRecognizer.GetRecognitionTask(_recognitionRequest, (result, error) =>         //throws when speech is heard
            {
                if (result != null&&_recognitionTask!=null)
                {
                    Console.WriteLine("Result found");
                    _recognizedString = result.BestTranscription.FormattedString;

                    if (!STTFlag)                                               //listen for mac or sam 
                    {
                        if (_recognizedString.ToUpper().Equals("MAC") ||        //if you hear 'Mac'
                        _recognizedString.ToUpper().Equals("HEY MAC") ||
                        _recognizedString.ToUpper().Equals("OK MAC") ||
                        _recognizedString.Contains(" Mac ") || _recognizedString.Contains(" Mac"))
                        {
                            Console.WriteLine("Mac found");
                            STTFlag = true;
                            sendResult("Mac");

                            ResetSilenceTimer();

                            Console.WriteLine("Mac Silence Timer Set");
                            _SilenceTimeoutTimer = NSTimer.CreateRepeatingScheduledTimer(6, delegate         //Stop listening if silence
                            {
                                STTFlag = false;
                                sendResult("_");
                            });

                        }
                        else if (_recognizedString.ToUpper().Equals("SAM") ||   //if you hear 'Sam'
                        _recognizedString.ToUpper().Equals("HEY SAM") ||
                        _recognizedString.ToUpper().Equals("OK SAM") ||
                        _recognizedString.Contains(" Sam ") || _recognizedString.Contains(" Sam"))
                        {
                            Console.WriteLine("Sam found");
                            STTFlag = true;
                            sendResult("Sam");

                            ResetSilenceTimer();

                            Console.WriteLine("Sam Silence Timer Set");
                            _SilenceTimeoutTimer = NSTimer.CreateRepeatingScheduledTimer(6, delegate         //Stop listening if silence
                            {
                                sendResult("_");
                                STTFlag = false;
                            });
                        }
                        else
                        {
                            _recognizedString = "";
                        }
                    }
                    else
                    {                                                           //listen to every word after Sam or MAc is heard
                        int cd = 1;

                        if (RoomState.GetCountdown() == 3)
                            cd = 3;
                        else if (RoomState.GetCountdown() == 5)
                            cd = 5;


                        ResetSilenceTimer();
                        ResetSpeechTimer();


                        _AfterSpeechTimer = NSTimer.CreateScheduledTimer(cd, delegate             //Stop listening if silence
                        {
                            
                            
                            sendResult(_recognizedString);
                            
                            STTFlag = false;

                        });


                        if (_recognizedString.Split(' ').Length > 12)                               //twelve word limit
                        {
                            ResetSilenceTimer();
                            ResetSpeechTimer();

                            STTFlag = false;
                            
                            sendResult("Error: Speech is too long");
                        }



                        Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": " + _recognizedString + " found");

                        MessagingCenter.Send<ISpeechToText, string>(this, "VTT", _recognizedString);
                    }



                }

                if (error != null)
                {

                    Console.WriteLine(error.LocalizedDescription);

                    if (error.Code.Equals(203))
                    {

                    }
                    else if (!error.Code.Equals(216) && !error.Code.Equals(209))
                    {
                        Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": Unrecognized error");
                    }
                }

            });
        }



        private void sendResult(string results)
        {
            ResetSpeechTimer();

            ResetTimeoutTimer();

            ResetSilenceTimer();

            Console.WriteLine("Returning: " + results);
            
            StopRecordingAndRecognition();
            MessagingCenter.Send<ISpeechToText, string>(this, "STT", results);
            //return results

        }


        private void ResetSilenceTimer()
        {
            if (_SilenceTimeoutTimer != null)
            {
                _SilenceTimeoutTimer.Invalidate();
                _SilenceTimeoutTimer = null;
                Console.WriteLine("Result sent, Silence timer stopped");
            }
        }

        private void ResetSpeechTimer()
        {
            if (_AfterSpeechTimer != null)                                      //reset timers
            {
                _AfterSpeechTimer.Invalidate();
                _AfterSpeechTimer = null;
            }

        }

        private void ResetTimeoutTimer()
        {
            if (_MaxTimeoutTimer != null)
            {
                _MaxTimeoutTimer.Invalidate();
                _MaxTimeoutTimer = null;
            }
        }

        private void StopRecordingAndRecognition(AVAudioSession aVAudioSession = null)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": Listening Stopped");
            if (_audioEngine.Running)
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": Clearing Engine");
                _audioEngine.InputNode.RemoveTapOnBus(0);
                _audioEngine.InputNode.Reset();

                _audioEngine.Stop();

                _recognitionTask.Finish();

                _recognitionRequest.EndAudio();
                _recognitionTask = null;
                _recognitionRequest = null;
            }

            ResetSilenceTimer();
            ResetTimeoutTimer();
            ResetSpeechTimer();

            return;
        }        
    }
}