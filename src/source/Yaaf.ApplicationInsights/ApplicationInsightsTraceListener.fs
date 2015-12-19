// ----------------------------------------------------------------------------
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
// ----------------------------------------------------------------------------
namespace Yaaf.ApplicationInsights

open System.Diagnostics
open Microsoft.ApplicationInsights
open Microsoft.ApplicationInsights.DataContracts

/// <summary>
/// Listener that routes all tracing and debugging output to ApplicationInsights logging framework.
/// The messages will be uploaded to the Application Insights cloud service.
/// </summary>
type ApplicationInsightsTraceListener(key) as x =
  inherit TraceListener()

  let client = new TelemetryClient()
  do
    if not <| System.String.IsNullOrEmpty key then
      client.Context.InstrumentationKey <- key;

  let getSeverityLevel(eventType : TraceEventType) =
    match (eventType) with
    | TraceEventType.Critical -> SeverityLevel.Critical
    | TraceEventType.Error -> SeverityLevel.Error
    | TraceEventType.Warning -> SeverityLevel.Warning
    | TraceEventType.Information -> SeverityLevel.Information
    | _ -> SeverityLevel.Verbose

  let addTraceData (eventCache : TraceEventCache, eventType : TraceEventType , id : int, source : string) (trace : TraceTelemetry) =
    trace.SeverityLevel <- System.Nullable(getSeverityLevel(eventType))
    let properties = trace.Properties
    properties.Add("SourceName", source)
    properties.Add("SourceType", "TraceListener")
    properties.Add("TraceEventType", eventType.ToString())
    properties.Add("EventId", id.ToString())
    if ((x.TraceOutputOptions &&& TraceOptions.Timestamp) = TraceOptions.Timestamp) then
      properties.Add("Timestamp", eventCache.Timestamp.ToString())

  let traceData (msg:string) (eventCache : TraceEventCache, eventType : TraceEventType , id : int, source : string) =
    let tel = new TraceTelemetry(msg)
    addTraceData (eventCache, eventType, id, source) tel
    client.Track(tel)

  new () = new ApplicationInsightsTraceListener(null)

  /// <summary>
  /// Writes trace information, a message, and event information to the listener specific output.
  /// </summary>
  /// <param name="eventCache">A TraceEventCache object that contains the current process ID, thread ID, and stack trace information.</param><param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param><param name="eventType">One of the TraceEventType values specifying the type of event that has caused the trace.</param><param name="id">A numeric identifier for the event.</param>
  override x.TraceEvent(eventCache : TraceEventCache, source : string, eventType: TraceEventType, id : int) =
    x.TraceEvent(eventCache, source, eventType, id, id.ToString(System.Globalization.CultureInfo.InvariantCulture :> System.IFormatProvider))

  /// <summary>
  /// Writes trace information, a message, and event information to the listener specific output.
  /// </summary>
  /// <param name="eventCache">A TraceEventCache object that contains the current process ID, thread ID, and stack trace information.</param><param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param><param name="eventType">One of the TraceEventType values specifying the type of event that has caused the trace.</param><param name="id">A numeric identifier for the event.</param><param name="format">A format string that contains zero or more format items, which correspond to objects in the args array.</param><param name="args">An object array containing zero or more objects to format.</param>
  override x.TraceEvent(eventCache: TraceEventCache, source: string, eventType : TraceEventType, id : int, format : string, [<System.ParamArray>] args : obj array) =
    if (isNull x.Filter || x.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null)) then
      let message = if isNull args then format else System.String.Format(System.Globalization.CultureInfo.InvariantCulture, format, args)
      x.TraceEvent(eventCache, source, eventType, id, message)

  /// <summary>
  /// Writes trace information, a message, and event information to the listener specific output.
  /// </summary>
  /// <param name="eventCache">A TraceEventCache object that contains the current process ID, thread ID, and stack trace information.</param><param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param><param name="eventType">One of the TraceEventType values specifying the type of event that has caused the trace.</param><param name="id">A numeric identifier for the event.</param><param name="message">A message to write.</param>
  override x.TraceEvent(eventCache :TraceEventCache, source:string , eventType:TraceEventType , id:int , message:string ) =
    if isNull x.Filter || x.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null) then
      traceData message (eventCache, eventType, id, source)

  /// <summary>
  /// Writes trace data to the listener specific output.
  /// </summary>
  /// <param name="eventCache">A TraceEventCache object that contains the current process ID, thread ID, and stack trace information.</param><param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param><param name="eventType">One of the TraceEventType values specifying the type of event that has caused the trace.</param><param name="id">A numeric identifier for the event.</param><param name="data">The trace data to emit.</param>
  override x.TraceData(eventCache: TraceEventCache, source : string, eventType : TraceEventType, id : int, data : obj) =
    if (isNull x.Filter || x.Filter.ShouldTrace(eventCache, source, eventType, id, System.String.Empty, null, data, null)) then
      x.TraceData(eventCache, source, eventType, id, [|data|])

  /// <summary>
  /// Writes trace data to the listener specific output.
  /// </summary>
  /// <param name="eventCache">A TraceEventCache object that contains the current process ID, thread ID, and stack trace information.</param><param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param><param name="eventType">One of the TraceEventType values specifying the type of event that has caused the trace.</param><param name="id">A numeric identifier for the event.</param><param name="data">An array of objects to emit as data.</param>
  override x.TraceData(eventCache: TraceEventCache , source:string , eventType:TraceEventType , id:int , [<System.ParamArray>] data : obj array) =
    if (isNull x.Filter || x.Filter.ShouldTrace(eventCache, source, eventType, id, System.String.Empty, null, null, data)) then
      let message = System.String.Join(", ", data |> Seq.map (fun o -> if isNull o then System.String.Empty else o.ToString()))
      traceData message (eventCache, eventType, id, source)

  /// <summary>
  /// Writes the specified message to the listener.
  /// </summary>
  /// <param name="message">A message to write.</param>
  override x.Write(message : string) =
    if (isNull x.Filter || x.Filter.ShouldTrace(null, System.String.Empty, TraceEventType.Verbose, 0, message, null, null, null)) then
      traceData message (new TraceEventCache(), TraceEventType.Verbose, 0, "unknown")

  /// <summary>
  /// Writes the specified message to the listener followed by a line terminator.
  /// </summary>
  /// <param name="message">A message to write.</param>
  override x.WriteLine(message: string) =
    x.Write(message + System.Environment.NewLine)

  member x.Client = client