using System.Reflection;
using System.Runtime.Loader;

namespace FifthLab.ExploreAttribute;

public static class AssemblyScanner
{
    private static bool HasAttribute(this MemberInfo type, Type attributeType)
    {
        return type.GetCustomAttributes(false).Any(attribute => attribute.GetType().FullName == attributeType.FullName);
    }

    private static Assembly? LoadDll(string assemblyPath)
    {
        var loadContext = new AssemblyLoadContext("MyLoadContext");

        Assembly? assembly;
        try
        {
            assembly = loadContext.LoadFromAssemblyPath(assemblyPath);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading assembly: " + e.Message);
            return null;
        }

        return assembly;
    }
    
    private static Assembly? LoadExe(string assemblyPath)
    {
        Assembly? assembly;
        try
        {
            assembly = Assembly.LoadFile(assemblyPath);
            
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading assemble: " + e.Message);
            return null;
        }

        return assembly;
    }
    
    public static void PrintAllMarkedTypes(string assemblyPath, Type attributeType)
    {
        var assembly = LoadDll(assemblyPath);
        
        if (assembly is null)
            return;
        
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsPublic && type.HasAttribute(attributeType))
            {
                Console.WriteLine(type.Name);
            }
        }
    }
}