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
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Cecil.LINQPad.Driver
{
	public class DataContext
	{
		public DataContext(AssemblyDefinition assembly)
		{
			this.assembly = assembly;
		}

		public IEnumerable<TypeDefinition> Types { get { return assembly.MainModule.GetAllTypes(); } }
		public IEnumerable<TypeDefinition> PublicTypes { get { return Types.Where(t => t.IsPublic || t.IsNestedPublic || t.IsNestedFamily || t.IsNestedFamilyOrAssembly); } }

		private AssemblyDefinition assembly;
	}
}