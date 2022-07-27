namespace Core.Tests.Models
{
    using Core.Models;

    using FluentAssertions;

    using System;

    using Xunit;

    public class EntityTest
    {
        public class ClasseGuid : Entity<Guid>
        {
            public ClasseGuid(Guid id)
                : base(id) { }

            public override bool IsConsistent()
            {
                throw new NotImplementedException();
            }
        }

        public class ClasseInt : Entity<int>
        {
            public ClasseInt(int id)
                : base(id) { }

            public override bool IsConsistent()
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void IdShouldBeEqualValuePassed()
        {
            Guid generatedGuid = Guid.NewGuid();

            ClasseGuid entity = new(generatedGuid);

            _ = generatedGuid.Should().Be(entity.Id);
        }

        [Fact]
        public void NotEqualsShouldBeTrueIfComparedWithNull()
        {
            Guid guidA = Guid.NewGuid();

            ClasseGuid entity = new(guidA);

            var result = entity != null;

            _ = result.Should().Be(true);
        }

        [Fact]
        public void NotEqualsShouldBeTrueIfComparedWithEntityOfOtherId()
        {
            Guid guidA = Guid.NewGuid();
            Guid guidB = Guid.NewGuid();

            ClasseGuid entityA = new(guidA);
            ClasseGuid entityB = new(guidB);

            var result = entityA != entityB;

            _ = result.Should().Be(true);
        }

        [Fact]
        public void NotEqualsShouldBeFalseIfComparedWithEntityOfSameId()
        {
            Guid guidA = Guid.NewGuid();

            ClasseGuid entityA = new(guidA);
            ClasseGuid entityB = new(guidA);

            var result = entityA != entityB;

            _ = result.Should().Be(false);
        }

        [Fact]
        public void EqualsShouldBeFalseIfComparedWithNullOnRight()
        {
            Guid guidA = Guid.NewGuid();

            ClasseGuid entity = new(guidA);

            var result = entity == null;

            _ = result.Should().Be(false);
        }

        [Fact]
        public void EqualsShouldBeFalseIfComparedWithNullOnLeft()
        {
            Guid guidA = Guid.NewGuid();

            ClasseGuid entity = new(guidA);
            ClasseGuid entityNull = null;

            var result = entityNull == entity;

            _ = result.Should().Be(false);
        }

        [Fact]
        public void EqualsShouldBeFalseIfComparedWithEntityOfOtherId()
        {
            Guid guidA = Guid.NewGuid();
            Guid guidB = Guid.NewGuid();

            ClasseGuid entityA = new(guidA);
            ClasseGuid entityB = new(guidB);

            var result = entityA == entityB;

            _ = result.Should().Be(false);
        }

        [Fact]
        public void EqualsShouldBeTrueIfComparedWithEntityOfSameId()
        {
            Guid guidA = Guid.NewGuid();

            ClasseGuid entityA = new(guidA);
            ClasseGuid entityB = new(guidA);

            var result = entityA == entityB;

            _ = result.Should().Be(true);
        }

        [Fact]
        public void EqualsShouldBeFalseIfComparedObjectIsNull()
        {
            Guid guidA = Guid.NewGuid();

            ClasseGuid entity = new(guidA);

            var result = entity.Equals(null);

            _ = result.Should().Be(false);
        }

        [Fact]
        public void EqualsShouldBeFalseIfComparedOtherType()
        {
            Guid guidA = Guid.NewGuid();

            ClasseGuid entity = new(guidA);

            var result = entity.Equals(10);

            _ = result.Should().Be(false);
        }

        [Fact]
        public void EqualsShouldBeFalseIfComparedOtherBaseType()
        {
            const int id = 1;
            Guid guidA = Guid.NewGuid();

            ClasseGuid entityGuid = new(guidA);
            ClasseInt entityInt = new(id);

            var result = entityGuid.Equals(entityInt);

            _ = result.Should().Be(false);
        }

        [Fact]
        public void EqualsShouldBeTrueIfHasSameReference()
        {
            Guid guidA = Guid.NewGuid();

            ClasseGuid entityGuid = new(guidA);

            var result = entityGuid.Equals(entityGuid);

            _ = result.Should().Be(true);
        }

        [Fact]
        public void EqualsShouldBeTrueIfIdIsSame()
        {
            const int id = 1;

            ClasseInt entityA = new(id);
            ClasseInt entityB = new(id);

            var result = entityA.Equals(entityB);

            _ = result.Should().Be(true);
        }
    }
}