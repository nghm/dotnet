module ``AsyncStepBuilder unit tests``

open System
open FsUnit.Xunit
open Hypermedia.AspNetCore.Builder
open Hypermedia.AspNetCore.Store
open Xunit

type builder  = IBuilder<Object>
type tuple    = ValueTuple<Type, System.Action<IAsyncBuildStep<builder, Object>>>
type store    = IStorage<tuple>
type executor = IIsolatedBuildStepExecutor<builder, Object>

[<Fact>]
let ``should throw argument null exception``() = 
    <@ (fun (a, b, c) -> new AsyncStepBuilder<builder, Object>(a b c)) @>
        |> should throw typeof<ArgumentNullException>