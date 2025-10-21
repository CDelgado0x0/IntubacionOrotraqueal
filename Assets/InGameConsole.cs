using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;

public class InGameConsole : MonoBehaviour
{
    [Header("UI References")]
    public GameObject consolePanel;
    public Text consoleText;
    public ScrollRect scrollRect;

    private StringBuilder logBuilder = new StringBuilder();
    private const int maxChars = 50000;
    private bool invertScrollDirection = false;

    void Awake()
    {
        // Si no se asigna panel, usar este mismo GameObject
        if (consolePanel == null)
            consolePanel = gameObject;
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
        DetectScrollDirection();
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void Update()
    {
        // 🔹 F1: mostrar / ocultar consola
        if (Input.GetKeyDown(KeyCode.F1))
        {
            consolePanel.SetActive(!consolePanel.activeSelf);
        }

        // 🔹 F2: limpiar consola
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ClearLog();
        }

        // 🔹 F3: ir arriba
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (scrollRect != null)
                scrollRect.verticalNormalizedPosition = invertScrollDirection ? 0f : 1f;
        }

        // 🔹 F4: ir abajo
        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (scrollRect != null)
                scrollRect.verticalNormalizedPosition = invertScrollDirection ? 1f : 0f;
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logBuilder.AppendLine($"[{type}] {logString}");

        if (logBuilder.Length > maxChars)
            logBuilder.Remove(0, logBuilder.Length - maxChars);

        consoleText.text = logBuilder.ToString();

        LayoutRebuilder.ForceRebuildLayoutImmediate(consoleText.rectTransform);
        Canvas.ForceUpdateCanvases();

        StartCoroutine(ScrollToBottomNextFrame());
    }

    private IEnumerator ScrollToBottomNextFrame()
    {
        yield return null;
        if (scrollRect != null)
            scrollRect.verticalNormalizedPosition = invertScrollDirection ? 1f : 0f;
    }

    public void ClearLog()
    {
        logBuilder.Clear();
        consoleText.text = "";
        if (scrollRect != null)
            scrollRect.verticalNormalizedPosition = invertScrollDirection ? 0f : 1f;
    }

    private void DetectScrollDirection()
    {
        if (scrollRect == null || scrollRect.content == null)
            return;

        invertScrollDirection = scrollRect.content.pivot.y < 0.5f;
    }
}
