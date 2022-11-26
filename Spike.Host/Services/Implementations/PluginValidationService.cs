using ICSharpCode.Decompiler.CSharp.Syntax;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler;
using Spike.DotNetCore.ILSpy._01.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.Decompiler.TypeSystem;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using App.Base.Host.Services;
using ICSharpCode.Decompiler.Metadata;

namespace App.Base.Host.Services.Implementations
{
    /// <summary>
    /// Implementation of the 
    /// <see cref="IPluginValidationService"/>
    /// </summary>
    public class PluginValidationService : IPluginValidationService
    {

        public bool ValidateAssembly(string assemblyFilePath, ValidationConstraintConfiguration validationConstraintConfiguration = null, string baseSearchDir = null)
        {
            var raw = ParseAssembly(assemblyFilePath, baseSearchDir);

            return ValidateAssembly(raw, validationConstraintConfiguration);
        }

        public bool ValidateAssembly(AssemblyDiscoveryRaw assemblyDiscoveriesRaw, ValidationConstraintConfiguration validationConstraintConfiguration = null)
        {
            if (assemblyDiscoveriesRaw == null)
            {
                return false;
            }
            if (assemblyDiscoveriesRaw.ExportedTypeNames.Count() == 0)
            {
                return false;
            }
            if (assemblyDiscoveriesRaw.ExportedControllerTypeNames.Count() == 0)
            {
                return false;
            }
            if (validationConstraintConfiguration == null)
            {
                validationConstraintConfiguration = new ValidationConstraintConfiguration();
                validationConstraintConfiguration.Excluded.Words.AddRange(new[] { "Activator", "FileStream", "FileReader", "FileWriter", "File" });
            }


            if (assemblyDiscoveriesRaw.ReferencedAssemblyNames.Any(x => validationConstraintConfiguration.Excluded.AssemblyNames.Contains(x)))
            {
                return false;
            }

            if (Contains(assemblyDiscoveriesRaw, validationConstraintConfiguration.Excluded.Words.ToArray()))
            {
                return false;
            }
            return true;
        }
        public static AssemblyDiscoveryRaw ParseAssembly(string assemblyFilePath, string baseSearchDir = null)
        {
            var results = new AssemblyDiscoveryRaw();

            DecompilerSettings decompilerSettings = DevelopDefaultCompilerSettings();

            UniversalAssemblyResolver uar =
                new UniversalAssemblyResolver(assemblyFilePath, false, null, null);

            // Develop decompiler
            var cSharpDecompiler =
                 new CSharpDecompiler(
                     assemblyFilePath,
                     uar,
                 decompilerSettings);



                if (!string.IsNullOrEmpty(baseSearchDir))
            {
                uar.AddSearchDirectory(baseSearchDir);
            }
            results.MainAssemblyMetadata = cSharpDecompiler.TypeSystem.MainModule;


            results.Namespaces.AddRange(cSharpDecompiler.TypeSystem.RootNamespace.ChildNamespaces);

            // What Other Assemblies is it talking to?
            // This is what one would use to see if a plugin
            // was talking to an assembly they shouldn't.
            results.ReferencedAssemblies.AddRange(cSharpDecompiler.TypeSystem.ReferencedModules);

            FindTypes(results, cSharpDecompiler);

            FindServicesAndTheirDefaultInterface(results);

            DevelopSourceCodeFiles(results, cSharpDecompiler);


            return results;

        }
        private static DecompilerSettings DevelopDefaultCompilerSettings()
        {
            DecompilerSettings decompilerSettings = new DecompilerSettings(LanguageVersion.Latest);

            // Decompile to latest version of C#:
            //decompilerSettings.SetLanguageVersion(LanguageVersion.Latest);
            //// Set up Configuration as to how to decompile:
            //decompilerSettings.AlwaysUseBraces = true;
            ////decompilerSettings.AnonymousMethods = true;
            ////decompilerSettings.AnonymousTypes = true;
            //decompilerSettings.AlwaysQualifyMemberReferences = true;
            //decompilerSettings.FileScopedNamespaces = false;
            ////decompilerSettings.RemoveDeadCode = false;
            ////decompilerSettings.ShowDebugInfo = true;
            //decompilerSettings.UseDebugSymbols = true;
            ////decompilerSettings.UseLambdaSyntax = false;
            //decompilerSettings.UsingStatement = true;
            ////decompilerSettings.UsingDeclarations = true;
            //decompilerSettings.UseEnhancedUsing = false;
            //decompilerSettings.StringConcat = false;
            //decompilerSettings.OptionalArguments = false;
            //decompilerSettings.NamedArguments = false;
            //decompilerSettings.AggressiveInlining = false;
            //decompilerSettings.AggressiveScalarReplacementOfAggregates = false;
            //decompilerSettings.AlwaysShowEnumMemberValues = true;
            return decompilerSettings;
        }

        private static void FindTypes(AssemblyDiscoveryRaw results, CSharpDecompiler cSharpDecompiler)
        {
            results.TypeDefinitions.AddRange(cSharpDecompiler.TypeSystem.MainModule.TypeDefinitions);

            // List of public classes developed in this Assembly:
            results.ExportedTypeDefinitions.AddRange(results.TypeDefinitions.Where(x => x.Accessibility == Accessibility.Public));

            results.ExportedControllerTypeDefinitions.AddRange(FindControllers(results.ExportedTypeDefinitions));
        }


        private static IEnumerable<ITypeDefinition> FindControllers(IEnumerable<ITypeDefinition> publicTypeDefinitions)
        {
            foreach (var publicType in publicTypeDefinitions)
            {
                //ITypeDefinition controllerBase = "FUCK";
                if (!publicType.DirectBaseTypes.Any(
                    x => string.Compare(x.FullName, typeof(ControllerBase).FullName /*"Microsoft.AspNetCore.Mvc.ControllerBase"*/, true) == 0))
                {
                    continue;
                }
                // Doesn't follow convention:
                if (!publicType.Name.EndsWith("Controller"))
                {
                    continue;
                }
                // If it's not public, why bother:
                if (publicType.EffectiveAccessibility() != Accessibility.Public)
                {
                    continue;
                }
                // If not one constructor is open, what's the point.
                if (!publicType.GetConstructors().Any(x => x.EffectiveAccessibility() == Accessibility.Public))
                {
                    continue;
                }
                //MIGHT BE WHAT WE ARE LOOKING FOR:
                var attributes = publicType.GetAttributes();
                foreach (var attribute in attributes)
                {
                    // For API controllers it will be:
                    // * ApiControllerAttribute
                    // * RouteAttribute (with arg 0 == route template)
                    // * ApiVersion
                    // For OData Controllers, there's going to be
                    // * ODataAttributeRouting (no args)
                    var attributeName = attribute.AttributeType.Name;
                    if (attributeName == "RouteAttribute")
                    {
                        var route = attribute.FixedArguments[0].Value;
                    }
                }
                // but eitherway, it's ok if they don't have the above Attributes...

                // Any of them routes?
                yield return publicType;
            }
        }


        private static void FindServicesAndTheirDefaultInterface(AssemblyDiscoveryRaw results)
        {
            foreach (var publicType in results.ExportedTypeDefinitions)
            {
                //ITypeDefinition controllerBase = "FUCK";
                //if (!publicType.DirectBaseTypes.Any(
                //    x => string.Compare(x.FullName, "" /*"Microsoft.AspNetCore.Mvc.ControllerBase"*/, true) == 0))
                //{
                //    continue;
                //}
                // Doesn't follow convention:
                // If it's not public, why bother:
                if (!publicType.Name.EndsWith("Service"))
                {
                    continue;
                }
                if (publicType.EffectiveAccessibility() != Accessibility.Public)
                {
                    continue;
                }
                // If not one constructor is open, what's the point.
                if (!publicType.GetConstructors().Any(x => x.EffectiveAccessibility() == Accessibility.Public))
                {
                    continue;
                }

                var baseTypes = publicType.GetAllBaseTypeDefinitions().Where(x => x.Kind == TypeKind.Interface);
                var interfaceType = baseTypes.FirstOrDefault();

                if (interfaceType == null)
                {
                    continue;
                }

                results.ExportedServiceTypeDefinitions.Add(publicType);
                results.ExportedServiceInterfaceTypeDefinitions.Add(interfaceType);
            }
        }


        private static void DevelopSourceCodeFiles(AssemblyDiscoveryRaw results, CSharpDecompiler cSharpDecompiler)
        {
            results.SourceCodeAll = cSharpDecompiler.DecompileWholeModuleAsString();

            foreach (var typeDefinition in cSharpDecompiler.TypeSystem.MainModule.TypeDefinitions)
            {
                var sourceCodeFile = cSharpDecompiler.DecompileAsString(typeDefinition.MetadataToken);

                results.SourceCodeFiles.Add(typeDefinition.FullName, sourceCodeFile);


                results.SourceCodeSyntaxTrees.Add(typeDefinition, cSharpDecompiler.Decompile(typeDefinition.MetadataToken));
            }
        }

        static bool Contains(AssemblyDiscoveryRaw assemblyDiscovery, params string[] keyWords)
        {
            return assemblyDiscovery.SourceCodeSyntaxTrees.Values.Any(x => Contains(x, keyWords));
        }

        static bool Contains(SyntaxTree syntaxTree, params string[] keyWords)
        {
            return syntaxTree.Children.Any(x => Contains(x, keyWords));
        }
        static bool Contains(AstNode astNode, params string[] keyWords)
        {
            var nodeType = astNode.NodeType;
            var output = astNode.ToString();
            Console.WriteLine(output);
            if (keyWords.Contains(output))
            {
                Console.WriteLine("Check");
                return true;
            }
            return astNode.Children.Any(x => Contains(x, keyWords));
        }
    }
}
