namespace TheatricalPlayersRefactoringKata
{
    public class Statement
    {
        public int Amount { get; }
        public int Credits { get; }
        public string Text { get; }

        public Statement(
            int amount,
            int credits,
            string text)
        {
            Amount = amount;
            Credits = credits;
            Text = text;
        }

        internal Statement Append(Statement line)
        {
            return new Statement(
                Amount + line.Amount,
                Credits + line.Credits,
                Text + line.Text
                );
        }
    }
}