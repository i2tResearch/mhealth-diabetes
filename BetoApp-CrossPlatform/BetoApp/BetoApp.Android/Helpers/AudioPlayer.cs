using System;
using Android.Media;

namespace BetoApp.Helpers
{
    // From https://github.com/NateRickard/Plugin.AudioRecorder/blob/master/Samples/Forms/AudioRecord.Forms.Android/AudioPlayer.cs

    public partial class AudioPlayer
    {
        private MediaPlayer _mediaPlayer;

        partial void PlatformPlay(string pathToAudioFile)
        {
            if (_mediaPlayer != null)
            {
                _mediaPlayer.Completion -= MediaPlayer_Completion;
                _mediaPlayer.Stop();
            }

            if (pathToAudioFile != null)
            {
                if (_mediaPlayer == null)
                {
                    _mediaPlayer = new MediaPlayer();

                    _mediaPlayer.Prepared += (sender, args) =>
                    {
                        _mediaPlayer.Start();
                        _mediaPlayer.Completion += MediaPlayer_Completion;
                    };
                }

                _mediaPlayer.Reset();

                _mediaPlayer.SetDataSource(pathToAudioFile);
                _mediaPlayer.PrepareAsync();
            }
        }

        private void MediaPlayer_Completion(object sender, EventArgs e)
        {
            FinishedPlaying?.Invoke(this, EventArgs.Empty);
        }

        partial void PlatformPause()
        {
            _mediaPlayer?.Pause();
        }

        partial void PlatformPlay()
        {
            _mediaPlayer?.Start();
        }
    }
}
