using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class SupabaseClient : MonoBehaviour
{
    /*
 * ┌────────────────────────────────────────────────────────────┐
 * │           SupabaseClient.cs – Registro de acciones         │
 * └────────────────────────────────────────────────────────────┘
 *
 * DESCRIPCIÓN:
 * Este script permite registrar acciones del usuario en una tabla de Supabase
 * (por ejemplo: respuestas correctas o errores), enviando los datos como JSON
 * mediante una llamada HTTP POST a la REST API de Supabase.
 *
 * FUNCIONALIDAD:
 * - Envía acciones a la tabla `actions` en Supabase (nombre configurable).
 * - Cada acción se enlaza con el usuario mediante su UID (user_id).
 * - Requiere que el usuario haya iniciado sesión (es decir, que haya un UID guardado en PlayerPrefs).
 * - Incluye un campo opcional de comentarios y una bandera de acierto/error (`wasCorrect`).
 *
 * CÓMO USAR:
 * 1. Añade este script a un objeto en la escena (por ejemplo, un GameObject llamado "SupabaseClient").
 * 2. En el Inspector, configura:
 *    - `supabaseUrl`: la URL base de tu proyecto Supabase.
 *    - `supabaseKey`: la clave pública (anon key).
 *    - `tableName`: el nombre exacto de la tabla (por defecto: `actions`).
 *
 * 3. Para registrar una acción, llama desde cualquier parte del código:
 *      FindObjectOfType<SupabaseClient>().LogAction("nombre_accion", true, "Comentario opcional");
 *
 * 4. El método solo funcionará si hay sesión activa (es decir, si existe "user_uid" en PlayerPrefs).
 *
 * NOTAS:
 * - Si no hay sesión activa, se muestra una advertencia y no se envía nada.
 * - No almacena ni gestiona contraseñas ni autenticación real.
 */

    [Header("Supabase Config")]
    private string supabaseUrl = "https://gwsxywqylgoipultteqn.supabase.co";
    private string supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imd3c3h5d3F5bGdvaXB1bHR0ZXFuIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQ3OTE3NzAsImV4cCI6MjA2MDM2Nzc3MH0.c76339oYcN1eq7Gb2ZghtrJ7qPMxpwzaB6DtfjLfwU0";
    private string tableName = "actions";

    //SOLO PARA PRUEBAS
    public void saveTEST()
    {
        // Método de prueba para guardar una acción
        LogAction("test_action", true, "Acción de prueba exitosa");
    }


    // Método público para subir cualquier acción, con callback opcional para respuesta o error
    public void LogAction(string actionName, bool wasCorrect, string comment = "")
    {
        // Verificar si hay UID guardado
        if (!PlayerPrefs.HasKey("user_uid"))
        {
            Debug.LogWarning(" El usuario no ha iniciado sesión. No se puede registrar la acción.");
            return;
        }

        string userId = PlayerPrefs.GetString("user_uid");

        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning(" UID vacío. No se puede registrar la acción.");
            return;
        }

        StartCoroutine(UploadCoroutine(userId, actionName, wasCorrect, comment));
    }

    private IEnumerator UploadCoroutine(string userId, string actionName, bool wasCorrect, string comment)
    {
        string jsonBody = $"{{" +
            $"\"user_id\": \"{userId}\"," +
            $"\"action_name\": \"{actionName}\"," +
            $"\"was_correct\": {wasCorrect.ToString().ToLower()}," +
            $"\"comment\": \"{EscapeJsonString(comment)}\"" +
            $"}}";

        string url = $"{supabaseUrl}/rest/v1/{tableName}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("apikey", supabaseKey);
        request.SetRequestHeader("Authorization", $"Bearer {supabaseKey}");
        request.SetRequestHeader("Prefer", "return=representation");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(" Acción registrada correctamente: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError(" Error al registrar acción: " + request.error + " | " + request.downloadHandler.text);
        }
    }

    private string EscapeJsonString(string str)
    {
        if (string.IsNullOrEmpty(str)) return "";
        return str.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
    }
}
