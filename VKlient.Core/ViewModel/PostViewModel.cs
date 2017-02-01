using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using OneVK.Model.Newsfeed;
using OneVK.Model.Profile;
using OneVK.Model.Common;
using OneVK.Helpers;
using OneVK.Model.Wall;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Модель представления страницы просмотра записи (поста).
    /// </summary>
    public class PostViewModel : BaseKeyedViewModel<PostViewModel>
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public PostViewModel(string uniqueKey, BaseVKPost post)
            : base(uniqueKey, 0)
        {
            Post = post;
        }
        #endregion

        #region Приватные поля
        private BaseVKPost _post;
        private double _scrollPosition;
        #endregion

        #region Свойства
        /// <summary>
        /// Пост, который отображается в данный момент.
        /// </summary>
        public BaseVKPost Post
        {
            get { return _post; }
            private set { Set(() => Post, ref _post, value); }
        }
        /// <summary>
        /// Позиция прокрутки на странице.
        /// </summary>
        public double ScrollPosition
        {
            get { return _scrollPosition; }
            set { Set(() => ScrollPosition, ref _scrollPosition, value); }
        }
        #endregion

        #region Команды
        #endregion

        #region Публичные методы
        #endregion

        #region Приватные методы
        #endregion
    }
}
