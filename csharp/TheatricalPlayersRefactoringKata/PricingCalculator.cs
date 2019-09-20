using LanguageExt;
using System;
using static LanguageExt.Prelude;

namespace TheatricalPlayersRefactoringKata
{
    internal static class PricingCalculator
    {
        private static readonly Map<string, Func<int, int>> amountMap = 
            Map<string, Func<int, int>>(("tragedy", CalculatePriceForTragedy), ("comedy", CalculatePriceForComedy));

        public static Try<int> CalculatePriceFor(string typeOfPerformance, int audience) 
            => () => amountMap[typeOfPerformance](audience);

        private static int CalculatePriceForTragedy(int audience)
            => audience > 30 ? 40000 + 1000 * (audience - 30) : 40000;

        private static int CalculatePriceForComedy(int audience)
            => 30000 + (300 * audience) + (audience > 20 ? 10000 + 500 * (audience - 20) : 0);
    }
}