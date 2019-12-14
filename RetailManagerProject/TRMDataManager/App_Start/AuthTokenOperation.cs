using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace TRMDataManager.App_Start
{
    public class AuthTokenOperation : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            // https://stackoverflow.com/questions/51117655/how-to-use-swagger-in-asp-net-webapi-2-0-with-token-based-authentication
            //Customizing Swagger to add a new route as a Post to get a token
            //To use the Token route in Swagger UI, we need to add this class to SwaggerConfig.cs
            swaggerDoc.paths.Add("/token", new PathItem
            {
                //type of route is Post
                post = new Operation 
                {   //place token route in auth category
                    tags = new List<string> { "Auth" },
                    
                    consumes = new List<string> 
                    {   
                        //type of body sent in post 
                        "application/x-www-form-urlencoded"
                    },
                    parameters = new List<Parameter>
                    { 
                        //parameters for token endpoint
                        //grant_type
                        new Parameter
                        { 
                            type ="string",
                            name = "grant_type",
                            required = true,
                            @in = "formData",
                            @default = "password"
                        },
                        //username
                        new Parameter
                        {
                            type ="string",
                            name = "username",
                            required = false,
                            @in = "formData"
                        },
                        //password
                          new Parameter
                        {
                            type ="string",
                            name = "password",
                            required = false,
                            @in = "formData"
                        }
                    }

                }
            });
        }
    }
}