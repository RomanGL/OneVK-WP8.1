using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.StoreApps;
using OneVK.Core.Models;
using OneVK.Core.Services;
using OneVK.Core.VK;
using PropertyChanged;

namespace OneVK.Core.ViewModels.Photo
{
    /// <summary>
    /// Represents list of albums
    /// </summary>
    [ImplementPropertyChanged] 
    public class PhotoAlbumsViewModel : ViewModelBase
    {
        #region Constructor

        public PhotoAlbumsViewModel(IVKService vkService, IAppNotificationsService appNotificationsService, ObservableCollection<AlbumModel> albumList)
        {
            this.appNotificationsService = appNotificationsService;
            this.vkService = vkService;
            
        }

        #endregion

        #region Services

        private IAppNotificationsService appNotificationsService;
        private IVKService vkService;
        #endregion

        #region Bindable properties

        /// <summary>
        /// List of all albums
        /// </summary>
        public ObservableCollection<AlbumModel> AlbumList { get; private set; }

        #endregion

        #region Private methods

        private async Task LoadAlbumList()
        {
            var paramDictionary = new Dictionary<string, string>
            {
                {"owner_id", "102549103"}
            };

            var response =
                await
                    this.vkService.ExecuteRequestAsync(new Request<List<AlbumModel>>("photos.getAlbums", paramDictionary));

            if (response.IsSuccess)
            {
                
            }
        }

        #endregion

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            await LoadAlbumList();

            base.OnNavigatedTo(e, viewModelState);
        }
    }
}
