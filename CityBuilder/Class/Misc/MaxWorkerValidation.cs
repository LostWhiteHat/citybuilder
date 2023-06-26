using CityBuilder.Class.Buildings;

namespace CityBuilder.Class.Misc;

public static class MaxWorkerValidation
{
    public static int GetMaxWorker(Building building)
    {
        return building.Level + 1;
    }
}