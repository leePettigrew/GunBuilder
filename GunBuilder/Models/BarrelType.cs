// Models/BarrelType.cs
namespace GunBuilder.Models
{
    /// <summary>
    /// Represents the type of barrel.
    /// </summary>
    public class BarrelType : WeaponComponent
    {
        public BarrelType(string name, double priceMod, double weightMod, int damageMod, int hideMod, int acMod, string note)
            : base(name, priceMod, weightMod, damageMod, hideMod, acMod, note)
        {
        }
    }
}