using UnityEngine;

public class SessionManager : MonoBehaviour
{
    /*
 * ┌────────────────────────────────────────────────────────────┐
 * │                SessionManager.cs – Borrar sesión           │
 * └────────────────────────────────────────────────────────────┘
 *
 * DESCRIPCIÓN:
 * Este script elimina la "sesión" del usuario (el UID guardado en PlayerPrefs)
 * automáticamente cuando se cierra la aplicación. Así se asegura que el usuario
 * siempre debe iniciar sesión de nuevo al abrir la app.
 *
 * FUNCIONALIDAD:
 * - Elimina el UID almacenado en PlayerPrefs al cerrar la app (OnApplicationQuit).
 * - Usa DontDestroyOnLoad para persistir entre escenas si se requiere.
 *
 * CÓMO USAR:
 * 1. Crea un GameObject vacío llamado "SessionManager" en la escena inicial.
 * 2. Asigna este script a ese GameObject.
 * 3. No requiere configuración adicional.
 *
 * NOTA:
 * Este script **no maneja el inicio de sesión ni guarda el UID**,
 * solo se encarga de borrarlo al salir de la app.
 */
    private void Start()
    {
        if (PlayerPrefs.HasKey("user_uid"))
            Debug.Log("Sesión activa. UID: " + PlayerPrefs.GetString("user_uid"));
        else
            Debug.Log("No hay sesión activa.");
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("user_uid");
        PlayerPrefs.Save();
        Debug.Log("Sesión eliminada al cerrar la app.");
    }
}
