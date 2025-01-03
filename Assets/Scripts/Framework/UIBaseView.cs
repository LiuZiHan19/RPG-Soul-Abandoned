using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class UIBaseView : MonoBehaviour
{
    public GameObject DisplayObject { get; private set; }

    protected RectTransform RectTransform;

    private CanvasGroup _canvasGroup;
    private UnityAction _onHideCompleted;
    private bool _isHide;
    private bool _isShow;
    private bool _isInProgress;

    protected virtual void Awake()
    {
        ParseComponent();
        AddEvent();
        DisplayObject = gameObject;
    }

    protected virtual void Update()
    {
        if (_isHide)
        {
            _canvasGroup.alpha -= Const.FADE_SPEED;
            if (_canvasGroup.alpha <= 0)
            {
                _canvasGroup.alpha = 0;
                gameObject.SetActive(false);
                _isHide = false;
                _isInProgress = false;
                _onHideCompleted?.Invoke();
            }
        }

        if (_isShow)
        {
            _canvasGroup.alpha += Const.FADE_SPEED;
            if (_canvasGroup.alpha >= 1)
            {
                _canvasGroup.alpha = 1;
                _isInProgress = false;
                _isShow = false;
            }
        }
    }

    protected Transform Find(string path) => transform.Find(path);

    #region Behaviour

    public virtual void Show()
    {
        if (_isInProgress) return;
        _isInProgress = true;
        gameObject.SetActive(true);
        _isShow = true;
    }

    public virtual void Refresh(params object[] parameters)
    {
    }

    public virtual void Hide(UnityAction completed = null)
    {
        if (_isInProgress) return;
        _isInProgress = true;
        _isHide = true;
        _onHideCompleted = completed;
    }

    #endregion

    #region Listen

    protected void ListenButton(Button btn, UnityAction action)
    {
        if (btn == null)
            return;
        btn.onClick.AddListener(action);
    }

    protected void UnListenButton(Button btn, UnityAction action)
    {
        if (btn == null)
            return;
        btn.onClick.RemoveListener(action);
    }

    #endregion

    #region Life Cycle

    protected virtual void ParseComponent()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        RectTransform = GetComponent<RectTransform>();
    }

    protected virtual void AddEvent()
    {
    }

    protected virtual void RemoveEvent()
    {
    }

    protected virtual void Dispose() => RemoveEvent();

    protected virtual void OnDestroy() => Dispose();

    #endregion
}