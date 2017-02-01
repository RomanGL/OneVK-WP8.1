using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using OneVK.Core;
using OneVK.Core.Collections;
using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Enums.LongPoll;
using OneVK.Enums.Message;
using OneVK.Enums.Profile;
using OneVK.Helpers;
using OneVK.Model.LongPoll;
using OneVK.Model.Message;
using OneVK.Model.Profile;
using OneVK.Request;
using OneVK.ViewModel;
using OneVK.ViewModel.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис работы с сообщениями внутри OneVK.
    /// </summary>
    internal class MessagesService
    {
        public MessagesService()
        {
            CachedUsers = new Dictionary<ulong, VKProfileChat>();
            Dialogs = new DialogsCollection();
            Conversations = new Dictionary<CachedConversationOld, MessagesCollection>();
            GetCachedDialogs().Wait();
            //ServiceHelper.VKLongPollService.MessagesReceived += VKLongPollService_MessagesReceived;
            ServiceHelper.VKLongPollService.UserIsTyping += VKLongPollService_UserIsTyping;
        }

        /// <summary>
        /// Возвращает список кэшированных пользователей.
        /// </summary>
        public Dictionary<ulong, VKProfileChat> CachedUsers { get; private set; }
        /// <summary>
        /// Возвращает коллекцию диалогов.
        /// </summary>
        public DialogsCollection Dialogs { get; private set; }
        /// <summary>
        /// Возвращает словарь открытых бесед.
        /// </summary>
        internal Dictionary<CachedConversationOld, MessagesCollection> Conversations { get; private set; }

        /// <summary>
        /// Получает кэшированные данные.
        /// </summary>
        private async Task GetCachedDialogs()
        {
            var dialogs = await ServiceHelper.MessagesCacheService.GetDialogsFromCache();
            if (dialogs != null)
            {
                for (int i = 0; i < dialogs.Count; i++)
                    Dialogs.Add(dialogs[i]);
            }
        }

        /// <summary>
        /// Возвращает кортеж из беседы и коллекции сообщений.
        /// </summary>
        /// <param name="convID">Идентификатор беседы.</param>
        public async Task<Tuple<CachedConversationOld, MessagesCollection>> GetMessages(long convID)
        {
            var pair = Conversations.FirstOrDefault(p => p.Key.ID == convID);
            if (pair.Key != null && pair.Value != null)
                return new Tuple<CachedConversationOld, MessagesCollection>(pair.Key, pair.Value);

            MessagesCollection msgs = null;
            if (convID < 0) msgs = new MessagesCollection((uint)-convID);
            else msgs = new MessagesCollection((ulong)convID);

            var cache = await ServiceHelper.MessagesCacheService.GetConversationFromCache(convID);
            if (cache != null)
            {
                for (int i = 0; i < cache.Users.Count; i++)
                {
                    ulong userID = cache.Users[i].ID;
                    var user = CachedUsers.Values.FirstOrDefault(u => u.ID == userID);

                    if (user != null) cache.Users[i] = user;
                    else CachedUsers[userID] = cache.Users[i];
                }

                foreach (var msg in cache.CachedMessages)
                {
                    if (cache.Users != null)
                        msg.Sender = cache.Users.FirstOrDefault(u => u.ID == msg.UserID);
                    msgs.Add(msg);
                }
                msgs.Unread = cache.Unread;
                Conversations[cache] = msgs;
                
                return new Tuple<CachedConversationOld, MessagesCollection>(cache, msgs);
            }

            if (convID < 0)
            {
                bool success = false;
                byte numOfRetries = 0;
                while (!success && numOfRetries <= 3)
                {
                    var request = new GetChatRequest()
                    {
                        ChatID = -convID,
                        IsSingle = true,
                        Fields = new List<VKUserFields> { VKUserFields.photo_50, VKUserFields.online, VKUserFields.sex }
                    };
                    var response = await request.ExecuteAsync();

                    if (response.Error.ErrorType == VKErrors.None)
                    {
                        success = true;
                        cache = new CachedConversationOld
                        {
                            ID = convID,
                            Title = response.Response.Title,
                            Users = new ObservableCollection<VKProfileChat>(response.Response.Users)
                        };
                        Conversations[cache] = msgs;
                        foreach (var user in cache.Users)
                            CachedUsers[user.ID] = user;

                        //msgs.UpdateSimilar();
                        return new Tuple<CachedConversationOld, MessagesCollection>(cache, msgs);
                    }
                    else
                    {
                        numOfRetries++;
                        CoreHelper.SendInAppPush(
                            "Ошибка соединения.\nПовтор через " + 3 * numOfRetries + " секунд",
                            null, PopupMessageType.Warning);
                        await Task.Delay(3000 * numOfRetries);
                    }
                }

                return null;
            }
            else
            {
                bool success = false;
                byte numOfRetries = 0;
                while (!success && numOfRetries <= 3)
                {
                    var request = new GetUsersRequest { UserID = (ulong)convID };
                    var response = await request.ExecuteAsync();

                    if (response.Error.ErrorType == VKErrors.None)
                    {
                        success = true;
                        var user = response.Response[0];
                        cache = new CachedConversationOld
                        {
                            ID = convID,
                            Title = user.FullName,
                            Users = new ObservableCollection<VKProfileChat>
                            {
                                new VKProfileChat
                                {
                                    ID = user.ID,
                                    LastName = user.LastName,
                                    FirstName = user.FirstName,
                                    Photo50 = user.Photo50,
                                    Online = user.Online,
                                    Sex = user.Sex
                                }
                            }
                        };
                        Conversations[cache] = msgs;
                        CachedUsers[user.ID] = cache.Users[0];
                        
                        return new Tuple<CachedConversationOld, MessagesCollection>(cache, msgs);
                    }
                    else
                    {
                        numOfRetries++;
                        CoreHelper.SendInAppPush(
                            "Ошибка соединения.\nПовтор через " + 3 * numOfRetries + " секунд",
                            null, PopupMessageType.Warning);
                        await Task.Delay(3000 * numOfRetries);
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Кэшировать беседу с указанным идентификатором.
        /// </summary>
        /// <param name="convID">Идентификатор беседы.</param>
        public async Task<bool> CacheConversation(long convID)
        {
            var cachedConv = Conversations.Keys.FirstOrDefault(c => c.ID == convID);
            if (cachedConv == null) return false;

            var list = Conversations[cachedConv];
            cachedConv.CachedMessages = new List<VKMessage>(list.Take(30));
            cachedConv.Unread = list.Unread;
            return await ServiceHelper.MessagesCacheService.CacheConversation(cachedConv.ID, cachedConv);
        }

        /// <summary>
        /// Вызывается при получении новых сообщений через LongPoll.
        /// </summary>
        private async void VKLongPollService_MessagesReceived(object sender, MessagesReceivedEventArgs e)
        {
            var ids = new ulong[e.Messages.Count];
            for (int i = 0; i < e.Messages.Count; i++)
                ids[i] = e.Messages[i].MessageID;

            List<VKMessage> resultMessages = null;            

            bool success = false;
            byte numOfRetries = 0;
            while (!success && numOfRetries <= 3)
            {
                var request = new GetByIDRequest { MessageIDs = ids.ToList() };
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType == VKErrors.None)
                {
                    success = true;
                    resultMessages = response.Response.Items;
                }
                else
                {
                    numOfRetries++;
                    CoreHelper.SendInAppPush(
                        "Ошибка соединения.\nПовтор через " + 3 * numOfRetries + " секунд",
                        null, PopupMessageType.Warning);
                    await Task.Delay(3000 * numOfRetries);
                }
            }
            
            if (!success)
            {
                CoreHelper.SendInAppPush(
                        "Не удалось получить подробные сообщения",
                        "LongPoll", PopupMessageType.Error);
                return;
            }

            var unknownUsers = new Dictionary<ulong, List<VKMessage>>();
            
            var conversations = new Dictionary<long, List<VKMessage>>();
            foreach (var msg in resultMessages)
            {
                long key = msg.IsChatMessage ? -msg.ChatID : (long)msg.UserID;
                if (conversations.ContainsKey(key))
                    conversations[key].Add(msg);
                else
                {
                    var list = new List<VKMessage>();
                    list.Add(msg);
                    conversations[key] = list;
                }

                if (CachedUsers.ContainsKey(msg.UserID))
                    msg.Sender = CachedUsers[msg.UserID];
                else
                {
                    if (unknownUsers.ContainsKey(msg.UserID))
                        unknownUsers[msg.UserID].Add(msg);
                    else
                    {
                        var list = new List<VKMessage>();
                        list.Add(msg);
                        unknownUsers[msg.UserID] = list;
                    }
                }
            }

            success = unknownUsers.Count == 0;
            numOfRetries = 0;
            while (!success && numOfRetries <= 3)
            {
                var usersRequest = new GetUsersRequest() { UserIDs = unknownUsers.Keys.ToList() };
                var usersResponse = await usersRequest.ExecuteAsync();

                if (usersResponse.Error.ErrorType == VKErrors.None)
                {
                    success = true;
                    foreach (var user in usersResponse.Response)
                    {
                        var sUser = new VKProfileChat()
                        {
                            ID = user.ID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Online = user.Online,
                            Photo50 = user.Photo50
                        };

                        var list = unknownUsers[user.ID];
                        for (int i = 0; i < list.Count; i++)
                            list[i].Sender = sUser;

                        CachedUsers[sUser.ID] = sUser;
                    }
                }
                else
                {
                    numOfRetries++;
                    CoreHelper.SendInAppPush(
                        "Ошибка соединения.\nПовтор через " + 3 * numOfRetries + " секунд",
                        null, PopupMessageType.Warning);
                    await Task.Delay(3000 * numOfRetries);
                }
            }

            if (!success)
            {
                CoreHelper.SendInAppPush(
                        "Не удалось получить информацию о пользователях сообщений",
                        null, PopupMessageType.Error);
            }
            
            foreach (long convID in conversations.Keys)
            {
                var msgsList = conversations[convID];
                var cachedConv = Conversations.Keys.FirstOrDefault(c => c.ID == convID);

                if (cachedConv != null)
                {
                    var list = Conversations[cachedConv];
                    foreach (var msg in msgsList)
                        list.Insert(0, msg);                    
                }
                else
                {
                    MessagesCollection list = null;
                    if (convID < 0) list = new MessagesCollection((uint)-convID);
                    else list = new MessagesCollection((ulong)convID);

                    cachedConv = await ServiceHelper.MessagesCacheService.GetConversationFromCache(convID);
                    if (cachedConv != null)
                    {
                        foreach (var msg in cachedConv.CachedMessages)
                            list.Add(msg);
                    }
                    else
                    {
                        if (convID < 0)
                        {
                            success = false;
                            numOfRetries = 0;
                            while (!success && numOfRetries <= 3)
                            {
                                var request = new GetChatRequest()
                                {
                                    ChatID = -convID,
                                    IsSingle = true,
                                    Fields = new List<VKUserFields> { VKUserFields.photo_50, VKUserFields.online, VKUserFields.sex }
                                };
                                var response = await request.ExecuteAsync();

                                if (response.Error.ErrorType == VKErrors.None)
                                {
                                    success = true;
                                    cachedConv = new CachedConversationOld
                                    {
                                        ID = convID,
                                        Title = response.Response.Title,
                                        Users = new ObservableCollection<VKProfileChat>(response.Response.Users),
                                        CachedMessages = msgsList,
                                        AdminID = response.Response.AdminID
                                    };
                                    Conversations[cachedConv] = list;

                                    foreach (var user in cachedConv.Users)
                                        CachedUsers[user.ID] = user;
                                }
                                else
                                {
                                    numOfRetries++;
                                    CoreHelper.SendInAppPush(
                                        "Ошибка соединения.\nПовтор через " + 3 * numOfRetries + " секунд",
                                        null, PopupMessageType.Warning);
                                    await Task.Delay(3000 * numOfRetries);
                                }
                            }
                        }
                        else
                        {
                            success = false;
                            numOfRetries = 0;
                            while (!success && numOfRetries <= 3)
                            {
                                var request = new GetUsersRequest { UserID = (ulong)convID };
                                var response = await request.ExecuteAsync();

                                if (response.Error.ErrorType == VKErrors.None)
                                {
                                    success = true;
                                    var user = response.Response[0];
                                    cachedConv = new CachedConversationOld
                                    {
                                        ID = convID,
                                        Title = user.FullName,
                                        Users = new ObservableCollection<VKProfileChat>
                                        {
                                            new VKProfileChat
                                            {
                                                ID = user.ID,
                                                LastName = user.LastName,
                                                FirstName = user.FirstName,
                                                Photo50 = user.Photo50,
                                                Online = user.Online
                                            }
                                        }
                                    };
                                    CachedUsers[user.ID] = cachedConv.Users[0];
                                }
                                else
                                {
                                    numOfRetries++;
                                    CoreHelper.SendInAppPush(
                                        "Ошибка соединения.\nПовтор через " + 3 * numOfRetries + " секунд",
                                        null, PopupMessageType.Warning);
                                    await Task.Delay(3000 * numOfRetries);
                                }
                            }
                        }                        
                    }

                    foreach (var msg in msgsList)
                        list.Insert(0, msg);

                    //list.UpdateSimilar();
                    Conversations[cachedConv] = list;
                }

                await CacheConversation(cachedConv.ID);

                WorkOnDialog(msgsList.Last());

                var lastMsg = msgsList.LastOrDefault(m => m.Type == VKMessageType.Received);
                if (lastMsg == null) continue;

                var navParameter = new NavigateToPageMessage
                {
                    Page = AppViews.ConversationView,
                    Parameter = convID
                };

                CoreHelper.SendInAppPush(lastMsg.Body,
                    cachedConv.Title, PopupMessageType.Default,
                    lastMsg.Sender.Photo50, parameter: navParameter);
            }

            await ServiceHelper.MessagesCacheService.CacheDialogs(Dialogs.Take(20).ToList());                                    
        }

        /// <summary>
        /// Вызывается, когда пользователь начал набирать текст в диалоге или в чате.
        /// </summary>
        private void VKLongPollService_UserIsTyping(object sender, UserIsTypingEventArgs e)
        {
            MessagesCollection msgs = null;
            if (e.Info.IsChat) msgs = Conversations.Values.FirstOrDefault(m => m.ChatID == e.Info.ChatID);
            else msgs = Conversations.Values.FirstOrDefault(m => m.UserID == e.Info.UserID);

            if (msgs != null) msgs.SetUserIsTyping(e.Info.UserID);
        }

        /// <summary>
        /// Обработать диалог для переданного сообщения.
        /// </summary>
        /// <param name="lastMessage">Последнее сообщение в диалоге.</param>
        private async void WorkOnDialog(VKMessage lastMessage)
        {
            if (Dialogs.Count == 0) return;

            VKDialog dialog = null;
            if (lastMessage.IsChatMessage)
                dialog = Dialogs.FirstOrDefault(d => d.Message.ChatID == lastMessage.ChatID);
            else dialog = Dialogs.FirstOrDefault(d => !d.Message.IsChatMessage && d.Message.UserID == lastMessage.UserID);

            if (dialog == null) return;

            dialog.Unread += 1;
            dialog.Message.Body = lastMessage.Body;
            dialog.Message.Date = lastMessage.Date;
            dialog.Message.Attachments = lastMessage.Attachments;
            dialog.Message.Sender = lastMessage.Sender;
            dialog.Message.UserID = lastMessage.UserID;
            dialog.Message.ChatID = lastMessage.ChatID;
            dialog.Message.Type = lastMessage.Type;
            dialog.Message.ReadState = lastMessage.ReadState;
            dialog.Message.Geo = lastMessage.Geo;
            dialog.Message.Action = lastMessage.Action;
            dialog.Message.ActionEmail = lastMessage.ActionEmail;
            dialog.Message.ActionMid = lastMessage.ActionMid;
            dialog.Message.ActionText = lastMessage.ActionText;
            dialog.Message.ID = lastMessage.ID;
            dialog.Message.IsDeleted = lastMessage.IsDeleted;
            dialog.Message.ForwardedMessages = lastMessage.ForwardedMessages;
            dialog.Message.HasEmoji = lastMessage.HasEmoji;
            dialog.Message.IsImportant = lastMessage.IsImportant;

            lock (Dialogs)
            {
                var temp = Dialogs[0];
                int index = Dialogs.IndexOf(dialog);
                Dialogs[0] = Dialogs[index];
                Dialogs[index] = temp;
            }
        }
    }
}
