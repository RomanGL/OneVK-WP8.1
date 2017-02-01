using System;
using System.Linq;
using System.Threading.Tasks;
using OneVK.Model.Message;
using OneVK.Helpers;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;
using GalaSoft.MvvmLight;
using System.Diagnostics;
using OneVK.Enums.Common;
using OneVK.Enums.App;
using OneVK.Request.Execute;
using OneVK.Service;

namespace OneVK.Core.Collections
{
    /// <summary>
    /// Представляет коллекцию диалогов пользователя с поддержкой инкрементной загрузки.
    /// </summary>
    public class DialogsCollectionOld : StateSupportCollection<VKDialog>, ISupportIncrementalLoading
    {
        private bool _canLoad = true;
        private ulong _totalCount = ulong.MaxValue;
        private readonly MessagesService _service = ServiceHelper.MessagesService;

        /// <summary>
        /// Обновить значение возможности подгрузки.
        /// </summary>
        private void UpdateCanLoad() { _canLoad = (ulong)Count < _totalCount; }

        /// <summary>
        /// Возвращает значение, указывающее на возможность загрузки.
        /// </summary>
        public bool HasMoreItems
        {
            get
            {
#if DEBUG
                if (ViewModelBase.IsInDesignModeStatic) return false;
#endif
                return _canLoad;
            }
        }

        /// <summary>
        /// Загрузить порцию элементов.
        /// </summary>
        /// <param name="count">Количество элементов для загрузки.</param>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return Task.Run<LoadMoreItemsResult>(async () =>
                {
                    State = ContentState.Loading;

                    uint resultCount = 0;
                    _canLoad = false;

                    if (count < 20) count = 20;
                    else if (count > 60) count = 60;

                    var request = new ExecuteGetDialogsRequest() { Count = count, Offset = (uint)Count, PreviewLength = 100 };
                    var result = await request.ExecuteAsync();

                    await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {           
                            if (result.Error.ErrorType == VKErrors.None)
                            {
                                _totalCount = (ulong)result.Response.Dialogs.Count;

                                for (int i = 0; i < result.Response.Dialogs.Items.Count; i++)
                                {
                                    var dialog = result.Response.Dialogs.Items[i];
                                    if (dialog.IsChat)
                                    {
                                        this.Add(dialog);
                                        continue;
                                    }

                                    var user = result.Response.Users.First(p => p.ID == dialog.Message.UserID);
                                    dialog.Message.Title = user.FullName;
                                    this.Add(dialog);
                                }

                                State = Count == 0 ? ContentState.NoData : ContentState.Normal;
                                UpdateCanLoad();
                            }
                            else
                            {
                                State = ContentState.Error;
                                _canLoad = false;
                            }
                        });

                    await ServiceHelper.MessagesCacheService.CacheDialogs(this.Take(30).ToList());
#if DEBUG
                    Debug.WriteLine("Total dialogs count: " + this.Count);
#endif
                    return new LoadMoreItemsResult { Count = resultCount };
                }).AsAsyncOperation<LoadMoreItemsResult>();
        }

        /// <summary>
        /// Загрузить следующую порцию элементов.
        /// </summary>
        public override async void Load()
        {
            await LoadMoreItemsAsync(20);
        }

        /// <summary>
        /// Сбрасывает состояние коллекции.
        /// </summary>
        public void Reset()
        {
            Clear();
            _totalCount = ulong.MaxValue;
            _canLoad = true;
        }
    }
}
