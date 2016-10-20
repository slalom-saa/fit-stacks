﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Slalom.FitStacks.Validation;

namespace Slalom.FitStacks.Messaging.Validation
{
    public class InRoleSecurityRule<TCommand> : SecurityValidationRule<TCommand> where TCommand : ICommand
    {
        private readonly string _role;
        private readonly ValidationError _error;

        public InRoleSecurityRule(string role, ValidationError error)
        {
            _role = role;
            _error = error;
        }

        protected override Task<ValidationError> Validate(TCommand instance)
        {
            if (!this.Context.User?.HasClaim(ClaimTypes.Role, _role) ?? true)
            {
                return Task.FromResult(_error);
            }
            return null;
        }
    }
}
