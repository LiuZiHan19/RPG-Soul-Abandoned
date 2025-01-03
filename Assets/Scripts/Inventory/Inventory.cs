using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
    // This list is used for display in the inspector, the dictionary is used for storage, and the slot is used to refresh the inventory.

    [SerializeField] private List<InventoryMaterialItem> _materialItemList;
    private Dictionary<MaterialItemData, InventoryMaterialItem> _materialItemDict;
    public Dictionary<MaterialItemData, InventoryMaterialItem> MaterialItemDict => _materialItemDict;

    [SerializeField] private List<InventoryEquipmentItem> _equipmentItemList;
    private Dictionary<EquipmentItemData, InventoryEquipmentItem> _equipmentItemDict;
    public Dictionary<EquipmentItemData, InventoryEquipmentItem> EquipmentItemDict => _equipmentItemDict;

    [SerializeField] private List<EquipmentItemData> _activeEquipmentItemList;

    private Dictionary<E_EquipmentType, EquipmentItemData> _activeEquipmentDict;
    public Dictionary<E_EquipmentType, EquipmentItemData> ActiveEquipmentItemDict => _activeEquipmentDict;

    protected override void Awake()
    {
        base.Awake();
        _materialItemList = new List<InventoryMaterialItem>();
        _materialItemDict = new Dictionary<MaterialItemData, InventoryMaterialItem>();
        _equipmentItemList = new List<InventoryEquipmentItem>();
        _equipmentItemDict = new Dictionary<EquipmentItemData, InventoryEquipmentItem>();
        _activeEquipmentItemList = new List<EquipmentItemData>();
        _activeEquipmentDict = new Dictionary<E_EquipmentType, EquipmentItemData>();
    }

    public void Initialize()
    {
        var materialData = DataManager.Instance.InventoryData.materialDict;
        if (materialData != null && materialData.Count > 0)
        {
            foreach (var kv in materialData)
            {
                var itemData = DataHelper.LoadItemDataById(kv.Key);
                InventoryMaterialItem item = new InventoryMaterialItem(itemData);
                item.SetStackSize(kv.Value);
                _materialItemList.Add(item);
                _materialItemDict.Add(itemData as MaterialItemData, item);
            }
        }

        var equipmentData = DataManager.Instance.InventoryData.equipmentDict;
        if (equipmentData != null && equipmentData.Count > 0)
        {
            foreach (var kv in equipmentData)
            {
                var itemData = DataHelper.LoadItemDataById(kv.Key);
                InventoryEquipmentItem item = new InventoryEquipmentItem(itemData);
                item.SetStackSize(kv.Value);
                _equipmentItemList.Add(item);
                _equipmentItemDict.Add(itemData as EquipmentItemData, item);
            }
        }
    }

    public void Equip(EquipmentItemData itemData)
    {
        if (itemData == null)
        {
           Logger.LogWarning("Class: Inventory. Method: Equip. ItemData is null.");
        }

        EquipmentItemData removeItem = new EquipmentItemData();

        // 是否有相同类型的
        foreach (var item in _activeEquipmentItemList)
        {
            if (itemData.equipmentType == item.equipmentType)
            {
                // 用装备槽的数据移除玩家属性
                PlayerManager.Instance.player.Stats.Unequip(item);
                removeItem = item;
            }
        }

        _equipmentItemDict[itemData].RemoveStack();
        _activeEquipmentItemList.Remove(removeItem);
        _activeEquipmentItemList.Add(itemData);
        PlayerManager.Instance.player.Stats.Equip(itemData);
    }

    public void AddItem(ItemData itemData)
    {
        switch (itemData.itemType)
        {
            case E_ItemType.Equipment:
                AddEquipmentItem(itemData as EquipmentItemData);
                break;
            case E_ItemType.Material:
                AddMaterialItem(itemData as MaterialItemData);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(itemData));
        }
    }

    private void AddMaterialItem(MaterialItemData item)
    {
        if (_materialItemDict.TryGetValue(item, out InventoryMaterialItem value))
        {
            value.AddStack();
        }
        else
        {
            _materialItemDict.Add(item, new InventoryMaterialItem(item));
        }
    }

    private void AddEquipmentItem(EquipmentItemData item)
    {
        if (_equipmentItemDict.TryGetValue(item, out InventoryEquipmentItem value))
        {
            value.AddStack();
        }
        else
        {
            _equipmentItemDict.Add(item, new InventoryEquipmentItem(item));
        }
    }
}