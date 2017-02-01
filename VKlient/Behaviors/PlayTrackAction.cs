using Microsoft.Xaml.Interactivity;
using System.Collections.Generic;
using OneVK.Core.Player;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using OneVK.Helpers;
using GalaSoft.MvvmLight.Messaging;
using OneVK.ViewModel.Messages;
using OneVK.Enums.App;

namespace OneVK.Behaviors
{
    /// <summary>
    /// Действие, предназначенное для воспроизведения трека из списка.
    /// </summary>
    public class PlayTrackAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            if (Track == null && parameter == null) return null;

            IAudioTrack track = null;
            if (Track == null)
            {
                if (InputConverter != null)
                    track = (IAudioTrack)InputConverter.Convert(parameter, null, null, null);
                else
                    track = (IAudioTrack)parameter;
            }
            else
                track = Track;

            Messenger.Default.Send(new PlayTrackMessage { Tracks = (IEnumerable<IAudioTrack>)TracksList, TrackToPlay = track });
            NavigationHelper.Navigate(AppViews.PlayerView);

            return null;
        }

        /// <summary>
        /// Трек для воспроизведения.
        /// </summary>
        public IAudioTrack Track
        {
            get { return (IAudioTrack)GetValue(TrackProperty); }
            set { SetValue(TrackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Track.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrackProperty =
            DependencyProperty.Register("Track", typeof(IAudioTrack), typeof(PlayTrackAction), new PropertyMetadata(default(IAudioTrack)));


        /// <summary>
        /// Конвертер, преобразующий входной параметр.
        /// </summary>
        public IValueConverter InputConverter
        {
            get { return (IValueConverter)GetValue(InputConverterProperty); }
            set { SetValue(InputConverterProperty, value); }
        }
        
        public static readonly DependencyProperty InputConverterProperty =
            DependencyProperty.Register("InputConverter", typeof(IValueConverter), typeof(PlayTrackAction), new PropertyMetadata(default(IValueConverter)));

        /// <summary>
        /// Коллекция треков, из которого будет проигрываться трек.
        /// </summary>
        public object TracksList
        {
            get { return (object)GetValue(TrackListProperty); }
            set { SetValue(TrackListProperty, value); }
        }
        
        public static readonly DependencyProperty TrackListProperty =
            DependencyProperty.Register("TracksList", typeof(object), typeof(PlayTrackAction), new PropertyMetadata(default(object)));
    }
}
