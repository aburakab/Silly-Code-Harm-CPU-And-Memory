using System;
using System.Collections.Generic;
using Models;

namespace CollectionsCapacity
{
    /*
     * Using DotTrace
     */
    internal static class Program
    {
        private const int Count = 1000000;

        private static void Main(string[] args)
        {
            FillCollection_WithoutCapacity();
            FillCollection_WithCapacity();
        }

        private static void FillCollection_WithoutCapacity()
        {
            var list1 = new List<LocalizationItem>();
            for (var i = 0; i < Count; i++)
            {
                var resourceId = Guid.NewGuid().ToString();
                list1.Add(new LocalizationItem
                {
                    ResourceId = resourceId,
                    Lang = "ar",
                    Value = $"لوريم ايبسوم دولار سيت أميت - ar - {resourceId}"
                });
                list1.Add(new LocalizationItem()
                {
                    ResourceId = resourceId,
                    Lang = "en",
                    Value = $"Lorem ipsum dolor sit amet, consectetur - en - {resourceId}"
                });
            }
        }

        private static void FillCollection_WithCapacity()
        {
            var list2 = new List<LocalizationItem>(Count * 2);
            for (var i = 0; i < Count; i++)
            {
                var resourceId = Guid.NewGuid().ToString();
                list2.Add(new LocalizationItem()
                {
                    ResourceId = resourceId,
                    Lang = "ar",
                    Value = $"لوريم ايبسوم دولار سيت أميت - ar - {resourceId}"
                });
                list2.Add(new LocalizationItem()
                {
                    ResourceId = resourceId,
                    Lang = "en",
                    Value = $"Lorem ipsum dolor sit amet, consectetur - en - {resourceId}"
                });
            }
        }
    }
}
