// Models/BarrelType.cs
namespace GunBuilder.Models.Ballistic
{
    /// <summary>
    /// Represents the type of barrel.
    /// </summary>
    public class BarrelType : WeaponComponent
    {
        public BarrelType(string name, double priceMod, double weightMod, int damageMod, int extraDice, int hideMod, int acMod, string note)
            : base(name, priceMod, weightMod, damageMod, extraDice, hideMod, acMod, note)
        {
        }
    }
}