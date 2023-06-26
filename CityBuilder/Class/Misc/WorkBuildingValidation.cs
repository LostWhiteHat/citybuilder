using CityBuilder.Class.Buildings;
using CityBuilder.Resources;

namespace CityBuilder.Class.Misc;

public static class WorkBuildingValidation
{
    public static string GetValidatedWorkBuilding(Type workerType)
    {
        if (workerType.Name == WorkerTypes.Doctor || workerType.Name == WorkerTypes.MedicalPracticeAssistant) 
            return "Hospital";
        else if (workerType.Name == WorkerTypes.Firefighter)
            return "FireDepartment";
        else if (workerType.Name == WorkerTypes.Police)
            return "PoliceDepartment";
        else if (workerType.Name == WorkerTypes.RetailSpecialist)
            return "Store";
        else if (workerType.Name == WorkerTypes.ApplicationDeveloper || workerType.Name == WorkerTypes.PlatformDeveloper)
            return "Office";
        else
            return "House";

    }
}