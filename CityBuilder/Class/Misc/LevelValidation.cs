using CityBuilder.Class.Buildings;

namespace CityBuilder.Class.Misc;

public static class LevelValidation
{
    public static bool DoValidation(Building building)
    {
        var maxLevel = ResManager.GetMaxLevelBuilding(building.GetType().Name);
        return building.Level < maxLevel;
    }
}