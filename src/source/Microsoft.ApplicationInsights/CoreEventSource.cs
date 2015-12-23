namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
    using System;
    
    using Yaaf.Logging;
    
    internal sealed class CoreEventSource // : EventSource
    {
        public static readonly CoreEventSource Log = new CoreEventSource();

        ApplicationNameProvider nameProvider = new ApplicationNameProvider();
        private readonly ITraceSource source = Yaaf.Logging.Log.Source("Microsoft.ApplicationInsights");
        
        /// <summary>
        /// Logs the information when there operation to track is null.
        /// </summary>
        //[Event(1, Message = "Operation object is null.", Level = EventLevel.Warning)]
        public void OperationIsNullWarning(string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Warning, 1, "Operation object is null.");
            //this.WriteEvent(1, this.nameProvider.Name);
        }

        /// <summary>
        /// Logs the information when there operation to stop does not match the current operation.
        /// </summary>
        //[Event(2, Message = "Operation to stop does not match the current operation.", Level = EventLevel.Error)]
        public void InvalidOperationToStopError(string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Error, 2, "Operation to stop does not match the current operation.");
            //this.WriteEvent(2, this.nameProvider.Name);
        }

        //[Event(
        //    3,
        //    Keywords = Keywords.VerboseFailure,
        //    Message = "[msg=Log verbose];[msg={0}]",
        //    Level = EventLevel.Verbose)]
        public void LogVerbose(string msg, string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Verbose, 3, msg);
            //this.WriteEvent(
            //    3,
            //    msg ?? string.Empty,
            //    this.nameProvider.Name);
        }

        //[Event(
        //    4,
        //    Keywords = Keywords.Diagnostics | Keywords.UserActionable,
        //    Message = "Diagnostics event throttling has been started for the event {0}",
        //    Level = EventLevel.Informational)]
        public void DiagnosticsEventThrottlingHasBeenStartedForTheEvent(
            int eventId,
            string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Information, 4, "Diagnostics event throttling has been started for the event {0}", eventId);
            //this.WriteEvent(4, eventId, this.nameProvider.Name);
        }

        //[Event(
        //    5,
        //    Keywords = Keywords.Diagnostics | Keywords.UserActionable,
        //    Message = "Diagnostics event throttling has been reset for the event {0}, event was fired {1} times during last interval",
        //    Level = EventLevel.Informational)]
        public void DiagnosticsEventThrottlingHasBeenResetForTheEvent(
            int eventId,
            int executionCount,
            string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Verbose, 5, "Diagnostics event throttling has been reset for the event {0}, event was fired {1} times during last interval", eventId, executionCount);
            //this.WriteEvent(5, eventId, executionCount, this.nameProvider.Name);
        }

        //[Event(
        //    6,
        //    Keywords = Keywords.Diagnostics,
        //    Message = "Scheduler timer dispose failure: {0}",
        //    Level = EventLevel.Warning)]
        public void DiagnoisticsEventThrottlingSchedulerDisposeTimerFailure(
            string exception,
            string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Warning, 6, "Scheduler timer dispose failure: {0}", exception);
            //this.WriteEvent(
            //6,
            //    exception ?? string.Empty,
            //    this.nameProvider.Name);
        }

        //[Event(
        //    7,
        //    Keywords = Keywords.Diagnostics,
        //    Message = "A scheduler timer was created for the interval: {0}",
        //    Level = EventLevel.Verbose)]
        public void DiagnoisticsEventThrottlingSchedulerTimerWasCreated(
            int intervalInMilliseconds,
            string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Verbose, 7, "A scheduler timer was created for the interval: {0}", intervalInMilliseconds);
            //this.WriteEvent(7, intervalInMilliseconds, this.nameProvider.Name);
        }

        //[Event(
        //    8,
        //    Keywords = Keywords.Diagnostics,
        //    Message = "A scheduler timer was removed",
        //    Level = EventLevel.Verbose)]
        public void DiagnoisticsEventThrottlingSchedulerTimerWasRemoved(string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Verbose, 8, "A scheduler timer was removed");
            //this.WriteEvent(8, this.nameProvider.Name);
        }

        //[Event(
        //    9,
        //    Message = "No Telemetry Configuration provided. Using the default TelemetryConfiguration.Active.",
        //    Level = EventLevel.Warning)]
        public void TelemetryClientConstructorWithNoTelemetryConfiguration(string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Warning, 9, "No Telemetry Configuration provided. Using the default TelemetryConfiguration.Active.");
            //this.WriteEvent(9, this.nameProvider.Name);
        }

        //[Event(
        //    10,
        //    Message = "Value for property '{0}' of {1} was not found. Populating it by default.",
        //    Level = EventLevel.Verbose)]
        public void PopulateRequiredStringWithValue(string parameterName, string telemetryType, string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Verbose, 10, "Value for property '{0}' of {1} was not found. Populating it by default.", parameterName, telemetryType);
            //this.WriteEvent(
            //    10,
            //    parameterName ?? string.Empty,
            //    telemetryType ?? string.Empty,
            //    this.nameProvider.Name);
        }

        //[Event(
        //    11,
        //    Message = "Invalid duration for Request Telemetry. Setting it to '00:00:00'.",
        //    Level = EventLevel.Warning)]
        public void RequestTelemetryIncorrectDuration(string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Warning, 11, "Invalid duration for Request Telemetry. Setting it to '00:00:00'.");
            //this.WriteEvent(11, this.nameProvider.Name);
        }

        //[Event(
        //   12,
        //   Message = "Telemetry tracking was disabled. Message is dropped.",
        //   Level = EventLevel.Verbose)]
        public void TrackingWasDisabled(string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Verbose, 12, "Telemetry tracking was disabled. Message is dropped.");
            //this.WriteEvent(12, this.nameProvider.Name);
        }

        //[Event(
        //   13,
        //   Message = "Telemetry tracking was enabled. Messages are being logged.",
        //   Level = EventLevel.Verbose)]
        public void TrackingWasEnabled(string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Verbose, 13, "Telemetry tracking was enabled. Messages are being logged.");
            //this.WriteEvent(13, this.nameProvider.Name);
        }

        //[Event(
        //    14,
        //    Keywords = Keywords.ErrorFailure,
        //    Message = "[msg=Log Error];[msg={0}]",
        //    Level = EventLevel.Error)]
        public void LogError(string msg, string appDomainName = "Incorrect")
        {
            source.TraceEvent(TraceEventType.Error, 14, msg);
            //this.WriteEvent(
            //14,
            //    msg ?? string.Empty,
            //    this.nameProvider.Name);
        }
    }
}
