using GalaSoft.MvvmLight.Messaging;
using OneVK.Core;
using OneVK.Core.Collections;
using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Model.Group;
using OneVK.Request;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления профиля сообщества.
    /// </summary>
    public class GroupInfoViewModel : BaseKeyedViewModel<GroupInfoViewModel>, IContentViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором
        /// модели представления и идентификатором сообщества.
        /// </summary>
        public GroupInfoViewModel(string uniqueKey, ulong groupID)
            : base(uniqueKey)
        {
            _groupID = groupID;
            _wall = new WallCollection(-(long)groupID);            
        }
        #endregion

        #region Приватные поля
        private readonly ulong _groupID;
        private ContentState _profileState;
        private VKGroupExtended _info;
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
        /// Индекс текущего элемента в Pivot.
        /// </summary>
        public int CurrentPivotIndex { get; set; }
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
        /// Возвращает информацию о текущем сообществе.
        /// </summary>
        public VKGroupExtended Info
        {
            get { return _info; }
            set { Set(() => Info, ref _info, value); }
        }
        /// <summary>
        /// Коллекция стены.
        /// </summary>
        public WallCollection Wall { get { return _wall; } }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Активирует модель представления.
        /// </summary>
        public override void Activate(NavigationMode mode = NavigationMode.New)
        {
            if (_groupID == 88111936 && IsLoaded)
            {
                var pop = new PopupMessage()
                {
                    Title = "Сообщество может нанести непоправимый вред здоровью",
                    Content = Info.Name,
                    ImageUrl = Info.Photo50,
                    Type = PopupMessageType.Warning
                };
                Messenger.Default.Send(pop);
            }
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
            var response = await (new GetGroupsByIDRequest(_groupID.ToString())).ExecuteAsync();
            if (response.Error.ErrorType == VKErrors.None)
            {
                if (response.Response.Count != 0)
                {
                    Info = response.Response[0];
                    ProfileState = ContentState.Normal;

                    if (_groupID == 88111936)
                    {
                        var pop = new PopupMessage()
                        {
                            Title = "Сообщество может нанести непоправимый вред здоровью",
                            Content = Info.Name,
                            ImageUrl = Info.Photo50,
                            Type = PopupMessageType.Warning
                        };
                        Messenger.Default.Send(pop);
                    }
                }
                else
                    ProfileState = ContentState.NoData;
            }
            else
                ProfileState = ContentState.Error;
        }
        #endregion
    }
}
