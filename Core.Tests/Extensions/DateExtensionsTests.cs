namespace Core.Tests.Extensions;

using Core.Extensions;

using FluentAssertions;

using System.Diagnostics.CodeAnalysis;

using Test.Types;

[ExcludeFromCodeCoverage]
[Trait("Core", value: "Extensions")]
public class DateExtensionsTests
{
    [Test]
    public void DeveRetornarDataEmFormatoUnix()
    {
        var date = DateTime.Now;
        var expected = ((DateTimeOffset)date.ToUniversalTime()).ToUnixTimeSeconds();

        var result = date.ToUnixEpoch();

        result.Should().Be(expected);
    }

    [Test]
    public void DeveRetornarDataEmFormatoUnixComoString()
    {
        var date = DateTime.Now;
        var expected = ((DateTimeOffset)date.ToUniversalTime()).ToUnixTimeSeconds().ToString();

        var result = date.ToUnixEpochToString();

        result.Should().Be(expected);
    }
}
