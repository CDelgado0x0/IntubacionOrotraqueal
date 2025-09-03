using System.Collections.Generic;
using UnityEngine;

public class SmoothBlendShape : MonoBehaviour
{
    [Header("Lista de Skinned Meshes")]
    public List<SkinnedMeshRenderer> meshRenderers = new List<SkinnedMeshRenderer>();

    [Header("Configuración Blend Shape")]
    public int blendShapeIndex = 0;
    public float transitionSpeed = 2.0f; // Velocidad de cambio

    private float currentWeight = 0f;    // Peso actual de la shape key
    private float targetWeight = 0f;     // Peso deseado

    [Header("Ritmo de insuflación")]
    public float minInterval = 4.5f;  // mínimo tiempo entre insuflaciones (s)
    public float maxInterval = 6.5f;  // máximo tiempo entre insuflaciones (s)
    public float requiredTime = 30f;  // tiempo total haciendo insuflaciones correctas

    private float lastInflationTime = -10f;  // momento de la última insuflación
    private float successTimer = 0f;         // tiempo acumulado correcto

    void Update()
    {
        if (Mathf.Abs(currentWeight - targetWeight) < 0.01f) return;

        // Interpolación suave hacia el valor objetivo
        currentWeight = Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * transitionSpeed);

        foreach (var renderer in meshRenderers)
        {
            if (renderer != null)
            {
                renderer.SetBlendShapeWeight(blendShapeIndex, currentWeight);
            }
        }

    }

    public void changeShape(bool isPress)
    {
        if (isPress)
        {
            // Solo registramos la insuflación al apretar
            float currentTime = Time.time;

            if (lastInflationTime > 0)
            {
                float interval = currentTime - lastInflationTime;
                if (interval >= minInterval && interval <= maxInterval)
                {
                    successTimer += interval;
                }
                else
                {
                    successTimer = 0f;
                }
            }

            lastInflationTime = currentTime;
            targetWeight = 100f; // expandir Ambú
        }
        else
        {
            // Al soltar, solo contraemos el Ambú
            targetWeight = 0f;
        }

        if (successTimer >= requiredTime)
        {
            Debug.Log("¡Ganaste! RCP exitosa.");
        }
    }
}
