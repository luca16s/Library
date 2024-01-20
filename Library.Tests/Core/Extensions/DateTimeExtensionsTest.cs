namespace Library.Tests.Core.Extensions;

using FluentAssertions;

using global::Core.Extensions;

using System;

using Xunit;

public class DateTimeExtensionsTest
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
