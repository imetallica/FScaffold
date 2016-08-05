module Web

open Suave
open Suave.Logging
open Suave.Files
open Suave.Operators
open Suave.DotLiquid
open System
open System.IO
open System.Net

DotLiquid.setTemplatesDir (Path.Combine(__SOURCE_DIRECTORY__, "Templates"))

let routes = 
  choose [ Filters.path "/" >=> Controllers.HomeController.routes
           // Should be the last one
           Files.browseHome
           RequestErrors.NOT_FOUND "Page not found." ]

let port = 
  let p = Environment.GetEnvironmentVariable("HTTP_PLATFORM_PORT")
  if isNull p then "8083"
  else p
  |> Sockets.Port.Parse

let staticPath = Path.Combine(__SOURCE_DIRECTORY__, "Content")

let config = 
  { defaultConfig with logger = Loggers.saneDefaultsFor LogLevel.Debug
                       homeFolder = Some staticPath
                       bindings = [ HttpBinding.mk HTTP IPAddress.Loopback port ] }

[<EntryPoint>]
let main argv = 
  startWebServer config routes
  0 // return an integer exit code
