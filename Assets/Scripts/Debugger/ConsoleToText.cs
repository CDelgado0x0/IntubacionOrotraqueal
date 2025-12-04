using TMPro;
using UnityEngine;
using System.Text;

public class ConsoleToText : MonoBehaviour
{
    public TextMeshProUGUI debugText;

    private StringBuilder sb = new StringBuilder();
    private const int maxChars = 8000; // límite de seguridad

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
        sb.Clear();
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Solo mostrar errores y excepciones
        if (type != LogType.Error && type != LogType.Exception)
            return;

        // Añadir mensaje
        sb.AppendLine("ERROR: " + logString);

        // Añadir stacktrace si existe
        if (!string.IsNullOrEmpty(stackTrace))
            sb.AppendLine(stackTrace);

        sb.AppendLine("-------------------------------------");

        // Limitar tamaño para evitar OutOfMemory
        if (sb.Length > maxChars)
        {
            sb.Remove(0, sb.Length - maxChars);
        }

        // Actualizar UI
        debugText.text = sb.ToString();
    }
}