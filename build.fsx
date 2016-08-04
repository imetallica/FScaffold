// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open System
open System.IO

// Directories
let buildDir = (Path.Combine(__SOURCE_DIRECTORY__, "build"))
// Filesets
let appReferences = !!"/**/*.csproj" ++ "/**/*.fsproj"
// version info
let version = "0.1" // or retrieve from CI server

// Targets
Target "Clean" (fun _ -> CleanDirs [ buildDir ])
Target "Build" (fun _ -> MSBuildRelease buildDir "Build" appReferences |> Log "AppBuild-Output: ")
Target "Deploy" (fun _ -> 
  let wwwRootDir = (Path.Combine(__SOURCE_DIRECTORY__, "..\wwwroot"))
  CleanDir wwwRootDir
  CopyRecursive buildDir wwwRootDir false |> ignore)
// Build order
"Clean" ==> "Build" ==> "Deploy"
// start build
RunTargetOrDefault "Deploy"
