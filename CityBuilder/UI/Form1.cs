using CityBuilder.Class.Misc;
using CityBuilder.Resources;

namespace CityBuilder;

/// <summary>
///     Author: Patrick Bürdel
///     Description: The frontend code for the Form
/// </summary>
public partial class Form1 : Form
{
    private ContextMenuStrip _contextMenu;

    //Author: Patrick Bürdel
    public Form1()
    {
        InitializeComponent();
        InitializeForm();
    }

    /// <summary>
    ///     Initialize the form and UI
    /// </summary>
    private void InitializeForm()
    {
        BackgroundImage = Images.city_background;
        //BackColor = Color.FromArgb(54, 57, 63);
        MouseClick += OpenContextMenu;
    }

    /// <summary>
    ///     Creates the context menu by left click on form
    /// </summary>
    /// <param name="pos"></param>
    private void CreateContextMenu(Point pos)
    {
        _contextMenu = new ContextMenuStrip();
        var buildingTypeNames = CityHandler.GetBuildingTypeNames();

        foreach (var buildingTypeName in buildingTypeNames)
            _contextMenu.Items.Add(buildingTypeName).Click += (sender, e) => GetBuilding(sender, e, pos);
    }

    /// <summary>
    ///     Get the building after click on context menu item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="pos"></param>
    private void GetBuilding(object? sender, EventArgs e, Point pos)
    {
        var menuItem = (ToolStripItem)sender;
        var building = CityHandler.GenerateBuilding(menuItem.Text);
        building.Location = new Point(pos.X, pos.Y);
        Controls.Add(building);
    }

    /// <summary>
    ///     clickevent for open the contextmenu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OpenContextMenu(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            CreateContextMenu(Cursor.Position);
            _contextMenu.Show(Cursor.Position);
        }
    }
}