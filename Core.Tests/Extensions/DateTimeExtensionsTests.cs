namespace Core.Tests.Extensions
{
    using Core.Extensions;

    using FluentAssertions;

    using System;

    using Xunit;

    public class DateTimeExtensionsTests
    {
        [Fact]
        public void ShouldReturnUnixDateFormat()
        {
            double expected = 1659139200;
            var date = new DateTime(2022, 07, 30, 00, 00, 00);
            var actual = date.ToUnixEpochDate();

            _ = expected.Should().Be(actual);
        }

        [Fact]
        public void ShouldReturnUnixDateFormatInString()
        {
            var expected = "1659139200";
            var date = new DateTime(2022, 07, 30, 00, 00, 00);
            var result = date.ToUnixEpochDateToString();

            Assert.Equal(expected, result);
        }
    }
}
