using UnityEngine.UI;

public class SkillTreeView : UIBaseView
{
    private Button _closeBtn;

    protected override void ParseComponent()
    {
        base.ParseComponent();
        _closeBtn = Find("BtnClose").GetComponent<Button>();
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        ListenButton(_closeBtn, OnCloseBtnClick);
    }

    private void OnCloseBtnClick()
    {
        Hide(() => { WindowManager.Instance.GetGameView().Show(); });
    }

    protected override void RemoveEvent()
    {
        UnListenButton(_closeBtn, OnCloseBtnClick);
        base.RemoveEvent();
    }
}