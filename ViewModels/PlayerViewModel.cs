using AudioBeta1._0.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AudioBeta1._0.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        

        public event PropertyChangedEventHandler? PropertyChanged;
        private MediaPlayer player = new MediaPlayer();
        private 
        void player_MediaFailed(object sender, ExceptionEventArgs e)
        {
            MessageBox.Show("Ошибка во время открытия файла.");
        }

        public PlayerViewModel()
        {
          
            changePlayOrPause = new RelayCommand(obj =>

            {
                
                var pos = player.Position;
                
                if (PlayOrPauseIsCheck == true)
                {
                    PlayOrPause = "Pause";
                    OnPropertyChanged(nameof(PlayOrPause));
                    
                    player.Open(new Uri(selectedTrack.Path, UriKind.Relative));
                    player.Position = pos;
                    player.Play();


                }
                else
                {
                    
                    PlayOrPause = "Play";
                    OnPropertyChanged(nameof(PlayOrPause));
                    player.Pause();
                   
                    
                }
            });
            NextTrack = new RelayCommand(obj =>

            {

               
                
                if(selectedIndexTrack<playlist.TracksPlayList.Count-1)
                {
                    player.Close();
                    OnPropertyChanged(nameof(selectedTrack));
                    player.Open(new Uri(selectedTrack.Path, UriKind.Relative));
                    player.Play();
                    selectedIndexTrack += 1;
                    OnPropertyChanged(nameof(selectedIndexTrack));
                    
                }
            });
            PreviousTrack = new RelayCommand(obj =>
            {
                
                
                if (selectedIndexTrack>=1)
                {
                    player.Close();
                    OnPropertyChanged(nameof(selectedTrack));
                    player.Open(new Uri(selectedTrack.Path, UriKind.Relative));
                    player.Play();
                    selectedIndexTrack -= 1;
                    OnPropertyChanged(nameof(selectedIndexTrack));
                    
                    
                }
               
            });
            AddTrack = new RelayCommand(obj =>
            {
                var dialog = new OpenFileDialog
                {
                    Filter = "Audio file|*.mp3"
                };
                var result = dialog.ShowDialog();
                if (result != true)
                {
                    return;
                }
                string newFilename = Guid.NewGuid().ToString().Replace("-", "") + ".mp3";
                string pathToCopy = $"Audios/{newFilename}";

                    
                try
                {
                    File.Copy(dialog.FileName, pathToCopy);
                    playlist.Load(pathToCopy);



                }
                catch
                {
                    MessageBox.Show("Ошибка при копировании файла!");
                }
            });

            RemoveTrack = new RelayCommand(obj =>
            {
                OnPropertyChanged(nameof(selectedTrack));
                playlist.TracksPlayList.Remove(selectedTrack);   
            });


        }
        public RelayCommand RemoveTrack { get; set; }
        public RelayCommand AddTrack { get; set; }
        public RelayCommand NextTrack { get; }

        public RelayCommand PreviousTrack { get; }

        public int selectedIndexTrack { get; set; }

        private Track selectedTrack;

        public Track SelectedTrack
        {
            get => selectedTrack;
            set { selectedTrack = value; 
                trackAuthor = selectedTrack.Author; 
                trackImage = selectedTrack.Photo;
                trackName = selectedTrack.Title; 
                OnPropertyChanged(nameof(trackName)); 
                OnPropertyChanged(nameof(trackAuthor)); 
                OnPropertyChanged(nameof(trackImage)); }
        }



        public Playlist playlist { get; set; } = new Playlist();


        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private string playOrPause = "Play";
        
        public string PlayOrPause
        {
            get => playOrPause;
            set 
            { 
                playOrPause = value;
                OnPropertyChanged(nameof(playOrPause));
            }
        }


        public RelayCommand changePlayOrPause { get; }
        

        private bool playOrPauseIsCheck;

        public bool PlayOrPauseIsCheck
        {
            get => playOrPauseIsCheck;
            set { playOrPauseIsCheck = value; }
        }


        private string trackName;

        public string TrackName
        {
            get => trackName;
            set { trackName = value; }
        }

        private string trackAuthor;

        public string TrackAuthor
        { 
            get => trackAuthor;
            set { trackAuthor = value; }
        }

        private BitmapImage trackImage;

        public BitmapImage TrackImage
        {
            get => trackImage;
            set { trackImage = value; }
        }






    }
}
