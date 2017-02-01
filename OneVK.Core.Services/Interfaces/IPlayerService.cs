using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneVK.Core.Models;
using Windows.Media.Playback;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис воспроизведения аудиозаписей.
    /// </summary>
    public interface IPlayerService
    {
        /// <summary>
        /// Просиходит при изменении идентификатора текущего трека.
        /// </summary>
        event EventHandler<int> TrackIDChanged;

        /// <summary>
        /// Происходит при изменении состояния плеера.
        /// </summary>
        event EventHandler<MediaPlayerState> PlayerStateChanged;

        /// <summary>
        /// Выполнить инициализацию сервиса.
        /// </summary>
        void StartService();

        /// <summary>
        /// Выполнить остановку сервиса.
        /// </summary>
        void StopService();

        /// <summary>
        /// Записывает новые треки в хранилище.
        /// </summary>
        /// <param name="tracksToPlay">Список треков для воспроизведения.</param>
        Task SetNewTracks(IList<IVKAudioTrack> tracksToPlay);

        /// <summary>
        /// Записывает новые перемешанные треки в хранилище.
        /// </summary>
        /// <param name="tracksToPlay">Список треков для воспроизведения.</param>
        Task SetNewShuffledTracks(IList<IVKAudioTrack> tracksToPlay);

        /// <summary>
        /// Возвращает нормальный плейлист.
        /// </summary>
        Task<IEnumerable<IVKAudioTrack>> GetNormalTracks();

        /// <summary>
        /// Возвращает перемешанный плейлист.
        /// </summary>
        Task<IEnumerable<IVKAudioTrack>> GetShuffledTracks();

        /// <summary>
        /// Обновить состояние случайного режима.
        /// </summary>
        void UpdateShuffleMode();

        /// <summary>
        /// Обновить состояние режима повтора.
        /// </summary>
        void UpdateRepeatMode();

        /// <summary>
        /// Возвращает или задает идентификатор текущего трека в плейлисте.
        /// </summary>
        int CurrenTrackID { get; }

        /// <summary>
        /// Возвращает текущее состояние фонового проигрывателя.
        /// </summary>
        MediaPlayerState CurrentState { get; }

        /// <summary>
        /// Возвращает длительность текущего трека.
        /// </summary>
        TimeSpan CurrentDuration { get; }

        /// <summary>
        /// Возвращает или задает текущую позицию.
        /// </summary>
        TimeSpan CurrentPosition { get; set; }

        /// <summary>
        /// Возвращает значение, запущен ли проигрыватель.
        /// </summary>
        bool IsTaskRunning { get; }

        /// <summary>
        /// Воспроизвести трек по его идентификатору в плейлисте.
        /// </summary>
        /// <param name="trackID">Идентификатор трека.</param>
        void PlayFromID(int trackID);

        /// <summary>
        /// Возобновить или приостановить воспроизведение.
        /// </summary>
        void PlayPause();

        /// <summary>
        /// Переключить на следующий трек.
        /// </summary>
        void SkipNext();

        /// <summary>
        /// Переключить на предыдущий трек.
        /// </summary>
        void SkipPrevious();
    }
}
