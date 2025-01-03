using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public Player player;

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
    }

    public void Equip(EquipmentItemData itemData)
    {
        agility.AddModifier(itemData.agility);
        intelligence.AddModifier(itemData.intelligence);
        strength.AddModifier(itemData.strength);
        vitality.AddModifier(itemData.vitality);
        damage.AddModifier(itemData.damage);
        criticalPower.AddModifier(itemData.criticalPower);
        criticalChance.AddModifier(itemData.criticalChance);
        maxHealth.AddModifier(itemData.maxHealth);
        evasion.AddModifier(itemData.evasion);
        armor.AddModifier(itemData.armor);
        magicResistance.AddModifier(itemData.magicResistance);
        igniteDamage.AddModifier(itemData.igniteDamage);
        iceDamage.AddModifier(itemData.iceDamage);
        thunderDamage.AddModifier(itemData.thunderDamage);
    }

    public void Unequip(EquipmentItemData itemData)
    {
        agility.RemoveModifier(itemData.agility);
        intelligence.RemoveModifier(itemData.intelligence);
        strength.RemoveModifier(itemData.strength);
        vitality.RemoveModifier(itemData.vitality);
        damage.RemoveModifier(itemData.damage);
        criticalPower.RemoveModifier(itemData.criticalPower);
        criticalChance.RemoveModifier(itemData.criticalChance);
        maxHealth.RemoveModifier(itemData.maxHealth);
        evasion.RemoveModifier(itemData.evasion);
        armor.RemoveModifier(itemData.armor);
        magicResistance.RemoveModifier(itemData.magicResistance);
        igniteDamage.RemoveModifier(itemData.igniteDamage);
        iceDamage.RemoveModifier(itemData.iceDamage);
        thunderDamage.RemoveModifier(itemData.thunderDamage);
    }
}