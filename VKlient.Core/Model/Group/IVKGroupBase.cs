using OneVK.Enums.Common;
using OneVK.Enums.Group;

namespace OneVK.Model.Group
{
    public interface IVKGroupBase
    {
        ulong ID { get; set; }
        string Name { get; set; }
        string ScreenName { get; set; }
        VKGroupIsClosed IsClosed { get; set; }
        VKIsDeactivated IsDeactivated { get; set; }
        VKUserIsAdmin AdminLevel { get; set; }
        VKBoolean IsMember { get; set; }
        string Photo50 { get; set; }
        string Photo100 { get; set; }
        string Photo200 { get; set; }
    }
}
