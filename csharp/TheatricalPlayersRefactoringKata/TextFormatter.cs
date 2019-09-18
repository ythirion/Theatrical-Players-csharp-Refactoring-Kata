using System;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    internal static class TextFormatter
    {
        private static readonly CultureInfo formatProvider = new CultureInfo("en-US");
        internal static string ForLine(string name, int amount, int audience) => 
            string.Format(formatProvider, "  {0}: {1:C} ({2} seats)\n", name, amount / 100, audience);

        internal static string ForStatement(
            string customer,
            Statement statement) =>
            string.Format(formatProvider,
                "Statement for {0}\n{1}Amount owed is {2:C}\nYou earned {3} credits\n",
                customer, statement.Text, Convert.ToDecimal(statement.Amount / 100), statement.Credits);
    }
}
