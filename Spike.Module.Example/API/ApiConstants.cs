namespace App.Modules.Example.API
{

    public partial class ApiConstants
    {
        // In general are in alignment with Modules:
        public partial class Areas
        {
            public class Module
            {
                public const string Name = "Module";
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
                            public const string RoutePrefix = $"api/{Protocol}/{Name}/v{{version}}";
                            public class Controllers
                            {
                                public class ExampleM1
                                {
                                    public const string Name = "ExampleM1";
                                    public const string Route = $"{RoutePrefix}/{Name}";
                                };
                                public class ExampleM2
                                {
                                    public const string Name = "ExampleM2";
                                    public const string Route = $"{RoutePrefix}/{Name}";
                                };
                                public class ExampleM3
                                {
                                    public const string Name = "ExampleM3";
                                    public const string Route = $"{RoutePrefix}/{Name}";
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
                                    public const string Name = "ExampleO1";
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


