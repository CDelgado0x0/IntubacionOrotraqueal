using UnityEngine;

public class SmoothBlendShape : MonoBehaviour
{
    public SkinnedMeshRenderer AmbuRenderer;
    public SkinnedMeshRenderer BolsaRenderer;
    public int blendShapeIndex = 0;

    public float transitionSpeed = 2.0f; // Velocidad de cambio
    private float currentWeight = 0f;    // Peso actual de la shape key
    private float targetWeight = 0f;     // Peso deseado

    void Update()
    {
        // Activar (por ejemplo, presionando espacio)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            changeAmbuShape();
        }

        // Interpolación suave hacia el valor objetivo
        currentWeight = Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * transitionSpeed);

        // Aplicar el valor interpolado a la blend shape
        AmbuRenderer.SetBlendShapeWeight(blendShapeIndex, currentWeight);
        BolsaRenderer.SetBlendShapeWeight(blendShapeIndex, currentWeight);

    }

    public void changeAmbuShape()
    {
        targetWeight = (targetWeight == 0) ? 100f : 0f;
    }
}
