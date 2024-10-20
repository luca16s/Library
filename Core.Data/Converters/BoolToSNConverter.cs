namespace Core.Data.Converters;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class BoolToSNConverter : BoolToStringConverter
{
    private static BoolToSNConverter? _instance;
    public static BoolToSNConverter Instance => _instance ??= new BoolToSNConverter();

    private BoolToSNConverter() : base("N", "S") { }
}