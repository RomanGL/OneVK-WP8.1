using System.Collections.Generic;
#if ONEVK_CORE || ONEVK_SDK_RT
using Windows.Foundation.Metadata;
#endif

namespace OneVK.Request
{
#if ONEVK_CORE || ONEVK_SDK_RT
	[ExclusiveTo(typeof(BaseRequest))]
#endif
	/// <summary>
	/// Интерфейс запроса к сервису. Должен применяться только к BaseRequest.
	/// </summary>
    public interface IRequest
    {
        Dictionary<string, string> GetParameters();
    }
}
