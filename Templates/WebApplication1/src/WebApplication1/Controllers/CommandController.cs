using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;
using WebApplication1.Formatting;

namespace WebApplication1.Controllers
{
    [Route("")]
    public class CommandController : Controller
    {
        private readonly ICommandCoordinator _commands;

        public CommandController(ICommandCoordinator commands)
        {
            _commands = commands;
        }

        [HttpPost("{*url}")]
        public async Task<object> ExecutePostByPath(string url)
        {
            var content = await this.Request.Body.ReadAsStringAsync();

            var result = await _commands.SendAsync(url, content);

            if (result.IsSuccessful)
            {
                var value = result.Response;
                if (value is IEvent)
                {
                    return new ObjectResult(value)
                    {
                        Formatters = new FormatterCollection<IOutputFormatter>(new List<IOutputFormatter> { new EventFormatter() }),
                        StatusCode = 200
                    };
                }
                return this.StatusCode(200, value);
            }
            if (result.RaisedException != null)
            {
                return this.StatusCode(500, "An unhandled exception was raised.  Please try again or contact support. " + result.CorrelationId);
            }
            if (result.ValidationErrors.Any())
            {
                if (result.ValidationErrors.Any(e => e.ErrorType == ValidationErrorType.Security))
                {
                    return this.StatusCode(403, result.ValidationErrors.Where(e => e.ErrorType == ValidationErrorType.Security));
                }
                return this.StatusCode(400, result.ValidationErrors);
            }

            return result;
        }
    }
}