using System;
using System.Threading.Tasks;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Audio;
using OneVK.Request;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;
using OneVK.Enums.App;

namespace OneVK.Core.Collections
{
    /// <summary>
    /// Представляет коллекцию популярных аудиозаписей.
    /// </summary>
    public class PopularAudiosCollection : StateSupportCollection<VKAudio>, ISupportIncrementalLoading
    {
        private uint _count;

        /// <summary>
        /// Имеются ли еще элемены.
        /// </summary>
        public bool HasMoreItems { get; private set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public PopularAudiosCollection()
        {
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

                var request = new GetPopularAudiosRequest { Count = count, Offset = _count };
                var response = await request.ExecuteAsync();

                await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (response.Error.ErrorType == VKErrors.None)
                    {
                        resultCount = (uint)response.Response.Count;

                        for (int i = 0; i < response.Response.Count; i++)
                            Add(response.Response[i]);

                        _count += count;

                        if (Count > 0)
                        {
                            State = ContentState.Normal;
                            HasMoreItems = _count < 300;
                        }
                        else State = ContentState.NoData;
                    }
                    else State = ContentState.Error;
                });
                
                return new LoadMoreItemsResult { Count = resultCount };
            }).AsAsyncOperation();
        }

        /// <summary>
        /// Обновляет коллекцию.
        /// </summary>
        public override void Refresh()
        {
            Clear();
            _count = 0;
            HasMoreItems = true;
        }

        /// <summary>
        /// Выполняет попытку подгрузить элементы.
        /// </summary>
        public override void Load()
        {
            HasMoreItems = true;
        }
    }
}
