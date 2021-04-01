/*
 * Copyright [2016] [Adriano Carlos Verona]
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Mono.Cecil;
using Mono.Cecil.Mdb;
using Mono.Cecil.Pdb;

namespace Cecil.LINQPad.Driver
{
    public class DataContext
    {
        public DataContext(string basePath)
        {
            LoadAssembliesRecursivelyFrom(basePath);

            container = new AssembliesContainer(assemblies);
        }

        public string SourceFor(IMemberDefinition member)
        {
            var declaringType = member.DeclaringType;
            var instruction = declaringType.Methods
                                        .Where(m => m.HasBody && m.Body.Instructions.Count > 0)
                                        .SelectMany(m => m.Body.Instructions)
                                        .FirstOrDefault(i => i.SequencePoint != null && i.SequencePoint.Document != null);

            return instruction?.SequencePoint.Document.Url;
        }

        public IEnumerable<TypeDefinition> Types { get { return assemblies.Types(); } }

        public IEnumerable<TypeDefinition> PublicTypes { get { return assemblies.PublicTypes(); } }

        public AssembliesContainer Assemblies { get { return container; } }

        private void LoadAssembliesRecursivelyFrom(string basePath)
        {
            var assemblyPaths = Directory.GetFiles(basePath, "*.dll");
            foreach (var assemblyPath in assemblyPaths)
            {
                AssemblyDefinition assembly;
                if (TryReadAssembly(assemblyPath, out assembly))
                    assemblies.Add(assembly);
            }

            foreach (var directory in Directory.GetDirectories(basePath))
            {
                LoadAssembliesRecursivelyFrom(directory);
            }
        }

        private bool TryReadAssembly(string assemblyPath, out AssemblyDefinition assembly)
        {
            try
            {
                var parameters = new ReaderParameters();
                if (File.Exists(assemblyPath + ".mdb"))
                {
                    parameters.SymbolReaderProvider = new MdbReaderProvider();
                }

                assembly = AssemblyDefinition.ReadAssembly(assemblyPath, parameters);
                return true;
            }
            catch (BadImageFormatException)
            {
                assembly = null;
                return false;
            }
        }

        private IList<AssemblyDefinition> assemblies = new List<AssemblyDefinition>();
        private AssembliesContainer container;
    }

    public class AssembliesContainer
    {
        public AssembliesContainer(IEnumerable<AssemblyDefinition> assemblies)
        {
            _assemblies = assemblies;
        }

        public IEnumerable<AssemblyDefinition> WithName(string regex)
        {
            var filterRegEx = new Regex(regex, RegexOptions.Compiled);
            return _assemblies.Where(a => filterRegEx.IsMatch(a.FullName));
        }

        private readonly IEnumerable<AssemblyDefinition> _assemblies;
    }
}
