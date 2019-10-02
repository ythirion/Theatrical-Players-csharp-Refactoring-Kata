namespace TheatricalPlayersRefactoringKata

open System
open System.Globalization

module StatementPrinter =
    type Performance = { 
        PlayId: string
        Audience: int
    }

    type Play = {
        Name: string
        Type: string
    }

    type Invoice = {
        Customer: string
        Performances: List<Performance>
    }

    type Statement = {
        Text: string
        Amount: int
        Credits: int
    }

    let formatProvider = CultureInfo.GetCultureInfo("en-US")

    let formatLineToText name amount audience = 
        String.Format(formatProvider, "  {0}: {1:C} ({2} seats)\n", name, amount / 100, audience)

    let formatStatement statement customer =        
        String.Format(formatProvider, "Statement for {0}\n{1}Amount owed is {2:C}\nYou earned {3} credits\n", customer, statement.Text, statement.Amount / 100, statement.Credits)

    let calculateComedyPrice audience =  
        30000 + (300 * audience) + if audience > 20 then 10000 + 500 * (audience - 20) else 0

    let calculateTragedyPrice audience =  
        if audience > 30 then 40000 + 1000 * (audience - 30) else 40000

    let calculateCredits performanceType audience = 
        Math.Max(audience - 30, 0) + if performanceType = "comedy" then audience / 5 else 0

    let createStatement (plays:Map<string, Play>, performance, formatLine) : Statement = 
        let performanceType = plays.[performance.PlayId].Type
        let amount = 
            match performanceType with
                | "comedy" -> calculateComedyPrice performance.Audience
                | "tragedy" -> calculateTragedyPrice performance.Audience
                | _ -> raise(System.Exception("unknown type: " + performanceType))

        let credits = calculateCredits performanceType performance.Audience
        let line = formatLine (plays.[performance.PlayId].Name) amount performance.Audience

        { Text= line; Amount = amount; Credits = credits }

    let printStatement (invoice, plays:Map<string, Play>) =
        invoice.Performances
            |> Seq.map(fun performance -> createStatement(plays, performance, formatLineToText))
            |> Seq.reduce(fun context line -> { Text = context.Text + line.Text; Amount = context.Amount + line.Amount; Credits = context.Credits + line.Credits })
            |> fun statement -> formatStatement statement invoice.Customer
   