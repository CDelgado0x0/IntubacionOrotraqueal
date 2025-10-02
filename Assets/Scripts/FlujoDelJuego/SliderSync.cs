using UnityEngine;
using UnityEngine.UI;

public class SliderSync : MonoBehaviour
{
    [SerializeField] private Slider sliderA;
    [SerializeField] private Slider sliderB;
    [SerializeField] private string sliderKey;

    private bool isUpdating = false;

    void Start()
    {
        if (PlayerPrefs.HasKey(sliderKey))
        {
            sliderA.value = PlayerPrefs.GetFloat(sliderKey);
            sliderB.value = PlayerPrefs.GetFloat(sliderKey);
        }
        sliderA.onValueChanged.AddListener(OnSliderAChanged);
        sliderB.onValueChanged.AddListener(OnSliderBChanged);
    }

    public void SaveSliderValue()
    {
        PlayerPrefs.SetFloat(sliderKey, sliderA.value);
        PlayerPrefs.Save();
    }

    private void OnSliderAChanged(float value)
    {
        if (isUpdating) return;
        isUpdating = true;
        sliderB.value = value;
        isUpdating = false;
    }

    private void OnSliderBChanged(float value)
    {
        if (isUpdating) return;
        isUpdating = true;
        sliderA.value = value;
        isUpdating = false;
    }
}
