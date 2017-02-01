using GalaSoft.MvvmLight;
using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Enums.Newsfeed;
using OneVK.Helpers;
using OneVK.Model;
using OneVK.Model.Newsfeed;
using OneVK.Request;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;

namespace OneVK.Core.Collections
{
    /// <summary>
    /// Представляет коллекцию новостной ленты.
    /// </summary>
    public class NewsfeedCollection : StateSupportCollection<VKNewsfeedPost>, ISupportIncrementalLoading
    {
        private static readonly List<VKNewsfeedFilters> _filters = new List<VKNewsfeedFilters> { VKNewsfeedFilters.post };
        private string _from;
        private bool _hasItems = true;

        public bool HasMoreItems { get { return _hasItems; } }

        /// <summary>
        /// Подгружает следующую партию элементов.
        /// </summary>
        /// <param name="count">Количество элементов, которое требуется подгрузить.</param>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
#if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
                return Task.Run<LoadMoreItemsResult>(() => new LoadMoreItemsResult { Count = 100 }).AsAsyncOperation<LoadMoreItemsResult>();
#endif

            return Task.Run<LoadMoreItemsResult>(async () =>
                {
                    State = ContentState.Loading;
                    _hasItems = false;
                    uint resultCount = 0;

                    uint requestedCount = count;
                    if (requestedCount < 10) requestedCount = 50;
                    else if (requestedCount > 100) requestedCount = 100;

                    var request = new GetNewsfeedRequest { Count = requestedCount, From = _from, Filters = _filters };
                    var response = await request.ExecuteAsync();

                    await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        if (response.Error.ErrorType == VKErrors.None)
                        {
                            var groups = response.Response.Groups;
                            var profiles = response.Response.Profiles;

                            for (int i = 0; i < response.Response.Items.Count; i++)
                            {
                                IVKItemOwner owner;
                                VKNewsfeedPost post = response.Response.Items[i];

                                if (response.Response.Items[i].LikesOwner < 0 && groups != null)
                                    owner = groups.First(o => (long)o.ID == -post.LikesOwner);
                                else
                                    owner = profiles.First(o => (long)o.ID == post.LikesOwner);

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
                            _from = response.Response.NextFrom;
                            State = ContentState.Normal;
                            _hasItems = true;
                        }
                        else
                        {
                            State = ContentState.Error;
                            _hasItems = false;
                        }
                    });
#if DEBUG
                    Debug.WriteLine("Total posts count: " + this.Count);
#endif                    
                    return new LoadMoreItemsResult { Count = resultCount };
                }).AsAsyncOperation<LoadMoreItemsResult>();
        }

        /// <summary>
        /// Очищает коллекцию для обновления.
        /// </summary>
        public override void Refresh()
        {
            _hasItems = false;
            _from = null;
            base.Refresh();
        }

        /// <summary>
        /// Загрузить данные.
        /// </summary>
        public override async void Load()
        {
            await LoadMoreItemsAsync(50);
        }
    }
}
