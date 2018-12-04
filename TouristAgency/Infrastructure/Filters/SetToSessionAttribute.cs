using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TouristAgency.Infrastructure.Filters
{
    public class SetToSessionAttribute : Attribute, IActionFilter
    {
        string name;

        public SetToSessionAttribute(string name)
        {
            this.name = name;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            // считывание данных из ModelState и запись в сессию
            if (context.ModelState != null)
            {
                foreach (var item in context.ModelState)
                {
                    dict.Add(item.Key, item.Value.AttemptedValue);
                }
                context.HttpContext.Session.Set(name, dict);
            }
        }
    }
}