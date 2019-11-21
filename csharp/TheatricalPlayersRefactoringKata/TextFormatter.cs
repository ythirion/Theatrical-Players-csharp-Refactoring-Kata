using System;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    internal static class TextFormatter
    {
        private static CultureInfo culture = new CultureInfo("EN-US");
        public delegate string Func<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);

        public static string Format(
           Statement statement,
           string customer)
        {
            var result = string.Format("Statement for {0}\n", customer);
            result += statement.Text;
            result += string.Format(culture, "Amount owed is {0:C}\n", Convert.ToDecimal(statement.Amount / 100));
            result += string.Format("You earned {0} credits\n", statement.Credits);

            return result;
        }

        public static string ToTextFormat(Performance perf, Play play, int amount)
        {
            return string.Format(culture, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(amount / 100), perf.Audience);
        }

    }
}