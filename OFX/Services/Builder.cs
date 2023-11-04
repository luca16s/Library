namespace OFX.Services
{
    using OFX.Enums;
    using OFX.Interfaces;
    using OFX.Models;

    using System.Xml.Linq;

    public class Builder : IBuilder
    {
        private const string MEMO = "MEMO";
        private const string DTEND = "DTEND";
        private const string FITID = "FITID";
        private const string BANKID = "BANKID";
        private const string ACCTID = "ACCTID";
        private const string BALAMT = "BALAMT";
        private const string DTASOF = "DTASOF";
        private const string CURDEF = "CURDEF";
        private const string TRNAMT = "TRNAMT";
        private const string DTSTART = "DTSTART";
        private const string STMTTRN = "STMTTRN";
        private const string TRNTYPE = "TRNTYPE";
        private const string LANGUAGE = "LANGUAGE";
        private const string DTSERVER = "DTSERVER";
        private const string ACCTTYPE = "ACCTTYPE";
        private const string DTPOSTED = "DTPOSTED";
        private const string CHECKNUM = "CHECKNUM";

        private readonly IConverter converter;

        private IEnumerable<(
            string id,
            string? type,
            string? value,
            DateTime date,
            string? checknum,
            string? description
        )> CreateTransaction(XDocument document)
        {
            foreach (XElement node in converter.GetNodes(STMTTRN, document))
            {
                var transactionId = converter.GetValue(FITID, node);
                var transactionType = converter.GetValue(TRNTYPE, node);
                var transactionValue = converter.GetValue(TRNAMT, node);
                var transactionDate = converter.GetValue(DTPOSTED, node);
                var transactionDescription = converter.GetValue(MEMO, node);
                var transactionCheckNum = converter.GetValue(CHECKNUM, node);

                yield return (
                    transactionId,
                    transactionType,
                    transactionValue,
                    converter.ConvertOfxDateToDateTime(transactionDate),
                    transactionCheckNum,
                    transactionDescription
                );

            }
        }

        public Builder(IConverter converter) => this.converter = converter;

        public Bank BuildBank(XDocument document)
        {
            var bank = new Bank();

            var idBanco = converter.GetValue(BANKID, document);

            bank.Add(idBanco);

            return bank;
        }

        public Header BuildHeader(XDocument document)
        {
            var header = new Header();

            var language = converter.GetValue(LANGUAGE, document);
            var serverDate = converter.GetValue(DTSERVER, document);

            header.Add(
                language,
                converter.ConvertOfxDateToDateTime(serverDate)
            );

            return header;
        }

        public Balance BuildBalance(XDocument document)
        {
            var balance = new Balance();

            var balanco = converter.GetValue(BALAMT, document);
            var dataUltimoLancamento = converter.GetValue(DTASOF, document);

            balance.Add(
                balanco,
                converter.ConvertOfxDateToDateTime(dataUltimoLancamento)
            );

            return balance;
        }

        public Account BuildAccount(XDocument document)
        {
            var contaBancaria = new Account();

            var accountId = converter.GetValue(ACCTID, document);
            var accountType = converter.GetValue(ACCTTYPE, document);

            contaBancaria.Add(
                EBankType.CC.ToString(),
                accountId,
                accountType
            );

            return contaBancaria;
        }

        public Statement BuildStatement(XDocument document)
        {
            var idBanco = converter.GetValue(BANKID, document);
            var balanco = converter.GetValue(BALAMT, document);
            var finalDate = converter.GetValue(DTEND, document);
            var currency = converter.GetValue(CURDEF, document);
            var accountId = converter.GetValue(ACCTID, document);
            var language = converter.GetValue(LANGUAGE, document);
            var serverDate = converter.GetValue(DTSERVER, document);
            var initialDate = converter.GetValue(DTSTART, document);
            var accountType = converter.GetValue(ACCTTYPE, document);
            var dataUltimoLancamento = converter.GetValue(DTASOF, document);

            var statement = new Statement();

            statement.AddBank(idBanco);

            statement.AddHeader(
                language,
                converter.ConvertOfxDateToDateTime(serverDate)
            );

            statement.AddBalance(
                balanco,
                converter.ConvertOfxDateToDateTime(dataUltimoLancamento)
            );

            statement.AddAccount(
                EBankType.CC.ToString(),
                accountId,
                accountType
            );

            foreach (var (id, type, value, date, checknum, description) in CreateTransaction(document))
            {
                statement.AddTransaction(
                   id: id,
                    type: type,
                    date: date,
                    value: value,
                    checknum: checknum,
                    description: description
            );
            }

            statement.Add(
                currency,
                converter.ConvertOfxDateToDateTime(finalDate),
                converter.ConvertOfxDateToDateTime(initialDate)
            );

            return statement;
        }

        public IOrderedEnumerable<Transaction> BuildTransactions(XDocument document)
        {
            var transacoes = new List<Transaction>();

            foreach (var (id, type, value, date, checknum, description) in CreateTransaction(document))
            {
                var transacao = new Transaction();

                transacao.Add(
                    id: id,
                    type: type,
                    date: date,
                    value: value,
                    checknum: checknum,
                    description: description
                );

                transacoes.Add(transacao);
            }

            return transacoes.OrderBy(t => t.Date);
        }
    }
}
