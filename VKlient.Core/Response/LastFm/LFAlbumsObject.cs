using Newtonsoft.Json;
using System.Collections.Generic;
using OneVK.Model.Audio;
using OneVK.Model.Common;
using Newtonsoft.Json.Linq;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет список альбомов Last.fm.
    /// </summary>
    public class LFAlbumsObject : ISupportValidation
    {
        private List<LFAlbumBase> _albums;
        private bool _isFilled = false;

        /// <summary>
        /// Список альбомов.
        /// </summary>        
        public List<LFAlbumBase> Albums
        {
            get
            {
                Fill();
                return _albums;
            }
        }

        /// <summary>
        /// Дополнительная информация об ответе.
        /// </summary>
        [JsonProperty("@attr")]
        public LFAditionalData AditionalData { get; set; }

        /// <summary>
        /// Объект альбомов. Динамический объект.
        /// </summary>
        [JsonProperty("album")]
        private dynamic Album { get; set; }

        /// <summary>
        /// Заполнить коллекцию.
        /// </summary>
        private void Fill()
        {
            if (_isFilled || Album == null)
                return;
            _albums = new List<LFAlbumBase>();

            if(Album.Type == JTokenType.Array)
                for (int i = 0; i < Album.Count; i++)
                        _albums.Add(Album[i].ToObject<LFAlbumBase>());
            else _albums.Add(Album.ToObject<LFAlbumBase>());

            _isFilled = true;
            Album = null;
        }

        /// <summary>
        /// Являются ли данные валидными.
        /// </summary>
        public bool IsValid()
        {
            Fill();
            return Albums != null && AditionalData != null;
        }
    }
}
