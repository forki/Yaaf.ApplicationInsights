// ----------------------------------------------------------------------------
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
// ----------------------------------------------------------------------------
module Yaaf.ApplicationInsights.Helper

let mutable client = Unchecked.defaultof<Microsoft.ApplicationInsights.TelemetryClient>
let newInsightsSession () = System.Guid.NewGuid().ToString()
 
let traceAsync name f =
  async {
    let w = System.Diagnostics.Stopwatch.StartNew()
    let! r = f ()
    w.Stop()
    let c = client
    if not <| isNull c then
      let ev = new Microsoft.ApplicationInsights.DataContracts.EventTelemetry(name)
      ev.Metrics.Add("Total Time Elapsed", w.Elapsed.TotalMilliseconds)
      c.TrackEvent(ev)
    return r
  }

let traceFunc name f =
  traceAsync name (fun _ -> async { return f () })
  |> Async.RunSynchronously

 