using CashOverflow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashOverflow.Application.Common.Helpers
{
    public static class SeedingHelpers
    {
        public static CounterPartyRole ParseCounterpartyRole(string val) => val.Trim().ToLowerInvariant() switch
        {
            "customer" => CounterPartyRole.Customer,
            "supplier" => CounterPartyRole.Supplier,
            "both" => CounterPartyRole.Both,
            _ => Enum.TryParse<CounterPartyRole>(val, true, out var r) ? r : CounterPartyRole.Customer
        };


        public static InvoiceDirection ParseInvoiceDirection(string val) => val.Trim().ToUpperInvariant() switch
        {
            "AR" or "RECEIVABLE" => InvoiceDirection.Receivable,
            "AP" or "PAYABLE" => InvoiceDirection.Payable,
            _ => Enum.TryParse<InvoiceDirection>(val, true, out var d) ? d : InvoiceDirection.Receivable
        };


        public static InvoiceStatus ParseInvoiceStatus(string val) => val.Trim().ToLowerInvariant() switch
        {
            "draft" => InvoiceStatus.Draft,
            "issued" => InvoiceStatus.Issued,
            "partially_paid" => InvoiceStatus.PartiallyPaid,
            "paid" => InvoiceStatus.Paid,
            "overdue" => InvoiceStatus.Overdue,
            "cancelled" => InvoiceStatus.Cancelled,
            _ => Enum.TryParse<InvoiceStatus>(val, true, out var s) ? s : InvoiceStatus.Draft
        };


        public static string[] ParseCsvLine(string line)
        {
            var tokens = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    tokens.Add(sb.ToString().Trim('"', ' '));
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }
            tokens.Add(sb.ToString().Trim('"', ' '));
            return tokens.ToArray();
        }
      
    }
}
