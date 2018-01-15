
open System
open System.Windows.Forms
open FSharp.Charting

let sigmoid x =
    1.0 / (1.0 + Math.E ** - x)

let swish x =
    x / (1.0 + Math.E ** -x)

[<EntryPoint>]
let main argv =
    let first = [ for x in -10.0 .. 10.0 -> (x, swish x) ]
    let second = [ for x in -10.0 .. 10.0 -> (x, sigmoid x) ]
    let chart = (Chart.Combine [ Chart.Line (first, Name = "Swish"); Chart.Line (second, Name = "Sigmoid") ]).ShowChart ()

    Application.Run chart

    0
