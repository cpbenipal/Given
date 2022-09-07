using System;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace Given.Api.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class StringQueryConstraintAttribute : ActionMethodSelectorAttribute
    {
        public StringQueryConstraintAttribute(string valueName, bool hasValue)
        {
            ValueName = valueName;
            HasValue = hasValue;
        }

        public string ValueName { get; }
        public bool HasValue { get; }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            var value = routeContext.HttpContext.Request.Query[ValueName];
            return HasValue ? !string.IsNullOrEmpty(value) : string.IsNullOrEmpty(value);
        }
    }    
}
