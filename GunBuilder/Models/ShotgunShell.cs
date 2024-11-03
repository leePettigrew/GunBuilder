// Models/ShotgunShell.cs
namespace GunBuilder.Models
{
    /// <summary>
    /// Represents shotgun shells with different gauges and special properties.
    /// </summary>
    public class ShotgunShell : WeaponComponent
    {
        public string Gauge { get; set; }

        public ShotgunShell(string name, string gauge, double priceMod, double weightMod, int damageMod, int hideMod, int acMod, string note)
            : base(name, priceMod, weightMod, damageMod, hideMod, acMod, note)
        {
            Gauge = gauge;
        }
    }
}