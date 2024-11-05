// Models/ShotgunShell.cs
namespace GunBuilder.Models
{
    /// <summary>
    /// Represents shotgun shells with different gauges and special properties.
    /// </summary>
    public class ShotgunShell : WeaponComponent
    {
        public string Gauge { get; set; }
        public int GaugeDamageMod { get; set; }

        public ShotgunShell(string name, string gauge, double priceMod, double weightMod, int damageMod, int extraDice, int hideMod, int acMod,int gaugeDamageMod, string note)
            : base(name, priceMod, weightMod, damageMod, extraDice, hideMod, acMod, note)
        {
            Gauge = gauge;
            GaugeDamageMod = gaugeDamageMod;
        }
    }
}