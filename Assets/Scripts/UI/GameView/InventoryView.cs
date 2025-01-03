using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : UIBaseView
{
    public ScrollRect EquipmentScrollRect { get; private set; }
    public ScrollRect MaterialScrollRect { get; private set; }

    private List<GameObject> _materialItemSlots;
    private List<GameObject> _equipmentItemSlots;
    private List<GameObject> _activeEquipmentItemSlots;

    private Button _closeBtn;
    private Button _materialBtn;
    private Button _equipmentBtn;

    // Equipment slots
    private Image _weaponImage;
    private Image _flaskImage;
    private Image _amuletImage;
    private Image _armorImage;
    private Button _weaponBtn;
    private Button _flaskBtn;
    private Button _amuletBtn;
    private Button _armorBtn;

    // Character stats
    private TextMeshProUGUI _agilityText;
    private TextMeshProUGUI _intelligenceText;
    private TextMeshProUGUI _strengthText;
    private TextMeshProUGUI _vitaliyText;
    private TextMeshProUGUI _damageText;
    private TextMeshProUGUI _criticalPowerText;
    private TextMeshProUGUI _criticalChanceText;
    private TextMeshProUGUI _maxHealthText;
    private TextMeshProUGUI _evasionText;
    private TextMeshProUGUI _armorText;
    private TextMeshProUGUI _magicResistanceText;
    private TextMeshProUGUI _igniteDamageText;
    private TextMeshProUGUI _iceDamageText;
    private TextMeshProUGUI _thunderDamageText;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameters">[0]装备槽数据</param>
    public override void Refresh(params object[] parameters)
    {
        base.Refresh(parameters);
        var stats = PlayerManager.Instance.player.Stats;

        _agilityText.text = "Agility: " + stats.agility.GetValue();
        _intelligenceText.text = "Intelligence: " + stats.intelligence.GetValue();
        _strengthText.text = "Strength: " + stats.strength.GetValue();
        _vitaliyText.text = "Vitality: " + stats.vitality.GetValue();

        _damageText.text = "Damage: " + stats.damage.GetValue();
        _criticalPowerText.text = "Critical Power: " + stats.criticalPower.GetValue();
        _criticalChanceText.text = "Critical Chance: " + stats.criticalChance.GetValue();

        _maxHealthText.text = "Max Health: " + stats.maxHealth.GetValue();
        _evasionText.text = "Evasion: " + stats.evasion.GetValue();
        _armorText.text = "Armor: " + stats.armor.GetValue();
        _magicResistanceText.text = "Magic Resistance: " + stats.magicResistance.GetValue();

        _igniteDamageText.text = "Ignite Damage: " + stats.igniteDamage.GetValue();
        _iceDamageText.text = "Ice Damage: " + stats.iceDamage.GetValue();
        _thunderDamageText.text = "Thunder Damage: " + stats.thunderDamage.GetValue();

        RefreshItemSlot(Inventory.Instance.EquipmentItemDict, Inventory.Instance.MaterialItemDict);

        if (parameters.Length > 0 && parameters[0] != null)
        {
            RefreshEquipmentSlot(parameters[0] as EquipmentItemData);
        }

        void RefreshItemSlot(Dictionary<EquipmentItemData, InventoryEquipmentItem> equipmentDict,
            Dictionary<MaterialItemData, InventoryMaterialItem> materialDict)
        {
            foreach (var item in _materialItemSlots) Destroy(item);
            foreach (var item in _equipmentItemSlots) Destroy(item);

            foreach (var kv in materialDict)
            {
                var itemData = DataHelper.LoadItemDataById(kv.Key.id.ToString());

                GameObject itemSlot =
                    Instantiate(Resources.Load<GameObject>("View/Game/Inventory/InventoryItemView"));

                itemSlot.transform.SetParent(WindowManager.Instance.GetInventoryView().MaterialScrollRect.content,
                    false);
                var inventoryItemView = itemSlot.GetComponent<InventoryItemView>();
                inventoryItemView.Refresh(itemData, kv.Value.StackSize);

                _materialItemSlots.Add(itemSlot);
            }

            foreach (var kv in equipmentDict)
            {
                var itemData = DataHelper.LoadItemDataById(kv.Key.id.ToString());
                GameObject itemSlot =
                    Instantiate(Resources.Load<GameObject>("View/Game/Inventory/InventoryItemView"));
                itemSlot.transform.SetParent(WindowManager.Instance.GetInventoryView().EquipmentScrollRect.content,
                    false);
                var inventoryItemView = itemSlot.GetComponent<InventoryItemView>();
                inventoryItemView.Refresh(itemData, kv.Value.StackSize);
                _equipmentItemSlots.Add(itemSlot);
            }
        }

        void RefreshEquipmentSlot(EquipmentItemData itemData)
        {
            switch (itemData.equipmentType)
            {
                case E_EquipmentType.Weapon:
                    _weaponImage.sprite = itemData.icon;
                    break;
                case E_EquipmentType.Flask:
                    _flaskImage.sprite = itemData.icon;
                    break;
                case E_EquipmentType.Armor:
                    _armorImage.sprite = itemData.icon;
                    break;
                case E_EquipmentType.Amulet:
                    _amuletImage.sprite = itemData.icon;
                    break;
            }
        }
    }

    public override void Show()
    {
        OnEquipmentBtnClick();
        Refresh();
        base.Show();
    }

    private void OnWeaponBtnClick()
    {
    }

    private void OnArmorBtnClick()
    {
    }

    private void OnFlaskBtnClick()
    {
    }

    private void OnAmuletBtnClick()
    {
    }

    private void OnMaterialBtnClick()
    {
        MaterialScrollRect.gameObject.SetActive(true);
        EquipmentScrollRect.gameObject.SetActive(false);
    }

    private void OnEquipmentBtnClick()
    {
        MaterialScrollRect.gameObject.SetActive(false);
        EquipmentScrollRect.gameObject.SetActive(true);
    }

    private void OnCloseBtnClick()
    {
        Hide(() =>
        {
            PlayerManager.Instance.player.interactable = true;
            UnityHelper.ResumeTime();
            WindowManager.Instance.GetGameView().Show();
        });
    }

    #region Lifecycle

    protected override void ParseComponent()
    {
        base.ParseComponent();
        _materialItemSlots = new List<GameObject>();
        _activeEquipmentItemSlots = new List<GameObject>();
        _equipmentItemSlots = new List<GameObject>();
        EquipmentScrollRect = Find("EquipmentView").GetComponent<ScrollRect>();
        MaterialScrollRect = Find("MaterialView").GetComponent<ScrollRect>();
        _closeBtn = Find("CloseBtn").GetComponent<Button>();
        _materialBtn = Find("MaterialBtn").GetComponent<Button>();
        _equipmentBtn = Find("EquipmentBtn").GetComponent<Button>();

        _weaponImage = Find("CharacterPanel/Equipment/Weapon/Pic").GetComponent<Image>();
        _flaskImage = Find("CharacterPanel/Equipment/Flask/Pic").GetComponent<Image>();
        _amuletImage = Find("CharacterPanel/Equipment/Amulet/Pic").GetComponent<Image>();
        _armorImage = Find("CharacterPanel/Equipment/Armor/Pic").GetComponent<Image>();
        _weaponBtn = Find("CharacterPanel/Equipment/Weapon").GetComponent<Button>();
        _flaskBtn = Find("CharacterPanel/Equipment/Flask").GetComponent<Button>();
        _amuletBtn = Find("CharacterPanel/Equipment/Amulet").GetComponent<Button>();
        _armorBtn = Find("CharacterPanel/Equipment/Armor").GetComponent<Button>();

        _agilityText = Find("CharacterPanel/Stats/Viewport/Content/Agility/Value").GetComponent<TextMeshProUGUI>();
        _intelligenceText = Find("CharacterPanel/Stats/Viewport/Content/Intelligence/Value")
            .GetComponent<TextMeshProUGUI>();
        _strengthText = Find("CharacterPanel/Stats/Viewport/Content/Strength/Value").GetComponent<TextMeshProUGUI>();
        _vitaliyText = Find("CharacterPanel/Stats/Viewport/Content/Vitality/Value").GetComponent<TextMeshProUGUI>();
        _damageText = Find("CharacterPanel/Stats/Viewport/Content/Damage/Value").GetComponent<TextMeshProUGUI>();
        _criticalPowerText = Find("CharacterPanel/Stats/Viewport/Content/CriticalPower/Value")
            .GetComponent<TextMeshProUGUI>();
        _criticalChanceText = Find("CharacterPanel/Stats/Viewport/Content/CriticalChance/Value")
            .GetComponent<TextMeshProUGUI>();
        _maxHealthText = Find("CharacterPanel/Stats/Viewport/Content/MaxHealth/Value").GetComponent<TextMeshProUGUI>();
        _evasionText = Find("CharacterPanel/Stats/Viewport/Content/Evasion/Value").GetComponent<TextMeshProUGUI>();
        _armorText = Find("CharacterPanel/Stats/Viewport/Content/Armor/Value").GetComponent<TextMeshProUGUI>();
        _magicResistanceText = Find("CharacterPanel/Stats/Viewport/Content/MagicResistence/Value")
            .GetComponent<TextMeshProUGUI>();
        _igniteDamageText = Find("CharacterPanel/Stats/Viewport/Content/IgniteDamage/Value")
            .GetComponent<TextMeshProUGUI>();
        _iceDamageText = Find("CharacterPanel/Stats/Viewport/Content/IceDamage/Value").GetComponent<TextMeshProUGUI>();
        _thunderDamageText = Find("CharacterPanel/Stats/Viewport/Content/ThunderDamage/Value")
            .GetComponent<TextMeshProUGUI>();
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        ListenButton(_closeBtn, OnCloseBtnClick);
        ListenButton(_materialBtn, OnMaterialBtnClick);
        ListenButton(_equipmentBtn, OnEquipmentBtnClick);
        ListenButton(_weaponBtn, OnWeaponBtnClick);
        ListenButton(_armorBtn, OnArmorBtnClick);
        ListenButton(_flaskBtn, OnFlaskBtnClick);
        ListenButton(_amuletBtn, OnAmuletBtnClick);
    }

    protected override void Dispose()
    {
        UnListenButton(_materialBtn, OnMaterialBtnClick);
        UnListenButton(_equipmentBtn, OnEquipmentBtnClick);
        UnListenButton(_closeBtn, OnCloseBtnClick);
        UnListenButton(_weaponBtn, OnWeaponBtnClick);
        UnListenButton(_armorBtn, OnArmorBtnClick);
        UnListenButton(_flaskBtn, OnFlaskBtnClick);
        UnListenButton(_amuletBtn, OnAmuletBtnClick);
        base.Dispose();
    }

    #endregion
}