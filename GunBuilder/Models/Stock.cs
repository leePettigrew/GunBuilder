// Models/Stock.cs
namespace GunBuilder.Models
{
    /// <summary>
    /// Represents the type of stock.
    /// </summary>
    public class Stock : WeaponComponent
    {
        public Stock(string name, double priceMod, double weightMod, int damageMod, int hideMod, int acMod, string note)
            : base(name, priceMod, weightMod, damageMod, hideMod, acMod, note)
        {
        }
    }
}