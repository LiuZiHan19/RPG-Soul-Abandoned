using UnityEngine;

public class WindowManager : Singleton<WindowManager>
{
    public Transform Canvas { get; private set; }
    public Transform BottomLayer { get; private set; }
    public Transform MiddleLayer { get; private set; }
    public Transform TopLayer { get; private set; }

    private MenuView _menuView;
    private MenuStoreView _menuStoreView;
    private GameView _gameView;
    private InventoryView _inventoryView;
    private SkillTreeView _skillTreeView;

    public void Initialize()
    {
        if (Canvas == null)
        {
            Canvas = GameObject.Instantiate(Resources.Load<GameObject>(ResConst.UIRoot)).transform;
            GameObject.DontDestroyOnLoad(Canvas.gameObject);
            TopLayer = Canvas.Find("Top").transform;
            MiddleLayer = Canvas.Find("Middle").transform;
            BottomLayer = Canvas.Find("Bottom").transform;
        }
    }

    #region Get View (Generic Method)

    private T GetView<T>(ref T viewField, string path) where T : Component
    {
        if (viewField == null)
        {
            var viewObject = GameObject.Instantiate(Resources.Load<GameObject>(path));
            viewObject.transform.SetParent(Canvas, false);
            viewObject.SetActive(false);
            viewField = viewObject.GetComponent<T>();
        }

        return viewField;
    }

    public MenuView GetMenuView() => GetView(ref _menuView, ResConst.MenuView.Menu_View);
    public GameView GetGameView() => GetView(ref _gameView, ResConst.GameView.Game_View);
    public InventoryView GetInventoryView() => GetView(ref _inventoryView, ResConst.GameView.InventoryView);
    public SkillTreeView GetSkillTreeView() => GetView(ref _skillTreeView, ResConst.GameView.SkillTreeView);

    #endregion

    public void SetViewLayer(Component view, Transform layer)
    {
        if (view == null || layer == null)
        {
            Debug.LogError("View or layer cannot be null.");
            return;
        }

        view.transform.SetParent(layer, false);
    }
}