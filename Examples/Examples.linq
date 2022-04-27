<Query Kind="Expression">
  <Connection>
    <ID>e764e85d-961a-43fc-b2c9-1cf2d68b4fd1</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="Cecil.LINQPad.Driver" PublicKeyToken="no-strong-name">Cecil.LINQPad.Driver.CecilContextDriver</Driver>
    <CustomAssemblyPathEncoded>C:\Users\adriano\AppData\Local\LINQPad\Drivers\DataContext\NetCore\Cecil.LINQPad.Driver\Cecil.LINQPad.Driver.dll</CustomAssemblyPathEncoded>
    <CustomTypeName>Cecil.LINQPad.Driver.DataContext</CustomTypeName>
    <DriverData>
      <assembly-base-path>Z:\trunk\build\LinuxEditor\x64\Debug\Data\Managed\UnityEngine</assembly-base-path>
    </DriverData>
  </Connection>
</Query>

// You can select the example you want to run and press F5

//list assemblies, inside any subfolder, matching the specified pattern
from a in Assemblies.WithName("UnityEngine.*|UnityEditor.*")
select a.FullName

// All public (top level / nested) types declared in the specified assemblies
from t in Assemblies.WithName("UnityEngine.*|UnityEditor.*").PublicTypes()
select t.FullName

// All recursive methods on public types
from t in Assemblies.WithName("UnityEngine.*|UnityEditor.*").PublicTypes()
from m in t.Methods
where m.Calls(m)
select m.FullName

// All public methods with more than 10 parameters!
from t in Assemblies.WithName("UnityEngine.*|UnityEditor.*").PublicTypes()
from m in t.Methods
orderby m.Parameters.Count ascending
where m.IsPublicAPI() &&  m.Parameters.Count > 10
select m.Parameters.Count + ": " + m.Name

// All public methods that contains underscores in their names, but are not property setter/getters, event add/removal or operators
from t in Assemblies.WithName("UnityEngine.*|UnityEditor.*").PublicTypes()
from m in t.Methods
where m.IsPublicAPI() && m.Name.Contains("_") && !m.IsGetter && !m.IsSetter && !m.IsAddOn && !m.IsRemoveOn && !m.Name.Contains("op_")
select m.DeclaringType.Name + "." + m.Name


// Name + getter body of all indexer properties that contains both get/set methods and that throws exceptions
from t in Assemblies.WithName("UnityEngine.*|UnityEditor.*").PublicTypes()
from p in t.Properties
where p.GetMethod != null && p.SetMethod != null && p.GetMethod.Parameters.Count == 1 && p.GetMethod.HasBody && p.GetMethod.Body.Instructions.Any(i => i.OpCode == Mono.Cecil.Cil.OpCodes.Throw)
select p.FullName + "\n" + p.GetMethod.AsString()


// Select all public enums from assemblies start starts with Unity
from t in PublicTypes
where Regex.IsMatch(t.Module.Name, "Unity[^\\.]+.+\\.dll") && t.IsEnum
select $"[{t.FullName}"