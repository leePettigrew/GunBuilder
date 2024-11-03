// Models/WeaponOption.cs
namespace GunBuilder.Models
{
    public class WeaponOption
    {
        public string Name { get; set; }
        public double WeightMod { get; set; }
        public double PriceMod { get; set; }
        public double ACMod { get; set; }
        public double HideMod { get; set; }
        public double InitiativeMod { get; set; }
        public string SpecialNote { get; set; }

        public WeaponOption(string name, double weightMod = 0, double priceMod = 0, double acMod = 0,
                           double hideMod = 0, double initiativeMod = 0, string specialNote = "")
        {
            Name = name;
            WeightMod = weightMod;
            PriceMod = priceMod;
            ACMod = acMod;
            HideMod = hideMod;
            InitiativeMod = initiativeMod;
            SpecialNote = specialNote;
        }
    }
}
