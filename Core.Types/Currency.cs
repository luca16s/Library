// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
namespace Core.Types;
// https://learn.microsoft.com/en-us/ef/core/modeling/constructors
public record Currency
{
    // EF
    private Currency(string value)
    {
        Value = value;
    }

    // Note: in entities with none default constructor, for setting constructor parameter, we need a private set property
    // when we didn't define this property in fluent configuration map, because for getting mapping list of properties to set
    // in the constructor it should not be read only without set (for bypassing calculate fields)- https://learn.microsoft.com/en-us/ef/core/modeling/constructors#read-only-properties
    public string Value { get; private set; } = default!;

    public static Currency Of(string value) =>
        // validations should be placed here instead of constructor
        //Guard.Against.InvalidCurrency(value, new DomainException($"Currency {value} is invalid."));
        new(value);

    public static implicit operator string(Currency value) => value.Value;
}
