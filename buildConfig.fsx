// ----------------------------------------------------------------------------
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
// ----------------------------------------------------------------------------

(*
    This file handles the complete build process of Yaaf.Logging

    The first step is handled in build.sh and build.cmd by bootstrapping a NuGet.exe and 
    executing NuGet to resolve all build dependencies (dependencies required for the build to work, for example FAKE)

    The secound step is executing this file which resolves all dependencies, builds the solution and executes all unit tests
*)

#if FAKE
#else
// Support when file is opened in Visual Studio
#load "packages/Yaaf.AdvancedBuilding/content/buildConfigDef.fsx"
#endif

open BuildConfigDef
open System.Collections.Generic
open System.IO

open Fake
open Fake.Git
open Fake.FSharpFormatting
open AssemblyInfoFile

if isMono then
    monoArguments <- "--runtime=v4.0 --debug"

let buildConfig =
 // Read release notes document
 let release = ReleaseNotesHelper.parseReleaseNotes (File.ReadLines "doc/ReleaseNotes.md")
 { BuildConfiguration.Defaults with
    ProjectName = "Yaaf.ApplicationInsights"
    CopyrightNotice = "Yaaf.ApplicationInsights Copyright © Matthias Dittrich 2015-2016"
    ProjectSummary = "Yaaf.ApplicationInsights is a simple helper library for using Microsofts ApplicationInsights with F# (designed for System.Diagnostic.TraceSource / Yaaf.Logging)."
    ProjectDescription = "Yaaf.ApplicationInsights is a simple helper library for using Microsofts ApplicationInsights with F# (designed for System.Diagnostic.TraceSource / Yaaf.Logging)."
    ProjectAuthors = ["Matthias Dittrich"]
    NugetTags =  "logging tracesource applicationsinsights C# F# dotnet .net"
    PageAuthor = "Matthias Dittrich"
    GithubUser = "matthid"
    Version = release.NugetVersion
    NugetPackages =
      [ "Yaaf.ApplicationInsights.nuspec", (fun config p ->
          { p with
              Version = config.Version
              ReleaseNotes = toLines release.Notes
              Dependencies = 
                [  "Microsoft.ApplicationInsights", GetPackageVersion "packages" "Microsoft.ApplicationInsights" ] }) ]
    UseNuget = true
    SetAssemblyFileVersions = (fun config ->
      let info =
        [ Attribute.Company config.ProjectName
          Attribute.Product config.ProjectName
          Attribute.Copyright config.CopyrightNotice
          Attribute.Version config.Version
          Attribute.FileVersion config.Version
          Attribute.InformationalVersion config.Version]
      CreateFSharpAssemblyInfo "./src/SharedAssemblyInfo.fs" info)
    RestrictReleaseToWindows = false
    DisableMSTest = true
    BuildTargets =
     [ { BuildParams.WithSolution with
          // The default build
          PlatformName = "Net40"
          SimpleBuildName = "net40" }
       { BuildParams.WithSolution with
          // The generated templates
          PlatformName = "Profile111"
          SimpleBuildName = "profile111"
          FindUnitTestDlls =
            // Don't run on mono.
            if isMono then (fun _ -> Seq.empty) else BuildParams.Empty.FindUnitTestDlls }
       { BuildParams.WithSolution with
          // The generated templates
          PlatformName = "Net45"
          SimpleBuildName = "net45" } ]
  }

