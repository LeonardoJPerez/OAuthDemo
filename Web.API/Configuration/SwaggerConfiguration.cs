using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.SwaggerGen;
using Swashbuckle.SwaggerGen.XmlComments;

namespace Web.API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwagger(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddSwaggerGen();
            services.ConfigureSwaggerDocument(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "API",
                    Description = "API Documentation",
                    TermsOfService = "None"
                });

                options.OperationFilter(new ApplyXmlActionComments(configuration["Swagger:Path"]));
            });
            services.ConfigureSwaggerSchema(options =>
            {
                options.DescribeAllEnumsAsStrings = true;
                options.ModelFilter(new ApplyXmlTypeComments(configuration["Swagger:Path"]));
            });
        }
    }
}