
using ICSharpCode.Decompiler.CSharp.Syntax;
using ICSharpCode.Decompiler.TypeSystem;

namespace Spike.DotNetCore.ILSpy._01.Services.Configuration
{
    public class AssemblyDiscoveryRaw
    {
        public string MainAssemblyName { get { return MainAssemblyMetadata.AssemblyName; } }
        public MetadataModule MainAssemblyMetadata { get; set; }

        public IEnumerable<string> NamespaceNames { get { return Namespaces.Select(x => x.FullName); } }
        public List<INamespace> Namespaces { get; } = new List<INamespace>();

        public string SourceCodeAll { get; set; }
        public Dictionary<string, string> SourceCodeFiles { get; } = new Dictionary<string, string>();


        public Dictionary<ITypeDefinition, SyntaxTree> SourceCodeSyntaxTrees { get; } = new Dictionary<ITypeDefinition, SyntaxTree>();


        public IEnumerable<string> TypeNames { get { return this.TypeDefinitions.Select(x => x.Name); } }
        public List<ITypeDefinition> TypeDefinitions { get; } = new List<ITypeDefinition>();



        public IEnumerable<string> ExportedTypeNames { get { return this.ExportedTypeDefinitions.Select(x => x.Name); } }
        public List<ITypeDefinition> ExportedTypeDefinitions { get; } = new List<ITypeDefinition>();

        
        
        public IEnumerable<string> ReferencedAssemblyNames { get { return this.ReferencedAssemblies.Select(x => x.Name); } }
        public List<IModule> ReferencedAssemblies { get; } = new List<IModule>();





        public IEnumerable<string> ExportedControllerTypeNames { get { return this.ExportedControllerTypeDefinitions.Select(x => x.Name); } }
        public List<ITypeDefinition> ExportedControllerTypeDefinitions { get; } = new List<ITypeDefinition>();



        public IEnumerable<string> ExportedServiceInterfaceTypeNames { get { return this.ExportedServiceInterfaceTypeDefinitions.Select(x => x.Name); } }
        public List<ITypeDefinition> ExportedServiceInterfaceTypeDefinitions { get; } = new List<ITypeDefinition>();

        public IEnumerable<string> ExportedServiceTypeNames { get { return this.ExportedServiceTypeDefinitions.Select(x => x.Name); } }
        public List<ITypeDefinition> ExportedServiceTypeDefinitions { get; } = new List<ITypeDefinition>();


        
    }
}
