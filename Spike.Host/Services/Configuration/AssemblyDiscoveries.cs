
using System.Reflection.Metadata;

namespace Spike.DotNetCore.ILSpy._01.Services.Configuration
{

    public class AssemblyDiscoveryResult
    {
        public string AssemblyName { get; set; }
        public string SourceCodeAll { get; set; }
        public Dictionary<string, string> SourceCodeFiles { get; set; }
        public List<string> ExportedTypeDefinitions { get; } = new List<string>();
        public List<string> ReferencedAssemblyNames { get; } = new List<string>();
        public List<string> ExportedControllerTypeNames { get; } = new List<string>();
        public List<string> ExportedServiceInterfaceTypeNames { get; } = new List<string>();
        public List<string> ExportedServiceTypeNames { get; } = new List<string>();
    }
}
