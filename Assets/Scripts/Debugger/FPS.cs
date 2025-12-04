using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    TextMeshProUGUI fpsText;
    public int refreshRate = 10;
    int frameCounter;
    float totalTime;
    void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        frameCounter = 0;
        totalTime = 0f;
    }

    void Update()
    {
        if (frameCounter == refreshRate)
        {
            float averageFps = (1.0f / (totalTime / refreshRate));
            fpsText.text = "FPS: " + averageFps.ToString("F1");
            frameCounter = 0;
            totalTime = 0f;
        }
        else
        {
            totalTime += Time.deltaTime;
            frameCounter++;
        }
    }
}
