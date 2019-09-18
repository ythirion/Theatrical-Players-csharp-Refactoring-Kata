using System;
using System.Collections.Generic;
using System.Linq;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(
            Invoice invoice, 
            Dictionary<string, Play> plays)
        {
            return Print(invoice, plays, TextFormatter.ForLine, TextFormatter.ForStatement);
        }
        
        private string Print(
            Invoice invoice,
            Dictionary<string, Play> plays,
            Func<string, int, int, string> lineFormatter,
            Func<string, Statement, string> statementFormatter)
        {
           return invoice.Performances
                .Select(performance => CreateStatement(plays, performance, lineFormatter))
                .Aggregate((context, line) => context.Append(line))
                ?.FormatFor(invoice.Customer, statementFormatter);
        }

        private static Statement CreateStatement(
            Dictionary<string, Play> plays,
            Performance performance,
            Func<string, int, int, string> lineFormatter)
        {
            var performanceType = plays[performance.PlayID].Type;
            var amount = PricingCalculator.Calculate(performanceType, performance.Audience);
            var credits = CreditsCalculator.Calculate(performanceType, performance.Audience);

            return new Statement(
                lineFormatter(plays[performance.PlayID].Name, amount, performance.Audience),
                amount,
                credits);
        }
    }
}
