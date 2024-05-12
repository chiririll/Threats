using System.IO;
using System.Reflection;
using System.Text;

namespace Threats.Data;

internal static class DataLoader
{
    private const string entitiesResource = "threats.json";

    private static readonly Assembly assembly;
    private static readonly string assemblyName;

    static DataLoader()
    {
        assembly = Assembly.GetAssembly(typeof(DataLoader))!;
        assemblyName = assembly.GetName().Name!;
    }

    internal static EntitiesData LoadEntitiesData()
    {
        var stream = GetResource(entitiesResource);
        var reader = new StreamReader(stream, Encoding.UTF8);

        var json = reader.ReadToEnd();

        reader.Close();
        stream.Close();

        return EntitiesData.FromJson(json);
    }

    private static Stream GetResource(string name) => assembly.GetManifestResourceStream($"{assemblyName}.{name}")!;
}
