using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using OneVK.Core.Collections;
using OneVK.Model.LongPoll;
using OneVK.Core;
using OneVK.Request;
using OneVK.Core.Messages;
using System.Collections.ObjectModel;
using OneVK.Model.Message;
using OneVK.Helpers;
using OneVK.Enums.Message;
using OneVK.Enums.Common;
using NotificationsExtensions.TileContent;
using OneVK.Enums.App;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;
using OneVK.ViewModel.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace OneVK.ViewModel
{
    public abstract class BaseConversationViewModel : BaseViewModel, IConversationViewModel
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseConversationViewModel"/>.
        /// </summary>
        public BaseConversationViewModel()
        {
            Attachments = new ObservableCollection<VKMessageAttachment>();
            ReloadCommand = new RelayCommand(() => Messages.Load());
            RefreshCommand = new RelayCommand(() => Messages.Refresh());
            SendMessageCommand = new RelayCommand(async () => await SendMessage(), 
                () => (!String.IsNullOrWhiteSpace(EnteredText) || Attachments.Count > 0) && Messages != null);
            PinToDesktop = new RelayCommand(PinTile);
            AddAttachments = new RelayCommand(() =>
            {
                Messenger.Default.Register<AddAttachmentsMessage>(this, ProcessAttachments);
                NavigationHelper.Navigate(AppViews.AttachmentsManagerView);
            });
        }

        #region Приватные поля
        private string _title;
        private string _enteredText;
        private IConversation _conversation;
        #endregion
        
        /// <summary>
        /// Информация о беседе.
        /// </summary>
        public IConversation Conversation
        {
            get { return _conversation; }
            protected set { Set(() => Conversation, ref _conversation, value); }
        }

        /// <summary>
        /// Введенный пользователем текст.
        /// </summary>
        public string EnteredText
        {
            get { return _enteredText; }
            set
            {
                Set(() => EnteredText, ref _enteredText, value);
                SendMessageCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Вложения в сообщение.
        /// </summary>
        public ObservableCollection<VKMessageAttachment> Attachments { get; private set; }

        /// <summary>
        /// Коллекция сообщений.
        /// </summary>
        public MessagesCollection Messages { get; protected set; }

        /// <summary>
        /// Возвращает команду отправки сообщения.
        /// </summary>
        public RelayCommand SendMessageCommand { get; private set; }
        /// <summary>
        /// Возвращает команду перезагрузки списка при ошибке.
        /// </summary>
        public RelayCommand ReloadCommand { get; private set; }
        /// <summary>
        /// Возвращает команду перезагрузки списка сообщений.
        /// </summary>
        public RelayCommand RefreshCommand { get; private set; }
        /// <summary>
        /// Открыть элемент по аватару беседы.
        /// </summary>
        public RelayCommand OpenConversationAvatar { get; protected set; }
        /// <summary>
        /// Команда добавления вложений в сообщение.
        /// </summary>
        public RelayCommand AddAttachments { get; private set; }
        /// <summary>
        /// Команда загрепления беседы на рабочем столе.
        /// </summary>
        public RelayCommand PinToDesktop { get; private set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public abstract ulong UserID { get; }
        /// <summary>
        /// Идентификатор чата.
        /// </summary>
        public abstract uint ChatID { get; }
        /// <summary>
        /// Является ли диалог чатом.
        /// </summary>
        public abstract bool IsChat { get; }

        /// <summary>
        /// Отправляет сообщение в беседу.
        /// </summary>
        private async Task SendMessage()
        {
            var text = EnteredText.Trim();
            EnteredText = String.Empty;
            var msg = new VKMessage
            {
                Body = text,
                Date = DateTime.Now,
                ChatID = IsChat ? ChatID : 0,
                UserID = ServiceHelper.SettingsService.AccessToken.UserID,
                Type = VKMessageType.Sent,
                SentType = VKSentMessageType.Sending,
                Attachments = Attachments.ToList()
            };

            Messages.Insert(0, msg);
            Messages.NewSentMessages.Add(msg);

            var param = new Dictionary<string, string>();
            param["message"] = text;

            if (IsChat) param["chat_id"] = ChatID.ToString();
            else param["user_id"] = UserID.ToString();

            var request = new UniversalVKRequest<ulong>(VKMethodsConstants.MessageSend, param);
            var response = await request.ExecuteAsync();

            if (response.Error.ErrorType == VKErrors.None)
            {
                msg.ID = response.Response;
                msg.SentType = VKSentMessageType.Unread;
            }
            else
                msg.SentType = VKSentMessageType.Error;
        }

        /// <summary>
        /// Прикрепляет тайл беседы к рабочему столу.
        /// </summary>
        private async void PinTile()
        {
            if (Conversation.Type == ConversationType.Chat)
            {
                var chat = (IChat)Conversation;

                //var wideTile = TileContentFactory.CreateTileWide310x150PeekImageCollection01();
                //var squareTile = TileContentFactory.CreateTileSquare150x150PeekImageAndText01();
                //wideTile.TextHeading.Text = chat.Title;
                //squareTile.TextHeading.Text = chat.Title;

                //if (chat.Users.Count == 1)
                //{
                //    wideTile.ImageMain.Src = chat.Users[0].Photo50;
                //    squareTile.Image.Src = chat.Users[0].Photo50;
                //}
                //if (chat.Users.Count == 2)
                //    wideTile.ImageSmallColumn1Row1.Src = chat.Users[1].Photo50;
                //if (chat.Users.Count == 3)
                //    wideTile.ImageSmallColumn1Row2.Src = chat.Users[2].Photo50;
                //if (chat.Users.Count == 4)
                //    wideTile.ImageSmallColumn2Row1.Src = chat.Users[3].Photo50;
                //if (chat.Users.Count == 5)
                //    wideTile.ImageSmallColumn2Row2.Src = chat.Users[4].Photo50;

                //wideTile.Square150x150Content = squareTile;

                var sTile = new SecondaryTile("conv" + Conversation.ID.ToString(), chat.Title, chat.Title, chat.ID.ToString(),
                    TileOptions.ShowNameOnLogo | TileOptions.ShowNameOnWideLogo, new Uri("ms-appx:///Assets/Logo.scale-240.png"), new Uri("ms-appx:///Assets/Logo.scale-240.png"));
                await sTile.RequestCreateAsync();
            }
            else
            {
                var dialog = (IDialog)Conversation;
                //var response = await (new GetUsersRequest { UserID = dialog.UserID }).ExecuteAsync();
                //if (response.Error.ErrorType != VKErrors.None)
                //{
                //    CoreHelper.SendInAppPush("Не удалось закрепить тайл", null, PopupMessageType.Error, null, TimeSpan.FromSeconds(5));
                //    return;
                //}

                var sTile = new SecondaryTile("conv" + Conversation.ID.ToString(), dialog.Title, dialog.Title, dialog.ID.ToString(),
                    TileOptions.ShowNameOnLogo | TileOptions.ShowNameOnWideLogo, new Uri("ms-appx:///Assets/Logo.scale-240.png"), new Uri("ms-appx:///Assets/Logo.scale-240.png"));
                await sTile.RequestCreateAsync();
            }
        }

        /// <summary>
        /// Обрабатывает добавление новых вложений в сообщение.
        /// </summary>
        /// <param name="msg">Сообщение с информацией.</param>
        private void ProcessAttachments(AddAttachmentsMessage msg)
        {

        }
    }
}
