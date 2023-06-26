namespace CityBuilder.Interface;

//Author: Patrick Bürdel
//Description: the abstract factory interface for building factory
public interface IAbstractBuildingFactory
{
    IAbstractBuilding CreateBuilding();
}