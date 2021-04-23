using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using Models;

namespace CollectionsBenchMark
{
    [MemoryDiagnoser]
    [HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.BranchInstructions)]
    public class Program
    {
        private string _resourceId;
        private string _lang;

        private static List<LocalizationItem> LocalizationList { get; set; }
        private static Dictionary<string, string> LocalizationDictionary { get; set; }

        private static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        [GlobalSetup]
        public void Setup()
        {
            LocalizationList = LocalizationItemService.GetList();

            var comparer = StringComparer.OrdinalIgnoreCase;
            LocalizationDictionary = LocalizationList.ToDictionary(x => $"{x.ResourceId}_{x.Lang}", x => x.Value, comparer);

            var lastItem = LocalizationList.Last();

            _resourceId = lastItem.ResourceId;
            _lang = "ar";
        }

        [Benchmark(Baseline = true)]
        public (string, TimeSpan) Translate_Base() { return Translate_Base(_resourceId, _lang); }
        private (string, TimeSpan) Translate_Base(string resourceId, string lang)
        {
            var sw = new Stopwatch();
            sw.Start();

            var value = LocalizationList
                .FirstOrDefault(x => x.ResourceId == resourceId && x.Lang == lang)?.Value;

            sw.Stop();
            return (value, sw.Elapsed);
        }

        [Benchmark]
        public (string, TimeSpan) Translate_IgnoreCase1() { return Translate_IgnoreCase1(_resourceId, _lang); }
        private (string, TimeSpan) Translate_IgnoreCase1(string resourceId, string lang)
        {
            var sw = new Stopwatch();
            sw.Start();

            var value = LocalizationList
                .FirstOrDefault(x => x.ResourceId.ToUpper() == resourceId.ToUpper() &&
                                     x.Lang.ToUpper() == lang.ToUpper())?.Value;

            sw.Stop();
            return (value, sw.Elapsed);
        }

        [Benchmark]
        public (string, TimeSpan) Translate_IgnoreCase2() { return Translate_IgnoreCase2(_resourceId, _lang); }
        private (string, TimeSpan) Translate_IgnoreCase2(string resourceId, string lang)
        {
            var sw = new Stopwatch();
            sw.Start();

            resourceId = resourceId.ToUpper();
            lang = lang.ToUpper();

            var value = LocalizationList
                .FirstOrDefault(x => x.ResourceId.ToUpper() == resourceId &&
                                     x.Lang.ToUpper() == lang)
                ?.Value;

            sw.Stop();
            return (value, sw.Elapsed);
        }

        [Benchmark]
        public (string, TimeSpan) Translate_IgnoreCase3() { return Translate_IgnoreCase3(_resourceId, _lang); }
        private (string, TimeSpan) Translate_IgnoreCase3(string resourceId, string lang)
        {
            var sw = new Stopwatch();
            sw.Start();

            var value = LocalizationList
                .FirstOrDefault(x => x.ResourceId.Equals(resourceId, StringComparison.OrdinalIgnoreCase) &&
                                     x.Lang.Equals(lang, StringComparison.OrdinalIgnoreCase))
                ?.Value;

            sw.Stop();
            return (value, sw.Elapsed);
        }

        [Benchmark]
        public (string, TimeSpan) Translate_IgnoreCase4() { return Translate_IgnoreCase4(_resourceId, _lang); }
        private (string, TimeSpan) Translate_IgnoreCase4(string resourceId, string lang)
        {
            var sw = new Stopwatch();
            sw.Start();

            var value = LocalizationDictionary[$"{resourceId}_{lang}"];

            sw.Stop();
            return (value, sw.Elapsed);
        }
    }
}
