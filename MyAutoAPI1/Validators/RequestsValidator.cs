﻿using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAutoAPI1.Validators.Currency;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MyAutoAPI1.Models.Responses;
using Microsoft.AspNetCore.Http;

namespace MyAutoAPI1.Validators
{

    public class RequestsValidator : TypeFilterAttribute
    {
        public RequestsValidator() : base(typeof(RolesFilterAttributeImpl))
        {

        }

        private class RolesFilterAttributeImpl : IActionFilter
        {
            private readonly Type type;

            public RolesFilterAttributeImpl(Type type)
            {
                this.type = type;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var argument = context.ActionArguments;

                var body = context.ActionArguments.Values.ToList()[0];

                var validator = Activator.CreateInstance(type);

                var methods = validator.GetType().GetMethods().FirstOrDefault(o => o.Name == "Validate");

                var validationResult = methods.Invoke(validator, new object[] { body }) as ValidationResult;

                if(!validationResult.IsValid)
                {
                    context.Result = new ObjectResult(context.ModelState) {
                        Value = string.Join(", ", validationResult.Errors.Select(o => o.ErrorMessage)),
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

            }
        }
    }

}

