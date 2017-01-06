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
using System;
using System.IO;
using System.Windows.Forms;
using Mono.Cecil;
using LINQPad.Extensibility.DataContext;

namespace Cecil.LINQPad.Driver
{
	public class CecilContextDriver : StaticDataContextDriver
	{
		public override string GetConnectionDescription(IConnectionInfo cxInfo)
		{
			var assemblyPath = (string)cxInfo.DriverData.Element("assembly-base-path");
			if (string.IsNullOrWhiteSpace(assemblyPath))
				MessageBox.Show("Unable to retrieve assembly path", "Error");

			return $"{Path.GetFileName(assemblyPath)} : {Path.GetDirectoryName(assemblyPath)}";
		}

		public override bool ShowConnectionDialog(IConnectionInfo cxInfo, bool isNewConnection)
		{
		    var folderBrowser = new FolderBrowserDialog();
			if (folderBrowser.ShowDialog() == DialogResult.OK)
			{
				cxInfo.CustomTypeInfo.CustomAssemblyPath = typeof(DataContext).Assembly.Location;
				cxInfo.CustomTypeInfo.CustomTypeName = typeof(DataContext).FullName;

				cxInfo.DriverData.SetElementValue("assembly-base-path", folderBrowser.SelectedPath);
				return true;
			}

			return false;
		}

		public override IEnumerable<string> GetNamespacesToAdd(IConnectionInfo cxInfo)
		{
			return new[] { GetType().Namespace };
		}

		public override IEnumerable<string> GetAssembliesToAdd(IConnectionInfo cxInfo)
		{
			return new[]
			{
				typeof(TypeDefinition).Assembly.Location,
				typeof(CecilContextDriver).Assembly.Location
			};
		}

		public override void InitializeContext(IConnectionInfo cxInfo, object context, QueryExecutionManager executionManager)
		{
		}

		public override ParameterDescriptor[] GetContextConstructorParameters(IConnectionInfo cxInfo)
		{
			return new[] { new ParameterDescriptor("assembly", typeof(string).FullName) };
		}

		public override object[] GetContextConstructorArguments(IConnectionInfo cxInfo)
		{
			var assemblyBasePath = (string) cxInfo.DriverData.Element("assembly-base-path");
			if (string.IsNullOrWhiteSpace(assemblyBasePath))
				MessageBox.Show("Unable to retrieve assembly path", "Error");

			return new object[] { assemblyBasePath };
		}

		public override string Name { get { return ".NET Metadata Query Support"; } }

		public override string Author { get { return "Adriano Carlos Verona"; } }

		public override List<ExplorerItem> GetSchema(IConnectionInfo cxInfo, Type customType)
		{
			return new List<ExplorerItem>
			{
				new ExplorerItem("Available Query Axis", ExplorerItemKind.Category, ExplorerIcon.Table)
				{
					Children = new List<ExplorerItem>
					{
						new ExplorerItem("Types", ExplorerItemKind.Category, ExplorerIcon.Column) { IsEnumerable = true },
						new ExplorerItem("PublicTypes", ExplorerItemKind.Category, ExplorerIcon.Column) { IsEnumerable = true },
						new ExplorerItem("Assemblies", ExplorerItemKind.Category, ExplorerIcon.Column) { IsEnumerable  = false },
					}
				}
			};
		}
	}
}
