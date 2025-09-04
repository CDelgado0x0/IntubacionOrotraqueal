using System.Collections.Generic;
using UnityEngine;

public class GeneralBlendShape : MonoBehaviour
{
    [Header("Skinned Meshes List")]

    public List<SkinnedMeshRenderer> meshRenderers = new List<SkinnedMeshRenderer>();

    [Header("Blend Shape Configuration")]

    public int blendShapeIndex = 0;
    public float transitionSpeed = 2.0f; // Velocidad de cambio

    private float currentWeight = 0f; // Peso actual de la shape key
    private float targetWeight = 0f; // Peso deseado
        
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
        
    public void changeShape()
    {
        targetWeight = (targetWeight == 0) ? 100f : 0f; 
    }
}
