using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TheatricalPlayersRefactoringKata
{
    internal static class PricingCalculator
    {
        private static readonly IReadOnlyDictionary<string, Func<int, int>> amountMap = new ReadOnlyDictionary<string, Func<int, int>>(
            new Dictionary<string, Func<int, int>>
            {
                { "tragedy", CalculatePriceForTragedy },
                { "comedy", CalculatePriceForComedy }
            });

        public static int CalculatePriceFor(string typeOfPerformance, int audience)
            => amountMap.ContainsKey(typeOfPerformance) ? amountMap[typeOfPerformance](audience)
                : throw new Exception("unknown type: " + typeOfPerformance);

        private static int CalculatePriceForTragedy(int audience)
            => audience > 30 ? 40000 + 1000 * (audience - 30) : 40000;

        private static int CalculatePriceForComedy(int audience)
            => 30000 + (300 * audience) + (audience > 20 ? 10000 + 500 * (audience - 20) : 0);
    }
}
