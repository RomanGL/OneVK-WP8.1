namespace OneVK
{
    /// <summary>
    /// Представляет собой набор констант ВКлиента.
    /// </summary>
#if !PLAYER_TASK
    public static class AppConstants
#else
    internal static class AppConstants
#endif
    {
        public const string ValidationCanceled = "validation_canceled";
        public const string NeedValidation = "need_validation";
        public const string InvalidClient = "invalid_client";
        public const string NeedCaptcha = "need_captcha";
        public const string ConnectionError = "connection_error";
        public const string CaptchaCanceled = "captcha_canceled";
        public const string AccessToken = "AccessToken";

        public const string PlayerCurrentTrackID = "PlayerCurrentTrackID";
        public const string PlayerShuffleMode = "PlayerShuffleMode";
        public const string PlayerRepeatMode = "PlayerRepeatMode";
        public const string PlayerNextTrack = "PlayerNextTrack";
        public const string PlayerPreviousTrack = "PlayerPreviousTrack";
        public const string PlayerPauseResume = "PlayerPauseResume";
        public const string PlayerTaskRunning = "PlayerTaskRunning";
        public const string PlayerMediaOpened = "PlayerMediaOpened";
        public const string PlayerMediaEnded = "PlayerMediaEnded";
        public const string PlayerMediaFailed = "PlayerMediaFailed";
        public const string PlayerBufferingStarted = "PlayerBufferingStarted";
        public const string PlayerBufferingEnded = "PlayerBufferingEnded";
        public const string PlayerNewPlaylist = "PlayerNewPlaylist";
        public const int PlayerReplayAgainTriggerSeconds = 4;

        public const string PlaylistFileName = "Playlist.vkspl";
        public const string ShuffledPlaylistFileName = "ShufflePlaylist.vkspl";

        public const string MaxPhotosSize = "MaxPhotosSize";
        public const string StayOfflineMode = "StayOfflineMode";
        public const string EnableMusicCache = "EnableMusicCache";
        public const string EnableCacheArtistsImages = "EnableCacheArtistsImages";
        public const string EnableCacheArtistsImagesOnlyFromWiFi = "EnableCacheArtistsImagesOnlyFromWiFi";

        public const string EnablePushNotifications = "EnablePushNotifications";
        public const string EnableTextInNotifications = "EnableTextInNotifications";
        public const string EnablePushMsgs = "EnablePushMsgs";
        public const string EnablePushChats = "EnablePushChats";
        public const string EnablePushLikes = "EnablePushLikes";
        public const string EnablePushReposts = "EnablePushReposts";
        public const string EnablePushWallPost = "EnablePushWallPost";
        public const string EnablePushComments = "EnablePushComments";
        public const string EnablePushMentions = "EnablePushMentions";
        public const string EnablePushReplies = "EnablePushReplies";
        public const string EnablePushFriendsRequests = "EnablePushFriendsRequests";
        public const string EnablePushGroupsInvites = "EnablePushGroupsInvites";
        public const string EnablePushEventsSoon = "EnablePushEventsSoon";
        public const string EnablePushFriendsBirthday = "EnablePushFriendsBirthday";

        public const string EnableInAppSound = "EnableInAppSound";
        public const string EnableInAppVibration = "EnableInAppVibration";
        public const string EnableInAppPopup = "EnableInAppPopup";

        public const string EnablePromo = "EnablePromo";
        public const string EmojiType = "EmojiType";

        public const int AudioTaskStartingTimeout = 3000;
    }
}
