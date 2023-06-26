using System.Diagnostics;
using System.Reflection;
using CityBuilder.Class.Buildings;
using CityBuilder.Class.Factory;
using CityBuilder.Class.Persons;
using CityBuilder.Resources;

namespace CityBuilder.Class.Misc;

/// <summary>
///     Author: Patrick Bürdel
///     Description: The interface from ui to backend
/// </summary>
internal static class CityHandler
{
    public static List<Person> Persons { get; set; } = new();
    public static List<Building> Buildings { get; set; } = new();

    /// <summary>
    ///     Generate a person into a house
    /// </summary>
    /// <returns></returns>
    public static Person GeneratePerson(string jobname, Building building)
    {
        var jobCounter = Persons.Where(e => e.GetType().Name == jobname).ToList().Count;
        var person = (Person)new PersonFactory { JobName = jobname }.CreatePerson();
        person.Residence = building;
        person.Firstname = jobname;
        person.Lastname = (jobCounter + 1).ToString();
        Persons.Add(person);
        return person;
    }

    /// <summary>
    ///     Generate a building
    /// </summary>
    /// <returns>Building</returns>
    public static Building GenerateBuilding(string buildingType)
    {
        var building = (Building)new BuildingFactory { BuildingType = buildingType }.CreateBuilding();
        building.Street = "Teststreet";
        building.Number = 1;
        Buildings.Add(building);
        return building;
    }

    /// <summary>
    ///     Retuns a list of name from the building class names
    /// </summary>
    /// <returns></returns>
    public static List<string> GetBuildingTypeNames()
    {
        var buildings = Assembly.GetExecutingAssembly().GetTypes()
            .Where(e => e.IsClass && e.Namespace == Namespaces.buildingNamespace && !e.IsAbstract).ToList();

        List<string> buildingTypeNames = new();
        foreach (var buildingType in buildings)
            buildingTypeNames.Add(buildingType.Name);

        return buildingTypeNames;
    }

    /// <summary>
    ///     Return a list of string with the class names from the persons
    /// </summary>
    /// <returns></returns>
    public static List<string> GetPersonTypeNames()
    {
        var persons = Assembly.GetExecutingAssembly().GetTypes()
            .Where(e => e.IsClass && e.Namespace == Namespaces.personNamespace && !e.IsAbstract).ToList();

        List<string> personTypeNames = new();
        foreach (var personType in persons) personTypeNames.Add(personType.Name);
        return personTypeNames;
    }

    /// <summary>
    /// Brings the person to Work
    /// </summary>
    /// <param name="workerType"></param>
    /// <param name="homeLocation"></param>
    public static void BringToWork(Person worker)
    {
        var workBuilding = WorkBuildingValidation.GetValidatedWorkBuilding(worker.GetType());
        List<Building> workLocations = Buildings.Where(e => e.GetType().Name == workBuilding).ToList();
        Building selectedWorkLocation = null;
        foreach (Building location in workLocations)
        {
            var controlsCount = location.Controls.Count;
            if (location.MaxWorkers > controlsCount)
                selectedWorkLocation = location;
        }
        if (selectedWorkLocation == null)
            MessageBox.Show($"You have to build a {workBuilding} first");
        else
        {
            worker.Parent.Controls.Remove(worker);
            worker.Location = WorkerPositionValidation.PlaceWorkerOnBuilding(worker, selectedWorkLocation);
            selectedWorkLocation.Controls.Add(worker);
        }
    }

    public static void BringBackHome(Person worker)
    {
        var homeBuildings = Buildings.Where(e => e.GetType().Name == "House").ToList();

        Building selectedHome = null;

        foreach (var homeBuilding in homeBuildings)
        {
            var controlsCount = homeBuilding.Controls.Count;
            if (homeBuilding.MaxWorkers > controlsCount)
                selectedHome = homeBuilding;
        }

        if (selectedHome == null)
            MessageBox.Show("Worker can't go home. There is no free House. You have to build one first");
        else
        {
            worker.Parent.Controls.Remove(worker);
            worker.Location = WorkerPositionValidation.PlaceWorkerOnBuilding(worker, selectedHome);
            selectedHome.Controls.Add(worker);
        }
    }
}