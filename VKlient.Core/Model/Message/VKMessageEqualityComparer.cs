using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Model.Message
{
    public class VKMessageEqualityComparer : IEqualityComparer<VKMessage>
    {
        public bool Equals(VKMessage x, VKMessage y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(VKMessage obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}
