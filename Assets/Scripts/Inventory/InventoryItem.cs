using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private int _stackSize;

    public int StackSize => _stackSize;

    public InventoryItem(ItemData itemData)
    {
        _itemData = itemData;
        AddStack();
    }

    public void AddStack(int quantity = 1)
    {
        _stackSize += quantity;
    }

    public void RemoveStack(int quantity = 1)
    {
        _stackSize -= quantity;
        if (_stackSize < 0) _stackSize = 0;
    }

    public void SetStackSize(int stackSize)
    {
        _stackSize = stackSize;
    }
}