using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemView : UIBaseView
{
    [SerializeField] private ItemData _itemData;
    private Image _Bg;
    private Image _icon;
    private TextMeshProUGUI _numberText;
    private Button _btn;
    private EventTrigger _eventTrigger;

    protected override void ParseComponent()
    {
        base.ParseComponent();
        _Bg = transform.Find("Bg").GetComponent<Image>();
        _icon = transform.Find("Icon").GetComponent<Image>();
        _numberText = transform.Find("Number").GetComponent<TextMeshProUGUI>();
        _btn = GetComponent<Button>();
        _eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry dragEntry = new EventTrigger.Entry();
        dragEntry.eventID = EventTriggerType.Drag;
        dragEntry.callback.AddListener(OnDrag);
        _eventTrigger.triggers.Add(dragEntry);
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        ListenButton(_btn, OnClick);
    }

    public void Refresh(ItemData data, int number)
    {
        _itemData = data;
        _icon.sprite = _itemData.icon;
        _numberText.text = number > 999 ? "999..." : number.ToString();
    }

    private void OnClick()
    {
        if (_itemData.itemType == E_ItemType.Equipment)
        {
            Inventory.Instance.Equip(_itemData as EquipmentItemData);
            WindowManager.Instance.GetInventoryView().Refresh(_itemData as EquipmentItemData);
        }
    }

    protected override void RemoveEvent()
    {
        UnListenButton(_btn, OnClick);
        base.RemoveEvent();
    }

    private void AddEventTrigger()
    {
        EventTrigger.Entry eventTrigger = new EventTrigger.Entry();
    }

    private void OnDrag(BaseEventData eventData)
    {
        RectTransform.anchoredPosition = (eventData as PointerEventData).position;
    }
}