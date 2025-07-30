using UnityEngine;

public class SyringeController : MonoBehaviour
{

    private Vector3 minPosition;
    private Vector3 maxPosition;
    [SerializeField] private float rangoMovimiento;

    [Range(0f, 100f)]
    public float sliderValue = 0f; // Resultado normalizado

    [SerializeField] private SkinnedMeshRenderer TubeRenderer;

    [HideInInspector] public bool isConected = false;

    void Start()
    {
        minPosition = transform.localPosition;
        maxPosition = transform.localPosition - new Vector3(rangoMovimiento, 0f, 0f);
    }

    public void OnControllerSelected() //Esto se llama desde el pointable unity event wrapper cuando se selecciona el objeto controlador
    {
        if (!isConected) return;
        sliderValue = GetNormalizedPosition(transform.localPosition) * 100f;
        TubeRenderer.SetBlendShapeWeight(0, sliderValue);

    }

    float GetNormalizedPosition(Vector3 currentPosition)
    {
        Vector3 direction = maxPosition - minPosition;
        Vector3 relativePosition = currentPosition - minPosition;
        float projected = Vector3.Dot(relativePosition, direction.normalized);
        float totalDistance = direction.magnitude;
        return Mathf.Clamp01(projected / totalDistance);
    }
}
