using UnityEngine;

public static class DataHelper
{
    /// <summary>
    /// 根据id加载Scriptable Object资源
    /// </summary>
    /// <param name="id">物品id</param>
    /// <returns></returns>
    public static ItemData LoadItemDataById(string id)
    {
        string path = string.Empty;
        string itemName = string.Empty;

        // Path
        if (int.Parse(id) < 101)
        {
            path = ResConst.ScriptableObjectAssets.Material;
        }
        else if (int.Parse(id) < 201)
        {
            path = ResConst.ScriptableObjectAssets.Weapon;
        }
        else if (int.Parse(id) < 301)
        {
            path = ResConst.ScriptableObjectAssets.Armor;
        }
        else if (int.Parse(id) < 401)
        {
            path = ResConst.ScriptableObjectAssets.Flask;
        }
        else if (int.Parse(id) < 501)
        {
            path = ResConst.ScriptableObjectAssets.Amulet;
        }

        // material: 1-100
        // weapon: 101-200
        // armor: 201-300
        // amulet: 301-400
        // flask: 401-500

        switch (id)
        {
            // Materials
            case "1": itemName = ResConst.ScriptableObjectAssets.Wood; break;
            case "2": itemName = ResConst.ScriptableObjectAssets.AnimalSkin; break;
            case "3": itemName = ResConst.ScriptableObjectAssets.Iron; break;
            case "4": itemName = ResConst.ScriptableObjectAssets.GoldBar; break;

            // Weapons
            case "101": itemName = ResConst.ScriptableObjectAssets.WoodenSword; break;
            case "102": itemName = ResConst.ScriptableObjectAssets.SteelSword; break;
            case "103": itemName = ResConst.ScriptableObjectAssets.FlameSword; break;
            case "104": itemName = ResConst.ScriptableObjectAssets.FrozenSword; break;
            case "105": itemName = ResConst.ScriptableObjectAssets.ThunderClaw; break;

            // Armor
            case "201": itemName = ResConst.ScriptableObjectAssets.WoodenArmor; break;
            case "202": itemName = ResConst.ScriptableObjectAssets.SteelArmor; break;
            case "203": itemName = ResConst.ScriptableObjectAssets.IceArmor; break;
            case "204": itemName = ResConst.ScriptableObjectAssets.MagicArmor; break;
            case "205": itemName = ResConst.ScriptableObjectAssets.HealthArmor; break;

            // Flask
            case "301": itemName = ResConst.ScriptableObjectAssets.HealthPotion; break;
            case "302": itemName = ResConst.ScriptableObjectAssets.IcePotion; break;
            case "303": itemName = ResConst.ScriptableObjectAssets.FlamePotion; break;
            case "304": itemName = ResConst.ScriptableObjectAssets.ThunderPotion; break;

            // Amulet
            case "401": itemName = ResConst.ScriptableObjectAssets.GoldenBand; break;
            case "402": itemName = ResConst.ScriptableObjectAssets.Ring; break;
            case "403": itemName = ResConst.ScriptableObjectAssets.Necklace; break;
            case "404": itemName = ResConst.ScriptableObjectAssets.Cord; break;
            case "405": itemName = ResConst.ScriptableObjectAssets.FangCharm; break;
            default:
                return Resources.Load<ItemData>(ResConst.ScriptableObjectAssets.DefaultRes);
        }

        return Resources.Load<ItemData>(path + itemName);
    }

    /// <summary>
    /// 随机加载一个物品数据
    /// </summary>
    /// <returns></returns>
    public static ItemData LoadItemDataRandomly()
    {
        int itemType = Random.Range(1, 6);
        int randomItem = Random.Range(1, 10);
        ItemData itemData = new ItemData();
        switch (itemType)
        {
            case 1:
                itemData = LoadItemDataById(randomItem.ToString());
                break;
            case 2:
                itemData = LoadItemDataById((randomItem + 100).ToString());
                break;
            case 3:
                itemData = LoadItemDataById((randomItem + 200).ToString());
                break;
            case 4:
                itemData = LoadItemDataById((randomItem + 300).ToString());
                break;
            case 5:
                itemData = LoadItemDataById((randomItem + 400).ToString());
                break;
        }

        if (itemData == null) itemData = Resources.Load<ItemData>("ScriptableObjectAssets/Material/AnimalSkin");
        return itemData;
    }
}