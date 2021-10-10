namespace CoreLibrary.Tests
{
    using System;

    using FluentAssertions;

    using KapiCoreLib.Models;

    using Xunit;

    public class BaseEntityTest
    {
        [Fact]
        public void CheckIfGuidPassedIsEqual()
        {
            //Arrange
            Guid generatedGuid = Guid.NewGuid();

            //Act
            BaseEntity entity = new(generatedGuid);

            //Verify
            _ = generatedGuid.Should().Be(entity.Id);
        }
    }
}