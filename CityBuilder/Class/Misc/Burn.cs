using CityBuilder.Resources;

namespace CityBuilder.Class.Misc;

public class Burn : PictureBox
{
    public Burn()
    {
        Image = Images.flame;
        SizeMode = PictureBoxSizeMode.AutoSize;
    }
}