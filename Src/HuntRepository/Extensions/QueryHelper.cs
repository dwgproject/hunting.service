using System.Collections.Generic;
using System.Linq;

namespace HuntRepository.Extensions
{
    public static class QueryHelper
    {
        public static bool IsAny<TClass>(this IEnumerable<TClass> list)
        {
            return list != null && list.Any();
        }
    }
}