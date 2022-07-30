namespace Core.Tests.Validations
{
    using Core.Models;
    using Core.Validations;
    using FluentAssertions;

    using System;

    using Xunit;

    public class DomainSpecificationTests
    {
        public class ClasseInt : Entity<int>
        {
            public string Nome { get; set; }

            public ClasseInt(int id)
                : base(id) { }

            public override bool IsConsistent()
            {
                throw new NotImplementedException();
            }
        }
        public class ClasseIntSpecification : DomainSpecification<ClasseInt, int>
        {
            public ClasseIntSpecification(ClasseInt entidade) : base(entidade) { }

            public override bool IsValid()
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void ShouldThrowExceptionWhenEntityIsNull()
        {
            _ = this.Invoking(g => new ClasseIntSpecification(null))
                 .Should()
                 .Throw<ArgumentNullException>()
                 .WithMessage("Entidade não pode ser nula. (Parameter 'entidade')");
        }
    }
}
