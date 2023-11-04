namespace OFX.Interfaces
{
    using System;
    using System.Xml.Linq;

    public interface IConverter
    {
        byte[] ConvertToStream(string path);

        string ConvertToString(string path);

        DateTime ConvertOfxDateToDateTime(string? date);

        string GetValue(string tag, XContainer container);

        IEnumerable<XElement> GetNodes(string tag, XDocument document);
    }
}
