using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class InGameConsole : MonoBehaviour
{
    public Text consoleText;
    private StringBuilder logBuilder = new StringBuilder();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logBuilder.AppendLine($"[{type}] {logString}");
        consoleText.text = logBuilder.ToString();
    }
}
