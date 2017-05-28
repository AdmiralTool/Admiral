using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace smartadmiral.parser.tests
{
    public class ShipsParserTests
    {
        private const string SHIPS_FILE_NAME = @"samples\ships.yml";

        public static IEnumerable<object[]> ExpectedHostnames()
        {
            yield return new object[]
            {
                "common",
                new[] {"192.168.40.228", "192.168.40.226"}
            };
            yield return new object[]
            {
                "child",
                new[] {"192.168.40.228", "192.168.40.226", "web.iis.shmiis", "secondhost"}
            };
            yield return new object[]
            {
                "very child",
                new[]
                {
                    "192.168.40.228", "192.168.40.226", "web.iis.shmiis", "secondhost", "web.iis.shmiis11",
                    "secondhost11"
                }
            };
        }

        [Theory]
        [MemberData(nameof(ExpectedHostnames))]
        public void GetHostnames_ReturnsCorrectHostnames(string groupName,
            IEnumerable<string> expectedHostnames)
        {
            var sut = new ShipsParser();

            var result = sut.GetHostnames(SHIPS_FILE_NAME, groupName);

            result.ShouldBeEquivalentTo(expectedHostnames);
        }
    }
}