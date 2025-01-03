using UnityEngine.UI;

public class HealthBarView : UIBaseView
{
    private CharacterStats _stats;
    private Image _fillImage;
    private Entity _entity;

    public void Initialize()
    {
        _entity = GetComponentInParent<Entity>();
        _stats = GetComponentInParent<CharacterStats>();
        _fillImage = transform.Find("BG/Fill").GetComponent<Image>();
        _entity.OnFlipped += OnParentFlipped;
        _stats.OnHealthChanged += Refresh;
    }

    private void Refresh()
    {
        float percentage = _stats.GetCurHealthPercentage();
        _fillImage.fillAmount = percentage;
    }

    private void OnParentFlipped()
    {
        transform.Rotate(0, 180, 0);
    }

    protected override void Dispose()
    {
        _entity.OnFlipped -= OnParentFlipped;
        _stats.OnHealthChanged -= Refresh;
        base.Dispose();
    }
}