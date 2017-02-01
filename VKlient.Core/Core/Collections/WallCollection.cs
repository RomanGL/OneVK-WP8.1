using System;
using System.Linq;
using System.Threading.Tasks;
using OneVK.Model.Wall;
using Windows.UI.Xaml.Data;
using Windows.Foundation;
using OneVK.Enums.App;
using OneVK.Request;
using OneVK.Helpers;
using Windows.UI.Core;
using OneVK.Enums.Common;
using OneVK.Model;

namespace OneVK.Core.Collections
{
    /// <summary>
    /// Представляет коллекцию стены пользователя или сообщества.
    /// </summary>
    public class WallCollection : StateSupportCollection<VKWallPost>, ISupportIncrementalLoading
    {
        private long _ownerID;
        private uint _count;

        /// <summary>
        /// Имеются ли еще элементы для подгрузки.
        /// </summary>
        public bool HasMoreItems { get; private set; }

        /// <summary>
        /// Инициализирует новый экзмпляр коллекции с заданным идентификатором
        /// владельца стены.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца стены.</param>
        public WallCollection(long ownerID)
        {
            _ownerID = ownerID;
            HasMoreItems = true;
        }
        
        /// <summary>
        /// Подгружает следующую партию элементов.
        /// </summary>
        /// <param name="count">Количество элементов, которые необходимо подгрузить.</param>
        /// <returns></returns>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return Task.Run(async () =>
            {
                HasMoreItems = false;
                State = ContentState.Loading;
                uint resultCount = 0;

                if (count < 10) count = 50;
                else if (count > 6000) count = 6000;

                var request = new GetWallExtendedRequest() { OwnerID = _ownerID, Count = count, Offset = (uint)Count };
                var response = await request.ExecuteAsync();

                await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        if (response.Error.ErrorType == VKErrors.None)
                        {
                            if (response.Response.Items.Count != 0)
                            {
                                var groups = response.Response.Groups;
                                var profiles = response.Response.Profiles;

                                for (int i = 0; i < response.Response.Items.Count; i++)
                                {
                                    IVKItemOwner owner;
                                    VKWallPost post = response.Response.Items[i];

                                    if (response.Response.Items[i].FromID < 0 && groups != null)
                                        owner = groups.First(o => (long)o.ID == -post.FromID);
                                    else
                                        owner = profiles.First(o => (long)o.ID == post.FromID);

                                    post.Owner = owner;

                                    if (post.HasCopyPost)
                                    {
                                        if (post.FirstCopyPost.OwnerID < 0 && groups != null)
                                            post.FirstCopyPost.Owner = groups.First(o => (long)o.ID == -post.FirstCopyPost.OwnerID);
                                        else
                                            post.FirstCopyPost.Owner = profiles.First(o => (long)o.ID == post.FirstCopyPost.OwnerID);
                                    }

                                    this.Add(post);
                                }

                                resultCount = (uint)response.Response.Items.Count;
                                _count = response.Response.Count;
                                State = ContentState.Normal;
                                HasMoreItems = Count < _count;
                            }
                            else
                                State = ContentState.NoData;
                        }
                        else
                            State = ContentState.Error;
                    });                
                return new LoadMoreItemsResult { Count = resultCount };
            }).AsAsyncOperation();
        }

        /// <summary>
        /// Обновляет коллекцию.
        /// </summary>
        public override void Refresh()
        {
            HasMoreItems = false;
            _count = 0;
            base.Refresh();
        }

        /// <summary>
        /// Выполняет попытку загрузить данные.
        /// </summary>
        public override async void Load()
        {
            await LoadMoreItemsAsync(50);
        }
    }
}