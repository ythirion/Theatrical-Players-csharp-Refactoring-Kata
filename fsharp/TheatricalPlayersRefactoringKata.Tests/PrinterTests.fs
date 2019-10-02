module PrinterTests

open TheatricalPlayersRefactoringKata.StatementPrinter
open ApprovalTests.Reporters
open System
open Xunit
open ApprovalTests

[<Fact>]
[<UseReporter(typedefof<DiffReporter>)>]
let ``test statement example`` () =
    let plays = Map.empty.
                    Add("hamlet", { Name="Hamlet"; Type= "tragedy" }).
                    Add("as-like", { Name="As You Like It"; Type= "comedy" }).
                    Add("othello", { Name="Othello"; Type= "tragedy" })

    let invoice = { Customer= "BigCo"; 
        Performances = [{ PlayId = "hamlet"; Audience= 55 }; 
                        { PlayId = "as-like"; Audience= 35 }; 
                        { PlayId = "othello"; Audience= 40 }] }

    let result = printStatement(invoice, plays) 
    Approvals.Verify result |> ignore

[<Fact>]
let ``With an unknown play types it must throw an exception`` () =
    let plays = Map.empty.
                    Add("henry-v", { Name="Henry V"; Type= "history" }).
                    Add("henry-v", { Name="As You Like It"; Type= "pastoral" })

    let invoice = { Customer= "BigCoII"; Performances = [{ PlayId = "henry-v"; Audience= 53 }; { PlayId = "as-like"; Audience= 55 }] }

    Assert.Throws<Exception>(fun() -> printStatement(invoice, plays) |> ignore) |> ignore

