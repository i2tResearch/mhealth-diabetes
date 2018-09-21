using System;

namespace BetoApp.Helpers
{
    // From https://github.com/NateRickard/Plugin.AudioRecorder/blob/master/Samples/Forms/AudioRecord.Forms/AudioPlayer.cs

    public partial class AudioPlayer
    {
        public event EventHandler FinishedPlaying;

        public void Play(string pathToAudioFile)
        {
            PlatformPlay(pathToAudioFile);
        }

        public void Pause()
        {
            PlatformPause();
        }

        public void Play()
        {
            PlatformPlay();
        }

        partial void PlatformPlay(string pathToAudioFile);
        partial void PlatformPause();
        partial void PlatformPlay();
    }
}
