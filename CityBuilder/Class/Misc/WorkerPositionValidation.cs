using CityBuilder.Class.Buildings;
using CityBuilder.Class.Persons;

namespace CityBuilder.Class.Misc;

public static class WorkerPositionValidation
{
    public static Point PlaceWorkerOnBuilding(Person worker, Building location)
    {
        return new Point(GetPersonLocationX(location), location.Height - worker.Height);
    }
    
    private static int GetPersonLocationX(Building location)
    {
        List<Person> persons = location.Controls.OfType<Person>().ToList();
        return 5 + persons.Count * 66;
    }
}