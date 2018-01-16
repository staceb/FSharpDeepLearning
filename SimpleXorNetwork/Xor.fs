open System
open System.Windows.Forms
open FSharp.Charting

let random = new Random ()

type Matrix (rows: int, cols: int) =
    member x.Rows = rows
    member x.Cols = cols
    member x.Data = Array2D.init<float> rows cols (fun x y -> (0.5 - random.NextDouble ()))
    
    static member (+) (a: Matrix, b: Matrix) =
        if a.Rows <> b.Rows || a.Cols <> b.Cols then
            failwith "Invalid Dimension."
        
        let ret = new Matrix (a.Rows, a.Cols)
        
        for r in 0 .. a.Rows - 1 do
            for c in 0 .. a.Cols - 1 do
                ret.Data.[r, c] <- a.Data.[r, c] + b.Data.[r, c]
        
        ret
    
    static member (*) (a: Matrix, b: Matrix) =
        if a.Cols <> b.Rows then
            failwith "Invalid Dimension."
        
        let ret = new Matrix (a.Rows, b.Cols)
        
        for r in 0 .. ret.Rows - 1 do
            for c in 0 .. ret.Cols - 1 do
                let mutable sum = 0.0

                for i in 0 .. a.Cols - 1 do
                    sum <- sum + a.Data.[r, i] * b.Data.[i, c]
                
                ret.Data.[r, c] <- sum

        ret
    
let swish (x: double): double =
    x / (1.0 + Math.E ** -x)

let derivative f x =
    let delta = 0.000001
    (f (x + delta) - f (x - delta)) / (2.0 * delta)

let eval (w: Matrix) (x: Matrix) (b: Matrix) =
    let ret = w * x + b
    
    for r in 0 .. ret.Rows - 1 do
        for c in 0 .. ret.Cols - 1 do
            ret.Data.[r, c] <- swish ret.Data.[r, c]

    ret

let error (targets: Matrix) (outputs: Matrix) =
    let mutable sum = 0.0

    for r in 0 .. targets.Rows - 1 do
        for c in 0 .. targets.Cols - 1 do
            sum <- sum + targets.Data.[r, c] - outputs.Data.[r, c]
    
    sum <- sum ** 2.0
    sum <- sum / 2.0

    sum

// Regular, plain gradient descent method.
// Look for other examples if you want stochastic gradient descent.
let train_output (weights: Matrix) (targets: Matrix) (outputs: Matrix) =
    if outputs.Rows <> targets.Rows then
        failwith "Invalid dimension."
    
    let gradients = new Matrix (weights.Rows, weights.Cols)
    let gradient t y = (y - t) * (derivative swish y)
    
    
    
    gradients

let train_hidden (weights: Matrix) (prev_gradients: Matrix) =
    let gradients = new Matrix (weights.Rows, weights.Cols)
    gradients

[<EntryPoint>]
let main argv = 
    // layers
    let first = new Matrix (2, 2)
    let second = new Matrix (3, 2)
    let third = new Matrix (1, 3)

    let biases = [| new Matrix (2, 1); new Matrix (3, 1); new Matrix (1, 1) |]
    
    let inputs = [| new Matrix (2, 1); new Matrix (2, 1); new Matrix (2, 1); new Matrix (2, 1) |]
    let targets = [| new Matrix (1, 1); new Matrix (1, 1); new Matrix (1, 1); new Matrix (1, 1) |]

    // 0 ^ 0 = 0
    inputs.[0].Data.[0, 0] <- 0.0;
    inputs.[0].Data.[1, 0] <- 0.0;
    targets.[0].Data.[0, 0] <- 0.0;
    
    // 1 ^ 0 = 1
    inputs.[1].Data.[0, 0] <- 1.0;
    inputs.[1].Data.[1, 0] <- 0.0;
    targets.[1].Data.[0, 0] <- 1.0;
    
    // 0 ^ 1 = 1
    inputs.[2].Data.[0, 0] <- 0.0;
    inputs.[2].Data.[1, 0] <- 1.0;
    targets.[2].Data.[0, 0] <- 1.0;
    
    // 1 ^ 1 = 0
    inputs.[3].Data.[0, 0] <- 1.0;
    inputs.[3].Data.[1, 0] <- 1.0;
    targets.[3].Data.[0, 0] <- 0.0;

    let errors = Array.zeroCreate 4000

    // 4000 iterations
    for i in 1 .. 4000 do
        let mutable outputs = eval first inputs.[i % 4] biases.[0]

        outputs <- eval second outputs biases.[1]
        outputs <- eval third outputs biases.[2]
        
        errors.[i - 1] <- error targets.[i % 4] outputs

        train_output targets.[i % 4] third outputs
    
    let chart = (Chart.Line errors).ShowChart ()
    
    Application.Run chart

    0
