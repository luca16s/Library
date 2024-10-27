namespace Core.Tests.Extensions;

using Core.Services.Extensions;

using FluentAssertions;

using System.Diagnostics.CodeAnalysis;

using Test.Types;

[ExcludeFromCodeCoverage]
[Trait("Core", value: "Extensions")]
public class DateExtensionsTests
{
    [Test]
    public void ToUnixEpochDeveRetornarDataEmFormatoUnix()
    {
        var date = DateTime.Now;
        var expected = ((DateTimeOffset)date.ToUniversalTime()).ToUnixTimeSeconds();

        var result = date.ToUnixEpoch();

        result.Should().Be(expected);
    }

    [Test]
    public void ToUnixEpochToStringDeveRetornarDataEmFormatoUnixComoString()
    {
        var date = DateTime.Now;
        var expected = ((DateTimeOffset)date.ToUniversalTime()).ToUnixTimeSeconds().ToString();

        var result = date.ToUnixEpochToString();

        result.Should().Be(expected);
    }
}
