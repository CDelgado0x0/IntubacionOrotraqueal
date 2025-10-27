using UnityEngine;
using UnityEngine.InputSystem;

public class RecenterOrigin : MonoBehaviour
{
    [Header("Asigna el OVRCameraRig")]
    public Transform rig; // OVRCameraRig principal


    [ContextMenu("Recentrar")]
    public void Recenter()
    {
        if (rig == null || Camera.main == null) return;

        // Calcula el desplazamiento de la cámara dentro del rig
        Vector3 offset = Camera.main.transform.position - rig.position;

        // Mueve el rig de forma que la cámara quede centrada donde está ahora
        rig.localPosition = Vector3.zero;
        rig.position -= new Vector3(offset.x, 0, offset.z);

        // Opcional: alinear orientación al frente del jugador
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        rig.rotation = Quaternion.LookRotation(forward, Vector3.up);

    }

}
