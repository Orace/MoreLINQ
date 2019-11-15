using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MoreLinq.Test.Properties;
using NUnit.Framework;

namespace MoreLinq.Test
{
    [TestFixture]
    public class DocumentationTest
    {
        [Test]
        public void OverloadCountTest2()
        {
            var libraryOverloadCounts = GetOverloadCounts(typeof(MoreEnumerable));
            var csproj = ParseOverloadCounts2();

            var keys = libraryOverloadCounts.Keys.Union(csproj).Distinct();

            var sb = new StringBuilder();
            foreach (var key in keys)
            {
                var inLib = libraryOverloadCounts.ContainsKey(key);
                var inProj = csproj.Contains(key);

                if (inLib != inProj)
                {
                    sb.AppendLine($"{key} in lib: {inLib}, in proj: {inProj}");
                }
            }

            var message = sb.ToString();

            Assert.That(string.IsNullOrEmpty(message), message);
        }

        public IReadOnlyList<string> ParseOverloadCounts2()
        {
            using var memoryStream = new MemoryStream(Resources.MoreLinq);
            using var reader = new StreamReader(memoryStream);

            var result = new List<string>();

            while (!reader.EndOfStream && !reader.ReadLine().Contains("This project enhances LINQ to Objects"))
            {
            }

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line == "    </Description>")
                    break;

                var matchName = Regex.Match(line, @"\s*\-\s*(?<methodName>.*)");
                if (matchName.Success)
                {
                    result.Add(matchName.Groups["methodName"].Value);
                }
            }

            return result;
        }

        [Test]
        public void OverloadCountTest()
        {
            var libraryOverloadCounts = GetOverloadCounts(typeof(MoreEnumerable));
            var readMeOverloadCounts = ParseOverloadCounts();

            var keys = libraryOverloadCounts.Keys.Union(readMeOverloadCounts.Keys).Distinct();

            var sb = new StringBuilder();
            foreach (var key in keys)
            {
                libraryOverloadCounts.TryGetValue(key, out var libCount);
                readMeOverloadCounts.TryGetValue(key, out var rmCount);

                if (libCount != rmCount)
                {
                    sb.AppendLine($"{key} has {libCount} overloads, {rmCount} in the readme");
                }
            }

            var message = sb.ToString();

            Assert.That(string.IsNullOrEmpty(message), message);
        }

        private static IReadOnlyDictionary<string, int> ParseOverloadCounts()
        {
            using var memoryStream = new MemoryStream(Resources.README);
            using var reader = new StreamReader(memoryStream);

            var result = new Dictionary<string, int>();

            while (!reader.EndOfStream && reader.ReadLine() != "## Operators")
            {
            }

            string methodName = null;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line == "## Experimental Operators")
                    break;

                var matchName = Regex.Match(line, @"###\s+~*(?<methodName>[^~]*)~*");
                if (matchName.Success)
                {
                    if (methodName != null)
                        result.Add(methodName, 1);
                    methodName = matchName.Groups["methodName"].Value;
                }

                var matchCount = Regex.Match(line, @"This method has (?<count>\d+) overloads.");
                if (matchCount.Success && methodName != null)
                {
                    var count = int.Parse(matchCount.Groups["count"].Value);
                    result.Add(methodName, count);
                    methodName = null;
                }
            }

            return result;
        }

        private static IReadOnlyDictionary<string, int> GetOverloadCounts(Type type)
        {
            return type.GetMethods().Where(m => m.IsPublic && m.DeclaringType == type).GroupBy(m => m.Name).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
