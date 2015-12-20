open System

open System.IO
let sourceFile = __SOURCE_DIRECTORY__
let projectsDir =
  sourceFile
  |> Path.GetDirectoryName // source
  |> Path.GetDirectoryName // src
  |> Path.GetDirectoryName // Yaaf.ApplicationInsights
  |> Path.GetDirectoryName // PROJECTS directory
let insightsClone = Path.Combine(projectsDir, "ApplicationInsights-dotnet")
if not <| Directory.Exists(insightsClone) then
  failwith "Please clone https://github.com/Microsoft/ApplicationInsights-dotnet.git side by side with Yaaf.ApplicationInsights"

Environment.CurrentDirectory <- insightsClone
let files = 
  Directory.EnumerateFiles("src/Core/Managed/Shared", "*.cs", SearchOption.AllDirectories)
  |> Seq.append <| Directory.EnumerateFiles("src/Core/Managed/PCL", "*.cs", SearchOption.AllDirectories)
  |> Seq.append [ "src/Core/Managed/Net40/Extensibility/Implementation/TelemetryConfigurationFactory.cs" ]
  |> Seq.map (fun f -> f.Replace("\\", "/"))
  |> Seq.cache
printfn "Add the following to paket.dependencies:"
printfn ""
for f in files do
  printfn "github Microsoft/ApplicationInsights-dotnet %s" f
printfn ""
printfn ""
printfn "Add the following to paket.references"
printfn ""
for f in files do
  printfn "File:%s" (Path.GetFileName f)