// Models/Ammo.cs
namespace GunBuilder.Models
{
    /// <summary>
    /// Represents ammunition for Pistol and Rifle types.
    /// </summary>
    public class Ammo : WeaponComponent
    {
        public string Type { get; set; } // "Pistol" or "Rifle"
        public double RoundSize { get; set; } // Size of round.

        public Ammo(string name, string type, double priceMod, double weightMod, int damageMod, int extraDice, int hideMod, int acMod, double roundSize, string note)
            : base(name, priceMod, weightMod, damageMod, extraDice, hideMod, acMod, note)
        {
            Type = type;
            RoundSize = roundSize;
        }
    }
}