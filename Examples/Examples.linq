<Query Kind="Expression">
  <Connection>
    <ID>b5f6bc61-b4e5-4371-90cc-7123a2c0a7b5</ID>
    <Persist>true</Persist>
    <Driver Assembly="Cecil.LINQPad.Driver" PublicKeyToken="4f171ea11968e96a">Cecil.LINQPad.Driver.CecilContextDriver</Driver>
    <CustomAssemblyPathEncoded>&lt;CommonApplicationData&gt;\LINQPad\Drivers\DataContext\4.0\Cecil.LINQPad.Driver (4f171ea11968e96a)\Cecil.LINQPad.Driver.dll</CustomAssemblyPathEncoded>
    <CustomAssemblyPath>C:\ProgramData\LINQPad\Drivers\DataContext\4.0\Cecil.LINQPad.Driver (4f171ea11968e96a)\Cecil.LINQPad.Driver.dll</CustomAssemblyPath>
    <CustomTypeName>Cecil.LINQPad.Driver.DataContext</CustomTypeName>
    <DriverData>
      <assembly-base-path>R:\Work\Repo\UnityTrunk\build\WindowsEditor\Data</assembly-base-path>
    </DriverData>
  </Connection>
</Query>

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