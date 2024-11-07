// GunBuilder.Models.Frame.cs
namespace GunBuilder.Models.Ballistic
{
    public class Frame
    {
        public string Name { get; set; }
        public double BaseWeightKg { get; set; }
        public double BasePriceCorium { get; set; } // New Property
        public int BaseDamage { get; set; } = 1;     // New Property

        public Frame(string name, double baseWeightKg, double basePriceCorium)
        {
            Name = name;
            BaseWeightKg = baseWeightKg;
            BasePriceCorium = basePriceCorium;
        }
    }
}
