using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private TextMeshProUGUI ValueText;

    public void OnChangeSlider(float Value)
    {
        ValueText.SetText($"{Value * 100f:F0}%");

        float volume = Mathf.Log10(Mathf.Max(Value, 0.0001f)) * 20f;
        Mixer.SetFloat("Volume", volume);
    }
}
