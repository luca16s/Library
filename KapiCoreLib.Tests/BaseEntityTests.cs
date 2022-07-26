namespace KapiCoreLib.Tests
{
    using System;

    using Core.Models;

    using FluentAssertions;

    using Xunit;

    public class BaseEntityTest
    {
        public class ClasseTeste : Entity<Guid>
        {
            public ClasseTeste(Guid id)
                : base(id)
            {
            }

            public override bool IsConsistent()
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void CheckIfGuidPassedIsEqual()
        {
            //Arrange
            Guid generatedGuid = Guid.NewGuid();

            //Act
            ClasseTeste entity = new(generatedGuid);

            //Verify
            _ = generatedGuid.Should().Be(entity.Id);
        }
    }
}