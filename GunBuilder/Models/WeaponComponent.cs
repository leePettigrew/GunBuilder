// Models/WeaponComponent.cs
namespace GunBuilder.Models
{
    /// <summary>
    /// Base class for all weapon components.
    /// </summary>
    public abstract class WeaponComponent
    {
        public string Name { get; set; }
        public double PriceMod { get; set; } // Percentage-based modifier (e.g., 0.05 for 5%)
        public double WeightMod { get; set; } // Percentage-based modifier (e.g., 0.05 for 5%)
        public int DamageMod { get; set; }
        public int ExtraDice { get; set; }
        public int HideMod { get; set; }
        public int ACMod { get; set; }
        public string Note { get; set; }

        protected WeaponComponent(string name, double priceMod, double weightMod, int damageMod, int extraDice, int hideMod, int acMod, string note)
        {
            Name = name;
            PriceMod = priceMod;
            WeightMod = weightMod;
            DamageMod = damageMod;
            HideMod = hideMod;
            ACMod = acMod;
            ExtraDice = extraDice;
            Note = note;
        }
    }
}