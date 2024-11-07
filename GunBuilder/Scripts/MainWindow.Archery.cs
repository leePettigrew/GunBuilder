// Scripts/Archery.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GunBuilder.Models.Archery;

namespace GunBuilder
{
    public partial class MainWindow : Window
    {
        // Archery Component Lists
        private List<Material> MaterialList { get; set; } = new List<Material>();
        private List<Arrow> ArrowList { get; set; } = new List<Arrow>();
        private List<Bow> BowList { get; set; } = new List<Bow>();

        // Selected Archery Components
        private Bow SelectedBow = null;
        private Arrow SelectedArrow = null;

        /// <summary>
        /// Loads all archery components into their respective lists and populates the UI.
        /// </summary>
        private void LoadArcheryComponents()
        {
            LoadMaterialData();
            LoadArrowData();
            LoadBowData();
            PopulateArcheryComboBoxes();
        }

        /// <summary>
        /// Loads material data into the MaterialList.
        /// </summary>
        private void LoadMaterialData()
        {
            MaterialList = new List<Material>
            {
                // name | accuracy mod | shot durability mod | damage mod | price mod | weight mod
                new Material("9 Rulers Stuck Together", -2, 1, -4, 0.1,0.3),
                new Material("Fiberglass", 0, 0, 0, 0.1, 0),
                new Material("Carbon Fiber", 1, 1, 2, 0.1, 0),
                new Material("Wood", -1, 0, -2, 0.1, 0.5),
                new Material("Composite", 2, 2, 3, 0.1, 0)
                // Add more materials as needed
            };
        }

        /// <summary>
        /// Loads arrow data into the ArrowList.
        /// </summary>
        private void LoadArrowData()
        {
            ArrowList = new List<Arrow>
            {
                //name | damage | armor penetration | note
                new Arrow("Bodkin", 8, 2, "2 Armor ignore"),
                new Arrow("Broadhead", 8, -2, "-2 Armor penetration"),
                new Arrow("Obsidian/Glass", 6, 0, "Breaks in enemies, chance of bleeding"),
                new Arrow("Improvised", 6, 0, "Basic, unreliable arrow"),
                new Arrow("Explosive", 10, 0, "Explosive attached (damage varies)")
                // Add more arrow types as needed
            };
        }

        /// <summary>
        /// Loads bow data into the BowList.
        /// </summary>
        private void LoadBowData()
        {
            BowList = new List<Bow>
                        //You shouldnt need to actually change these weights or lengths or materials, Just a baseplate. Name and note.
            {           // name | length | material | draw weight | base price | base weight | note
                new Bow("Basic Bow", 200, MaterialList.FirstOrDefault(m => m.Name == "Fiberglass"), 80, 100, 3, "Basic"),
                new Bow("Advanced Bow", 180, MaterialList.FirstOrDefault(m => m.Name == "Carbon Fiber"), 50,200,4, "Advanced"),
                new Bow("Lightweight Bow", 160, MaterialList.FirstOrDefault(m => m.Name == "Composite"), 30,150, 2, "Lightweight"),
                new Bow("Heavy Bow", 220, MaterialList.FirstOrDefault(m => m.Name == "9 Rulers Stuck Together"), 80,180,8, "Heavy")
                // Add more bows as needed
            };
        }

        /// <summary>
        /// Populates the Archery ComboBoxes with loaded data.
        /// </summary>
        private void PopulateArcheryComboBoxes()
        {
            // Populate BowComboBox
            BowComboBox.Items.Clear();
            foreach (var bow in BowList)
            {
                BowComboBox.Items.Add(new ComboBoxItem { Content = bow.Name });
            }

            // Populate MaterialComboBox
            MaterialComboBox.Items.Clear();
            foreach (var material in MaterialList)
            {
                MaterialComboBox.Items.Add(new ComboBoxItem { Content = material.Name });
            }

            // Populate ArrowComboBox
            ArrowComboBox.Items.Clear();
            foreach (var arrow in ArrowList)
            {
                ArrowComboBox.Items.Add(new ComboBoxItem { Content = arrow.Name });
            }

            // Set default selections if available
            if (BowComboBox.Items.Count > 0)
                BowComboBox.SelectedIndex = 0;
            if (MaterialComboBox.Items.Count > 0)
                MaterialComboBox.SelectedIndex = 0;
            if (ArrowComboBox.Items.Count > 0)
                ArrowComboBox.SelectedIndex = 0;

            // Manually set the selected items
            SetSelectedBow();
            SetSelectedMaterial();
            SetSelectedArrow();
        }

        private void SetSelectedBow()
        {
            if (BowComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string bowName = selectedItem.Content.ToString();
                SelectedBow = BowList.FirstOrDefault(b => b.Name == bowName);
            }
        }

        private void SetSelectedMaterial()
        {
            if (MaterialComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string materialName = selectedItem.Content.ToString();
                var material = MaterialList.FirstOrDefault(m => m.Name == materialName);
                if (material != null && SelectedBow != null)
                {
                    SelectedBow.Material = material;
                }
            }
        }

        private void SetSelectedArrow()
        {
            if (ArrowComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string arrowName = selectedItem.Content.ToString();
                SelectedArrow = ArrowList.FirstOrDefault(a => a.Name == arrowName);
            }
        }

        /// <summary>
        /// Handles the selection change of the Bow ComboBox.
        /// </summary>
        private void BowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (BowComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string bowName = selectedItem.Content.ToString();
                    SelectedBow = BowList.FirstOrDefault(b => b.Name == bowName);
                    // Do not auto-set DrawWeightTextBox and BowLengthTextBox
                    // Allow user to input these manually
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting bow: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        /// <summary>
        /// Handles the selection change of the Material ComboBox.
        /// </summary>
        private void MaterialComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (MaterialComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string materialName = selectedItem.Content.ToString();
                    var material = MaterialList.FirstOrDefault(m => m.Name == materialName);
                    if (material != null && SelectedBow != null)
                    {
                        SelectedBow.Material = material;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting material: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the selection change of the Arrow ComboBox.
        /// </summary>
        private void ArrowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (ArrowComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string arrowName = selectedItem.Content.ToString();
                    SelectedArrow = ArrowList.FirstOrDefault(a => a.Name == arrowName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting arrow: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Calculates and displays archery properties based on current selections.
        /// </summary>
        // MainWindow.xaml.cs

        private void CalculateAndDisplayArcheryProperties()
        {
            try
            {
                if (SelectedBow == null || SelectedArrow == null)
                {
                    MessageBox.Show("Please select both a bow and an arrow.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Calculate Strength Requirement and Dice to Roll
                int strReq = SelectedBow.StrRequirement;
                int diceToRoll = SelectedBow.DiceToRoll;

                // Apply Material Modifiers
                int accuracyMod = SelectedBow.Material.AccuracyMod;
                int shotDurabilityMod = SelectedBow.Material.ShotDurabilityMod;
                int damageMod = SelectedBow.Material.DamageMod;

                // Calculate Damage
                int baseDamageDice = diceToRoll; // Number of dice to roll
                int baseDamage = SelectedArrow.Damage; // Damage per die

                // Halve the Damage Dice and round up
                int finalDamageDice = (int)Math.Ceiling(baseDamageDice / 2.0);

                // Define Damage String
                string damageString = $"{finalDamageDice}d{baseDamage}";

                // Calculate Total Price and Weight
                double totalPrice = SelectedBow.CalculateTotalPrice();
                double totalWeight = SelectedBow.CalculateTotalWeight();


                // Compile Weapon Properties Section
                StringBuilder weaponProperties = new StringBuilder();
                weaponProperties.AppendLine("**Archery Properties:**");
                weaponProperties.AppendLine("----------------------------");
                weaponProperties.AppendLine($"**Selected Bow:** {SelectedBow.Name}");
                weaponProperties.AppendLine($"**Length:** {SelectedBow.LengthCm} cm");
                weaponProperties.AppendLine($"**Material:** {SelectedBow.Material.Name}");
                weaponProperties.AppendLine($"- Accuracy Modifier: {SelectedBow.Material.AccuracyMod}");
                weaponProperties.AppendLine($"- Shot Durability Modifier: {SelectedBow.Material.ShotDurabilityMod}");
                weaponProperties.AppendLine($"- Damage Modifier: {SelectedBow.Material.DamageMod}");
                weaponProperties.AppendLine($"**Draw Weight:** {SelectedBow.DrawWeightKg} kg");
                weaponProperties.AppendLine($"**Strength Requirement:** {strReq} str");
                weaponProperties.AppendLine($"**Selected Arrow:** {SelectedArrow.Name}");
                weaponProperties.AppendLine($"- Damage per Die: {SelectedArrow.Damage}");
                weaponProperties.AppendLine($"- Armor Penetration: {SelectedArrow.ArmorPenetration}");
                weaponProperties.AppendLine($"**Final Damage:** {damageString}");
                weaponProperties.AppendLine("----------------------------");

                // Compile Price and Weight Section
                StringBuilder priceWeightInfo = new StringBuilder();
                priceWeightInfo.AppendLine("**Price and Weight:**");
                priceWeightInfo.AppendLine("----------------------------");
                priceWeightInfo.AppendLine($"**Base Price:** ${SelectedBow.BasePrice}");
                priceWeightInfo.AppendLine($"**Material Price Modifier:** {SelectedBow.Material.PriceMod * 100}%");
                priceWeightInfo.AppendLine($"**Total Price:** ${totalPrice:F2}");
                priceWeightInfo.AppendLine($"**Base Weight:** {SelectedBow.BaseWeight} kg");
                priceWeightInfo.AppendLine($"**Material Weight Modifier:** {SelectedBow.Material.WeightMod * 100}%");
                priceWeightInfo.AppendLine($"**Total Weight:** {totalWeight:F2} kg");
                priceWeightInfo.AppendLine("----------------------------");


                // Compile Special Notes Section
                StringBuilder specialNotes = new StringBuilder();
                specialNotes.AppendLine("**Special Notes:**");
                specialNotes.AppendLine("----------------------------");
                specialNotes.AppendLine($"- Accuracy is modified by {accuracyMod} due to material.");
                specialNotes.AppendLine($"- Shot durability is modified by {shotDurabilityMod} due to material.");
                specialNotes.AppendLine($"- Damage is modified by {damageMod} due to material.");
                specialNotes.AppendLine($"- {SelectedArrow.Note}");
                specialNotes.AppendLine($"- {SelectedBow.Note}");
                specialNotes.AppendLine("----------------------------");

                // Compile Debug Information Section
                StringBuilder debugInfo = new StringBuilder();
                debugInfo.AppendLine("**Debug Information:**");
                debugInfo.AppendLine("============================");
                debugInfo.AppendLine($"Selected Bow: {SelectedBow.Name}");
                debugInfo.AppendLine($"Bow Length: {SelectedBow.LengthCm} cm");
                debugInfo.AppendLine($"Bow Material: {SelectedBow.Material.Name}");
                debugInfo.AppendLine($"Draw Weight: {SelectedBow.DrawWeightKg} kg");
                debugInfo.AppendLine($"Strength Requirement: {strReq} str");
                debugInfo.AppendLine($"Dice to Roll: {diceToRoll}");
                debugInfo.AppendLine($"Selected Arrow: {SelectedArrow.Name}");
                debugInfo.AppendLine($"Arrow Damage: {SelectedArrow.Damage}");
                debugInfo.AppendLine($"Arrow Armor Penetration: {SelectedArrow.ArmorPenetration}");
                debugInfo.AppendLine($"Damage Modifier from Material: {damageMod}");
                debugInfo.AppendLine($"Final Damage: {damageString}");
                debugInfo.AppendLine($"Total Price: ${totalPrice:F2}");
                debugInfo.AppendLine($"Total Weight: {totalWeight:F2} kg");
                debugInfo.AppendLine("============================");

                // Compile Final Output
                StringBuilder finalOutput = new StringBuilder();
                finalOutput.AppendLine("**Archery Configuration:**");
                finalOutput.AppendLine("----------------------------");
                finalOutput.Append(weaponProperties.ToString());
                finalOutput.AppendLine(priceWeightInfo.ToString());
                finalOutput.AppendLine(specialNotes.ToString());
                finalOutput.AppendLine(debugInfo.ToString());

                // Update the OutputTextBox
                if (OutputTextBox != null)
                {
                    OutputTextBox.Text = finalOutput.ToString();
                }
                else
                {
                    MessageBox.Show("OutputTextBox is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in CalculateAndDisplayArcheryProperties: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the click event of the Generate Archery Output Button.
        /// </summary>
        private void GenerateArcheryOutputButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                // Validate Draw Weight and Bow Length Inputs
                if (double.TryParse(DrawWeightTextBox.Text, out double drawWeight) &&
                    double.TryParse(BowLengthTextBox.Text, out double bowLength))
                {
                    if (SelectedBow != null)
                    {
                        // Update SelectedBow properties with user inputs
                        SelectedBow.DrawWeightKg = drawWeight;
                        SelectedBow.LengthCm = bowLength;

                        // Recalculate requirements based on new draw weight and length
                        SelectedBow.CalculateRequirements();

                        // Update BowList without changing the bow type
                        // Since bow types now only determine materials or other non-length/draw properties
                        // No need to recreate the Bow object
                        // Ensure that the bow's LengthCm and DrawWeightKg are updated

                        // Optionally, if other properties are tied to bow type, handle them accordingly

                        // Recalculate and display properties
                        CalculateAndDisplayArcheryProperties();
                    }
                    else
                    {
                        MessageBox.Show("Please select a bow.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid draw weight and bow length.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while generating archery output: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        /// <summary>
        /// Handles the TextChanged event of DrawWeightTextBox.
        /// </summary>
        private void DrawWeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            // Optional: Implement real-time validation or updates
        }
    }
}
