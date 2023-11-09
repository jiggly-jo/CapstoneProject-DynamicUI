namespace Smartroom.Models
{
    public interface ISpeechToText    //interface to interact with OS specifc sound functions
    {
        void StartSpeechToText();                               //Start the Speech REcognition service
        void StopSpeechToText();                                //Stop the Speech Recognition Service
        void PlayOKSound();                                     //Play the devices OK Sound
        void PlayYesSound();                                    //play the devices Query sound
        void PlayNoSound();                                     //play the devices No Sound
        void PlayYesSpeech();                                   //play the query speech
        void SetElevFlag(bool setTo);                           //elevFlagset
        void SetSTTFlag(bool setTo);                            //Speech-to-text set
        void PlaySpeech(int recordNum);                         //play speech audio
    }
}