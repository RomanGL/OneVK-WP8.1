namespace OneVK.Core.Services
{
    /// <summary>
    /// Содержит константы медиапроигрывателя.
    /// </summary>
    internal static class PlayerConstants
    {
        public const int PLAYER_REPLAY_AGAIN_DELAY_SECONDS = 4;

        public const string NORMAL_PLAYLIST_FILE = "Normal.vkspl";
        public const string SHUFFLED_PLAYLIST_FILE = "Shuffled.vkspl";

        public const string PLAYER_SHUFFLE_MODE = "PlayerShuffleMode";
        public const string PLAYER_REPEAT_MODE = "PlayerRepeatMode";

        public const string TASK_RUNNING = "PlayerTaskRunning";
        public const string APP_RUNNING = "AppRunning";
        public const string CURRENT_TRACK_ID = "CurrentTrackId";
        public const string PLAYER_NEXT_TRACK = "NextTrack";
        public const string PLAYER_PREVIOUS_TRACK = "PreviousTrack";
        public const string UPDATE_PLAYLIST = "UpdatePlaylist";
        public const string UPDATE_SHUFFLE_MODE = "UpdateShuffleMode";
        public const string UPDATE_REPEAT_MODE = "UpdateRepeatMode";
        public const string PLAY_TRACK_ID = "PlayTrackId";
        public const string PLAYER_PLAY_PAUSE = "PlayPause";
    }
}
