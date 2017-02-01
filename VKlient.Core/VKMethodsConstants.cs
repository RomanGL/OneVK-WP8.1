namespace OneVK
{
    /// <summary>
    /// Коллекция констант названий методов ВКонтакте.
    /// </summary>
    internal static class VKMethodsConstants
    {
        public const string ApiVersion = "5.29";
        public const string VKRequestURL = "https://api.vk.com/method/";
        public const string VKDirectAuthURL = "https://oauth.vk.com/token?";
        public const string CaptchaForce = "captcha.force?";
        public const string ExecuteSubstring = "execute.";
        public const string BaseProfileFields = "status,online,online_mobile,photo_50,photo_100,photo_200,sex";
        public const string ExtendedProfileFields = "sex,bdate,city,country,photo_50,photo_100"
                + ",photo_200_orig,photo_200,photo_400_orig,photo_max,photo_max_orig,online,online_mobile"
                + ",has_mobile,site,education,universities,schools,can_post,can_see_all_posts,counters"
                + ",can_see_audio,can_write_private_message,status,relation,occupation,activities"
                + ",interests,music,movies,tv,books,games,about,quotes,blacklisted,verified";
        public const string GroupInfoFields = "city,country,place,description,wiki_page,members_count,counters"
            + ",start_date,finish_date,can_post,can_see_all_posts,activity,status,contacts,links,fixed_post"
            + ",verified,site,ban_info";

        #region Audio
        public const string AudioGet = "audio.get?";
        public const string AudioGetByID = "audio.getById?";
        public const string AudioGetLyrics = "audio.getLyrics?";
        public const string AudioSearch = "audio.search?";
        public const string AudioGetUploadServer = "audio.getUploadServer?";
        public const string AudioSave = "audio.save?";
        public const string AudioAdd = "audio.add?";
        public const string AudioDelete = "audio.delete?";
        public const string AudioEdit = "audio.edit?";
        public const string AudioReorder = "audio.reorder?";
        public const string AudioRestore = "audio.restore?";
        public const string AudioGetAlbums = "audio.getAlbums?";
        public const string AudioAddAlbum = "audio.addAlbum?";
        public const string AudioEditAlbum = "audio.editAlbum?";
        public const string AudioDeleteAlbum = "audio.deleteAlbum?";
        public const string AudioMoveToAlbum = "audio.moveToAlbum?";
        public const string AudioSetBroadcast = "audio.setBroadcast?";
        public const string AudioGetBroadcastList = "audio.getBroadcastList?";
        public const string AudioGetRecommendations = "audio.getRecommendations?";
        public const string AudioGetPopular = "audio.getPopular?";
        public const string AudioGetCount = "audio.getCount?";
        #endregion

        #region Users
        public const string UsersGet = "users.get?";
        public const string UsersSearch = "users.search?";
        public const string UserReport = "users.report?";
        public const string UserGetFollowers = "users.getFollowers?";
        #endregion

        #region Friends
        public const string FriendsGet = "friends.get?";
        public const string FriendsGetOnline = "friends.getOnline?";
        public const string FriendsGetMutual = "friends.getMutual?";
        public const string FriendsGetRecent = "friends.getRecent?";
        public const string FriendsEdit = "friends.edit?";
        public const string FriendsAdd = "friends.add?";
        public const string FriendsDelete = "friends.delete?";
        public const string FriendsGetLists = "friends.getLists?";
        public const string FriendsAddList = "friends.addList?";
        public const string FriendsEditList = "friends.editList?";
        public const string FriendsDeleteList = "friends.deleteList?";
        public const string FriendsGetAppUsers = "friends.getAppUsers?";
        public const string FriendsDeleteAllRequests = "friends.deleteAllRequests?";
        public const string FriendsSearch = "friends.search?";
        #endregion

        #region Video
        public const string VideoGet = "video.get?";
        public const string VideoEdit = "video.edit?";
        public const string VideoAdd = "video.add?";
        public const string VideoSave = "video.save?";
        public const string VideoDelete = "video.delete?";
        public const string VideoRestore = "video.restore?";
        public const string VideoSearch = "video.search?";
        public const string VideoGetUserVideos = "video.getUserVideos?";
        public const string VideoGetAlbums = "video.getAlbums?";
        public const string VideoGetAlbumByID = "video.getAlbumById?";
        public const string VideoAddAlbum = "video.addAlbum?";
        public const string VideoEditAlbum = "video.editAlbum?";
        public const string VideoDeleteAlbum = "video.deleteAlbum?";
        public const string VideoReorderAlbums = "video.reoderAlbums?";
        public const string VideoAddToAlbum = "video.addToAlbum?";
        public const string VideoRemoveFromAlbum = "video.removeFromAlbum?";
        public const string VideoGetAlbumsByVideo = "video.getAlbumsByVideo?";
        public const string VideoGetComments = "video.getComments?";
        public const string VideoCreateComment = "video.createComment?";
        public const string VideoDeleteComment = "video.deleteComment?";
        public const string VideoRestoreComment = "video.restoreComment?";
        public const string VideoEditComment = "video.editComment?";
        public const string VideoGetTags = "video.getTags?";
        public const string VideoPutTag = "video.putTag?";
        public const string VideoRemoveTag = "video.removeTag?";
        public const string VideoGetNewTags = "video.getNewTags?";
        public const string VideoReport = "video.report?";
        public const string VideoReportComment = "video.reportComment?";
        #endregion

        #region Photo
        public const string PhotoGet = "photos.get?";
        public const string PhotoGetByID = "photos.getById?";
        public const string PhotoCreateAlbum = "photos.createAlbum?";
        public const string PhotoGetAlbums = "photos.getAlbums?";
        public const string PhotoEditAlbum = "photos.editAlbum?";
        public const string PhotoGetAlbumsCount = "photos.getAlbumsCount?";
        public const string PhotoGetUploadServer = "photos.getUploadServer?";
        public const string PhotoGetOwnerUploadServer = "photos.getOwnerPhotoUploadServer?";
        public const string PhotoGetChatUploadServer = "photos.getChatUploadServer?";
        public const string PhotoSaveOwnerPhoto = "photos.saveOwnerPhoto?";
        public const string PhotoSaveWallPhoto = "photos.saveWallPhoto?";
        public const string PhotoGetWallUploadServer = "photos.getWallUploadServer?";
        public const string PhotoGetMessagesUploadServer = "photos.getMessagesUploadServer?";
        public const string PhotoSaveMessagesPhoto = "photos.saveMessagesPhoto?";
        public const string PhotoReport = "photos.report?";
        public const string PhotoReportComment = "photos.reportComment?";
        public const string PhotoSearch = "photos.search?";
        public const string PhotoSave = "photos.save?";
        public const string PhotoCopy = "photos.copy?";
        public const string PhotoEdit  = "photos.edit?";
        public const string PhotoMove = "photos.move?";
        public const string PhotoMakeCover = "photos.makeCover?";
        public const string PhotoReorderAlbums = "photos.reorderAlbums?";
        public const string PhotoReorderPhotos = "photos.reorderPhotos?";
        public const string PhotoGetAll = "photos.getAll?";
        public const string PhotoDeleteAlbum = "photos.deleteAlbum?";
        public const string PhotoDelete = "photos.delete?";
        public const string PhotoComfirmTag = "photos.confirmTag?";
        public const string PhotoGetUserPhotos = "photos.getUserPhotos?";
        public const string PhotoDeleteComment = "photos.deleteComment?";
        public const string PhotoRestoreComment = "photos.restoreComment?";
        public const string PhotoGetTags = "photos.getTags?";
        public const string PhotoPutTag = "photos.putTag?";
        public const string PhotoRemoveTag = "photos.removeTag?";
        public const string PhotoGetNewTags = "photos.getNewTags?";
        public const string PhotoGetComments = "photos.getComments?";
        public const string PhotoGetAllComments = "photos.getAllComments?";
        public const string PhotoCreateComment = "photos.createComment?";
        public const string PhotoGetProfile = "photos.getProfile?";
        public const string PhotoEditComment = "photos.editComment?";
        #endregion

        #region Newsfeed
        public const string NewsfeedGet = "newsfeed.get?";
        public const string NewsfeedGetRecommended = "newsfeed.getRecommended?";
        public const string NewsfeedGetComments = "newsfeed.getComments?";
        public const string NewsfeedGetMentions = "newsfeed.getMentions?";
        public const string NewsfeedGetBanned = "newsfeed.getBanned?";
        public const string NewsfeedAddBan = "newsfeed.addBan?";
        public const string NewsfeedDeleteBan = "newsfeed.deleteBan?";
        public const string NewsfeedIgnoreItem = "newsfeed.ignoreItem?";
        public const string NewsfeedUnignoreItem = "newsfeed.unignoreItem?";
        public const string NewsfeedSearch = "newsfeed.search?";
        public const string NewsfeedGetLists = "newsfeed.getLists?";
        public const string NewsfeedSaveList = "newsfeed.saveList?";
        public const string NewsfeedDeleteList = "newsfeed.deleteList?";
        public const string NewsfeedUnsubscribe = "newsfeed.unsubscribe?";
        public const string NewsfeedGetSuggestedSources = "newsfeed.getSuggestedSources?";
        #endregion

        #region Wall
        public const string WallGet = "wall.get?";
        public const string WallSearch = "wall.search?";
        public const string WallGetByID = "wall.getByID?";
        public const string WallPost = "wall.post?";
        public const string WallRepost = "wall.repost?";
        public const string WallGetReposts = "wall.getReposts?";
        public const string WallEdit = "wall.edit?";
        public const string WallDelete = "wall.delete?";
        public const string WallRestore = "wall.restore?";
        public const string WallPin = "wall.pin?";
        public const string WallUnpin = "wall.unpin?";
        public const string WallGetComments = "wall.getComments?";
        public const string WallAddComment = "wall.addComment?";
        public const string WallEditComment = "wall.editComment?";
        public const string WallDeleteComment = "wall.deleteComment?";
        public const string WallRestoreCommemnt = "wall.restoreComment?";
        public const string WallReportPost = "wall.reportPost?";
        public const string WallReportComment = "wall.reportComment?";
        #endregion

        #region Status
        public const string StatusGet = "status.get?";
        public const string StatusSet = "status.set?";
        #endregion

        #region Message
        public const string MessageGet = "messages.get?";
        public const string MessageGetDialogs = "messages.getDialogs?";
        public const string MessageGetByID = "messages.getById?";
        public const string MessageSearch = "messages.search?";
        public const string MessageGetHistory = "messages.getHistory?";
        public const string MessageSend = "messages.send?";
        public const string MessageDelete = "messages.delete?";
        public const string MessageDeleteDialog = "messages.deleteDialog?";
        public const string MessageRestore = "messages.restore?";
        public const string MessageMarkAsRead = "messages.markAsRead?";
        public const string MessageMarkAsImportant = "messages.markAsImportant?";
        public const string MessageGetLongPollServer = "messages.getLongPollServer?";
        public const string MessageGetLongPollHistory = "messages.getLongPollHistory?";
        public const string MessageGetChat = "messages.getChat?";
        public const string MessageCreateChat = "messages.createChat?";
        public const string MessageEditChat = "messages.editChat?";
        public const string MessageGetChatUsers = "messages.getChatUsers?";
        public const string MessageSetActivity = "messages.setActivity?";
        public const string MessageSearchDialogs = "messages.searchDialogs?";
        public const string MessageAddChatUser = "messages.addChatUser?";
        public const string MessageRemoveChatUser = "messages.removeChatUser?";
        public const string MessageGetLastActivity = "messages.getLastActivity?";
        public const string MessageSetChatPhoto = "messages.setChatPhoto?";
        public const string MessageDeleteChatPhoto = "messages.deleteChatPhoto?";
        #endregion

        #region Likes
        public const string LikesAdd = "likes.add?";
        public const string LikesDelete = "likes.delete?";
        public const string LikesIsLiked = "likes.isLiked?";
        public const string LikesGetList = "likes.getList?";
        #endregion

        #region Doc
        public const string DocsGet = "docs.get?";
        public const string DocsGetByID = "docs.getById?";
        public const string DocsGetUploadServer = "docs.getUploadServer?";
        public const string DocsGetWallUploadServer = "docs.getWallUploadServer?";
        public const string DocSave = "docs.save?";
        public const string DocsAdd = "docs.add?";
        public const string DocsDelete = "docs.delete?";
        #endregion

        #region Gifts
        public const string GiftsGet = "gifts.get?";
        #endregion

        #region Polls
        public const string PollsGetByID = "polls.getById?";
        public const string PollsAddVote = "polls.addVote?";
        public const string PollsDeleteVote = "polls.deleteVote?";
        public const string PollsGetVoters = "polls.getVoters?";
        public const string PollsCreate = "polls.create?";
        public const string PollsEdit = "polls.edit?";
        #endregion

        #region Groups
        public const string GroupsIsMember = "groups.isMember?";
        public const string GroupsGetByID = "groups.getById?";
        public const string GroupsGet = "groups.get?";
        public const string GroupsEditManager = "groups.editManager?";
        public const string GroupsInvite = "groups.invite?";
        public const string GroupsAddLink = "groups.addLink?";
        public const string GroupsDeleteLink = "groups.deleteLink?";
        public const string GroupsEditLink = "groups.editLink?";
        public const string GroupsReorderLink = "groups.reorderLink?";
        public const string GroupsRemoveUser = "groups.removeUser?";
        public const string GroupsApproveRequest = "groups.approveRequest?";
        #endregion

        #region Account
        public const string AccountRegisterDevice = "account.registerDevice?";
        public const string AccountGetCounters = "account.getCounters?";
        #endregion

        #region Notification
        public const string NotificationsGet = "notifications.get?";
        public const string NotificationsMarkAsViewed = "notifications.markAsViewed?";
        #endregion

        #region Execute
        public const string ExecuteGetPromo = "getPromo?";
        public const string ExecuteGetPromoGroup = "getPromoGroup?";
        #endregion
    }
}
