// Models/Archery/Bow.cs
namespace GunBuilder.Models.Archery
{
    /// <summary>
    /// Represents a bow with specific length, material, draw weight, strength requirement, and dice to roll.
    /// </summary>
    public class Bow
    {
        public string Name { get; set; }
        public double LengthCm { get; set; }
        public Material Material { get; set; }
        public double DrawWeightKg { get; set; }
        public int StrRequirement { get; set; }
        public int DiceToRoll { get; set; } 
        public string Damage { get; set; }
        public int DamageDice { get; set; }
        public string Note { get; set; }
        public double BasePrice { get; set; } 
        public double BaseWeight { get; set; } 

        public Bow(string name, double lengthCm, Material material, double drawWeightKg, double basePrice, double baseWeight, String note)
        {
            Name = name;
            LengthCm = lengthCm;
            Material = material;
            DrawWeightKg = drawWeightKg;
            BasePrice = basePrice;
            BaseWeight = baseWeight;
            Note = note;
            CalculateRequirements();
            // Damage is calculated based on arrow type and other modifiers
            Damage = "0"; // Base damage
        }

        /// <summary>
        /// Updates the bow's length and draw weight, then recalculates requirements.
        /// </summary>
        public void UpdateBow(double newLengthCm, double newDrawWeightKg)
        {
            LengthCm = newLengthCm;
            DrawWeightKg = newDrawWeightKg;
            CalculateRequirements();
        }

        /// <summary>
        /// Calculates the strength requirement and dice to roll based on bow length and draw weight.
        /// </summary>
        public void CalculateRequirements()
        {
            // Length Bands
            int lengthStrMod = 0;
            int lengthDiceMod = 0;
            if (LengthCm <= 80)
            {
                lengthStrMod += 10;
                lengthDiceMod += 1;
            }
            else if (LengthCm <= 120)
            {
                lengthStrMod += 8;
                lengthDiceMod += 1;
            }
            else if (LengthCm <= 180)
            {
                lengthStrMod += 6;
                lengthDiceMod += 1;
            }
            else // >180cm
            {
                lengthStrMod += 6;
                lengthDiceMod += 1;
            }

            // Draw Weight Bands
            int drawWeightStrMod = 0;
            int drawWeightDiceMod = 0;
            if (DrawWeightKg <= 10)
            {
                drawWeightStrMod += 2;
                drawWeightDiceMod += 1;
            }
            else if (DrawWeightKg <= 30)
            {
                drawWeightStrMod += 4;
                drawWeightDiceMod += 2;
            }
            else if (DrawWeightKg <= 50)
            {
                drawWeightStrMod += 6;
                drawWeightDiceMod += 3;
            }
            else if (DrawWeightKg <= 70)
            {
                drawWeightStrMod += 8;
                drawWeightDiceMod += 4;
            }
            else // >=71kg
            {
                drawWeightStrMod += 10;
                drawWeightDiceMod += 5;
            }

            // Total Strength Requirement
            StrRequirement = lengthStrMod + drawWeightStrMod;

            // Total Dice to Roll
            DiceToRoll = lengthDiceMod + drawWeightDiceMod;

            // Ensure DiceToRoll is at least 1
            if (DiceToRoll < 1)
                DiceToRoll = 1;




        }

        // Models/Archery/Bow.cs
        public double CalculateTotalPrice()
        {
            double totalPrice = BasePrice;
            double totalPriceModPercentage = 0.0;

            if (Material != null)
            {
                totalPriceModPercentage += Material.PriceMod;
            }



            totalPrice *= (1 + totalPriceModPercentage);

            return totalPrice;
        }

        public double CalculateTotalWeight()
        {
            double totalWeight = BaseWeight;
            double totalWeightModPercentage = 0.0;

            if (Material != null)
            {
                totalWeightModPercentage += Material.WeightMod;
            }



            totalWeight *= (1 + totalWeightModPercentage);

            return totalWeight;
        }

    }
}
