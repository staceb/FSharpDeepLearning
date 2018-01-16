open System

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

let eval (w: Matrix) (x: Matrix) (b: Matrix) =
    let ret = w * x + b
    ret.Data |> Array2D.map (fun x -> swish x)

[<EntryPoint>]
let main argv = 
    let weights = new Matrix (3, 3)
    let inputs = new Matrix (3, 1)
    let bias = new Matrix (3, 1)
    
    let output = eval weights inputs bias
    
    printfn "%A" output
    Console.ReadLine ()

    0
