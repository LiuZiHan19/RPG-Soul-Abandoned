public class GameManager : MonoSingleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        ResourceLoader.Instance.LoadObject(ResConst.Manager.DataManager);
        ResourceLoader.Instance.LoadObject(ResConst.Manager.PlayerManager);
        ResourceLoader.Instance.LoadObject(ResConst.Manager.SkillManager);
    }

    private void Start()
    {
        WindowManager.Instance.Initialize();
        MenuView menuView = WindowManager.Instance.GetMenuView();
        WindowManager.Instance.SetViewLayer(WindowManager.Instance.GetMenuView().transform,
            WindowManager.Instance.TopLayer);
        menuView.Show();
    }
}