﻿namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
    using System;

#if NET40 || NET35
    using Microsoft.Diagnostics.Tracing;
#endif
#if CORE_PCL || NET45 || WINRT || UWP || NET46
    using System.Diagnostics.Tracing;
#endif

    [EventSource(Name = "Microsoft-ApplicationInsights-Core")]
    internal sealed class CoreEventSource : EventSource
    {
        public static readonly CoreEventSource Log = new CoreEventSource();

        private readonly ApplicationNameProvider nameProvider = new ApplicationNameProvider();

        /// <summary>
        /// Logs the information when there operation to track is null.
        /// </summary>
        //[Event(1, Message = "Operation object is null.", Level = EventLevel.Warning)]
        public void OperationIsNullWarning(string appDomainName = "Incorrect")
        {
            this.WriteEvent(1, this.nameProvider.Name);
        }

        /// <summary>
        /// Logs the information when there operation to stop does not match the current operation.
        /// </summary>
        //[Event(2, Message = "Operation to stop does not match the current operation.", Level = EventLevel.Error)]
        public void InvalidOperationToStopError(string appDomainName = "Incorrect")
        {
            this.WriteEvent(2, this.nameProvider.Name);
        }

        //[Event(
        //    3,
        //    Keywords = Keywords.VerboseFailure,
        //    Message = "[msg=Log verbose];[msg={0}]",
        //    Level = EventLevel.Verbose)]
        public void LogVerbose(string msg, string appDomainName = "Incorrect")
        {
            this.WriteEvent(
                3,
                msg ?? string.Empty,
                this.nameProvider.Name);
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
            this.WriteEvent(4, eventId, this.nameProvider.Name);
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
            this.WriteEvent(5, eventId, executionCount, this.nameProvider.Name);
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
            this.WriteEvent(
                6,
                exception ?? string.Empty,
                this.nameProvider.Name);
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
            this.WriteEvent(7, intervalInMilliseconds, this.nameProvider.Name);
        }

        //[Event(
        //    8,
        //    Keywords = Keywords.Diagnostics,
        //    Message = "A scheduler timer was removed",
        //    Level = EventLevel.Verbose)]
        public void DiagnoisticsEventThrottlingSchedulerTimerWasRemoved(string appDomainName = "Incorrect")
        {
            this.WriteEvent(8, this.nameProvider.Name);
        }

        //[Event(
        //    9,
        //    Message = "No Telemetry Configuration provided. Using the default TelemetryConfiguration.Active.",
        //    Level = EventLevel.Warning)]
        public void TelemetryClientConstructorWithNoTelemetryConfiguration(string appDomainName = "Incorrect")
        {
            this.WriteEvent(9, this.nameProvider.Name);
        }

        //[Event(
        //    10,
        //    Message = "Value for property '{0}' of {1} was not found. Populating it by default.",
        //    Level = EventLevel.Verbose)]
        public void PopulateRequiredStringWithValue(string parameterName, string telemetryType, string appDomainName = "Incorrect")
        {
            this.WriteEvent(
                10,
                parameterName ?? string.Empty,
                telemetryType ?? string.Empty,
                this.nameProvider.Name);
        }

        //[Event(
        //    11,
        //    Message = "Invalid duration for Request Telemetry. Setting it to '00:00:00'.",
        //    Level = EventLevel.Warning)]
        public void RequestTelemetryIncorrectDuration(string appDomainName = "Incorrect")
        {
            this.WriteEvent(11, this.nameProvider.Name);
        }

        //[Event(
        //   12,
        //   Message = "Telemetry tracking was disabled. Message is dropped.",
        //   Level = EventLevel.Verbose)]
        public void TrackingWasDisabled(string appDomainName = "Incorrect")
        {
            this.WriteEvent(12, this.nameProvider.Name);
        }

        //[Event(
        //   13,
        //   Message = "Telemetry tracking was enabled. Messages are being logged.",
        //   Level = EventLevel.Verbose)]
        public void TrackingWasEnabled(string appDomainName = "Incorrect")
        {
            this.WriteEvent(13, this.nameProvider.Name);
        }

        //[Event(
        //    14,
        //    Keywords = Keywords.ErrorFailure,
        //    Message = "[msg=Log Error];[msg={0}]",
        //    Level = EventLevel.Error)]
        public void LogError(string msg, string appDomainName = "Incorrect")
        {
            this.WriteEvent(
                14,
                msg ?? string.Empty,
                this.nameProvider.Name);
        }

        /// <summary>
        /// Keywords for the PlatformEventSource.
        /// </summary>
        public sealed class Keywords
        {
            /// <summary>
            /// Key word for user actionable events.
            /// </summary>
            public const EventKeywords UserActionable = (EventKeywords)EventSourceKeywords.UserActionable;

            /// <summary>
            /// Keyword for errors that trace at Verbose level.
            /// </summary>
            public const EventKeywords Diagnostics = (EventKeywords)EventSourceKeywords.Diagnostics;

            /// <summary>
            /// Keyword for errors that trace at Verbose level.
            /// </summary>
            public const EventKeywords VerboseFailure = (EventKeywords)EventSourceKeywords.VerboseFailure;

            /// <summary>
            /// Keyword for errors that trace at Error level.
            /// </summary>
            public const EventKeywords ErrorFailure = (EventKeywords)EventSourceKeywords.ErrorFailure;
        }
    }
}
