using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Communication.Logging;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.Logging.ApplicationInsights
{
    public class LogStore : ILogStore
    {
        private readonly TelemetryClient _client;

        public LogStore(IConfiguration configuration)
        {
            _client = new TelemetryClient();
            _client.InstrumentationKey = configuration["ApplicationInsights:InstrumentationKey"];
        }

        public Task AppendAsync(ICommand command, ICommandResult result, ExecutionContext context)
        {
            var request = new RequestTelemetry(command.CommandName, result.Started, result.Elapsed ?? TimeSpan.FromSeconds(0), GetStatusCode(result), result.IsSuccessful);
            request.Context.User.Id = context.User?.Identity?.Name;
            request.Context.Session.Id = context.SessionId;
            request.Context.Operation.Name = command.CommandName;
            if (!String.IsNullOrWhiteSpace(context.Path))
            {
                request.Url = new Uri(context.Path);
            }
            request.Context.Operation.Id = command.Id.ToString();
            request.Properties.Add("CorrelationId", context.CorrelationId);
            if (result.ValidationErrors.Any())
            {
                request.Properties.Add("ValidationErrors.Count", result.ValidationErrors.Count().ToString());
                request.Properties.Add("ValidationErrors.Items", String.Join("; ", result.ValidationErrors.Select(e => e.ErrorType + ": " + e.Message)));
            }

            _client.TrackRequest(request);


            if (result.RaisedException != null)
            {
                var exception = new ExceptionTelemetry(result.RaisedException);
                exception.Context.User.Id = context.User?.Identity?.Name;
                exception.Context.Session.Id = context.SessionId;
                exception.Context.Operation.Name = command.CommandName;
                exception.Context.Operation.Id = command.Id.ToString();
                exception.Properties.Add("CorrelationId", context.CorrelationId);
                _client.TrackException(exception);
            }

            _client.Flush();

            return Task.FromResult(0);
        }

        private static string GetStatusCode(ICommandResult result)
        {
            string status = "200";
            if (!result.IsSuccessful)
            {
                if (result.ValidationErrors.Any())
                {
                    if (result.ValidationErrors.Any(e => e.ErrorType == Validation.ValidationErrorType.Security))
                    {
                        status = "403";
                    }
                    else
                    {
                        status = "400";
                    }
                }

                else
                {
                    status = "500";
                }
            }
            return status;
        }
    }
}