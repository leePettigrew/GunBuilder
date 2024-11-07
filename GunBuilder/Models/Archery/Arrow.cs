// Models/Archery/Arrow.cs
namespace GunBuilder.Models.Archery
{
    /// <summary>
    /// Represents an arrow type with specific damage and armor penetration characteristics.
    /// </summary>
    public class Arrow
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int ArmorPenetration { get; set; }
        public string Note { get; set; }

        public Arrow(string name, int damage, int armorPenetration, string note)
        {
            Name = name;
            Damage = damage;
            ArmorPenetration = armorPenetration;
            Note = note;
        }
    }
}
