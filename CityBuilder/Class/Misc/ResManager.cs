using System.Reflection;
using System.Resources;
using CityBuilder.Resources;

namespace CityBuilder.Class.Misc;

//Author: Patrick Bürdel
//Description: The RessourceManager can dynamically add the correct image for all Person and Buildings
public static class ResManager
{
    private static ResourceManager _resourceManager;

    /// <summary>
    ///     The Method gets with the ResourceManager the image for the building
    /// </summary>
    /// <param name="type"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static Image GetImageBuilding(string type, int level)
    {
        _resourceManager = new ResourceManager(Namespaces.imagesNamespace, Assembly.GetExecutingAssembly());
        return (Image)_resourceManager.GetObject($"{type}{level}");
    }

    public static int GetMaxLevelBuilding(string type)
    {
        _resourceManager = new ResourceManager(Namespaces.maxLevelNamespace, Assembly.GetExecutingAssembly());
        return int.Parse(_resourceManager.GetString($"{type}"));
    }

    public static Image GetImagePerson(string type)
    {
        _resourceManager = new ResourceManager(Namespaces.imagesNamespace, Assembly.GetExecutingAssembly());
        return (Image)_resourceManager.GetObject($"{type}");
    }
}