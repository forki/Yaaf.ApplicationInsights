namespace System
open System.Reflection

[<assembly: AssemblyCompanyAttribute("Yaaf.ApplicationInsights")>]
[<assembly: AssemblyProductAttribute("Yaaf.ApplicationInsights")>]
[<assembly: AssemblyCopyrightAttribute("Yaaf.ApplicationInsights Copyright © Matthias Dittrich 2015-2016")>]
[<assembly: AssemblyVersionAttribute("0.0.1")>]
[<assembly: AssemblyFileVersionAttribute("0.0.1")>]
[<assembly: AssemblyInformationalVersionAttribute("0.0.1")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.1"
