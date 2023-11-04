namespace OFX.Converters
{
    using OFX.Enums;
    using OFX.Interfaces;

    using System;
    using System.Text;
    using System.Xml.Linq;

    public class Converter : IConverter
    {
        private const string OpeningMark = "<";
        private const string ClosingMark = ">";
        private const string OpeningEndMark = "</";
        private const string TagBalanco = "<BALAMT>";
        private const string TagRecebidos = "<PRINYTD>";
        private const string TagEmprestimos = "<PRINLTD>";

        private static string BuildClosingTag(
            string content
        )
        {
            var opening = content.IndexOf(OpeningMark);
            var closing = content.IndexOf(ClosingMark);

            return opening == -1 || closing == -1 || closing - opening <= 2
                ? string.Empty
                : content
                    .Substring(opening, closing - opening + 1)
                    .Replace(OpeningMark, OpeningEndMark);
        }

        private static string TranslateToXml(
            string path
        )
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Caminho do arquivo OFX não é válido: " + path);
            }

            StringBuilder xml = new();

            using (StreamReader sr = File.OpenText(path))
            {
                string? line;
                var level = 0;

                while ((line = sr.ReadLine()) is not null or "")
                {
                    line = line.Trim();

                    var hasOpeningMark = line.StartsWith(OpeningMark);
                    var hasCloasingMark = line.EndsWith(ClosingMark);
                    var hasOpeningEndMark = line.StartsWith(OpeningEndMark);

                    if (hasOpeningEndMark && hasCloasingMark)
                    {
                        level--;
                        _ = xml.Append(line);
                        continue;
                    }

                    if (hasOpeningMark && hasCloasingMark)
                    {
                        //Ajuste para possibilidade de tags vazias.
                        //(Por padrão não deveriam existir nos arquivos OFX)
                        if (line is TagBalanco or TagRecebidos or TagEmprestimos)
                        {
                            _ = xml.Append(line);
                            _ = xml.Append(BuildClosingTag(line));
                            continue;
                        }

                        level++;
                        _ = xml.Append(line);
                        continue;
                    }

                    if (hasOpeningMark && !hasCloasingMark)
                    {
                        _ = xml.Append(line);
                        _ = xml.Append(BuildClosingTag(line));
                    }
                }
            }

            return xml.ToString();
        }

        private static int GetPartOfOfxDate(
            string date,
            EPartDateTime partDateTime
        )
        {
            return partDateTime switch
            {
                EPartDateTime.DAY => int.Parse(date[6..8]),
                EPartDateTime.YEAR => int.Parse(date[..4]),
                EPartDateTime.MONTH => int.Parse(date[4..6]),
                EPartDateTime.HOUR => int.Parse(date[8..10]),
                EPartDateTime.MINUTE => int.Parse(date[10..12]),
                EPartDateTime.SECOND => int.Parse(date[12..14]),
                _ => 0
            };
        }

        public string GetValue(
            string tag,
            XContainer element
        )
        {
            IEnumerable<string> result = from node
                           in element.Descendants(tag)
                                         select node.Value;

            return result.FirstOrDefault() ?? string.Empty;
        }

        public byte[] ConvertToStream(
            string path
        ) => Encoding.UTF8.GetBytes(TranslateToXml(path));

        public string ConvertToString(
            string path
        ) => TranslateToXml(path);

        public IEnumerable<XElement> GetNodes(
            string tag,
            XDocument document
        )
        {
            return from node
                     in document.Descendants(tag)
                   select node;
        }

        public DateTime ConvertOfxDateToDateTime(
            string? date
        )
        {
            if (string.IsNullOrWhiteSpace(date))
                throw new ArgumentNullException($"Data não pode estar nula ou vazia: {date}");

            var day = GetPartOfOfxDate(date, EPartDateTime.DAY);
            var year = GetPartOfOfxDate(date, EPartDateTime.YEAR);
            var hour = GetPartOfOfxDate(date, EPartDateTime.HOUR);
            var month = GetPartOfOfxDate(date, EPartDateTime.MONTH);
            var minute = GetPartOfOfxDate(date, EPartDateTime.MINUTE);
            var second = GetPartOfOfxDate(date, EPartDateTime.SECOND);

            return new DateTime(year, month, day, hour, minute, second);
        }
    }
}
