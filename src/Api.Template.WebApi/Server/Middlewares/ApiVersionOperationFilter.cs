using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Api.Template.CI.WebApi.Server.Middlewares
{
    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var actionApiVersionModel = context.ApiDescription.ActionDescriptor.GetApiVersion();
            if (actionApiVersionModel == null) return;

            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            var webApiAssembly = Assembly.GetEntryAssembly();
            var apiVersions = GetApiVersions(webApiAssembly);

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "x-api-version",
                In = "header",
                Required = true,
                Format = "enum",
                Type = "string",
                Enum = apiVersions.ToList()
            });
        }

        private static IEnumerable<object> GetApiVersions(Assembly webApiAssembly)
        {
            var apiVersion = webApiAssembly.DefinedTypes
                .Where(x => x.IsSubclassOf(typeof(Controller)) && x.GetCustomAttributes<ApiVersionAttribute>().Any())
                .Select(y => y.GetCustomAttribute<ApiVersionAttribute>())
                .SelectMany(v => v.Versions)
                .Distinct()
                .OrderBy(x => x);

            return apiVersion.Select(x => x.ToString());
        }
    }
}