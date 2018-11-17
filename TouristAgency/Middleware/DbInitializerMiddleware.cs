using TouristAgency.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task Invoke(HttpContext context, AgencyContext dbContext)
        {
            DbInitializer.Initialize(dbContext);
            return _next.Invoke(context);
        }
    }
}
