using System.Collections.Generic;

namespace Models
{
    public class LocalizationItem
    {
        public string ResourceId { get; init; }
        public string Value { get; init; }
        public string Lang { get; init; }
    }

    public class LocalizationItemService
    {
        public static List<LocalizationItem> GetList()
        {
            var localizationList = new List<LocalizationItem>();
            for (var i = 0; i < 1000000; i++)
            {
                var resourceId = $"ResourceId_{i}";
                var loc1 = new LocalizationItem()
                {
                    ResourceId = resourceId,
                    Value = $"{i} القيمة",
                    Lang = "ar"
                };
                var loc2 = new LocalizationItem()
                {
                    ResourceId = resourceId,
                    Value = $"Value {i}",
                    Lang = "en"
                };
                localizationList.Add(loc1);
                localizationList.Add(loc2);
            }

            return localizationList;
        }
    }
}