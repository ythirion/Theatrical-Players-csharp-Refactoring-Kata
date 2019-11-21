using LanguageExt;
using System;
using System.Collections.Generic;
using System.Globalization;
using static LanguageExt.Prelude;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(
            Invoice invoice, 
            Dictionary<string, Play> plays)
        {
            return
                TextFormatter.Format(
                invoice.Performances
                    .Map(p => CreateStatement(p, plays, TextFormatter.ToTextFormat))
                    .Reduce((agg, line) => agg.Append(line)), invoice.Customer);
        }

        private static Statement CreateStatement(
            Performance perf,
            Dictionary<string, Play> plays,
            Func<Performance, Play, int, string> lineFormatter)
        {
            var play = plays[perf.PlayID];
            var amount = CalculateAmountFor(perf, play)
                    .Match(a => a, fail => throw new Exception("unknown type: " + play.Type));

            return new Statement(
                amount,
                CalculateCredits(perf, play),
                lineFormatter(perf, play, amount));
        }

        private static int CalculateCredits(Performance perf, Play play) =>
            CalculateDefaultCredits(perf) + ("comedy" == play.Type ? (int)Math.Floor((decimal)perf.Audience / 5) : 0);

        private static int CalculateDefaultCredits(Performance perf) => Math.Max(perf.Audience - 30, 0);

        private static Map<string, Func<Performance, int>> calculateMap = Map<string, Func<Performance, int>>(("tragedy", CalculateTragedyAmount), ("comedy", CalculateComedyAmount));

        private static Try<int> CalculateAmountFor(Performance perf, Play play) => () => calculateMap[play.Type](perf);

        private static int CalculateComedyAmount(Performance performance)
        {
            int amount = 30000;
            if (performance.Audience > 20)
            {
                amount += 10000 + 500 * (performance.Audience - 20);
            }
            amount += 300 * performance.Audience;

            return amount;
        }

        private static int CalculateTragedyAmount(Performance performance)
        {
            int amount = 40000;
            if (performance.Audience > 30)
            {
                amount += 1000 * (performance.Audience - 30);
            }

            return amount;
        }
    }
}
