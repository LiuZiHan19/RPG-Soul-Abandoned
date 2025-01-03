using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuStoreView : UIBaseView
{
    private int _curItem;
    private int _lastItem;

    private ScrollRect _scrollRect;
    private RectTransform _contentPanel;
    private RectTransform _item;
    private HorizontalLayoutGroup _HLG;
    private List<MenuStoreItemView> _storeItemList;

    protected override void ParseComponent()
    {
        base.ParseComponent();
        _scrollRect = Find("Scroll View").GetComponent<ScrollRect>();
        _contentPanel = _scrollRect.content.GetComponent<RectTransform>();
        _HLG = _contentPanel.GetComponent<HorizontalLayoutGroup>();
        _item = _contentPanel.Find("StoreItemView").GetComponent<RectTransform>();

        _storeItemList = new List<MenuStoreItemView>();

        for (int i = 0; i < _contentPanel.childCount; i++)
        {
            _storeItemList.Add(_contentPanel.GetChild(i).GetComponent<MenuStoreItemView>());
        }
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        AddEventToScrollView();
    }

    protected override void Awake()
    {
        base.Awake();
        InitToCurItem();
    }

    /// <summary>
    /// 获取当前ItemIndex
    /// </summary>
    /// <param name="position"></param>
    private void OnScrollValueChanged(Vector2 position)
    {
        _curItem = Mathf.RoundToInt(0 - _contentPanel.anchoredPosition.x / (_item.rect.width + _HLG.spacing));
        _curItem = Mathf.Clamp(_curItem, 0, _storeItemList.Count - 1);

        if (_curItem != _lastItem)
        {
            _storeItemList[_lastItem].transform.DOScale(1f, 0.25f);
            _storeItemList[_curItem].transform.DOScale(1.2f, 0.25f);
            _lastItem = _curItem;
        }
    }

    /// <summary>
    /// 定位
    /// </summary>
    /// <param name="eventData"></param>
    private void OnEndDrag(PointerEventData eventData)
    {
        _scrollRect.velocity = Vector2.zero;
        float endValue = 0 - _curItem * (_item.rect.width + _HLG.spacing);
        _contentPanel.GetComponent<RectTransform>().DOAnchorPosX(endValue, 0.25f);
    }
    
    /// <summary>
    /// 初始化定位
    /// </summary>
    private void InitToCurItem()
    {
        _curItem = 6;
        _storeItemList[_curItem].transform.DOScale(1.2f, 0.25f);
        float endValue = 0 - _curItem * (_item.rect.width + _HLG.spacing);
        _contentPanel.GetComponent<RectTransform>().DOAnchorPosX(endValue, 0.25f);
    }

    /// <summary>
    /// ScrollView添加监听事件
    /// </summary>
    private void AddEventToScrollView()
    {
        _scrollRect.onValueChanged.AddListener(OnScrollValueChanged);

        // 获取或添加 EventTrigger 组件
        EventTrigger eventTrigger = _scrollRect.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = _scrollRect.gameObject.AddComponent<EventTrigger>();
        }

        // 创建一个新的 Entry 供 OnEndDrag 使用
        EventTrigger.Entry onEndDragEntry = new EventTrigger.Entry();
        onEndDragEntry.eventID = EventTriggerType.EndDrag;
        onEndDragEntry.callback.AddListener((eventData) => { OnEndDrag((PointerEventData)eventData); });

        // 将 Entry 添加到 EventTrigger 事件列表中
        eventTrigger.triggers.Add(onEndDragEntry);
    }
}