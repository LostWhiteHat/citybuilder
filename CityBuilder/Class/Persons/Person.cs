using System.Diagnostics;
using CityBuilder.Class.Buildings;
using CityBuilder.Class.Misc;
using CityBuilder.Interface;

namespace CityBuilder.Class.Persons;

/// <summary>
///     Person base class
/// </summary>
public abstract class Person : PictureBox, IAbstractPerson
{
    private bool _isWorking = false;
    protected Person()
    {
        Image = ResManager.GetImagePerson(GetType().Name);
        SizeMode = PictureBoxSizeMode.AutoSize;
        BackColor = Color.Transparent;
        MouseClick += (sender, args) => ControlClicked(sender, args);
    }

    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public Building? Residence { get; set; }

    public virtual void DoEat()
    {
        Debug.WriteLine("I'm hungry");
    }

    private void ControlClicked(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            DoWork();
        else if (e.Button == MouseButtons.Right)
            Debug.WriteLine($"My name is {Firstname} {Lastname}");
    }
    
    public virtual void DoWork()
    {
        if (!_isWorking)
        {
            CityHandler.BringToWork(this);
            _isWorking = true;
        }
        else
        {
            GoSleep();
        }
    }

    public virtual void GoSleep()
    {
        CityHandler.BringBackHome(this);
        _isWorking = false;
    }
}