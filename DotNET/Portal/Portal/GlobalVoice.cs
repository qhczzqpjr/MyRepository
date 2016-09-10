using System.Configuration;
using System.Speech.Synthesis;

namespace Portal
{
    public class GlobalVoice
    {
        public static void SayHello()
        {
            const string strUserName = "Thomas";
            using (var synth = new SpeechSynthesizer())
            {
                var strVoice = ConfigurationManager.AppSettings["Voice"];
                synth.SelectVoice(strVoice);
                synth.SetOutputToDefaultAudioDevice();
                synth.Speak("Welcome to Portal ," + strUserName);
            }
        }
    }
}