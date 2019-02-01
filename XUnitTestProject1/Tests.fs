module ``Unit tests``

open Xunit
open FsUnit.Xunit
open System
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Linq.RuntimeHelpers
open System.Linq.Expressions

let invoke(fn: Func<'b>) =
    fn.Invoke()

let test (expr: Expr<'a -> 'b>) =     
    let linq = LeafExpressionConverter.QuotationToExpression expr
    let call = linq :?> MethodCallExpression
    let lambda = call.Arguments.[0] :?> LambdaExpression
    let fnc = Expression.Lambda<Func<'b>>(lambda.Body, lambda.Parameters)
    fnc.Compile()

[<Fact>]
let ``[ctor] throws argument null exception``() = 
    <@ fun () -> new ArgumentException() @> 
    |> test
    |> invoke