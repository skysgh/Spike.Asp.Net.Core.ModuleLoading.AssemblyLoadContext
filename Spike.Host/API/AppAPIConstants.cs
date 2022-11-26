namespace App.Base.API
{

    public partial class AppAPIConstants
    {
        public class OpenAPI
        {
            public class Generation
            {
                public class Output
                {
                    //Must not end with slash (so also must not be '/')
                    //And if no start slash, will be relative to above docs:
                    //but if slash included, will be relative to root:
                    //And in general, probably best to stick to something generic.
                    //so...?
                    public const string FileRoot = "/openAPI";

                    public const string FileName = "openapi.json";//swagger.json
                }
                public class UI
                {
                    public class Swagger
                    {
                        //Must not end with slash (so also must not be '/')
                        //And this one must not start with a slash either:
                        //IMPORTANT: When you change this, you also have to change
                        //matching string in "launchUrl": "docs/api/swagger" 
                        //setting within launchsettings.json
                        public const string SwaggerDocRoot = "docs/api/swagger";
                    }
                    public class ReDoc
                    {
                        public const string ReDocRoot = "docs/api/redoc";
                    }
                }

            }
        }

        // In general are in alignment with Modules:
        public partial class Areas
        {
            public class Base
            {
                public const string Name = "Base";
                public class Rest
                {
                    public class V1
                    {
                        public const string Protocol = "Rest";
                        public const string Version = "1";
                        public class Documentation
                        {
                            public const string ID = $"{Name}{Protocol}V{Version}";
                            public const string Title = $"{Name} {Protocol} V{Version} APIs";
                        }
                        public class Routing
                        {
                            public const string RootPrefix = $"api/{Protocol}/{Name}/v{{version}}";
                            public class Controllers
                            {
                                public class WeatherForecast
                                {
                                    public const string Name = "WeatherForecast";
                                    public const string Route = $"{RootPrefix}/{Name}";
                                };
                                public class ExampleH1
                                {
                                    public const string Name = "ExampleH1";
                                    public const string Route = $"{RootPrefix}/{Name}";
                                };
                                public class ExampleH2
                                {
                                    public const string Name = "ExampleH2";
                                    public const string Route = $"{RootPrefix}/{Name}";
                                };
                                public class LoadModule
                                {
                                    public const string Name = "LoadModule";
                                    public const string Route = $"{RootPrefix}/{Name}";
                                };
                            }
                        }
                    }
                }
                public class OData
                {
                    public class V1
                    {
                        public const string Protocol = "OData";
                        public const string Version = "1";
                        public class Documentation
                        {
                            public const string ID = $"{Name}{Protocol}V{Version}";
                            public const string Title = $"{Name} {Protocol} V{Version} APIs";
                        }
                        public class Routing
                        {
                            public const string RoutePrefix = $"api/{Protocol}/{Name}/v{{version}}";
                            public class Controllers
                            {
                                public class Controller1
                                {
                                    public const string Name = "exampleO1";
                                    public const string Route = $"{RoutePrefix}/{Name}";
                                }
                            }
                        }
                    }
                }
            }

        }

    }
}


