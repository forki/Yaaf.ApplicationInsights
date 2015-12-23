# Yaaf.ApplicationInsights

## [Documentation](https://matthid.github.io/Yaaf.ApplicationInsights)

[![Join the chat at https://gitter.im/matthid/Yaaf](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/matthid/Yaaf?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Build status

**Development Branch**

[![Build Status](https://travis-ci.org/matthid/Yaaf.ApplicationInsights.svg?branch=develop)](https://travis-ci.org/matthid/Yaaf.ApplicationInsights)
[![Build status](https://ci.appveyor.com/api/projects/status/811142y9mfn67m2b/branch/develop?svg=true)](https://ci.appveyor.com/project/matthid/yaaf-applicationinsights/branch/develop)

**Master Branch**

[![Build Status](https://travis-ci.org/matthid/Yaaf.ApplicationInsights.svg?branch=master)](https://travis-ci.org/matthid/Yaaf.ApplicationInsights)
[![Build status](https://ci.appveyor.com/api/projects/status/811142y9mfn67m2b/branch/master?svg=true)](https://ci.appveyor.com/project/matthid/yaaf-applicationinsights/branch/master)

## Overview


This is a very thin layer on top of https://github.com/Microsoft/ApplicationInsights-dotnet/ for F# and mono compatibility.
Because I also needed "profile111" support this package bundles `Microsoft.ApplicationInsights` and has therefore no dependency to it.
I changed some things such that it is `compilable` on a mono system and removed an incompatible module (using the `EventListener` class).
Besides some removed functionality this are the unmodified bits (in fact we copy the original files in the build process via `paket`).

> Note some files like `CoreEventSource.cs` are included and modified slightly (see git history for details)

The dependency to `Yaaf.Logging` is required because we replaced the Event-Logging feature with logging to regular TraceSource
with the "Microsoft.applicationinsights" source name. Additionally `Yaaf.Logging` allows us to compile for portable profiles
and lets you choose the concrete implementation in the main application. 
(Because `System.Diagnostics` is not available on most portable profiles but it is available on most platforms you can most likely use it).