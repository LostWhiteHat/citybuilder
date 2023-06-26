using System.Reflection;
using CityBuilder.Interface;
using CityBuilder.Resources;

namespace CityBuilder.Class.Factory;

public class PersonFactory : IAbstractPersonFactory
{
    private string _namespacePerson;
    private List<Type> _persons;

    public PersonFactory()
    {
        GetPersonNamespace();
        LoadPersons();
    }

    public string JobName { get; init; }

    /// <summary>
    ///     returns the created instance of an person object
    /// </summary>
    /// <returns></returns>
    public IAbstractPerson CreatePerson()
    {
        var person = _persons.FirstOrDefault(e => e.Name == JobName);
        return (IAbstractPerson)Activator.CreateInstance(person);
    }

    /// <summary>
    ///     Getting the namespace from persons
    /// </summary>
    private void GetPersonNamespace()
    {
        _namespacePerson = string.Format(Namespaces.personNamespace);
    }

    /// <summary>
    ///     Load all types of Persons into a List of Types
    /// </summary>
    private void LoadPersons()
    {
        _persons = Assembly.GetExecutingAssembly().GetTypes()
            .Where(e => e.IsClass && e.Namespace == _namespacePerson).ToList();
    }
}