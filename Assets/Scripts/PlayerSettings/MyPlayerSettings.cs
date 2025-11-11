using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyPlayerSettings : MonoBehaviour
{
    [Header("Asigna el OVRCameraRig")]
    [SerializeField] private Transform rig;

    [Header("Asigna el OVRPlayerController")]
    [SerializeField] private OVRPlayerController playerController;

    [Header("Asigna el slider y texto de velocidad de movimiento")]
    [SerializeField] private Slider movementSpeedSlider;
    [SerializeField] private TextMeshProUGUI movementSliderText;
    private float percentageMovement;

    [Header("Asigna el slider y texto de velocidad de rotacion")]
    [SerializeField] private Slider rotationSpeedSlider;
    [SerializeField] private TextMeshProUGUI rotationSliderText;
    private float percentageRotation;

    void Start()
    {
        //Valores iniciales de movimiento
        movementSpeedSlider.value = playerController.Acceleration;
        percentageMovement = (playerController.Acceleration - movementSpeedSlider.minValue) / (movementSpeedSlider.maxValue - movementSpeedSlider.minValue) * 100f;
        movementSliderText.SetText($"{percentageMovement:0}%");

        //Valores iniciales de rotacion
        rotationSpeedSlider.value = playerController.RotationAmount;
        percentageRotation = (playerController.RotationAmount - rotationSpeedSlider.minValue) / (rotationSpeedSlider.maxValue - rotationSpeedSlider.minValue) * 100f;
        rotationSliderText.SetText($"{percentageRotation:0}%");
    }

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
    public void SetSnapTurn(bool snapRotation)
    {
        playerController.SnapRotation = snapRotation;
    }

    public void OnChangeMovementSpeedSlider(float Value)
    {
        percentageMovement = (Value - movementSpeedSlider.minValue) / (movementSpeedSlider.maxValue - movementSpeedSlider.minValue) * 100f;

        movementSliderText.SetText($"{percentageMovement:0}%");
        playerController.Acceleration = Value;
    }

    public void OnChangeRotationSpeedSlider(float Value)
    {
        percentageRotation = (Value - rotationSpeedSlider.minValue) / (rotationSpeedSlider.maxValue - rotationSpeedSlider.minValue) * 100f;

        rotationSliderText.SetText($"{percentageRotation:0}%");
        playerController.RotationAmount = Value;
    }
}
