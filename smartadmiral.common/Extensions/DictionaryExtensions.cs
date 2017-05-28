using System.Collections.Generic;

namespace smartadmiral.common.Extensions
{
    public static class DictionaryExtensions
    {
        public static T2 TryFindValue<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key)
        {
            T2 result;
            dictionary.TryGetValue(key, out result);
            return result;
        }
    }
}