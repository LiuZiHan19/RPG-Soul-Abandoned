using UnityEngine.UI;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

public class MenuView : UIBaseView
{
    private Button _newBtn;
    private Button _continueBtn;
    private Button _settingBtn;
    private Button _quitBtn;
    private Button _storeBtn;

    protected override void ParseComponent()
    {
        base.ParseComponent();
        _newBtn = Find("MenuItemList/NewGameBtn").GetComponent<Button>();
        _continueBtn = Find("MenuItemList/ContinueBtn").GetComponent<Button>();
        _settingBtn = Find("MenuItemList/SettingBtn").GetComponent<Button>();
        _quitBtn = Find("MenuItemList/QuitBtn").GetComponent<Button>();
        _storeBtn = Find("StoreBtn").GetComponent<Button>();
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        ListenButton(_newBtn, OnClickNewGameBtn);
        ListenButton(_continueBtn, OnClickContinueBtn);
        ListenButton(_settingBtn, OnClickSettingBtn);
        ListenButton(_quitBtn, OnClickQuitBtn);
        ListenButton(_storeBtn, OnClickStoreBtn);
    }

    private void OnClickNewGameBtn()
    {
        Hide();
        SceneManager.LoadScene("GameScene");
    }

    private void OnClickContinueBtn()
    {
        Hide();
    }

    private void OnClickSettingBtn()
    {
        Hide();
    }

    private void OnClickQuitBtn()
    {
        Hide();
    }

    private void OnClickStoreBtn()
    {
        Hide();
    }

    protected override void RemoveEvent()
    {
        UnListenButton(_newBtn, OnClickNewGameBtn);
        UnListenButton(_continueBtn, OnClickContinueBtn);
        UnListenButton(_settingBtn, OnClickSettingBtn);
        UnListenButton(_quitBtn, OnClickQuitBtn);
        UnListenButton(_storeBtn, OnClickStoreBtn);
        base.RemoveEvent();
    }
}