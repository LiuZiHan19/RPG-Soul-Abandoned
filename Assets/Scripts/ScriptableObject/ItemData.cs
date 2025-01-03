using UnityEngine;
using UnityEngine.Serialization;

public class ItemData : ScriptableObject
{
    public int id;
    public E_ItemType itemType;
    public string itemName;
    public Sprite icon;
}