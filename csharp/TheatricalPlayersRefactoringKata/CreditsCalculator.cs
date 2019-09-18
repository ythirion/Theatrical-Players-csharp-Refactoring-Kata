using System;

namespace TheatricalPlayersRefactoringKata
{
    internal static class CreditsCalculator
    {
        public static int CalculateCreditsFor(string performanceType, int audience)
            => Math.Max(audience - 30, 0) + (performanceType == "comedy" ? (int)Math.Floor((decimal)audience / 5) : 0);
    }
}
 