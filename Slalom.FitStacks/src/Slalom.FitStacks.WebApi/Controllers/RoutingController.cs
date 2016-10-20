using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Slalom.FitStacks.Messaging;
using Slalom.FitStacks.Messaging.Validation;
using Slalom.FitStacks.Runtime;

namespace Slalom.FitStacks.WebApi.Controllers
{

    #region Test Commands

    public class GetCommandEventHandler : IHandleEvent<GetCommandEvent>
    {
        public Task Handle(GetCommandEvent instance, ExecutionContext context)
        {
            var user = context.User;

            return Task.FromResult(0);
        }
    }

    public class must_be_administrator : InRoleSecurityRule<GetCommand>
    {
        public must_be_administrator()
            : base("Administrator", "You must be an administrator to perform that action.")
        {
        }
    }

    public class GetCommandEvent : Event, IHaveReturnMessage
    {
        public Guid InstanceId { get; set; } = Guid.NewGuid();

        public dynamic GetReturnMessage()
        {
            return new
            {
                Id = this.InstanceId
            };
        }
    }

    public class PostCommand : Command<string>
    {
        public PostCommand(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }
    }

    public class PostCommandHandler : CommandHandler<PostCommand, string>
    {
        public override Task<string> Handle(PostCommand command)
        {
            var user = this.Context.User;

            throw new Exception("soe");

            return Task.FromResult(command.Text);
        }
    }

    public class GetCommand : Command<GetCommandEvent>
    {
    }

    public class GetCommandHandler : CommandHandler<GetCommand, GetCommandEvent>
    {
        public override Task<GetCommandEvent> Handle(GetCommand command)
        {
            return Task.FromResult(new GetCommandEvent());
        }
    }

    #endregion

    public class BaseController : Controller
    {
        public dynamic CreateResponse<TResult>(CommandResult<TResult> result)
        {
            if (result.IsSuccessful)
            {
                if (result.Value is Event)
                {
                    var value = result.Value as IHaveReturnMessage;
                    if (value != null)
                    {
                        return value.GetReturnMessage();
                    }
                    return "";
                }
                return result.Value;
            }
            if (result.ValidationErrors.Any())
            {
                this.HttpContext.Response.StatusCode = result.ValidationErrors.Any(e=>e.ErrorType == Validation.ValidationErrorType.Security) ? (int)HttpStatusCode.Unauthorized : (int)HttpStatusCode.BadRequest;

                return JsonConvert.SerializeObject(result.ValidationErrors, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                });
            }
            if (result.RaisedException != null)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return "An unhandled exception was raised on the server.  Please try again, or contact the system administrator.  Error code " + result.CorrelationId + ".";
            }

            return "Something went wrong in handling your request.  Please try again, or contact the system administrator.";
        }

        protected Type GetCommandType(string name)
        {
            var type = Type.GetType(name, false);

            if (type == null)
            {
                throw new InvalidOperationException("The command could not be found.");
            }
            return type;
        }
    }

    public class RoutingController : BaseController
    {
        private readonly IMessageBus _bus;

        public RoutingController(IMessageBus bus)
        {
            _bus = bus;
        }

        [HttpGet, Route("send"), AllowAnonymous]
        public async Task<dynamic> Get(string name)
        {
            var type = this.GetCommandType(name);

            return CreateResponse(await _bus.Send((dynamic)Activator.CreateInstance(type)));
        }

        [HttpPost, Route("send"), AllowAnonymous]
        public async Task<dynamic> Post(string name)
        {
            var type = this.GetCommandType(name);

            if (this.Request.Body.CanSeek)
            {
                this.Request.Body.Position = 0;
            }

            using (var streamReader = new StreamReader(this.Request.Body))
            {
                var body = await streamReader.ReadToEndAsync();

                return CreateResponse(await _bus.Send((dynamic)JsonConvert.DeserializeObject(body, type)));
            }
        }

        [HttpGet, Route("secure/send"), Authorize]
        public async Task<dynamic> SecureGet(string name)
        {
            var type = this.GetCommandType(name);

            return CreateResponse(await _bus.Send((dynamic)Activator.CreateInstance(type)));
        }

        [HttpPost, Route("secure/send"), Authorize]
        public async Task<dynamic> SecurePost(string name)
        {
            var type = this.GetCommandType(name);

            if (this.Request.Body.CanSeek)
            {
                this.Request.Body.Position = 0;
            }

            using (var streamReader = new StreamReader(this.Request.Body))
            {
                var body = await streamReader.ReadToEndAsync();

                return CreateResponse(await _bus.Send((dynamic)JsonConvert.DeserializeObject(body, type)));
            }
        }
    }
}