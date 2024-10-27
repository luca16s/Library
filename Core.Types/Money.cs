namespace Core.Types;

using Ardalis.GuardClauses;

public class Money
{
    private Money() { }

    public decimal Value { get; private set; }
    public string Currency { get; private set; } = default!;

    public static Money Of(decimal value, string currency)
    {
        Guard.Against.NegativeOrZero(value, nameof(value));
        Guard.Against.NullOrWhiteSpace(currency, nameof(currency));

        return new Money
        {
            Value = value,
            Currency = currency
        };
    }

    public static Money operator *(int left, Money right)
    {
        Guard.Against.Null(right, nameof(right));

        return Of(right.Value * left, right.Currency);
    }
}
