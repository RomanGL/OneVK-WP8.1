using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using OneVK.Core.Collections;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Audio;
using VKSaver.Commands.SDK;

namespace OneVK.ViewModel
{
    public class AudiosViewModel : BaseKeyedViewModel<AudiosViewModel>
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public AudiosViewModel(string uniqueKey, long ownerID)
            : base(uniqueKey)
        {
            _audios = new AudiosCollection(ownerID);
            _recommendations = new RecommendedAudiosCollection((ulong)ownerID);
            _popular = new PopularAudiosCollection();
            _albums = new AudioAlbumsCollection(ownerID);
            Refresh = new RelayCommand(() => Audios.Refresh());
            RefreshAlbums = new RelayCommand(() => Albums.Refresh());
            RefreshPopular = new RelayCommand(() => Popular.Refresh());
            RefreshRecommended = new RelayCommand(() => Recommendations.Refresh());
            DeleteCommand = new RelayCommand<VKAudio>(async audio =>
            {
                var request = new Request.DeleteAudioRequest(audio.ID, audio.OwnerID);
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType == VKErrors.None)
                    Audios.Remove(audio);
                else
                    await ServiceHelper.DialogService.ShowMessageBox("Произошла ошибка: " + response.Error.ErrorType.ToString(),
                        "Не удалось удалить аудиозапись.");
            });
            DownloadAudio = new RelayCommand<VKAudio>(async audio =>
            {
                var command = new VKSaverDownloadCommand();
                command.Downloads.Add(CoreHelper.GetDownload(audio));

                await command.TryExecute();
            });

#if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
                for (int i = 0; i < 71; i++)
                {
                    Audios.Add(DesignDataHelper.GetAudio());
                    Recommendations.Add(DesignDataHelper.GetAudio());
                    Popular.Add(DesignDataHelper.GetAudio());

                }
#endif
        }
        #endregion

        #region Приватные поля
        private AudiosCollection _audios;
        private RecommendedAudiosCollection _recommendations;
        private PopularAudiosCollection _popular;
        private AudioAlbumsCollection _albums;
        private double _allAudiosScrollOffset;
        private double _albumsScrollOffset;
        private double _popularScrollOffset;
        private double _recommendationsScrollOffset;
        private int _pivotIndex;
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекия списка аудиозаписей.
        /// </summary>
        public AudiosCollection Audios { get { return _audios; } }
        /// <summary>
        /// Коллекция списка рекоммендованых аудиозаписей.
        /// </summary>
        public RecommendedAudiosCollection Recommendations { get { return _recommendations; } }
        /// <summary>
        /// Коллекция списка популярных аудиозаписей.
        /// </summary>
        public PopularAudiosCollection Popular { get { return _popular; } }
        /// <summary>
        /// Коллекция списка аудиоальбомов.
        /// </summary>
        public AudioAlbumsCollection Albums { get { return _albums; } }

        /// <summary>
        /// Смещение списка всех аудиозаписей по вертикали.
        /// </summary>
        public double AllAudiosScrollOffset
        {
            get { return _allAudiosScrollOffset; }
            set { Set(() => AllAudiosScrollOffset, ref _allAudiosScrollOffset, value); }
        }
        /// <summary>
        /// Смещение списка альбомов по вертикали.
        /// </summary>
        public double AlbumsScrollOffset
        {
            get { return _albumsScrollOffset; }
            set { Set(() => AlbumsScrollOffset, ref _albumsScrollOffset, value); }
        }
        /// <summary>
        /// Смещение списка популярных аудиозаписей по вертикали.
        /// </summary>
        public double PopularScrollOffset
        {
            get { return _popularScrollOffset; }
            set { Set(() => PopularScrollOffset, ref _popularScrollOffset, value); }
        }
        /// <summary>
        /// Смещение списка рекомендованных аудиозаписей по вертикали.
        /// </summary>
        public double RecommendationsScrollOffset
        {
            get { return _recommendationsScrollOffset; }
            set { Set(() => RecommendationsScrollOffset, ref _recommendationsScrollOffset, value); }
        }
        /// <summary>
        /// Индекс текущего элемента в элементе сводки.
        /// </summary>
        public int PivotIndex
        {
            get { return _pivotIndex; }
            set { Set(() => PivotIndex, ref _pivotIndex, value); }
        }
        #endregion

        #region Команды
        /// <summary>
        /// Команда обновления списка аудиозаписей.
        /// </summary>
        public RelayCommand Refresh { get; private set; }
        /// <summary>
        /// Команда удаления аудиозаписи.
        /// </summary>
        public RelayCommand<VKAudio> DeleteCommand { get; private set; }
        /// <summary>
        /// Команда обновления списка альбомов.
        /// </summary>
        public RelayCommand RefreshAlbums { get; private set; }
        /// <summary>
        /// Команда обновления списка популярных аудиозаписей.
        /// </summary>
        public RelayCommand RefreshPopular { get; private set; }
        /// <summary>
        /// Команда обновления списка рекомендованных аудиозаписей.
        /// </summary>
        public RelayCommand RefreshRecommended { get; private set; }
        /// <summary>
        /// Команда для загрузки трека.
        /// </summary>
        public RelayCommand<VKAudio> DownloadAudio { get; private set; }
        #endregion

        #region Приватные методы
        #endregion

        #region Публичные методы
        #endregion
    }
}
