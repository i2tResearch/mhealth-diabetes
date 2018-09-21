using System;
using System.IO;
using Plugin.AudioRecorder;
using Xamarin.Forms;
using BetoApp.Helpers;
using BetoApp.Models;

namespace BetoApp.ViewModels
{
    public class FeelingsViewModel : BindableBase
    {
        private AudioRecorderService audioRecorder;
        private AudioPlayer audioPlayer;

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                NotifyPropertyChanged();
            }
        }

        private string _feeling;
        public string Feeling
        {
            get => _feeling;
            set
            {
                _feeling = value;
                NotifyPropertyChanged();
            }
        }

        public bool Recording
        {
            get => audioRecorder.IsRecording;
        }

        private bool _recorded;
        public bool Recorded
        {
            get => _recorded;
            set
            {
                _recorded = value;
                NotifyPropertyChanged();
            }
        }

        private bool _playing;
        public bool Playing
        {
            get => _playing;
            set
            {
                _playing = value;
                NotifyPropertyChanged();
            }
        }

        public FeelingsViewModel()
        {
            RecordAudioCommand = new Command(RecordAudio);
            PlayAudioCommand = new Command(PlayAudio);
            DiscardAudioCommand = new Command(DiscardAudio);
            SendAudioCommand = new Command(SendAudio);

            audioRecorder = new AudioRecorderService
            {
                StopRecordingOnSilence = false,
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(180)
            };

            audioPlayer = new AudioPlayer();
            audioPlayer.FinishedPlaying += Player_FinishedPlaying;
        }

        public Command RecordAudioCommand { get; }
        private async void RecordAudio()
        {
            try
            {
                if (!Recording)
                {
                    var recordTask = await audioRecorder.StartRecording();
                    NotifyPropertyChanged("Recording");
                    await recordTask;
                }
                else
                {
                    await audioRecorder.StopRecording();
                }
            }
            catch
            {
                await AlertHelper.ShowRecordErrorAlert();
            }
            finally
            {
                NotifyPropertyChanged("Recording");
                Recorded = audioRecorder.GetAudioFilePath() != null;
            }
        }

        public Command PlayAudioCommand { get; }
        private void PlayAudio()
        {
            if (!Playing)
            {
                var audioFile = audioRecorder.GetAudioFilePath();
                audioPlayer.Play(audioFile);
                Playing = true;
            }
            else
            {
                audioPlayer.Pause();
                Playing = false;
            }
        }

        public Command DiscardAudioCommand { get; }
        private void DiscardAudio()
        {
            try
            {
                var audioFile = audioRecorder.GetAudioFilePath();
                File.Delete(audioFile);
            }
            finally
            {
                Recorded = false;
            }
        }

        public Command SendAudioCommand { get; }
        private async void SendAudio()
        {
            if (Recorded)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        void Player_FinishedPlaying(object sender, EventArgs e)
        {
            Playing = false;
        }
    }
}
