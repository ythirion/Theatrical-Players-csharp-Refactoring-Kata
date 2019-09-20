using System;
using System.Collections.Generic;
using System.Linq;
using static TheatricalPlayersRefactoringKata.TextFormatter;
using static TheatricalPlayersRefactoringKata.PricingCalculator;
using static TheatricalPlayersRefactoringKata.CreditsCalculator;


namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(
            Invoice invoice, 
            Dictionary<string, Play> plays)
        {
            return Print(invoice, plays, FormatLineToText, FormatStatementToText);
        }
        
        private string Print(
            Invoice invoice,
            Dictionary<string, Play> plays,
            Func<string, int, int, string> lineFormatter,
            Func<string, Statement, string> statementFormatter)
        {
           return invoice.Performances
                .Map(performance => CreateStatement(plays, performance, lineFormatter))
                .Reduce((context, line) => context.Append(line))
                ?.FormatFor(invoice.Customer, statementFormatter);
        }

        private static Statement CreateStatement(
            Dictionary<string, Play> plays,
            Performance performance,
            Func<string, int, int, string> lineFormatter)
        {
            var performanceType = plays[performance.PlayID].Type;
            var amount = CalculatePrice(performance, performanceType);
            var credits = CalculateCreditsFor(performanceType, performance.Audience)

            return new Statement(
                lineFormatter(plays[performance.PlayID].Name, amount, performance.Audience),
                amount,
                credits);
        }

        private static int CalculatePrice(Performance performance, string performanceType)
        {
            var amount = 0;
            CalculatePriceFor(performanceType, performance.Audience)
                            .Match(s => amount = s,
                                   fail => throw new Exception("unknown type: " + performanceType));
            return amount;
        }
    }
}
