// MainWindow.xaml.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GunBuilder.Models.Ballistic;

// Alias 'WeaponFrame' to refer to 'GunBuilder.Models.Frame' to resolve ambiguity
using WeaponFrame = GunBuilder.Models.Ballistic.Frame;

namespace GunBuilder
{
    public partial class MainWindow : Window
    {
        // Lists to store weapon components
        private List<Ammo> AmmoList;
        private List<ShotgunShell> ShotgunShellList;
        private List<BarrelType> BarrelTypeList;
        private List<Stock> StockList;
        private List<Attachment> AttachmentList;
        private List<WeaponFrame> FrameList; // Updated with alias

        // Selected Components
        private Ammo SelectedAmmo = null;
        private ShotgunShell SelectedShotgunShell = null;
        private BarrelType SelectedBarrelType = null;
        private Stock SelectedStock = null;
        private List<Attachment> SelectedAttachments = new List<Attachment>();

        // Additional Selected Components
        private string SelectedAction = null;
        private string SelectedMagazineStyle = null;

        private bool isInitialized = false;

        public MainWindow()
        {
            InitializeComponent();             // Initializes all UI components
            LoadWeaponComponents();           // Loads ballistic data
            LoadArcheryComponents();          // Loads archery data

            // Temporarily unsubscribe event handlers to prevent unwanted triggers during initialization
            WeaponTypeComboBox.SelectionChanged -= WeaponTypeComboBox_SelectionChanged;
            FrameComboBox.SelectionChanged -= FrameComboBox_SelectionChanged;
            ActionComboBox.SelectionChanged -= ActionComboBox_SelectionChanged;
            MagazineStyleComboBox.SelectionChanged -= MagazineStyleComboBox_SelectionChanged;
            BarrelTypeComboBox.SelectionChanged -= BarrelTypeComboBox_SelectionChanged;
            RiflingComboBox.SelectionChanged -= RiflingComboBox_SelectionChanged;
            ShotgunShellTypeComboBox.SelectionChanged -= ShotgunShellTypeComboBox_SelectionChanged;
            ShotgunShellGaugeComboBox.SelectionChanged -= ShotgunShellGaugeComboBox_SelectionChanged;
            AttachmentsListBox.SelectionChanged -= AttachmentsListBox_SelectionChanged;
            StockComboBox.SelectionChanged -= StockComboBox_SelectionChanged;
            BarrelLengthTextBox.TextChanged -= BarrelLengthTextBox_TextChanged;

            // Archery Event Handlers
            BowComboBox.SelectionChanged -= BowComboBox_SelectionChanged;
            MaterialComboBox.SelectionChanged -= MaterialComboBox_SelectionChanged;
            ArrowComboBox.SelectionChanged -= ArrowComboBox_SelectionChanged;

            InitializeDefaultSelections();    // Sets default selections which might trigger events
            UpdateAmmoVisibility();           // Adjusts ammo visibility based on default frame

            // Re-subscribe event handlers after initialization
            WeaponTypeComboBox.SelectionChanged += WeaponTypeComboBox_SelectionChanged;
            FrameComboBox.SelectionChanged += FrameComboBox_SelectionChanged;
            ActionComboBox.SelectionChanged += ActionComboBox_SelectionChanged;
            MagazineStyleComboBox.SelectionChanged += MagazineStyleComboBox_SelectionChanged;
            BarrelTypeComboBox.SelectionChanged += BarrelTypeComboBox_SelectionChanged;
            RiflingComboBox.SelectionChanged += RiflingComboBox_SelectionChanged;
            ShotgunShellTypeComboBox.SelectionChanged += ShotgunShellTypeComboBox_SelectionChanged;
            ShotgunShellGaugeComboBox.SelectionChanged += ShotgunShellGaugeComboBox_SelectionChanged;
            AttachmentsListBox.SelectionChanged += AttachmentsListBox_SelectionChanged;
            StockComboBox.SelectionChanged += StockComboBox_SelectionChanged;
            BarrelLengthTextBox.TextChanged += BarrelLengthTextBox_TextChanged;

            // Archery Event Handlers
            BowComboBox.SelectionChanged += BowComboBox_SelectionChanged;
            MaterialComboBox.SelectionChanged += MaterialComboBox_SelectionChanged;
            ArrowComboBox.SelectionChanged += ArrowComboBox_SelectionChanged;

            isInitialized = true;
        }

        /// <summary>
        /// Initializes default selections for ComboBoxes.
        /// </summary>
        private void InitializeDefaultSelections()
        {
            WeaponTypeComboBox.SelectedIndex = 0; // Default to Ballistic
            FrameComboBox.SelectedIndex = 0;
            ActionComboBox.SelectedIndex = 0;
            MagazineStyleComboBox.SelectedIndex = 0;
            BarrelTypeComboBox.SelectedIndex = 0;
            RiflingComboBox.SelectedIndex = 0;
            StockComboBox.SelectedIndex = 0;

            InitializeSelectedComponents();
        }

        /// <summary>
        /// Initializes Selected Components based on default selections.
        /// </summary>
        private void InitializeSelectedComponents()
        {
            // Initialize SelectedAmmo based on default frame
            if (FrameComboBox.SelectedItem is ComboBoxItem selectedFrame)
            {
                string frameType = selectedFrame.Content.ToString();
                SelectedAmmo = AmmoList.FirstOrDefault(a => a.Type == frameType);
            }

            // Initialize SelectedBarrelType based on default selection
            if (BarrelTypeComboBox.SelectedItem is ComboBoxItem selectedBarrel)
            {
                string barrelName = selectedBarrel.Content.ToString();
                SelectedBarrelType = BarrelTypeList.FirstOrDefault(b => b.Name == barrelName);
            }

            // Initialize other Selected components similarly if needed
        }

        /// <summary>
        /// Loads all weapon components into their respective lists.
        /// </summary>
        private void LoadWeaponComponents()
        {
            LoadAmmoData();
            LoadShotgunShellData();
            LoadBarrelTypeData();
            LoadStockData();
            LoadAttachmentData();
            LoadFrameData(); // Load frames with base prices and base damage
        }

        /// <summary>
        /// Loads ammo data into the AmmoList.
        /// </summary>
        private void LoadAmmoData()
        {
            AmmoList = new List<Ammo>
            {
                // Pistol Calibers
                // You can change all of these or add more as needed.
                //       Name                 Type      Price  Weight  Damage  Dice  Hide  AC roundsize  Note
                new Ammo("9x19mm Parabellum", "Pistol", 50, 2.0, 6, 0, 0, 0,0.336, "Standard pistol ammunition."),
                new Ammo(".45 ACP", "Pistol", 70, 1.2, 6, 0, 0, 0,0.411, "Higher stopping power."),
                new Ammo(".40 S&W", "Pistol", 65, 1.1, 6, 0, 0, 0,0.369, "Balanced performance."),
                new Ammo(".380 ACP", "Pistol", 40, 0.8, 4, 0, 0, 0,0.32, "Compact pistol ammo."),
                new Ammo(".357 Magnum", "Pistol", 90, 1.5, 6, 0, 0, 0,0.39, "High-velocity rounds."),
                new Ammo(".38 Special", "Pistol", 60, 1.0, 4, 0, 0, 0,0.375, "Reliable and accurate."),
                new Ammo(".22 LR", "Pistol", 30, 0.5, 4, 0, 0, 0,0.237, "Low recoil and cost."),
                new Ammo(".44 Magnum", "Pistol", 110, 1.8, 8, 0, 0, 0,0.444, "Maximum power for pistols."),
                new Ammo(".50 Action Express", "Pistol", 130, 2.0, 8, 0, 0, 0,0.484, "Extreme stopping power."),
                new Ammo(".500 Linebaugh", "Pistol", 150, 2.2, 10, 0, 0, 0,0.505, "Custom heavy-duty rounds."),
                new Ammo("Musket Balls", "Pistol", 20, 0.4, 4, 0, 0, 0,0.48, "Homemade ammunition."),

                // Rifle Calibers
                //       Name                 Type      Price  Weight  Damage  Dice  Hide  AC roundsize  Note
                new Ammo("5.56x45mm NATO", "Rifle", 80, 2.0, 6, 0, 0, 0,0.377, "Standard rifle ammunition."),
                new Ammo("7.62x39mm", "Rifle", 90, 2.5, 8, 0, 0, 0, 0.415, "Popular intermediate cartridge."),
                new Ammo("7.62x51mm NATO (.308 Winchester)", "Rifle", 100, 3.0, 12, 0, 0, 0,0.473, "High-precision rounds."),
                new Ammo(".223 Remington", "Rifle", 85, 2.1, 8, 0, 0, 0,0.377, "Versatile and accurate."),
                new Ammo(".30-06 Springfield", "Rifle", 120, 3.5, 12, 0, 0, 0,0.527, "Classic hunting cartridge."),
                new Ammo(".300 Winchester Magnum", "Rifle", 130, 3.8, 12, 0, 0, 0,0.557, "Long-range precision."),
                new Ammo(".30-30 Winchester", "Rifle", 95, 2.8, 10, 0, 0, 0,0.469, "Reliable for hunting."),
                new Ammo(".45-70 Government", "Rifle", 140, 4.0, 10, 0, 0, 0,0.549, "Powerful large-caliber."),
                new Ammo("6.5mm Creedmoor", "Rifle", 110, 2.9, 12, 0, 0, 0,0.446, "Popular for competition shooting."),
                new Ammo(".50 BMG", "Rifle", 200, 5.0, 8, 2, 0, 0,0.859, "Massive long-range rounds."),
                new Ammo("7.62x54mm", "Rifle", 115, 3.2, 12, 0, 0, 0,0.494, "Military standard ammunition."),
                new Ammo(".338 Magnum", "Rifle", 125, 3.5, 12, 0, 0, 0,0.567, "High-performance hunting rounds."),
                new Ammo("4.73x33mm Caseless", "Rifle", 160, 4.5, 8, 0, 0, 0,0.281, "Advanced caseless technology."),
                new Ammo("14.5x114mm", "Rifle", 300, 6.0, 8, 2, 0, 0,1.0, "Heavy anti-materiel rounds."),
                new Ammo("Musket Balls", "Rifle", 50, 1.5, 6, 0, 0, 0, 0.330, "Homemade ammunition ")
            };
        }

        /// <summary>
        /// Loads shotgun shell data into the ShotgunShellList.
        /// </summary>
        private void LoadShotgunShellData()
        {
            ShotgunShellList = new List<ShotgunShell>
            {
                //               Name        Gauge  Price  Weight  Damage  Hide  AC DamageDiceMod Note
                new ShotgunShell("Birdshot", "10", 30, 1.0, 12, 0, 0, 0,3, "Small pellets for hunting birds."),
                new ShotgunShell("Birdshot", "12", 35, 1.1, 12, 0, 0, 0,2, "Standard bird hunting rounds."),
                new ShotgunShell("Birdshot", "16", 40, 1.2, 10, 0, 0, 0,1, "Smaller pellets for tighter patterns."),
                new ShotgunShell("Birdshot", "20", 45, 1.3, 10, 0, 0, 0,1, "Very small pellets for precision."),
                new ShotgunShell("Birdshot", "28", 50, 1.4, 8, 0, 0, 0,1, "Extra small pellets for minimal damage."),
                new ShotgunShell("Birdshot", ".410", 55, 1.5, 8, 0, 0, 0,0, "Small caliber birdshot."),

                new ShotgunShell("Buckshot", "10", 60, 2.0, 12, 0, 0, 0,3, "Large pellets for big game."),
                new ShotgunShell("Buckshot", "12", 65, 2.1, 12, 0, 0, 0,2, "Standard buckshot rounds."),
                new ShotgunShell("Buckshot", "16", 70, 2.2, 10, 0, 0, 0,1, "Mid-sized pellets for balanced performance."),
                new ShotgunShell("Buckshot", "20", 75, 2.3, 10, 0, 0, 0,1, "Smaller pellets for tighter spread."),
                new ShotgunShell("Buckshot", "28", 80, 2.4, 8, 0, 0, 0,1, "Extra small buckshot for controlled damage."),
                new ShotgunShell("Buckshot", ".410", 85, 2.5, 8, 0, 0, 0,0, "Small caliber buckshot."),

                new ShotgunShell("Dragonsbreath", "10", 100, 3.0, 10, 0, 0, 0,3, "Incendiary rounds that ignite targets."),
                new ShotgunShell("Dragonsbreath", "12", 105, 3.1, 8, 0, 0, 0,2, "Burns targets for additional damage."),
                new ShotgunShell("Dragonsbreath", "16", 110, 3.2, 8, 0, 0, 0,1, "Enhanced incendiary effects."),
                new ShotgunShell("Dragonsbreath", "20", 115, 3.3, 8, 0, 0, 0,1, "Targets burn for 1d4 rounds."),
                new ShotgunShell("Dragonsbreath", "28", 120, 3.4, 8, 0, 0, 0,1, "Smaller incendiary pellets."),
                new ShotgunShell("Dragonsbreath", ".410", 125, 3.5, 6, 0, 0, 0,0, "High-efficiency incendiary rounds."),

                new ShotgunShell("Flechette", "10", 90, 2.5, 12, 0, 0, 0,3, "Armour-piercing flechettes."),
                new ShotgunShell("Flechette", "12", 95, 2.6, 12, 0, 0, 0,2, "Pierces through armor."),
                new ShotgunShell("Flechette", "16", 100, 2.7, 10, 0, 0, 0,1, "High-velocity flechettes."),
                new ShotgunShell("Flechette", "20", 105, 2.8, 8, 0, 0, 0,1, "Enhanced piercing capabilities."),
                new ShotgunShell("Flechette", "28", 110, 2.9, 8, 0, 0, 0,1, "Small flechettes with high penetration."),
                new ShotgunShell("Flechette", ".410", 115, 3.0, 8, 0, 0, 0,0, "Compact flechette rounds."),

                new ShotgunShell("Beanbag", "10", 50, 1.0, 8, 0, 0, 0,3, "Non-lethal beanbag rounds."),
                new ShotgunShell("Beanbag", "12", 55, 1.1, 8, 0, 0, 0,2, "Causes concussive impact."),
                new ShotgunShell("Beanbag", "16", 60, 1.2, 8, 0, 0, 0,1, "Minimal penetration, maximum blunt force."),
                new ShotgunShell("Beanbag", "20", 65, 1.3, 8, 0, 0, 0,1, "Non-lethal and effective."),
                new ShotgunShell("Beanbag", "28", 70, 1.4, 8, 0, 0, 0,1, "High-impact non-lethal rounds."),
                new ShotgunShell("Beanbag", ".410", 75, 1.5, 6, 0, 0, 0,0, "Small caliber non-lethal ammo."),

                new ShotgunShell("Slug", "10", 120, 3.0, 12, 0, 0, 0,3, "Heavy slugs for maximum damage."),
                new ShotgunShell("Slug", "12", 125, 3.1, 12, 0, 0, 0,2, "Standard slug rounds."),
                new ShotgunShell("Slug", "16", 130, 3.2, 12, 0, 0, 0,1, "High-penetration slugs."),
                new ShotgunShell("Slug", "20", 135, 3.3, 12, 0, 0, 0,1, "Enhanced accuracy slugs."),
                new ShotgunShell("Slug", "28", 140, 3.4, 12, 0, 0, 0,1, "Small, precise slugs."),
                new ShotgunShell("Slug", ".410", 145, 3.5, 10, 0, 0, 0,0, "Compact slugs for light rifles.")
            };
        }

        /// <summary>
        /// Loads barrel type data into the BarrelTypeList.
        /// </summary>
        private void LoadBarrelTypeData()
        {
            BarrelTypeList = new List<BarrelType>
            {
                //              Name     PriceMod  WeightMod  DamageMod  HideMod  ACMod  Note
                new BarrelType("Normal", 0, 0, 0, 0, 0, 0, "Standard barrel with no modifications."),
                new BarrelType("Heavy Barrel", 0.10, 0.02, 0, 0, 0, 0, "Increases durability and heat dissipation."),
                new BarrelType("Fluted", 0.15, 0.015, 0, 0, 0, 0, "Reduces weight while maintaining strength."),
                new BarrelType("Choked", 0.20, 0.025, 0, 0, 0, 0, "Improves shot pattern for tighter spread."),
                new BarrelType("Light", -0.05, -0.01, 0, 0, 0, 0, "Enhances maneuverability by reducing weight.")
            };
        }

        /// <summary>
        /// Loads stock data into the StockList.
        /// </summary>
        private void LoadStockData()
        {
            StockList = new List<Stock>
            {
                //              Name     PriceMod  WeightMod  DamageMod  HideMod  ACMod  Note
                new Stock("Standard", 0, 0, 0, 0, 0, 0, "Standard stock with no modifications."),
                new Stock("Wireframe", 0.05, -0.005, 0, 0, 0, 0, "Reduces weight with a wireframe design."),
                new Stock("Thumbhole", 0.075, 0.002, 0, 0, 0, 0, "Improves handling and control."),
                new Stock("Skeleton", 0.10, -0.007, 0, 0, 0, 0, "Significantly reduces weight with a skeletal structure."),
                new Stock("Collapsible", 0.125, 0.003, 0, 0, 0, 0, "Allows for compact storage and adjustable length."),
                new Stock("Pistol Grip", 0.15, 0.005, 0, 0, 0, 0, "Improves handling and control.")
            };
        }

        /// <summary>
        /// Loads attachment data into the AttachmentList.
        /// </summary>
        private void LoadAttachmentData()
        {
            AttachmentList = new List<Attachment>
            {
                new Attachment("Scope", 0.10, 0.05, 0, 0, 0, 2, "Enhances aiming accuracy."),
                new Attachment("Laser Module", 0.08, 0.03, 0, 0, 0, 1, "Improves target acquisition."),
                new Attachment("Flashlight", 0.05, 0.02, 0, 0, 0, 0, "Increases visibility in low-light conditions."),
                new Attachment("Underbarrel Grenade Launcher", 0.50, 0.20, 10, 0, 0, 0, "Adds explosive capabilities."),
                new Attachment("Bipod", 0.25, 0.07, 0, 0, 0, 0, "Stabilizes the weapon for precise shooting."),
                new Attachment("Foregrip", 0.17, 0.05, 0, 0, 0, 0, "Improves weapon handling and reduces recoil."),
                new Attachment("Suppressor", 0.30, 0.04, 0, 0, 0, 0, "Reduces noise and muzzle flash."),
                new Attachment("Muzzle Brake", 0.20, 0.03, 0, 0, 0, 0, "Reduces recoil by diverting gases."),
                new Attachment("Compensator", 0.22, 0.03, 0, 0, 0, 0, "Improves recoil control."),
                new Attachment("Bayonet", 0.18, 0.06, 5, 0, 0, 0, "Adds melee capability.")
            };
        }

        /// <summary>
        /// Loads frame data into the FrameList.
        /// </summary>
        private void LoadFrameData()
        {
            FrameList = new List<WeaponFrame>
            {
                //              Name     BaseWeightKg  BasePriceCorium
                new WeaponFrame("Pistol", 1.0, 50.0),       // BasePriceCorium = 50 Corium
                new WeaponFrame("Carbine", 2.5, 150.0),
                new WeaponFrame("Rifle", 3.5, 200.0),
                new WeaponFrame("Shotgun", 4.0, 180.0),
                new WeaponFrame("DMR", 3.2, 170.0),
                new WeaponFrame("SMG", 2.0, 130.0),
                new WeaponFrame("LMG", 5.0, 250.0),
                new WeaponFrame("Sniper", 4.5, 220.0)
            };
        }

        /// <summary>
        /// Handles the selection change of Weapon Type ComboBox.
        /// Shows or hides relevant option panels based on weapon type.
        /// </summary>
        private void WeaponTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (WeaponTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string weaponType = selectedItem.Content.ToString();
                    if (weaponType == "Ballistic")
                    {
                        BallisticOptionsPanel.Visibility = Visibility.Visible;
                        EnergyOptionsPanel.Visibility = Visibility.Collapsed;
                    }
                    else if (weaponType == "Energy")
                    {
                        BallisticOptionsPanel.Visibility = Visibility.Collapsed;
                        EnergyOptionsPanel.Visibility = Visibility.Visible;
                    }

                    // Optionally, reset selections if weapon type changes
                }
                else
                {
                    // Handle cases where SelectedItem is not a ComboBoxItem
                    BallisticOptionsPanel.Visibility = Visibility.Collapsed;
                    EnergyOptionsPanel.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or display a user-friendly message
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the selection change of Frame ComboBox.
        /// Updates ammo visibility based on selected frame.
        /// </summary>
        private void FrameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                UpdateAmmoVisibility();
                // Removed ResetWeaponProperties() to ensure output is only generated on button click
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating ammo visibility: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the selection change of Action ComboBox.
        /// </summary>
        private void ActionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (ActionComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    SelectedAction = selectedItem.Content.ToString();
                    // Implement any logic needed when the action changes
                    // For example, update UI elements or recalculate properties
                    // Currently, all logic is handled in GenerateOutputButton_Click
                }
                else
                {
                    SelectedAction = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting action: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the selection change of Magazine Style ComboBox.
        /// </summary>
        private void MagazineStyleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (MagazineStyleComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    SelectedMagazineStyle = selectedItem.Content.ToString();
                    // Implement any logic needed when the magazine style changes
                    // For example, update UI elements or recalculate properties
                    // Currently, all logic is handled in GenerateOutputButton_Click
                }
                else
                {
                    SelectedMagazineStyle = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting magazine style: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the selection change of Barrel Type ComboBox.
        /// </summary>
        private void BarrelTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (BarrelTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string barrelName = selectedItem.Content.ToString();
                    SelectedBarrelType = BarrelTypeList.FirstOrDefault(b => b.Name == barrelName);
                }
                else
                {
                    SelectedBarrelType = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting barrel type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the selection change of Rifling ComboBox.
        /// </summary>
        private void RiflingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                // No action needed here since damage is calculated on button click
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting rifling: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the selection change of Shotgun Shell Type ComboBox.
        /// </summary>
        private void ShotgunShellTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (ShotgunShellTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string shellType = selectedItem.Content.ToString();
                    // Find all shotgun shells with the selected type
                    var shellsOfType = ShotgunShellList.Where(s => s.Name == shellType).ToList();
                    if (shellsOfType.Count > 0)
                    {
                        // Populate the Gauge ComboBox based on selected shell type
                        ShotgunShellGaugeComboBox.Items.Clear();
                        foreach (var shell in shellsOfType)
                        {
                            ShotgunShellGaugeComboBox.Items.Add(new ComboBoxItem { Content = shell.Gauge });
                        }
                        ShotgunShellGaugeComboBox.SelectedIndex = -1;
                        SelectedShotgunShell = null;
                    }
                    else
                    {
                        ShotgunShellGaugeComboBox.Items.Clear();
                        ShotgunShellGaugeComboBox.SelectedIndex = -1;
                        SelectedShotgunShell = null;
                    }
                }
                else
                {
                    ShotgunShellGaugeComboBox.Items.Clear();
                    ShotgunShellGaugeComboBox.SelectedIndex = -1;
                    SelectedShotgunShell = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting shotgun shell type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the selection change of Shotgun Shell Gauge ComboBox.
        /// </summary>
        private void ShotgunShellGaugeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (ShotgunShellGaugeComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string selectedGauge = selectedItem.Content.ToString();
                    if (ShotgunShellTypeComboBox.SelectedItem is ComboBoxItem selectedTypeItem)
                    {
                        string shellType = selectedTypeItem.Content.ToString();
                        SelectedShotgunShell = ShotgunShellList.FirstOrDefault(s => s.Name == shellType && s.Gauge == selectedGauge);
                    }
                }
                else
                {
                    SelectedShotgunShell = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting shotgun shell gauge: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the selection change of Attachments ListBox.
        /// </summary>
        private void AttachmentsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                // Clear previous selections
                SelectedAttachments.Clear();

                foreach (ListBoxItem item in AttachmentsListBox.SelectedItems)
                {
                    var attachment = AttachmentList.FirstOrDefault(a => a.Name == item.Content.ToString());
                    if (attachment != null)
                    {
                        SelectedAttachments.Add(attachment);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting attachments: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the selection change of Stock ComboBox.
        /// </summary>
        private void StockComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (StockComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string stockName = selectedItem.Content.ToString();
                    SelectedStock = StockList.FirstOrDefault(s => s.Name == stockName);
                }
                else
                {
                    SelectedStock = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting stock: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of BarrelLengthTextBox.
        /// </summary>
        private void BarrelLengthTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            // No action needed here since output is generated on button click
        }

        /// <summary>
        /// Validates that only numbers are entered in Barrel Length TextBox.
        /// </summary>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Generates the weapon configuration output based on selections.
        /// </summary>
        private void GenerateOutputButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                CalculateAndDisplayWeaponProperties();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while generating output: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Calculates and displays weapon properties based on current selections.
        /// </summary>
        /// <summary>
        /// Calculates and displays weapon properties based on current selections.
        /// </summary>
        private void CalculateAndDisplayWeaponProperties()
        {
            try
            {
                // Initialize base frame
                WeaponFrame selectedFrame = null;
                if (FrameComboBox.SelectedItem is ComboBoxItem selectedFrameItem)
                {
                    string frameName = selectedFrameItem.Content.ToString();
                    selectedFrame = FrameList.FirstOrDefault(f => f.Name == frameName);
                }

                if (selectedFrame == null)
                {
                    MessageBox.Show("Please select a valid frame.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Initialize base values
                double basePriceCorium = selectedFrame.BasePriceCorium;
                double baseWeightKg = selectedFrame.BaseWeightKg;
                int baseDamage = selectedFrame.BaseDamage;

                double totalPriceModPercentage = 0.0; // Sum of percentage-based price modifiers
                double totalWeightModPercentage = 0.0; // Sum of percentage-based weight modifiers
                int totalDamageMod = baseDamage;       // Start with base damage
                int totalHideMod = 0;
                int totalACMod = 0;

                // StringBuilders for different sections
                StringBuilder weaponProperties = new StringBuilder();
                StringBuilder attachmentsSection = new StringBuilder();
                StringBuilder specialNotes = new StringBuilder();
                StringBuilder debugInfo = new StringBuilder();

                // Start compiling debug information
                debugInfo.AppendLine("=== Debug Information ===");
                debugInfo.AppendLine($"Selected Frame: {selectedFrame.Name}");
                debugInfo.AppendLine($"Base Price: {basePriceCorium} Corium");
                debugInfo.AppendLine($"Base Weight: {baseWeightKg} kg");
                debugInfo.AppendLine($"Base Damage: {baseDamage}");
                debugInfo.AppendLine("----------------------------");

                // Weapon Type
                string weaponType = "Unknown";
                if (WeaponTypeComboBox.SelectedItem is ComboBoxItem selectedWeaponType)
                {
                    weaponType = selectedWeaponType.Content.ToString();
                    debugInfo.AppendLine($"Weapon Type Selected: {weaponType}");
                }
                else
                {
                    debugInfo.AppendLine("Weapon Type not selected.");
                }

                // Action
                if (ActionComboBox.SelectedItem is ComboBoxItem selectedAction)
                {
                    string action = selectedAction.Content.ToString();
                    debugInfo.AppendLine($"Action Selected: {action}");

                    // Modify price and weight based on action (percentage-based)
                    switch (action)
                    {
                        case "Flintlock":
                            totalPriceModPercentage += 0.05; // 5%
                            totalWeightModPercentage += 0.10; // 10%
                            specialNotes.AppendLine("Action: Flintlock increases weapon weight by 10% and price by 5%.");
                            debugInfo.AppendLine("Action Modifier Applied: +5% Price, +10% Weight");
                            break;
                        case "Matchlock":
                            totalPriceModPercentage += 0.04; // 4%
                            totalWeightModPercentage += 0.08; // 8%
                            specialNotes.AppendLine("Action: Matchlock moderately increases weapon weight by 8% and price by 4%.");
                            debugInfo.AppendLine("Action Modifier Applied: +4% Price, +8% Weight");
                            break;
                        case "Pump Action":
                            totalPriceModPercentage += 0.06; // 6%
                            totalWeightModPercentage += 0.12; // 12%
                            specialNotes.AppendLine("Action: Pump Action increases weapon weight by 12% and price by 6%.");
                            debugInfo.AppendLine("Action Modifier Applied: +6% Price, +12% Weight");
                            break;
                        case "Lever Action":
                            totalPriceModPercentage += 0.055; // 5.5%
                            totalWeightModPercentage += 0.11;  // 11%
                            specialNotes.AppendLine("Action: Lever Action moderately increases weapon weight by 11% and price by 5.5%.");
                            debugInfo.AppendLine("Action Modifier Applied: +5.5% Price, +11% Weight");
                            break;
                        case "Bolt Action":
                            totalPriceModPercentage += 0.08; // 8%
                            totalWeightModPercentage += 0.15; // 15%
                            specialNotes.AppendLine("Action: Bolt Action significantly increases weapon weight by 15% and price by 8%.");
                            debugInfo.AppendLine("Action Modifier Applied: +8% Price, +15% Weight");
                            break;
                        case "Break Action":
                            totalPriceModPercentage += 0.07; // 7%
                            totalWeightModPercentage += 0.13; // 13%
                            specialNotes.AppendLine("Action: Break Action moderately increases weapon weight by 13% and price by 7%.");
                            debugInfo.AppendLine("Action Modifier Applied: +7% Price, +13% Weight");
                            break;
                        case "Single Action":
                            totalPriceModPercentage += 0.045; // 4.5%
                            totalWeightModPercentage += 0.09;  // 9%
                            specialNotes.AppendLine("Action: Single Action increases weapon weight by 9% and price by 4.5%.");
                            debugInfo.AppendLine("Action Modifier Applied: +4.5% Price, +9% Weight");
                            break;
                        case "Double Action":
                            totalPriceModPercentage += 0.05; // 5%
                            totalWeightModPercentage += 0.10; // 10%
                            specialNotes.AppendLine("Action: Double Action moderately increases weapon weight by 10% and price by 5%.");
                            debugInfo.AppendLine("Action Modifier Applied: +5% Price, +10% Weight");
                            break;
                        case "Semi-Automatic":
                            totalPriceModPercentage += 0.10; // 10%
                            totalWeightModPercentage += 0.18; // 18%
                            specialNotes.AppendLine("Action: Semi-Automatic significantly increases weapon weight by 18% and price by 10%.");
                            debugInfo.AppendLine("Action Modifier Applied: +10% Price, +18% Weight");
                            break;
                        case "Burst":
                            totalPriceModPercentage += 0.11; // 11%
                            totalWeightModPercentage += 0.20; // 20%
                            specialNotes.AppendLine("Action: Burst mode significantly increases weapon weight by 20% and price by 11%.");
                            debugInfo.AppendLine("Action Modifier Applied: +11% Price, +20% Weight");
                            break;
                        case "Automatic":
                            totalPriceModPercentage += 0.15; // 15%
                            totalWeightModPercentage += 0.25; // 25%
                            specialNotes.AppendLine("Action: Automatic mode greatly increases weapon weight by 25% and price by 15%.");
                            debugInfo.AppendLine("Action Modifier Applied: +15% Price, +25% Weight");
                            break;
                        case "Semi/Burst":
                            totalPriceModPercentage += 0.12; // 12%
                            totalWeightModPercentage += 0.22; // 22%
                            specialNotes.AppendLine("Action: Semi/Burst mode significantly increases weapon weight by 22% and price by 12%.");
                            debugInfo.AppendLine("Action Modifier Applied: +12% Price, +22% Weight");
                            break;
                        case "Semi/Full":
                            totalPriceModPercentage += 0.13; // 13%
                            totalWeightModPercentage += 0.24; // 24%
                            specialNotes.AppendLine("Action: Semi/Full mode greatly increases weapon weight by 24% and price by 13%.");
                            debugInfo.AppendLine("Action Modifier Applied: +13% Price, +24% Weight");
                            break;
                        case "Semi/Burst/Full":
                            totalPriceModPercentage += 0.16; // 16%
                            totalWeightModPercentage += 0.28; // 28%
                            specialNotes.AppendLine("Action: Semi/Burst/Full mode drastically increases weapon weight by 28% and price by 16%.");
                            debugInfo.AppendLine("Action Modifier Applied: +16% Price, +28% Weight");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    debugInfo.AppendLine("Action not selected.");
                }

                // Magazine Style
                if (MagazineStyleComboBox.SelectedItem is ComboBoxItem selectedMagazine)
                {
                    string magazineStyle = selectedMagazine.Content.ToString();
                    debugInfo.AppendLine($"Magazine Style Selected: {magazineStyle}");

                    // Modify price and weight based on magazine style (percentage-based)
                    switch (magazineStyle)
                    {
                        case "Muzzle Loader":
                            totalPriceModPercentage += 0.08; // 8%
                            totalWeightModPercentage += 0.15; // 15%
                            specialNotes.AppendLine("Magazine Style: Muzzle Loader increases weapon weight by 15% and price by 8%.");
                            debugInfo.AppendLine("Magazine Style Modifier Applied: +8% Price, +15% Weight");
                            break;
                        case "Breachloader":
                            totalPriceModPercentage += 0.07; // 7%
                            totalWeightModPercentage += 0.12; // 12%
                            specialNotes.AppendLine("Magazine Style: Breachloader moderately increases weapon weight by 12% and price by 7%.");
                            debugInfo.AppendLine("Magazine Style Modifier Applied: +7% Price, +12% Weight");
                            break;
                        case "Tube-fed":
                            totalPriceModPercentage += 0.06; // 6%
                            totalWeightModPercentage += 0.10; // 10%
                            specialNotes.AppendLine("Magazine Style: Tube-fed moderately increases weapon weight by 10% and price by 6%.");
                            debugInfo.AppendLine("Magazine Style Modifier Applied: +6% Price, +10% Weight");
                            break;
                        case "Cylinder":
                            totalPriceModPercentage += 0.09; // 9%
                            totalWeightModPercentage += 0.14; // 14%
                            specialNotes.AppendLine("Magazine Style: Cylinder significantly increases weapon weight by 14% and price by 9%.");
                            debugInfo.AppendLine("Magazine Style Modifier Applied: +9% Price, +14% Weight");
                            break;
                        case "Clip":
                            totalPriceModPercentage += 0.05; // 5%
                            totalWeightModPercentage += 0.08; // 8%
                            specialNotes.AppendLine("Magazine Style: Clip moderately increases weapon weight by 8% and price by 5%.");
                            debugInfo.AppendLine("Magazine Style Modifier Applied: +5% Price, +8% Weight");
                            break;
                        case "En Bloc Clip":
                            totalPriceModPercentage += 0.065; // 6.5%
                            totalWeightModPercentage += 0.09;  // 9%
                            specialNotes.AppendLine("Magazine Style: En Bloc Clip moderately increases weapon weight by 9% and price by 6.5%.");
                            debugInfo.AppendLine("Magazine Style Modifier Applied: +6.5% Price, +9% Weight");
                            break;
                        case "Box Magazine":
                            totalPriceModPercentage += 0.10; // 10%
                            totalWeightModPercentage += 0.16; // 16%
                            specialNotes.AppendLine("Magazine Style: Box Magazine significantly increases weapon weight by 16% and price by 10%.");
                            debugInfo.AppendLine("Magazine Style Modifier Applied: +10% Price, +16% Weight");
                            break;
                        case "Drum Magazine":
                            totalPriceModPercentage += 0.12; // 12%
                            totalWeightModPercentage += 0.20; // 20%
                            specialNotes.AppendLine("Magazine Style: Drum Magazine greatly increases weapon weight by 20% and price by 12%.");
                            debugInfo.AppendLine("Magazine Style Modifier Applied: +12% Price, +20% Weight");
                            break;
                        case "Detachable Magazine":
                            totalPriceModPercentage += 0.11; // 11%
                            totalWeightModPercentage += 0.18; // 18%
                            specialNotes.AppendLine("Magazine Style: Detachable Magazine significantly increases weapon weight by 18% and price by 11%.");
                            debugInfo.AppendLine("Magazine Style Modifier Applied: +11% Price, +18% Weight");
                            break;
                        case "Belt-fed":
                            totalPriceModPercentage += 0.14; // 14%
                            totalWeightModPercentage += 0.22; // 22%
                            specialNotes.AppendLine("Magazine Style: Belt-fed greatly increases weapon weight by 22% and price by 14%.");
                            debugInfo.AppendLine("Magazine Style Modifier Applied: +14% Price, +22% Weight");
                            break;
                        case "Integrated Drum":
                            totalPriceModPercentage += 0.16; // 16%
                            totalWeightModPercentage += 0.25; // 25%
                            specialNotes.AppendLine("Magazine Style: Integrated Drum drastically increases weapon weight by 25% and price by 16%.");
                            debugInfo.AppendLine("Magazine Style Modifier Applied: +16% Price, +25% Weight");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    debugInfo.AppendLine("Magazine Style not selected.");
                }

                // Ammo or Shotgun Shells
                if (PistolAmmoGrid != null && PistolAmmoGrid.Visibility == Visibility.Visible)
                {
                    if (SelectedAmmo != null && SelectedAmmo.Type == "Pistol")
                    {
                        totalHideMod += SelectedAmmo.HideMod;
                        totalACMod += SelectedAmmo.ACMod;
                        attachmentsSection.AppendLine($"- Ammo: {SelectedAmmo.Name} ({SelectedAmmo.Note})");
                        debugInfo.AppendLine($"Pistol Ammo Selected: {SelectedAmmo.Name}");
                    }
                    else
                    {
                        debugInfo.AppendLine("Pistol Ammo not selected.");
                    }
                }
                else if (RifleAmmoGrid != null && RifleAmmoGrid.Visibility == Visibility.Visible)
                {
                    if (SelectedAmmo != null && SelectedAmmo.Type == "Rifle")
                    {
                        totalHideMod += SelectedAmmo.HideMod;
                        totalACMod += SelectedAmmo.ACMod;
                        attachmentsSection.AppendLine($"- Ammo: {SelectedAmmo.Name} ({SelectedAmmo.Note})");
                        debugInfo.AppendLine($"Rifle Ammo Selected: {SelectedAmmo.Name}");
                    }
                    else
                    {
                        debugInfo.AppendLine("Rifle Ammo not selected.");
                    }
                }
                else if (ShotgunShellGrid != null && ShotgunShellGrid.Visibility == Visibility.Visible)
                {
                    if (SelectedShotgunShell != null)
                    {
                        //totalDamageMod += SelectedShotgunShell.DamageMod;
                        totalHideMod += SelectedShotgunShell.HideMod;
                        totalACMod += SelectedShotgunShell.ACMod;
                        attachmentsSection.AppendLine($"- Shotgun Shell: {SelectedShotgunShell.Name} Gauge {SelectedShotgunShell.Gauge} ({SelectedShotgunShell.Note})");
                        debugInfo.AppendLine($"Shotgun Shell Selected: {SelectedShotgunShell.Name} Gauge {SelectedShotgunShell.Gauge}");
                    }
                    else
                    {
                        debugInfo.AppendLine("Shotgun Shell not selected.");
                    }
                }

                // Barrel Type
                if (SelectedBarrelType != null)
                {
                    // Apply percentage-based price and weight modifiers
                    totalPriceModPercentage += SelectedBarrelType.PriceMod;
                    totalWeightModPercentage += SelectedBarrelType.WeightMod;

                    // Apply fixed modifiers
                    totalDamageMod += SelectedBarrelType.DamageMod;
                    totalHideMod += SelectedBarrelType.HideMod;
                    totalACMod += SelectedBarrelType.ACMod;

                    attachmentsSection.AppendLine($"- Barrel Type: {SelectedBarrelType.Name} ({SelectedBarrelType.Note})");
                    debugInfo.AppendLine($"Barrel Type Selected: {SelectedBarrelType.Name}");
                }

                // Rifling
                if (RiflingComboBox.SelectedItem is ComboBoxItem selectedRifling)
                {
                    string rifling = selectedRifling.Content.ToString();
                    debugInfo.AppendLine($"Rifling Selected: {rifling}");

                    if (rifling == "Rifled")
                    {
                        totalDamageMod += 2;
                        specialNotes.AppendLine("Rifling: Rifled barrels increase accuracy and damage.");
                        debugInfo.AppendLine("Rifling Modifier Applied: +2 Damage");
                    }
                    else
                    {
                        // Assuming smooth bore decreases damage by 0
                        specialNotes.AppendLine("Rifling: Smooth bore barrels decrease accuracy and damage.");
                        debugInfo.AppendLine("Rifling Modifier Applied: +0 Damage");
                    }
                }

                // Attachments
                if (SelectedAttachments != null && SelectedAttachments.Count > 0)
                {
                    foreach (var attachment in SelectedAttachments)
                    {
                        totalPriceModPercentage += attachment.PriceModPercentage;
                        totalWeightModPercentage += attachment.WeightModPercentage;
                        totalDamageMod += attachment.DamageMod;
                        totalHideMod += attachment.HideMod;
                        totalACMod += attachment.ACMod;

                        attachmentsSection.AppendLine($"- Attachment: {attachment.Name} ({attachment.Note})");
                        debugInfo.AppendLine($"Attachment Added: {attachment.Name}");
                    }
                }

                // Stock
                if (SelectedStock != null)
                {
                    // Apply percentage-based price and weight modifiers
                    totalPriceModPercentage += SelectedStock.PriceMod;
                    totalWeightModPercentage += SelectedStock.WeightMod;

                    // Apply fixed modifiers
                    totalDamageMod += SelectedStock.DamageMod;
                    totalHideMod += SelectedStock.HideMod;
                    totalACMod += SelectedStock.ACMod;

                    attachmentsSection.AppendLine($"- Stock: {SelectedStock.Name} ({SelectedStock.Note})");
                    debugInfo.AppendLine($"Stock Selected: {SelectedStock.Name}");
                }

                // Barrel Length in mm
                if (BarrelLengthTextBox != null && double.TryParse(BarrelLengthTextBox.Text, out double lengthMm))
                {
                    // Determine damage dice based on barrel length and weapon type
                    int additionalDice = 0;
                    string lengthCategory = "Unknown";

                    if (lengthMm < 200)
                    {
                        additionalDice = 0;
                        lengthCategory = "<200mm: Pistols";
                    }
                    else if (lengthMm >= 200 && lengthMm <= 550)
                    {
                        additionalDice = 1;
                        lengthCategory = "200–550mm: Short to Medium Firearms";
                    }
                    else if (lengthMm > 550 && lengthMm <= 700)
                    {
                        additionalDice = 2;
                        lengthCategory = "550–700mm: Medium to Long Firearms";
                    }
                    else if (lengthMm > 700)
                    {
                        additionalDice = 3;
                        lengthCategory = ">700mm: Very Long Firearms (e.g., Sniper Rifles)";
                    }

                    // Special case: If weapon type is Sniper/DMR/LMG and in the lowest band, subtract 1 die
                    if ((weaponType == "Sniper" || weaponType == "DMR" || weaponType == "LMG") && lengthMm < 200)
                    {
                        additionalDice -= 1;
                        lengthCategory += " (Sniper/DMR/LMG: -1 die)";
                        debugInfo.AppendLine("Special Case Applied: Sniper/DMR/LMG with barrel length <200mm");
                    }

                    // Ensure additionalDice is not negative
                    if (additionalDice < 0)
                        additionalDice = 0;

                    totalDamageMod += additionalDice;
                    specialNotes.AppendLine($"Barrel Length: {lengthMm} mm ({lengthCategory})");
                    debugInfo.AppendLine($"Barrel Length Entered: {lengthMm} mm, Category: {lengthCategory}, Damage Modifier: +{additionalDice}");
                }

                // Calculate Dice Modifier based on Ammo roundsize
                int roundsizeDiceModifier = 0;
                if (SelectedAmmo != null)
                {
                    double roundsize = SelectedAmmo.RoundSize; // Assuming roundsize is a property in Ammo class

                    // Determine dice modifier based on roundsize
                    if (roundsize >= 0.01 && roundsize <= 0.09)
                    {
                        roundsizeDiceModifier = -1;
                    }
                    else if (roundsize >= 0.1 && roundsize <= 0.199)
                    {
                        roundsizeDiceModifier = 0;
                    }
                    else if (roundsize >= 0.2 && roundsize <= 0.399)
                    {
                        roundsizeDiceModifier = 1;
                    }
                    else if (roundsize >= 0.4 && roundsize <= 0.599)
                    {
                        roundsizeDiceModifier = 2;
                    }
                    else if (roundsize >= 0.6 && roundsize <= 0.849)
                    {
                        roundsizeDiceModifier = 3;
                    }
                    else if (roundsize >= 0.85 && roundsize <= 1.0)
                    {
                        roundsizeDiceModifier = 4;
                    }
                    else if (roundsize > 1.0)
                    {
                        roundsizeDiceModifier = 5;
                    }

                    specialNotes.AppendLine($"Roundsize Dice Modifier: {roundsizeDiceModifier}");
                    debugInfo.AppendLine($"Ammo Roundsize: {roundsize}, Dice Modifier: {roundsizeDiceModifier}");
                }
                else
                {
                    debugInfo.AppendLine("Ammo not selected for roundsize dice modifier.");
                }





                int ammoDamagePerDie;

                // Check if the selected frame is a Shotgun and a Shotgun Shell is selected
                if (selectedFrame != null && selectedFrame.Name.Equals("Shotgun", StringComparison.OrdinalIgnoreCase) && SelectedShotgunShell != null)
                {
                    ammoDamagePerDie = SelectedShotgunShell.DamageMod;
                    totalDamageMod += SelectedShotgunShell.GaugeDamageMod;
                    // Halve and round up the total damage
                    totalDamageMod = (int)Math.Ceiling(totalDamageMod / 2.0);
                    debugInfo.AppendLine($"Total Damage Modifier after Halving and Rounding Up: {totalDamageMod}");
                    // Check if there's an extra dice modifier for the selected ammo
                    if (SelectedShotgunShell != null && SelectedShotgunShell.ExtraDice > 0)
                    {
                        totalDamageMod += SelectedShotgunShell.ExtraDice; // Add extra dice modifier
                        debugInfo.AppendLine($"Extra Dice Modifier for {SelectedShotgunShell.Name}: {SelectedShotgunShell.ExtraDice}");
                    }
                }
                else
                {
                    // Add the roundsizeDiceModifier to totalDamageMod
                    totalDamageMod += roundsizeDiceModifier;
                    debugInfo.AppendLine($"Total Damage Modifier after Roundsize: {totalDamageMod}");

                    // Check if there's an extra dice modifier for the selected ammo
                    if (SelectedAmmo != null && SelectedAmmo.ExtraDice > 0)
                    {
                        totalDamageMod += SelectedAmmo.ExtraDice; // Add extra dice modifier
                        debugInfo.AppendLine($"Extra Dice Modifier for {SelectedAmmo.Name}: {SelectedAmmo.ExtraDice}");
                    }

                    // Set ammo damage per die if the selected ammo has a DamageMod
                    ammoDamagePerDie = SelectedAmmo != null ? SelectedAmmo.DamageMod : 0;

                    // Halve and round up the total damage
                    totalDamageMod = (int)Math.Ceiling(totalDamageMod / 2.0);
                    debugInfo.AppendLine($"Total Damage Modifier after Halving and Rounding Up: {totalDamageMod}");
                }

                // Final calculations
                double finalPriceCorium = basePriceCorium * (1 + totalPriceModPercentage);
                double finalWeightKg = baseWeightKg * (1 + totalWeightModPercentage);
                int damageDice = totalDamageMod;
                


                // Compile Weapon Properties Section
                weaponProperties.AppendLine("**Weapon Properties:**");
                weaponProperties.AppendLine("----------------------------");
                weaponProperties.AppendLine($"**Final Price:** {finalPriceCorium:F2} Corium");
                weaponProperties.AppendLine($"**Final Weight:** {finalWeightKg:F2} kg");
                weaponProperties.AppendLine($"**Final Damage:** {damageDice}d{ammoDamagePerDie}");
                weaponProperties.AppendLine($"**Total Hide Modifier:** {totalHideMod}");
                weaponProperties.AppendLine($"**Total AC Modifier:** {totalACMod}");
                weaponProperties.AppendLine("----------------------------");

                // Compile Attachments Section
                if (attachmentsSection.Length > 0)
                {
                    attachmentsSection.Insert(0, "**Attachments:**\n");
                    attachmentsSection.AppendLine("----------------------------");
                }
                else
                {
                    attachmentsSection.AppendLine("**Attachments:**");
                    attachmentsSection.AppendLine("None selected.");
                    attachmentsSection.AppendLine("----------------------------");
                }

                // Compile Special Notes Section
                if (specialNotes.Length > 0)
                {
                    specialNotes.Insert(0, "**Special Notes:**\n");
                    specialNotes.AppendLine("----------------------------");
                }
                else
                {
                    specialNotes.AppendLine("**Special Notes:**");
                    specialNotes.AppendLine("None.");
                    specialNotes.AppendLine("----------------------------");
                }

                // Compile Debug Information Section
                debugInfo.AppendLine("============================");
                debugInfo.Insert(0, "**Debug Information:**\n");
                debugInfo.AppendLine("============================");

                // Compile Final Output
                StringBuilder finalOutput = new StringBuilder();
                finalOutput.AppendLine("**Weapon Configuration:**");
                finalOutput.AppendLine("----------------------------");
                finalOutput.Append(weaponProperties.ToString());
                finalOutput.AppendLine(attachmentsSection.ToString());
                finalOutput.AppendLine(specialNotes.ToString());
                finalOutput.AppendLine(debugInfo.ToString());

                // Update the OutputTextBox
                if (OutputTextBox != null)
                {
                    OutputTextBox.Text = finalOutput.ToString();
                }
                else
                {
                    MessageBox.Show("OutputTextBox is null.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in CalculateAndDisplayWeaponProperties: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Gets the base weight of the selected frame in kilograms.
        /// </summary>
        private double GetFrameWeight()
        {
            if (FrameComboBox.SelectedItem is ComboBoxItem selectedFrame)
            {
                string frameName = selectedFrame.Content.ToString();
                var frame = FrameList.FirstOrDefault(f => f.Name == frameName);
                if (frame != null)
                    return frame.BaseWeightKg;
            }
            return 0;
        }


        /// <summary>
        /// Handles the selection change of the Pistol Ammo ListBox.
        /// Updates the SelectedAmmo property based on user selection.
        /// </summary>
        private void PistolAmmoListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (PistolAmmoListBox.SelectedItem is ListBoxItem selectedItem)
                {
                    string ammoName = selectedItem.Content.ToString();
                    SelectedAmmo = AmmoList.FirstOrDefault(a => a.Name == ammoName && a.Type == "Pistol");
                }
                else
                {
                    SelectedAmmo = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting pistol ammo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the selection change of the Rifle Ammo ListBox.
        /// Updates the SelectedAmmo property based on user selection.
        /// </summary>
        private void RifleAmmoListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized)
                return;

            try
            {
                if (RifleAmmoListBox.SelectedItem is ListBoxItem selectedItem)
                {
                    string ammoName = selectedItem.Content.ToString();
                    SelectedAmmo = AmmoList.FirstOrDefault(a => a.Name == ammoName && a.Type == "Rifle");
                }
                else
                {
                    SelectedAmmo = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting rifle ammo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Updates the visibility of ammo options based on selected frame.
        /// </summary>
        private void UpdateAmmoVisibility()
        {
            if (FrameComboBox.SelectedItem is ComboBoxItem selectedFrame)
            {
                string frameType = selectedFrame.Content.ToString();

                // Determine ammo type based on frame
                string ammoType = frameType switch
                {
                    "Pistol" => "Pistol",
                    "Carbine" => "Rifle",
                    "Rifle" => "Rifle",
                    "Shotgun" => "Shotgun",
                    "DMR" => "Rifle",
                    "SMG" => "Pistol",
                    "LMG" => "Rifle",
                    "Sniper" => "Rifle",
                    _ => "Rifle",
                };

                // Show relevant ammo grids
                PistolAmmoGrid.Visibility = ammoType == "Pistol" ? Visibility.Visible : Visibility.Collapsed;
                RifleAmmoGrid.Visibility = ammoType == "Rifle" ? Visibility.Visible : Visibility.Collapsed;
                ShotgunShellGrid.Visibility = ammoType == "Shotgun" ? Visibility.Visible : Visibility.Collapsed;

                // Clear previous selections
                PistolAmmoListBox.UnselectAll();
                RifleAmmoListBox.UnselectAll();
                ShotgunShellTypeComboBox.SelectedIndex = -1;
                ShotgunShellGaugeComboBox.Items.Clear();
                ShotgunShellGaugeComboBox.SelectedIndex = -1;

                SelectedAmmo = null;
                SelectedShotgunShell = null;
            }
        }
    }
}
