// Models/Ammo.cs
namespace GunBuilder.Models
{
    /// <summary>
    /// Represents ammunition for Pistol and Rifle types.
    /// </summary>
    public class Ammo : WeaponComponent
    {
        public string Type { get; set; } // "Pistol" or "Rifle"

        public Ammo(string name, string type, double priceMod, double weightMod, int damageMod, int hideMod, int acMod, string note)
            : base(name, priceMod, weightMod, damageMod, hideMod, acMod, note)
        {
            Type = type;
        }
    }
}