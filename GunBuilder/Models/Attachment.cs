// Models/Attachment.cs
namespace GunBuilder.Models
{
    /// <summary>
    /// Represents weapon attachments with percentage-based modifiers.
    /// </summary>
    public class Attachment : WeaponComponent
    {
        public double PriceModPercentage { get; set; } // e.g., 0.05 for 5%
        public double WeightModPercentage { get; set; } // e.g., 0.05 for 5%

        public Attachment(string name, double priceModPercentage, double weightModPercentage, int damageMod, int hideMod, int acMod, string note)
            : base(name, 0, 0, damageMod, hideMod, acMod, note) // PriceMod and WeightMod are handled separately
        {
            PriceModPercentage = priceModPercentage;
            WeightModPercentage = weightModPercentage;
        }
    }
}