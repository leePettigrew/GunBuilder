// Models/Archery/Material.cs
namespace GunBuilder.Models.Archery
{
    /// <summary>
    /// Represents the material of a bow, affecting its accuracy, durability, and damage.
    /// </summary>
    public class Material
    {
        public string Name { get; set; }
        public int AccuracyMod { get; set; }
        public int ShotDurabilityMod { get; set; }
        public int DamageMod { get; set; }
        public double PriceMod { get; set; }
        public double WeightMod { get; set; }


        public Material(string name, int accuracyMod, int shotDurabilityMod, int damageMod, double priceMod, double weightMod)
        {
            Name = name;
            AccuracyMod = accuracyMod;
            ShotDurabilityMod = shotDurabilityMod;
            DamageMod = damageMod;
            PriceMod = priceMod;
            WeightMod = weightMod;


        }
    }
}
