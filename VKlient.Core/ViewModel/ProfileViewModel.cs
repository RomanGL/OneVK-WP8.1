using GalaSoft.MvvmLight.Command;
using OneVK.Core.Collections;
using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Wall;
using OneVK.Request.Execute;
using OneVK.Response.Execute;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления профиля пользователя.
    /// </summary>
    public class ProfileViewModel : BaseKeyedViewModel<ProfileViewModel>, IContentViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором
        /// модели представления и идентификатором пользователя.
        /// </summary>
        public ProfileViewModel(string uniqueKey, ulong userID)
            : base(uniqueKey)
        {
            _userID = userID;
            _wall = new WallCollection((long)userID);
            NewPost = new RelayCommand(() => NavigationHelper.Navigate(AppViews.NewPostView, _userID));
            NewMessage = new RelayCommand(() => NavigationHelper.Navigate(AppViews.ConversationView, (long)_userID));
        }
        #endregion

        #region Приватные поля
        private readonly ulong _userID;
        private ContentState _profileState;
        private ExecuteGetProfileInfoResponse _info;
        private WallCollection _wall;
        private double _wallScrollOffset;
        private int _firstVisibleIndex;
        #endregion

        #region Свойства
        /// <summary>
        /// Индекс первого видимого элемента.
        /// </summary>
        public int FirstVisibleIndex
        {
            get { return _firstVisibleIndex; }
            set { Set(() => FirstVisibleIndex, ref _firstVisibleIndex, value); }
        }
        /// <summary>
        /// Возвращает или задает смещение прокрутки страницы.
        /// </summary>
        public double WallScrollOffset
        {
            get { return _wallScrollOffset; }
            set { Set(() => WallScrollOffset, ref _wallScrollOffset, value); }
        }
        /// <summary>
        /// Загружены ли данные в модель представления.
        /// </summary>
        public bool IsLoaded
        {
            get { return GetIsLoaded(ProfileState); }
        }
        /// <summary>
        /// Выполняется ли в данный момент загрузка данных.
        /// </summary>
        public bool IsLoading
        {
            get { return ProfileState == ContentState.Loading; }
        }
        /// <summary>
        /// Состояние загрузки информации профиля.
        /// </summary>
        public ContentState ProfileState
        {
            get { return _profileState; }
            private set { Set(() => ProfileState, ref _profileState, value); }
        }
        /// <summary>
        /// Возвращает информацию о текущем пользователе.
        /// </summary>
        public ExecuteGetProfileInfoResponse Info
        {
            get { return _info; }
            set { Set(() => Info, ref _info, value); }
        }
        /// <summary>
        /// Коллекция стены.
        /// </summary>
        public WallCollection Wall { get { return _wall; } }
        #endregion

        #region Команды
        /// <summary>
        /// Команда создания нового поста на стене.
        /// </summary>
        public RelayCommand NewPost { get; private set; }
        /// <summary>
        /// Команда открытия беседы.
        /// </summary>
        public RelayCommand NewMessage { get; private set; }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Активирует модель представления.
        /// </summary>
        public override void Activate(NavigationMode mode = NavigationMode.New)
        {
            LoadData();
        }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Загружает информацию о пользователе.
        /// </summary>
        private async void LoadData()
        {
            if (IsLoaded || IsLoading) return;

            ProfileState = ContentState.Loading;
            var response = await (new ExecuteGetProfileInfoRequest(_userID)).ExecuteAsync();
            if (response.Error.ErrorType == VKErrors.None)
            {
                Info = response.Response;
                ProfileState = ContentState.Normal;
            }
            else
                ProfileState = ContentState.Error;
        }
        #endregion        
    }
}
