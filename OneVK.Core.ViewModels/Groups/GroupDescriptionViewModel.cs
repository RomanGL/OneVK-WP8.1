using Microsoft.Practices.Prism.StoreApps;
using Newtonsoft.Json;
using OneVK.Core.VK.Models.Groups;
using PropertyChanged;
using System.Collections.Generic;

namespace OneVK.Core.ViewModels
{
    /// <summary>
    /// Представляет модель представления страницы описания сообщества.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class GroupDescriptionViewModel : ViewModelBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GroupDescriptionViewModel"/>.
        /// </summary>
        public GroupDescriptionViewModel() { }

        /// <summary>
        /// Сообщество.
        /// </summary>
        public VKGroupExtended Group { get; private set; }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            Group = JsonConvert.DeserializeObject<VKGroupExtended>(e.Parameter.ToString());

            base.OnNavigatedTo(e, viewModelState);
        }
    }
}
