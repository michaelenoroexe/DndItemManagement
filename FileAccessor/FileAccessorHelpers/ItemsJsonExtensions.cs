using DataAccess;
using Newtonsoft.Json;

namespace FileAccessor.FileAccessorHelpers
{
    internal static class ItemsJsonExtensions
    {
        public static string GetJsonString(this IEnumerable<Item> items) => JsonConvert.SerializeObject(items);

        public static List<Item>? GetItemList(this string items) => JsonConvert.DeserializeObject<List<Item>>(items);
    }
}
