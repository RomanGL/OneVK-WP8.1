using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Video;
using OneVK.Request;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;

namespace OneVK.Core.Collections
{
    /// <summary>
    /// Представляет коллекцию видеозаписей.
    /// </summary>
    public class VideosCollection : StateSupportCollection<VKVideoBase>, ISupportIncrementalLoading
    {
        private long _ownerID;
        private uint _count;
        private uint _totalCount;

        /// <summary>
        /// Имеются ли ещё элементы.
        /// </summary>
        public bool HasMoreItems { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр коллекции с заданным идентификатором владельца.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца.</param>
        public VideosCollection(long ownerID)
        {
            _ownerID = ownerID;
            HasMoreItems = true;
        }

        /// <summary>
        /// Подгружает следующую порцию элементов.
        /// </summary>
        /// <param name="count">Количество элементов, которое необходимо подгрузить.</param>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return Task.Run(async () =>
            {
                HasMoreItems = false;
                State = ContentState.Loading;
                uint resultCount = 0;

                if (count < 10) count = 50;
                else if (count > 6000) count = 6000;

                var request = new VideoGetRequest() { Count = count, Offset = _count, OwnerID = _ownerID };
                var response = await request.ExecuteAsync();

                await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (response.Error.ErrorType == VKErrors.None)
                    {
                        _totalCount = response.Response.Count;
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
        /// Выполняет попытку подрузить элементы.
        /// </summary>
        public override void Load()
        {
            HasMoreItems = true;
        }

        /// <summary>
        /// Обновляет коллекцию.
        /// </summary>
        public override void Refresh()
        {
            _count = 0;
            HasMoreItems = true;
            Clear();
        }
    }
}
