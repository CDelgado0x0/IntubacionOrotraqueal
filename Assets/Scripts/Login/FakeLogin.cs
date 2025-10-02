using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using TMPro;

public class FakeLogin : MonoBehaviour
{
    [SerializeField] private GameObject logInPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private TextMeshProUGUI usernameText;
    /*
 * ┌────────────────────────────────────────────────────────────┐
 * │               FakeLogin.cs – Simulación de login           │
 * └────────────────────────────────────────────────────────────┘
 *
 * DESCRIPCIÓN:
 * Este script simula el inicio de sesión de un usuario comprobando su email y
 * contraseña mediante una Supabase Edge Function (check_user).
 * Si las credenciales son correctas, se guarda el UID en PlayerPrefs.
 *
 * FUNCIONALIDAD:
 * - Envia email y contraseña a una función de Supabase (pública).
 * - Si la función devuelve un UID, lo guarda en PlayerPrefs ("user_uid").
 * - Muestra mensajes en pantalla mediante un campo de texto TMP.
 *
 * CÓMO USAR:
 * 1. Asigna el script a un objeto de la escena (por ejemplo, un panel de login).
 * 2. Asigna en el Inspector:
 *    - El TMP_InputField del email
 *    - El TMP_InputField de la contraseña
 *    - El TMP_Text para mostrar mensajes
 * 3. Conecta el botón de login al método `OnFakeLoginButtonPressed`.
 * 4. Asegúrate de haber desplegado la función `check_user` en Supabase.
 *
 * IMPORTANTE:
 * Este login no usa Supabase Auth real, solo valida si el email y password
 * coinciden con algún usuario en la tabla de Auth mediante una función personalizada.
 */
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;

    // Texto para informar al usuario
    public TMP_Text mensajeTexto;

    // Pon aquí la URL que te dio supabase al desplegar
    private string functionUrl = "https://gwsxywqylgoipultteqn.supabase.co/functions/v1/check_user";

    public bool LoginExitoso { get; private set; } = false;

    public void OnFakeLoginButtonPressed()
    {
        string email = emailInputField.text.Trim().ToLower();
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            mensajeTexto.text = "El email y la contraseña son obligatorios.";
            Debug.LogWarning("Email and password are required");
            return;
        }

        mensajeTexto.text = "Verificando usuario...";
        StartCoroutine(CheckUserByEmailAndPassword(email, password));
    }

    private IEnumerator CheckUserByEmailAndPassword(string email, string password)
    {
        string jsonBody = $"{{\"email\": \"{Escape(email)}\", \"password\": \"{Escape(password)}\"}}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = new UnityWebRequest(functionUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        // No ponemos Authorization porque la función es pública

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;

            if (responseText.Contains("uid"))
            {
                UIDWrapper result = JsonUtility.FromJson<UIDWrapper>(responseText);
                mensajeTexto.text = "Inicio de sesión correcto.";
                Debug.Log("Login successful. UID: " + result.uid);

                PlayerPrefs.SetString("user_uid", result.uid);

                string emailSinDominio = emailInputField.text.Replace("@uji.es", "");
                PlayerPrefs.SetString("user_al", emailSinDominio);
                PlayerPrefs.Save();
                usernameText.text = emailSinDominio;

                LoginExitoso = true;

                logInPanel.SetActive(false);
                mainMenuPanel.SetActive(true);

                emailInputField.text = "";
                passwordInputField.text = "";
                mensajeTexto.text = "";
            }
            else
            {
                mensajeTexto.text = "Error: Usuario o contraseña incorrectos.";
                Debug.LogWarning("Login failed: " + responseText);
                LoginExitoso = false;
            }
        }
        else
        {
            mensajeTexto.text = $"Error en la conexión: {request.responseCode}";
            Debug.LogError("HTTP error: " + request.responseCode + " - " + request.downloadHandler.text);
            LoginExitoso = false;
        }
    }

    private string Escape(string str)
    {
        return str.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }

    [System.Serializable]
    private class UIDWrapper
    {
        public string uid;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("user_uid");
        PlayerPrefs.DeleteKey("user_al");
        PlayerPrefs.Save(); // Opcional pero recomendable
    }

    public void OnAutoFillToggleChanged(bool isOn)
    {
        if (isOn)
        {
            emailInputField.text = "al408758@uji.es";
            passwordInputField.text = "Lluc1234";
        }
    }

}
