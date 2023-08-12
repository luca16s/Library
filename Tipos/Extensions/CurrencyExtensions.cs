namespace Tipos.Extensions
{
    public static class CurrencyExtensions
    {
        public static decimal GetValorFromPercentual(this decimal value, decimal division) => value * (division / 100);
    }
}
