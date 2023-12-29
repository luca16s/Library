namespace OFX.Interfaces;

using OFX.Models;

using System.Xml.Linq;

/// <summary>
/// 
/// </summary>
public interface IBuilder
{
    Bank BuildBank(XDocument document);

    Header BuildHeader(XDocument document);

    Balance BuildBalance(XDocument document);

    Account BuildAccount(XDocument document);

    Statement BuildStatement(XDocument document);

    IOrderedEnumerable<Transaction> BuildTransactions(XDocument document);
}
