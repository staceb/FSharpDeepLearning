open System
open System.Windows.Forms
open FSharp.Charting

let linear x =
    x

let relu x =
    if x < 0.0 then 0.0
    else x

let sigmoid x =
    1.0 / (1.0 + Math.E ** - x)

let swish x =
    x / (1.0 + Math.E ** -x)

[<EntryPoint>]
let main argv =
    let linearline = [ for x in -3.0 .. 0.01 .. 3.0 -> (x, linear x) ] |> Chart.Line
    let reluline = [ for x in -3.0 .. 0.01 .. 3.0 -> (x, relu x) ] |> Chart.Line
    let swishline = [ for x in -3.0 .. 0.01 .. 3.0 -> (x, swish x) ] |> Chart.Line
    let sigmoidline = [ for x in -3.0 .. 0.01 .. 3.0 -> (x, sigmoid x) ] |> Chart.Line
    
    let combined = [ linearline; 
                     reluline; 
                     swishline; 
                     sigmoidline ]

    let chart = (Chart.Combine combined).ShowChart ()
    
    Application.Run chart

    0
