namespace Core.Tests.Extensions
{
    using Core.Extensions;

    using System;

    using Xunit;

    public class DateTimeExtensionsTests
    {
        [Fact]
        public void ShouldReturnUnixDateFormat()
        {
            var expected = 1659139200;
            var date = new DateTime(2022, 07, 30, 00, 00, 00);
            var result = date.ToUnixEpochDate();

            Assert.Equal(expected, result);
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
