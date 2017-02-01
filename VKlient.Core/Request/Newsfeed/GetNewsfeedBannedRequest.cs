using OneVK.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Request
{
    public class GetNewsfeedBannedRequest : GetNewsfeedBannedBaseRequest<VKNewsfeedGetBannedObject>
    {
        GetNewsfeedBannedRequest()
            : base() { }
    }
}
