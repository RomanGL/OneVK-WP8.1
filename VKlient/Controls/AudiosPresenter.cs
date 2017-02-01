using System.Collections.ObjectModel;
using System.Collections.Generic;
using OneVK.Model.Audio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using OneVK.Core.Collections;
using GalaSoft.MvvmLight.Messaging;
using OneVK.ViewModel.Messages;
using OneVK.Core.Player;
using OneVK.Enums.App;
using OneVK.Helpers;

namespace OneVK.Controls
{
    /// <summary>
    /// Является элементом для отображения списка вложений типа аудиозаписей.
    /// </summary>
    public class AudiosPresenter : Control
    {
        private const string ListPresenterName = "ListPresenter";

        /// <summary>
        /// Инициализирует новый экземрляр класса.
        /// </summary>
        public AudiosPresenter()
        {
            this.DefaultStyleKey = typeof(AudiosPresenter);
            this.Audios = new ObservableCollection<VKAudio>();
        }

        #region Свойства
        /// <summary>
        /// Источник данных аудиозаписей.
        /// </summary>
        public ObservableCollection<VKAudio> Audios
        {
            get { return (ObservableCollection<VKAudio>)GetValue(AudiosSourceProperty); }
            set { SetValue(AudiosSourceProperty, value); }
        }

        public static readonly DependencyProperty AudiosSourceProperty =
            DependencyProperty.Register("Audios", typeof(ObservableCollection<VKAudio>), typeof(AudiosPresenter), new PropertyMetadata(default(ObservableCollection<VKAudio>)));
        #endregion

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var list = GetTemplateChild(ListPresenterName) as ListViewBase;
            list.ItemClick += (s, e) =>
            {
                Messenger.Default.Send(new PlayTrackMessage { Tracks = Audios, TrackToPlay = (IAudioTrack)e.ClickedItem });
                //NavigationHelper.Navigate(AppViews.PlayerView);
            };
            Tapped += (s, e) => e.Handled = true;
        }
    }
}
