using UnityEngine;

public class EntityFx : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Material _originMat;

    [SerializeField] private Vector3 _particleScale;
    private GameObject _igniteParticle;
    private GameObject _iceParticle;
    private GameObject _thunderParticle;

    private void Awake()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();
        _originMat = _sr.material;
    }

    public void ApplyIgnite()
    {
        if (!IsInvoking(nameof(IgniteFx)))
        {
            RemoveElementFx();
            ShowIgnitePraticle();
            InvokeRepeating(nameof(IgniteFx), 0, .15f);
        }
    }

    public void ApplyIce()
    {
        RemoveElementFx();
        ShowIcePraticle();
        Invoke(nameof(IceFx), 0);
    }

    public void ApplyThunder()
    {
        if (!IsInvoking(nameof(ThunderFx)))
        {
            RemoveElementFx();
            ShowThudnerPraticle();
            InvokeRepeating(nameof(ThunderFx), 0, .3f);
        }
    }

    public void RemoveElementFx()
    {
        CancelInvoke();
        _igniteParticle?.SetActive(false);
        _iceParticle?.SetActive(false);
        _thunderParticle?.SetActive(false);
        _sr.material = _originMat;
        _sr.color = Color.white;
    }

    private void IgniteFx() => _sr.color = _sr.color == Color.white ? Color.red : Color.white;
    private void IceFx() => _sr.color = Color.cyan;
    private void ThunderFx() => _sr.color = _sr.color == Color.white ? Color.yellow : Color.white;

    private void ShowIgnitePraticle()
    {
        if (_igniteParticle == null)
        {
            _igniteParticle = Instantiate(Resources.Load<GameObject>(ResConst.Fx.IgniteStatusFx));
            _igniteParticle.transform.SetParent(transform, false);
            _igniteParticle.transform.localScale = _particleScale;
        }

        _igniteParticle.SetActive(true);
    }

    private void ShowIcePraticle()
    {
        if (_iceParticle == null)
        {
            _iceParticle = Instantiate(Resources.Load<GameObject>(ResConst.Fx.IceStatusFx));
            _iceParticle.transform.SetParent(transform, false);
            _iceParticle.transform.localScale = _particleScale;
        }

        _iceParticle.SetActive(true);
    }

    private void ShowThudnerPraticle()
    {
        if (_thunderParticle == null)
        {
            _thunderParticle = Instantiate(Resources.Load<GameObject>(ResConst.Fx.ThunderStatusFx));
            _thunderParticle.transform.SetParent(transform, false);
            _thunderParticle.transform.localScale = _particleScale;
        }

        _thunderParticle.SetActive(true);
    }
}