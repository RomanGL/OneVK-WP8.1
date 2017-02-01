using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Photo;
using OneVK.Request;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;

namespace OneVK.Core.Collections
{
    /// <summary>
    /// Представляет коллекцию фотоальбомов пользователя
    /// или сообщества.
    /// </summary>
    public class PhotoAlbumsCollection : StateSupportCollection<VKPhotoAlbum>, ISupportIncrementalLoading
    {
        long _ownerID;
        private uint _totalCount;

        /// <summary>
        /// Имеются ли еще элементы для подгрузки.
        /// </summary>
        public bool HasMoreItems { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр коллекции с 
        /// заданным идентификатором пользователя или
        /// сообщества.
        /// </summary>
        /// <param name="ownerID">Идентификатор пользователя или
        /// сообщества, фотоальбомы которого необходимо загрузить.</param>
        public PhotoAlbumsCollection(long ownerID)
        {
            _ownerID = ownerID;
            HasMoreItems = true;
        }

        /// <summary>
        /// Загружает новую партию элементов.
        /// </summary>
        /// <param name="count">Количество элементов, которое
        /// необходимо загрузить.</param>
        /// <returns></returns>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return Task.Run(async () =>
                {
                    HasMoreItems = false;
                    State = ContentState.Loading;
                    uint resultCount = 0;

                    if (count < 10) count = 20;
                    else if (count > 6000) count = 6000;

                    var request = new GetPhotoAlbumsRequest
                    {
                        Count = count,
                        Offset = (uint)this.Items.Count,
                        OwnerID = _ownerID,
                        ReturnSystem = VKBoolean.True,
                        ReturnCovers = VKBoolean.True
                    };

                    var response = await request.ExecuteAsync();

                    await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            if (response.Error.ErrorType == VKErrors.None)
                            {
                                _totalCount = (uint)response.Response.Count;
                                resultCount = (uint)response.Response.Items.Count;

                                for (int i = 0; i < response.Response.Items.Count; i++)
                                    Add(response.Response.Items[i]);

                                if (Count > 0)
                                {
                                    State = ContentState.Normal;
                                    HasMoreItems = Count < _totalCount;
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

        /// <summary>
        /// Обновляет коллекцию.
        /// </summary>
        public override void Refresh()
        {
            HasMoreItems = true;
            Clear();
        }

    }
}
