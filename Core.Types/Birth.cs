namespace Core.Types;
// https://learn.microsoft.com/en-us/ef/core/modeling/constructors
public record Birth
{
    // EF Core
    private Birth() { }

    public DateTime Value { get; private set; }

    public static Birth Of(DateTime value)
    {
        // validations should be placed here instead of constructor
        //if (value == default)
        //    throw new DomainException($"BirthDate {value} cannot be null");

        DateTime minDateOfBirth = DateTime.Now.AddYears(-115);
        DateTime maxDateOfBirth = DateTime.Now.AddYears(-15);

        // Validate the minimum age.
        //if (value < minDateOfBirth || value > maxDateOfBirth)
        //    throw new DomainException("The minimum age has to be 15 years.");

        return new Birth { Value = value };
    }

    public static implicit operator DateTime(Birth value) => value.Value;
}
