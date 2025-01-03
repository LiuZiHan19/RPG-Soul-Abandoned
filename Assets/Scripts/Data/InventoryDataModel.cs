using System;
using System.Collections.Generic;

[Serializable]
public class InventoryDataModel
{
    // id : number, must be public & string key
    public Dictionary<string, int> materialDict;
    public Dictionary<string, int> equipmentDict;

    // todo 玩家装备数据

    public InventoryDataModel()
    {
        materialDict = new Dictionary<string, int>();
        equipmentDict = new Dictionary<string, int>();
    }

    public void AddItemData(ItemData itemData)
    {
        switch (itemData.itemType)
        {
            case E_ItemType.None:
                break;
            case E_ItemType.Material:
                AddMaterialItemData(itemData as MaterialItemData);
                break;
            case E_ItemType.Equipment:
                AddEquipmentItemData(itemData as EquipmentItemData);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void AddMaterialItemData(MaterialItemData itemData)
    {
        string id = itemData.id.ToString();
        if (materialDict.ContainsKey(id))
        {
            materialDict[id]++;
        }
        else
        {
            materialDict.Add(id, 1);
        }
    }

    private void AddEquipmentItemData(EquipmentItemData itemData)
    {
        string id = itemData.id.ToString();
        if (equipmentDict.ContainsKey(id))
        {
            equipmentDict[id]++;
        }
        else
        {
            equipmentDict.Add(id, 1);
        }
    }

    public void RemoveItemData(ItemData itemData)
    {
        switch (itemData.itemType)
        {
            case E_ItemType.Material:
                RemoveMaterialItemData(itemData as MaterialItemData);
                break;
            case E_ItemType.Equipment:
                RemoveEquipmentItemData(itemData as EquipmentItemData);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void RemoveMaterialItemData(MaterialItemData itemData)
    {
        string id = itemData.id.ToString();
        if (materialDict.ContainsKey(id))
        {
            materialDict[id]--;
            if (materialDict[id] <= 0)
            {
                materialDict.Remove(id);
            }
        }
    }

    private void RemoveEquipmentItemData(EquipmentItemData itemData)
    {
        string id = itemData.id.ToString();
        if (equipmentDict.ContainsKey(id))
        {
            equipmentDict[id]--;
            if (equipmentDict[id] <= 0)
            {
                equipmentDict.Remove(id);
            }
        }
    }
}