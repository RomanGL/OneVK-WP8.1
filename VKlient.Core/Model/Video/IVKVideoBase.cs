using System;

namespace OneVK.Model.Video
{
    public interface IVKVideoBase
    {
        ulong ID { get; set; }
        long OwnerID { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        TimeSpan Duration { get; set; }
        string URL240 { get; set; }
        string URL360 { get; set; }
        string URL480 { get; set; }
        string URL720 { get; set; }
        string Photo130 { get; set; }
        string Photo320 { get; set; }
        string Photo640 { get; set; }
        DateTime Date { get; set; }
        long ViewsCount { get; set; }
        long CommentsCount { get; set; }
        string Player { get; set; }
        string AccessKey { get; set; }
    }
}
