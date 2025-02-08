namespace Core.Types;

using Ardalis.GuardClauses;

using System;

public class Money
{
    private Money() { }

    public decimal Value { get; private set; }
    public string Currency { get; private set; } = "R$ "!;

    public static Money Of(
        decimal value,
        string currency
    )
    {
        Guard.Against.Negative(value, nameof(value));
        Guard.Against.NullOrWhiteSpace(currency, nameof(currency));

        return new Money
        {
            Value = value,
            Currency = currency
        };
    }

    public static Money Of(
        decimal value
    )
    {
        Guard.Against.Negative(value, nameof(value));

        return new Money
        {
            Value = value
        };
    }

    public static Money Of(
        Money value
    )
    {
        Guard.Against.NegativeOrZero(value.Value, nameof(value));

        return new Money
        {
            Value = value.Value
        };
    }

    #region int to Money

    public static Money operator +(int left, Money right)
    {
        Guard.Against.Null(right, nameof(right));

        return Of(right.Value + left, right.Currency);
    }

    public static Money operator +(Money left, int right)
    {
        Guard.Against.Null(left, nameof(left));

        return Of(left.Value + right, left.Currency);
    }

    public static Money operator -(int left, Money right)
    {
        Guard.Against.Null(right, nameof(right));

        return Of(right.Value - left, right.Currency);
    }

    public static Money operator -(Money left, int right)
    {
        Guard.Against.Null(left, nameof(left));

        return Of(left.Value - right, left.Currency);
    }

    public static Money operator *(int left, Money right)
    {
        Guard.Against.Null(right, nameof(right));

        return Of(right.Value * left, right.Currency);
    }

    public static Money operator *(Money left, int right)
    {
        Guard.Against.Null(left, nameof(left));

        return Of(left.Value * right, left.Currency);
    }

    public static Money operator /(int left, Money right)
    {
        Guard.Against.Null(right, nameof(right));

        return Of(right.Value / left, right.Currency);
    }

    public static Money operator /(Money left, int right)
    {
        Guard.Against.Null(left, nameof(left));

        return Of(left.Value / right, left.Currency);
    }

    #endregion

    #region Decimal to Money

    public static Money operator +(decimal left, Money right)
    {
        Guard.Against.Null(right, nameof(right));

        return Of(right.Value + left, right.Currency);
    }

    public static Money operator +(Money left, decimal right)
    {
        Guard.Against.Null(left, nameof(left));

        return Of(left.Value + right, left.Currency);
    }

    public static Money operator -(decimal left, Money right)
    {
        Guard.Against.Null(right, nameof(right));

        return Of(right.Value - left, right.Currency);
    }

    public static Money operator -(Money left, decimal right)
    {
        Guard.Against.Null(left, nameof(left));

        return Of(left.Value - right, left.Currency);
    }

    public static Money operator *(decimal left, Money right)
    {
        Guard.Against.Null(right, nameof(right));

        return Of(right.Value * left, right.Currency);
    }

    public static Money operator *(Money left, decimal right)
    {
        Guard.Against.Null(left, nameof(left));

        return Of(left.Value * right, left.Currency);
    }

    public static Money operator /(decimal left, Money right)
    {
        Guard.Against.Null(right, nameof(right));

        return Of(right.Value / left, right.Currency);
    }

    public static Money operator /(Money left, decimal right)
    {
        Guard.Against.Null(left, nameof(left));

        return Of(left.Value / right, left.Currency);
    }

    #endregion

    #region Money to Money

    public static Money operator +(Money left, Money right)
    {
        Guard.Against.Null(left, nameof(left));
        Guard.Against.Null(right, nameof(right));

        return left + right;
    }

    public static Money operator -(Money left, Money right)
    {
        Guard.Against.Null(left, nameof(left));
        Guard.Against.Null(right, nameof(right));

        return right - left;
    }

    public static Money operator *(Money left, Money right)
    {
        Guard.Against.Null(left, nameof(left));
        Guard.Against.Null(right, nameof(right));

        return right * left;
    }

    public static Money operator /(Money left, Money right)
    {
        Guard.Against.Null(left, nameof(left));
        Guard.Against.Null(right, nameof(right));

        return right / left;
    }

    #endregion

    public static implicit operator decimal(Money v)
    {
        throw new NotImplementedException();
    }
}
