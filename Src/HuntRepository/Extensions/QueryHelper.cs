using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GravityZero.HuntingSupport.Repository.Extension
{
    public static class EnumerableHelper
    {
        public static bool IsNotNullAndAny(this IEnumerable<object> list)
        {
            return list != null && list.Any();
        }
    }
}