public static class Const
{
    public const float FADE_SPEED = 0.1F;
}

public static class ResConst
{
    public const string UIRoot = "UI/UIRoot";

    public static class MenuView
    {
        public const string Menu_View = "UI/MenuView/MenuView";
    }

    public static class GameView
    {
        public const string Game_View = "UI/GameView/GameView";
        public const string InventoryView = "UI/GameView/InventoryView";
        public const string SkillTreeView = "UI/GameView/SkillTreeView";
        public const string HealthBarView = "UI/GameView/HealthBarView";
    }

    public static class ScriptableObjectAssets
    {
        public const string DefaultRes = "ScriptableObjectAssets/Material/" + AnimalSkin;
        public const string Material = "ScriptableObjectAssets/Material/";
        public const string Weapon = "ScriptableObjectAssets/Weapon/";
        public const string Armor = "ScriptableObjectAssets/Armor/";
        public const string Flask = "ScriptableObjectAssets/Flask/";
        public const string Amulet = "ScriptableObjectAssets/Amulet/";

        // Materials
        public const string Wood = "Wood";
        public const string AnimalSkin = "AnimalSkin";
        public const string Iron = "Iron";
        public const string GoldBar = "GoldBar";

        // Weapons
        public const string WoodenSword = "WoodenSword";
        public const string SteelSword = "SteelSword";
        public const string FlameSword = "FlameSword";
        public const string FrozenSword = "FrozenSword";
        public const string ThunderClaw = "ThunderClaw";

        // Armor
        public const string WoodenArmor = "WoodenArmor";
        public const string SteelArmor = "SteelArmor";
        public const string IceArmor = "IceArmor";
        public const string MagicArmor = "MagicArmor";
        public const string HealthArmor = "HealthArmor";

        // Flask
        public const string HealthPotion = "HealthPotion";
        public const string IcePotion = "IcePotion";
        public const string FlamePotion = "FlamePotion";
        public const string ThunderPotion = "ThunderPotion";

        // Amulet
        public const string GoldenBand = "GoldenBand";
        public const string Ring = "Ring";
        public const string Necklace = "Necklace";
        public const string Cord = "Cord";
        public const string FangCharm = "FangCharm";
    }

    public static class Fx
    {
        public const string IceStatusFx = "Fx/IceStatus";
        public const string IgniteStatusFx = "Fx/IgniteStatus";
        public const string ThunderStatusFx = "Fx/ThunderStatus";
    }

    public static class Skill
    {
        public const string SkillClone = "Skill/PlayerClone";
        public const string ElectricOrb = "Skill/ElectricOrb";
        public const string EnergyOrb = "Skill/EnergyOrb";
        public const string MagicOrb = "Skill/MagicOrb";
        public const string ThrowSword = "Skill/ThrowSword";
    }

    public static class Manager
    {
        public static string SkillManager = "Manager/SkillManager";
        public static string PlayerManager = "Manager/PlayerManager";
        public static string DataManager = "Manager/DataManager";
    }

    public const string InventoryItem = "Inventory/InventoryItem";
}