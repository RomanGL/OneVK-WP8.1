using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using System.Linq;
using OneVK.Core.Models;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace OneVK.Core.ViewModels.Collections
{
    /// <summary>
    /// Paginated collection.
    /// </summary>
    public class PaginatedCollection<T> : StateSupportCollection<T>, ISupportIncrementalLoading
    {
        #region Constructors

        /// <summary>
        /// Creates the new instance of <see cref="PaginatedCollction"/> class.
        /// </summary>
        public PaginatedCollection() 
            : base()
        {
            this.HasMoreItems = false;
        }

        /// <summary>
        /// Creates the new instance of <see cref="PaginatedCollection"/> class.
        /// </summary>
        /// <param name="func">delegate for more items loading</param>
        public PaginatedCollection(Func<uint, Task<IEnumerable<T>>> func) 
            : base()
        {
            this.HasMoreItems = true;
            this.LoadMoreItems = func;
        }

        /// <summary>
        /// Creates the new instance of <see cref="PaginatedCollection"/> class.
        /// </summary>
        /// <param name="collection">elements type of <paramref name="T"/></param>
        public PaginatedCollection(IEnumerable<T> collection) 
            : base(collection)
        {
            this.HasMoreItems = true;
        }

        /// <summary>   
        /// Creates new instance of PaginatedCollection class.
        /// </summary>
        /// <param name="func">delegate for more items loading</param>
        /// <param name="collection">elements type of <paramref name="T"/></param>
        public PaginatedCollection(IEnumerable<T> collection, Func<uint, Task<IEnumerable<T>>> func)
            : base(collection)
        {
            this.HasMoreItems = true;
            this.LoadMoreItems = func;
        }

        #endregion

        #region ISupportIncrementalLoading members

        /// <summary>
        /// Has more items?
        /// </summary>
        public bool HasMoreItems { get; protected set; }

        /// <summary>
        /// Loads more items synchronously
        /// </summary>
        /// <param name="count">count of loaded elements</param>
        /// <returns></returns>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return LoadMore().AsAsyncOperation();
        }

        #endregion
        
        #region Private methods

        /// <summary>
        /// Loads more.
        /// </summary>
        protected async Task<LoadMoreItemsResult> LoadMore()
        {
            try
            {                
                ContentState = ContentState.Loading;
                HasMoreItems = false;

                List<T> data = new List<T>();
                data = (await LoadMoreItems(page)).ToList();
                page++;

                await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    foreach (var item in data)
                        this.Add(item);
                });

                ContentState = this.Any() ? ContentState.Normal : ContentState.NoData;
                this.HasMoreItems = data.Any();
                //set page to previous state if no data
                page = data.Any() ? page : page - 1;
                return new LoadMoreItemsResult() { Count = (uint)data.Count() };
            }
            catch
            {               
                this.HasMoreItems = false;
                ContentState = ContentState.Error;
                return new LoadMoreItemsResult() { Count = 0 };
            }
        }

        /// <summary>
        /// Load items.
        /// </summary>
        public override async void Load()
        {
            await LoadMore();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Delegate for more items loading
        /// </summary>
        public Func<uint, Task<IEnumerable<T>>> LoadMoreItems { get; set; }

        #endregion

        #region Fields

        /// <summary>
        /// Page number
        /// </summary>
        private uint page = 0;

        #endregion
    }
}
