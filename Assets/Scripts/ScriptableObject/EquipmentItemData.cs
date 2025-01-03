using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentData", menuName = "Scriptable Object/ItemData/EquipmentData")]
public class EquipmentItemData : ItemData
{
    public E_EquipmentType equipmentType;
    public int agility;
    public int intelligence;
    public int strength;
    public int vitality;
    public int damage;
    public int criticalPower;
    public int criticalChance;
    public int maxHealth;
    public int evasion;
    public int armor;
    public int magicResistance;
    public int igniteDamage;
    public int iceDamage;
    public int thunderDamage;
}
