module Controllers.HomeController

open Suave
open Suave.Operators
open Suave.DotLiquid

type IndexResult = 
  { FOO : string }

let index() = 
  let toPage = { FOO = "I'm from server side" }
  page "index.html" toPage

let routes = choose [ Filters.GET >=> index() ]
