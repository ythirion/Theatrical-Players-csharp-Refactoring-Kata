using System;

namespace TheatricalPlayersRefactoringKata
{
    internal class Statement
    {
        public string Text { get; }
        public int Amount { get; }
        public int Credits { get; }

        public Statement(
            string text,
            int amount,
            int credits)
        {
            Text = text;
            Amount = amount;
            Credits = credits;
        }

        internal Statement Append(
            Statement statement)
        {
            return new Statement(
                Text + statement.Text, 
                Amount + statement.Amount, 
                Credits + statement.Credits);
        }
    }

    internal static class Extensions
    {
        public static string FormatFor(
            this Statement statement,
            string customer,
            Func<string, Statement, string> formatter)
            => formatter(customer, statement);
    }
}