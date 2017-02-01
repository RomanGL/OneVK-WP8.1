using GalaSoft.MvvmLight;
using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Audio;
using OneVK.Request;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;

namespace OneVK.Core.Collections
{
    /// <summary>
    /// Представляет коллекцию рекомендуемых аудиозаписей.
    /// </summary>
    public class RecommendedAudiosCollection : StateSupportCollection<VKAudio>, ISupportIncrementalLoading
    {
        private uint _count;
        private uint _totalCount;
        private ulong _userID;

        /// <summary>
        /// Имеются ли еще элемены.
        /// </summary>
        public bool HasMoreItems { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр коллекции рекомендованных аудиозаписей
        /// для пользователя с указанным идентификатором.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        public RecommendedAudiosCollection(ulong userID)
        {
            _userID = userID;
            HasMoreItems = true;
        }

        /// <summary>
        /// Подгружает следующую партию элементов.
        /// </summary>
        /// <param name="count">Количество элементов, которое требуется подгрузить.</param>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return Task.Run(async () =>
            {
                State = ContentState.Loading;
                HasMoreItems = false;
                uint resultCount = 0;

                if (count < 10) count = 50;
                else if (count > 6000) count = 6000;

                var request = new GetAudioRecommendationsRequest() { Count = count, Offset = _count };
                var response = await request.ExecuteAsync();

                await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (response.Error.ErrorType == VKErrors.None)
                    {
                        _totalCount = (uint)response.Response.Count;
                        resultCount = (uint)response.Response.Items.Count;

                        for (int i = 0; i < response.Response.Items.Count; i++)
                            Add(response.Response.Items[i]);

                        _count += count;

                        if (Count > 0)
                        {
                            State = ContentState.Normal;
                            HasMoreItems = _count < _totalCount;
                        }
                        else State = ContentState.NoData;
                    }
                    else State = ContentState.Error;
                });
                
                return new LoadMoreItemsResult { Count = resultCount };
            }).AsAsyncOperation();
        }

        /// <summary>
        /// Выполняет попытку подгрузить элементы.
        /// </summary>
        public override void Load()
        {
            HasMoreItems = true;
        }

        public override void Refresh()
        {
            Clear();
            HasMoreItems = true;
            _count = 0;
        }
    }
}
