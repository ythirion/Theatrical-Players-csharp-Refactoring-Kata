using LanguageExt;
using System;
using System.Collections.Generic;
using System.Globalization;
using static LanguageExt.Prelude;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach(var perf in invoice.Performances)
            {
                var play = plays[perf.PlayID];
                var thisAmount = CalculateAmountFor(perf, play)
                    .Match(amount => amount, fail => throw new Exception("unknown type: " + play.Type));

                volumeCredits += CalculateCredits(perf, play);

                // print line for this order
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                totalAmount += thisAmount;
            }
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }

        private static int CalculateCredits(Performance perf, Play play) =>
            CalculateDefaultCredits(perf) + ("comedy" == play.Type ? (int)Math.Floor((decimal)perf.Audience / 5) : 0);

        private static int CalculateDefaultCredits(Performance perf) => Math.Max(perf.Audience - 30, 0);

        /* // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
*/

        private static Map<string, Func<Performance, int>> calculateMap = Map<string, Func<Performance, int>>(("tragedy", CalculateTragedyAmount), ("comedy", CalculateComedyAmount));

        private static Try<int> CalculateAmountFor(Performance perf, Play play) => () => calculateMap[play.Type](perf);

        private static int CalculateComedyAmount(Performance perf)
        {
            int thisAmount = 30000;
            if (perf.Audience > 20)
            {
                thisAmount += 10000 + 500 * (perf.Audience - 20);
            }
            thisAmount += 300 * perf.Audience;
            return thisAmount;
        }

        private static int CalculateTragedyAmount(Performance perf)
        {
            int thisAmount = 40000;
            if (perf.Audience > 30)
            {
                thisAmount += 1000 * (perf.Audience - 30);
            }

            return thisAmount;
        }
    }
}
