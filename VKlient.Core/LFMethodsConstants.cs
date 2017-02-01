namespace OneVK
{
    /// <summary>
    /// Содержит константы методов для сервиса Last.fm.
    /// </summary>
    internal static class LFMethodsConstants
    {
        public const string ApiRoot = "http://ws.audioscrobbler.com/2.0/?";

        public const string ChartGetTopTracks = "chart.getTopTracks";
        public const string ChartGetTopArtists = "chart.getTopArtists";

        public const string ArtistGetTopTracks = "artist.getTopTracks";
        public const string ArtistGetTopAlbums = "artist.getTopAlbums";
        public const string ArtistsGetSimilar = "artist.getSimilar";
        public const string ArtistsGetInfo = "artist.getInfo";

        public const string TrackGetInfo = "track.getInfo";
    }
}
