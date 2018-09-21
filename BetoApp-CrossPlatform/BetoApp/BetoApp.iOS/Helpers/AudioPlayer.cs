using System;
using AVFoundation;
using Foundation;

namespace BetoApp.Helpers
{
    // From https://github.com/NateRickard/Plugin.AudioRecorder/blob/master/Samples/Forms/AudioRecord.Forms.iOS/AudioPlayer.cs

    public partial class AudioPlayer
    {
        private AVAudioPlayer _audioPlayer = null;

        partial void PlatformPlay(string pathToAudioFile)
        {
            // Check if _audioPlayer is currently playing
            if (_audioPlayer != null)
            {
                _audioPlayer.FinishedPlaying -= Player_FinishedPlaying;
                _audioPlayer.Stop();
            }

            string localUrl = pathToAudioFile;
            _audioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename(localUrl));
            _audioPlayer.FinishedPlaying += Player_FinishedPlaying;
            _audioPlayer.Play();
        }

        private void Player_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            FinishedPlaying?.Invoke(this, EventArgs.Empty);
        }

        partial void PlatformPause()
        {
            _audioPlayer?.Pause();
        }

        partial void PlatformPlay()
        {
            _audioPlayer?.Play();
        }
    }
}
