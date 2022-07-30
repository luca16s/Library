namespace Core.Tests.Validations
{
    using Core.Models;
    using Core.Validations;
    using FluentAssertions;

    using System;

    using Xunit;

    public class DomainValidatorTests
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
        public class ClasseIntValidator : DomainValidator<ClasseInt, int>
        {
            public ClasseIntValidator(ClasseInt entidade) : base(entidade) { }

            protected override void Validar()
            {
            }
        }

        [Fact]
        public void ShouldThrowExceptionWhenEntityIsNull()
        {
            _ = this.Invoking(g => new ClasseIntValidator(null))
                 .Should()
                 .Throw<ArgumentNullException>()
                 .WithMessage("Entidade não pode ser nula. (Parameter 'entidade')");
        }
    }
}
