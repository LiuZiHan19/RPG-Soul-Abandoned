using UnityEngine.UI;

public class GameView : UIBaseView
{
    private Button _skillTreeBtn;
    private Button _inventoryBtn;
    private Button _homeBtn;

    private Image _healthImage;

    // Item Skill Cooldown
    private Image _potionImage;
    private Image _weaponImage;
    private Image _counterAttackImage;
    private Image _dashImage;
    private Image _throwSwordImage;
    private Image _backHoleImage;
    private Image _crystalImage;

    protected override void ParseComponent()
    {
        base.ParseComponent();
        _skillTreeBtn = transform.Find("BtnSkillTree").GetComponent<Button>();
        _inventoryBtn = transform.Find("BtnInventory").GetComponent<Button>();
        _homeBtn = transform.Find("BtnHome").GetComponent<Button>();
        _healthImage = transform.Find("HealthFill").GetComponent<Image>();
        _potionImage = transform.Find("Item/Item_Potion/ImgCooldown").GetComponent<Image>();
        _weaponImage = transform.Find("Item/Item_Weapon/ImgCooldown").GetComponent<Image>();
        _counterAttackImage = transform.Find("Skill/Skill_CounterAttack/ImgCooldown").GetComponent<Image>();
        _dashImage = transform.Find("Skill/Skill_Dash/ImgCooldown").GetComponent<Image>();
        _throwSwordImage = transform.Find("Skill/Skill_ThrowSword/ImgCooldown").GetComponent<Image>();
        _backHoleImage = transform.Find("Skill/Skill_BlackHole/ImgCooldown").GetComponent<Image>();
        _crystalImage = transform.Find("Skill/Skill_Crystal/ImgCooldown").GetComponent<Image>();
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        ListenButton(_inventoryBtn, OnInventoryBtnClick);
        ListenButton(_skillTreeBtn, OnSkillTreeBtnClick);
        ListenButton(_homeBtn, OnHomeBtnClick);
        PlayerManager.Instance.player.Stats.OnHealthChanged += RefreshHealthBar;
    }

    private void RefreshHealthBar()
    {
        _healthImage.fillAmount = PlayerManager.Instance.player.Stats.GetCurHealthPercentage();
    }

    private void OnHomeBtnClick()
    {
        Hide(() => { WindowManager.Instance.GetMenuView().Show(); });
    }

    private void OnInventoryBtnClick()
    {
        Hide(() =>
        {
            PlayerManager.Instance.player.interactable = false;
            UnityHelper.PauseTime();
            WindowManager.Instance.GetInventoryView().Show();
        });
    }

    private void OnSkillTreeBtnClick()
    {
        Hide(() => { WindowManager.Instance.GetSkillTreeView().Show(); });
    }

    protected override void Dispose()
    {
        UnListenButton(_skillTreeBtn, OnSkillTreeBtnClick);
        UnListenButton(_inventoryBtn, OnInventoryBtnClick);
        UnListenButton(_homeBtn, OnHomeBtnClick);
        PlayerManager.Instance.player.Stats.OnHealthChanged -= RefreshHealthBar;
        base.Dispose();
    }
}