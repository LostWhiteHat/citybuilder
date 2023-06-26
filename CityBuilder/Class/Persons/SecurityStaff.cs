using CityBuilder.Interface;

namespace CityBuilder.Class.Persons;

public abstract class SecurityStaff : Person, IDrive
{
    public virtual void GoToDispatch()
    {
    }
}