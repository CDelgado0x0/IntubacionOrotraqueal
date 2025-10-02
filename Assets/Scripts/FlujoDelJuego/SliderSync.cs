using UnityEngine;
using UnityEngine.UI;

public class SliderSync : MonoBehaviour
{
    [SerializeField] private Slider sliderA;
    [SerializeField] private Slider sliderB;

    private bool isUpdating = false;

    void Start()
    {
        sliderA.onValueChanged.AddListener(OnSliderAChanged);
        sliderB.onValueChanged.AddListener(OnSliderBChanged);
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
