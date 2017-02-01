namespace OneVK.Enums.App
{
    /// <summary>
    /// Перечисление представлений приложения.
    /// </summary>
    public enum AppViews : byte
    {
        /// <summary>
        /// Неизвестное представление.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Доступ к приложению отсутствует.
        /// </summary>
        AccessDeniedView = 1,
        /// <summary>
        /// Представление браузерной авторизации.
        /// </summary>
        LoginView,
        /// <summary>
        /// Представление новостей.
        /// </summary>
        NewsView,
        /// <summary>
        /// Представление профиля пользователя.
        /// </summary>
        ProfileView,
        /// <summary>
        /// Представление списка друзей.
        /// </summary>
        FriendsView,
        /// <summary>
        /// Представление списка аудиозаписей.
        /// </summary>
        AudiosView,
        /// <summary>
        /// Представление списка видеозаписей.
        /// </summary>
        VideosView,
        /// <summary>
        /// Представление страницы сведений о видеозаписи.
        /// </summary>
        VideoInfoView,
        /// <summary>
        /// Представление страницы видеоальбома.
        /// </summary>
        VideoAlbumView,
        /// <summary>
        /// Представление диалогов пользователя.
        /// </summary>
        MessagesView,
        /// <summary>
        /// Представление списка фотографий.
        /// </summary>
        PhotosView,
        /// <summary>
        /// Представление ответов пользователю.
        /// </summary>
        FeedbackView,
        /// <summary>
        /// Представление закладок пользователя.
        /// </summary>
        FavoritesView,
        /// <summary>
        /// Представление списка групп.
        /// </summary>
        GroupsView,
        /// <summary>
        /// Представление для отображения исключения.
        /// </summary>
        ErrorView,
        /// <summary>
        /// Представление просмотра записи (поста).
        /// </summary>
        PostView,
        /// <summary>
        /// Представление просмотра изображений.
        /// </summary>
        ImageView,
        /// <summary>
        /// Представление настроек.
        /// </summary>
        SettingsView,
        /// <summary>
        /// Тестовое представление.
        /// </summary>
        TestView,
        /// <summary>
        /// Представление бота ВКонтакте.
        /// </summary>
        BotView,
        /// <summary>
        /// Представление страницы сведений о сообществе.
        /// </summary>
        GroupInfoView,
        /// <summary>
        /// Представление смены статуса.
        /// </summary>
        ChangeStatusView,
        /// <summary>
        /// Представление текста трека.
        /// </summary>
        TrackLyricsView,
        /// <summary>
        /// Представление проигрывателя.
        /// </summary>
        PlayerView,
        /// <summary>
        /// Страница создания нового поста.
        /// </summary>
        NewPostView,
        /// <summary>
        /// Представление страницы настроек уведомлений.
        /// </summary>
        NotificationsSettingsView,
        /// <summary>
        /// Представление страницы общих настроек приложения.
        /// </summary>
        CommonSettingsView,
        /// <summary>
        /// Представление страницы диалога или чата.
        /// </summary>
        ConversationView,
        /// <summary>
        /// Представление с информацией о приложении.
        /// </summary>
        AboutView,
        /// <summary>
        /// Представление параметров чата.
        /// </summary>
        ChatSettingsView,
        /// <summary>
        /// Представление выбора друзей.
        /// </summary>
        ChoiceFriendsView,
        /// <summary>
        /// Представление выбора аудиозаписей.
        /// </summary>
        ChoiceAudiosView,
        /// <summary>
        /// Представление выбора документов.
        /// </summary>
        ChoiceDocsView,
        /// <summary>
        /// Представление выбора видеозаписей.
        /// </summary>
        ChoiceVideoView,
        /// <summary>
        /// Страница рекламной акции.
        /// </summary>
        PromoView,
        /// <summary>
        /// Страница всех рекламных акций.
        /// </summary>
        AllPromosView,
        /// <summary>
        /// Страница менеджера вложений.
        /// </summary>
        AttachmentsManagerView
    }
}
