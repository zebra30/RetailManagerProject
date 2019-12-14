using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace TRMDataManager.App_Start
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        //Add Parameter to every route 
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            //verify the routes parameter list is not null, instantiate if need be
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            //Add access token parameter to each route
            operation.parameters.Add(
                new Parameter
                { 
                    name = "Authorization",
                    @in = "header",
                    description = "access token",
                    required = false,
                    type = "string"
                });
        }
    }
}