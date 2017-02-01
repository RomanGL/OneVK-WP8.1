using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneVK.Core.Player;
using OneVK.Enums.App;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет интерфейс источника данных проигрывателя, не зависимого от платформы выполнения.
    /// </summary>
    public interface IPlayerService
    {
        /// <summary>
        /// Происходит при изменении идентификатора текущего трека.
        /// </summary>
        event EventHandler<int> TrackIDChanged;
        /// <summary>
        /// Происходит при изменении состояния проигрывателя.
        /// </summary>
        event EventHandler<PlayerState> PlayerStateChanged;

        /// <summary>
        /// Продолжить или приостановить воспроизведение.
        /// </summary>
        void ResumePause();
        /// <summary>
        /// Воспроизводит трек из текущего плейлиста по его идентификатору.
        /// </summary>
        /// <param name="trackId">Идентификатор трека.</param>
        void PlayFromID(int trackId);
        /// <summary>
        /// Указывает проигрывателю переключиться на смешанный плейлист, если необходимо.
        /// </summary>
        void UpdateShuffleMode();
        /// <summary>
        /// Указывает проигрывателю на необходимость обновления параметра повтора.
        /// </summary>
        void UpdateRepeatMode();
        /// <summary>
        /// Указывает проигрывателю на необходимость обновления текущего плейлиста.
        /// </summary>
        void UpdateNewPlaylist();
        /// <summary>
        /// Инициализирует работу провайдера проигрывателя.
        /// </summary>
        void Initialize();
        /// <summary>
        /// Деинициализирует провайдер проигрывателя.
        /// </summary>
        void Deinitialize();
        /// <summary>
        /// Следующий трек.
        /// </summary>
        void NextTrack();
        /// <summary>
        /// Предыдуший трек.
        /// </summary>
        void PreviousTrack();
        /// <summary>
        /// Устанавливает новый плейлист.
        /// </summary>
        /// <param name="tracks">Список треков.</param>
        Task SetNewPlaylist(IEnumerable<IAudioTrack> tracks);
        /// <summary>
        /// Устанавливает новый перемешанный плейлист.
        /// </summary>
        /// <param name="tracks">Список треков.</param>
        Task SetNewShuffledPlaylist(IEnumerable<IAudioTrack> tracks);
        /// <summary>
        /// Возвращает сохраненный плейлист.
        /// </summary>
        Task<IEnumerable<IAudioTrack>> GetPlaylist();
        /// <summary>
        /// Возвращает сохраненный перемешанный плейлист.
        /// </summary>
        Task<IEnumerable<IAudioTrack>> GetShuffledPlaylist();

        /// <summary>
        /// Текущее состояние проигрывателя.
        /// </summary>
        PlayerState CurrentState { get; }
        /// <summary>
        /// Идентификатор текущего трека.
        /// </summary>
        int CurrentTrackID { get; }
        /// <summary>
        /// Текущая позиция в треке.
        /// </summary>
        TimeSpan CurrentPosition { get; set; }
        /// <summary>
        /// Длительность текущего трека.
        /// </summary>
        TimeSpan Duration { get; }
    }
}
