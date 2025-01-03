using UnityEngine;
using UnityEngine.UI;

public class LoadingView : UIBaseView
{
    private float currentTime;
    private float _progress;
    private Animator _anim;
    private Slider _slider;

    protected override void Awake()
    {
        base.Awake();
        _anim = transform.Find("Animator").GetComponent<Animator>();
        _slider = transform.Find("Slider").GetComponent<Slider>();
    }

    private void Start()
    {
        HideSlider();
    }

    public void UpdateSlider(float progress) => _slider.value = progress;
    public void FadeIn() => _anim.SetTrigger("FadeIn");
    public void FadeOut() => _anim.SetTrigger("FadeOut");
    public float GetSliderValue() => _slider.value;
    public void ShowSlider() => _slider.gameObject.SetActive(true);
    public void HideSlider() => _slider.gameObject.SetActive(false);
}