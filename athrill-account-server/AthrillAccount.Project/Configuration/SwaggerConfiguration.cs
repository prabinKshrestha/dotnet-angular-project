using AT.Common.Api.Attributes;
using AT.Common.Api.Infrastructure;
using AT.Common.Constants;
using AT.Common.Enum;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using static AT.Common.Helpers.ServiceHelper;
using static AT.Common.Api.Constants.ApiConstants;

namespace AthrillAccount.Project.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void SwaggerGenOptionsConfiguration(this SwaggerGenOptions opt)
        {
            opt.OperationFilter<AddODataParametersOperationFilter>();

            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Athrill Account API", Version = "v1" });

            //swagger  authorization
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"Enter 'Bearer [space] {Token}' to authorize.<br>
                                Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);
        }
        public static void SwaggerUIOptionsConfiguration(this SwaggerUIOptions opt, IConfiguration configuration)
        {
            opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Athrill Account API");
            opt.RoutePrefix = string.Empty;
            opt.DocExpansion(DocExpansion.None);
            opt.EnableFilter();
            opt.DocumentTitle = $"{configuration.GetValue<string>("ATAppSettings:ApplicationName")} API Docs";
        }
    }
    public class AddODataParametersOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // check for ODAtaCapabilities Filter
            CustomAttributeData oDataCapabilitiesAttributeData = context.MethodInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(ODataQueryCapabilitiesAttribute));
            if (oDataCapabilitiesAttributeData != null)
            {
                // we are  not using following odata query parameter
                List<ODataCapabilities> listOfSystemCapabilities = new List<ODataCapabilities>() {
                        ODataCapabilities.NoSelect
                    };

                if (!context.ApiDescription.SupportedResponseTypes.FirstOrDefault().Type.IsGenericType)
                {
                    // this will do for single get,  post, put 
                    listOfSystemCapabilities.AddRange(GetODataCapabilitiesForSingleResponse());
                }

                if (HttpMethods.IsHead(context.ApiDescription.HttpMethod))
                {
                    // for head disable following
                    listOfSystemCapabilities.AddRange(GetODataCapabilitiesForHead());
                }

                ReadOnlyCollection<CustomAttributeTypedArgument> rawODataCapabilitiesParams = (ReadOnlyCollection<CustomAttributeTypedArgument>)oDataCapabilitiesAttributeData.ConstructorArguments.FirstOrDefault(y => y.ArgumentType == typeof(ODataCapabilities[])).Value;
                List<ODataCapabilities> oDataCapabilitiesParams = rawODataCapabilitiesParams != null ? rawODataCapabilitiesParams.Select(y => (ODataCapabilities)y.Value).ToList() : new List<ODataCapabilities>();

                listOfSystemCapabilities.AddRange(oDataCapabilitiesParams);

                if (operation.Parameters == null)
                {
                    operation.Parameters = new List<OpenApiParameter>();
                }

                listOfSystemCapabilities = listOfSystemCapabilities.Distinct().ToList();
                foreach (KeyValuePair<ODataCapabilities, string> dict in ODataQueryCapabilities.ODATA_QUERY_CAPABILITIES_CONSTANTS.Where(x => !listOfSystemCapabilities.Contains(x.Key)))
                {
                    operation.Parameters.Add(new OpenApiParameter()
                    {
                        Name = dict.Value,
                        In = ParameterLocation.Query,
                        Required = false,
                        Schema = new OpenApiSchema()
                        {
                            Type = ODataQueryCapabilities.ODATA_QUERY_CAPABILITIES_NAME_CONSTANTS_DATATYPE[dict.Key]
                        }
                    });
                }

            }

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = RequestHeaderProperties.APP_CONTEXT,
                In = ParameterLocation.Header,
                Description = $"Include Application Context.<br>{GetAppContext()}",
                Schema = new OpenApiSchema
                {
                    Type = "String",
                    Default = new OpenApiString(ATAppContext.AT_SUPPORT_SITE),

                }
            });
        }

        private string GetAppContext()
        {
            string retVal = string.Empty;
            foreach(KeyValuePair<string, ClientApplication> entry in ATAppContext.ClientApplicationEnumHeaderValuePair)
            {
                retVal += $@"<b>{entry.Value.GetDisplayName()}</b> : {entry.Key} <br>";
            }
            return retVal;
        }

        private IEnumerable<ODataCapabilities> GetODataCapabilitiesForSingleResponse()
        {
            return new List<ODataCapabilities>()
                {
                    ODataCapabilities.NoSelect,
                    ODataCapabilities.NoOrderBy,
                    ODataCapabilities.NoSearch,
                    ODataCapabilities.NoTop,
                    ODataCapabilities.NoSkip,
                    ODataCapabilities.NoPaging
                };
        }

        private IEnumerable<ODataCapabilities> GetODataCapabilitiesForHead()
        {
            return new List<ODataCapabilities>()
                {
                    ODataCapabilities.NoExpand,
                    ODataCapabilities.NoSelect,
                    ODataCapabilities.NoOrderBy,
                    ODataCapabilities.NoSearch,
                    ODataCapabilities.NoTop,
                    ODataCapabilities.NoSkip,
                    ODataCapabilities.NoPaging
                };
        }
    }
}