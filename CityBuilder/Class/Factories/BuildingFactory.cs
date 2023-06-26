using System.Reflection;
using CityBuilder.Interface;
using CityBuilder.Resources;

namespace CityBuilder.Class.Factory;

//Author: Patrick Bürdel
//Description: the building factory
public class BuildingFactory : IAbstractBuildingFactory
{
    private List<Type> _buildings;
    private string _namespaceBuilding;

    public BuildingFactory()
    {
        GetBuildingNamespace();
        LoadBuildings();
    }

    public string BuildingType { get; set; }

    /// <summary>
    ///     returns the created instance of the IAbstractBuilding
    /// </summary>
    /// <returns></returns>
    public IAbstractBuilding CreateBuilding()
    {
        var building = _buildings.FirstOrDefault(e => e.Name == BuildingType);
        return (IAbstractBuilding)Activator.CreateInstance(building);
    }

    /// <summary>
    ///     Getting the building Namespace from a resource directory
    /// </summary>
    private void GetBuildingNamespace()
    {
        _namespaceBuilding = string.Format(Namespaces.buildingNamespace);
    }

    /// <summary>
    ///     Load all building types from namespace
    /// </summary>
    private void LoadBuildings()
    {
        _buildings = Assembly.GetExecutingAssembly().GetTypes()
            .Where(e => e.IsClass && e.Namespace == _namespaceBuilding).ToList();
    }
}