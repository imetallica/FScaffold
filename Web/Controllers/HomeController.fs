module Controllers.HomeController

open Suave
open Suave.Operators
open Suave.DotLiquid

let index() = page ("index.html") ()
let routes = choose [ Filters.GET >=> index() ]
