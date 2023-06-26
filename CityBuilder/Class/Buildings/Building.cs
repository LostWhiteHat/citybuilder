using CityBuilder.Class.Misc;
using CityBuilder.Class.Persons;
using CityBuilder.Interface;
using CityBuilder.Resources;

namespace CityBuilder.Class.Buildings;

/// <summary>
///     Author: Patrick Bürdel
///     Description: The motherclass for all buildings
/// </summary>
public abstract class Building : PictureBox, IAbstractBuilding
{
    private ContextMenuStrip _contextMenu;
    private ToolStripMenuItem _personMenuItem;

    protected Building()
    {
        Image = ResManager.GetImageBuilding(GetType().Name, Level);
        SizeMode = PictureBoxSizeMode.AutoSize;
        BackColor = Color.Transparent;
        MaxWorkers = MaxWorkerValidation.GetMaxWorker(this);
        MouseClick += Building_Click;
        BuildContextSubmenu();
        BuildContextMenu();
    }

    public string Street { get; set; }
    public int Number { get; set; }
    public static int BuildingCounter { get; set; }
    public int MaxWorkers { get; set; }
    public int Level { get; private set; } = 1;

    /// <summary>
    ///     Lets a Building burn in at a random time
    /// </summary>
    public virtual void DoBurn()
    {
        Controls.Add(new Burn());
    }

    /// <summary>
    ///     Can do an Upgrade for a building
    /// </summary>
    public virtual void DoUpgrade()
    {
        if (LevelValidation.DoValidation(this))
        {
            Level++;
            MaxWorkers = MaxWorkerValidation.GetMaxWorker(this);
            Image = ResManager.GetImageBuilding(GetType().Name, Level);
            foreach (var person in Controls.OfType<Person>())
            {
                person.Location = new Point(person.Location.X, Height - person.Height);
            }
        }
        else
        {
            MessageBox.Show(MaxLevels.maxLevelReachedText, MaxLevels.maxLevelReachedTitle,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    /// <summary>
    ///     Create add person submenu for context menu
    /// </summary>
    private void BuildContextSubmenu()
    {
        _personMenuItem = new ToolStripMenuItem { Text = "Add person" };
        var personTypes = CityHandler.GetPersonTypeNames();

        foreach (var personType in personTypes)
            _personMenuItem.DropDownItems.Add(personType).Click += (sender, e) => GetPerson(sender, e);
    }

    /// <summary>
    ///     The click event for the person context menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GetPerson(object? sender, EventArgs e)
    {
        var menuItem = (ToolStripItem)sender;
        if (Controls.OfType<Person>().Count() < MaxWorkers)
        {
            var person = CityHandler.GeneratePerson(menuItem.Text, this);
            person.Location = WorkerPositionValidation.PlaceWorkerOnBuilding(person, this);
            Controls.Add(person);
        }
        else
        {
            MessageBox.Show(MaxLevels.maxWorkersReachedText, MaxLevels.maxWorkersReachedTitle,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    /// <summary>
    ///     Create the context menu for the building
    /// </summary>
    private void BuildContextMenu()
    {
        _contextMenu = new ContextMenuStrip();
        _contextMenu.Items.Add("Upgrade").Click += (sender, e) => DoUpgrade();
        _contextMenu.Items.Add(_personMenuItem);
        _contextMenu.Items.Add("Burn").Click += (sender, e) => DoBurn();
    }

    /// <summary>
    ///     Click Event for the building
    ///     calling the context
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Building_Click(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right && GetType().Name == "House")
            _contextMenu.Show(Cursor.Position);
    }
}