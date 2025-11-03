using UnityEngine;

public class MyPlayerSettings : MonoBehaviour
{
    [Header("Asigna el OVRCameraRig")]
    public Transform rig; // OVRCameraRig principal

    [Header("Asigna el OVRPlayerController")]
    public OVRPlayerController playerController; // OVRCameraRig principal


    [ContextMenu("Recentrar")]
    public void RecenterOrigin()
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

    [ContextMenu("Cambiar tipo rotacion")]
    public void SetSnapTurn()
    {
        if (playerController.SnapRotation)
        {
            playerController.SnapRotation = false;
        }
        else
        {
            playerController.SnapRotation = true;
        }
    }
}
