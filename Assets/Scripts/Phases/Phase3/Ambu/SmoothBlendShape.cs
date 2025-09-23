using System.Collections.Generic;
using UnityEngine;

public class SmoothBlendShape : MonoBehaviour
{

    // Este script está diseñado especificamente para el control del ambú, existe un controlador de BlendShape general llamado GeneralBlendShape

    [Header("Lista de Skinned Meshes")]
    public List<SkinnedMeshRenderer> meshRenderers = new List<SkinnedMeshRenderer>();

    [Header("Configuración Blend Shape")]
    public int blendShapeIndex = 0;
    public float inflationDuration = 1f; // tiempo total de cada insuflación (s)

    [Header("Ritmo de insuflación")]
    public float minInterval = 4.5f;  // mínimo tiempo entre insuflaciones (s)
    public float maxInterval = 6.5f;  // máximo tiempo entre insuflaciones (s)
    public float requiredTime = 30f;  // tiempo total haciendo insuflaciones correctas

    private float currentWeight = 0f;    // Peso actual de la shape key

    // Variables internas
    private bool isInflating = false;
    private bool isPressing = false;
    private bool reachedPeak = false;
    private float inflationTimer = 0f;

    private bool isReturning = false;
    private float returnDuration = 0f;
    private float returnTimer = 0f;
    private float startWeight = 0f;

    private float lastInflationTime = -10f;
    private float successTimer = 0f;

    [SerializeField] private AudioSource respiracionCorrecta;
    [SerializeField] private AmbuContraints Ambu;

    private bool primerUso = false;
    private bool segundoUso = false;

    private void ComprobarPasos(GameState state)
    {
        primerUso = false;
        segundoUso = false;

        if (state == GameState.oxigenarPaciente)
        {
            primerUso = true;
        }
        else if (state == GameState.insuflarRealizandoAuscultacion)
        {
            segundoUso = true;
        }
    }

    void Start()
    {
        GameManager.onGameStateChanged += ComprobarPasos;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= ComprobarPasos;
    }

    void Update()
    {
        // Subida del blend shape mientras mantiene presionado
        if (isInflating && isPressing)
        {
            inflationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(inflationTimer / (inflationDuration / 2f));
            currentWeight = Mathf.Lerp(0f, 100f, t);
            ApplyBlendShape();

            if (t >= 1f && !reachedPeak)
            {
                reachedPeak = true;
                ValidateInflation();
            }
        }

        // Bajada después de soltar (pico alcanzado o interrumpido)
        if (isReturning)
        {
            returnTimer += Time.deltaTime;
            float t = Mathf.Clamp01(returnTimer / returnDuration);
            currentWeight = Mathf.Lerp(startWeight, 0f, t);
            ApplyBlendShape();

            if (t >= 1f)
            {
                isReturning = false;
                currentWeight = 0f;
            }
        }
    }

    private void ApplyBlendShape()
    {
        foreach (var renderer in meshRenderers)
        {
            if (renderer != null)
                renderer.SetBlendShapeWeight(blendShapeIndex, currentWeight);
        }
    }

    public void OnPress()
    {
        isPressing = true;

        if (!isInflating)
        {
            isInflating = true;
            inflationTimer = 0f;
            reachedPeak = false;
        }
    }

    public void OnRelease()
    {
        isPressing = false;

        if (currentWeight > 0f)
        {
            // Comenzar la bajada suave desde el punto actual
            isReturning = true;
            startWeight = currentWeight;

            if (reachedPeak)
                returnDuration = inflationDuration / 2f; // segunda mitad si alcanzó pico
            else
                returnDuration = (inflationDuration / 2f) * (currentWeight / 100f); // proporcional si interrumpido

            returnTimer = 0f;
        }

        // Reiniciar variables de subida
        isInflating = false;
        inflationTimer = 0f;
        reachedPeak = false;
    }

    private void ValidateInflation()
    {
        float currentTime = Time.time;

        if (lastInflationTime > 0f)
        {
            float interval = currentTime - lastInflationTime;
            if (interval >= minInterval && interval <= maxInterval)
            {
                successTimer += interval;
                respiracionCorrecta.Play();
            }
            else
            {
                successTimer = 0f;
            }
        }

        lastInflationTime = currentTime;

        if (successTimer >= requiredTime)
        {
            if (primerUso) {

                Ambu.AmbuMovement();
                GameManager.applicationController.updateGameState(GameState.extraerAmbuYCanula);
                successTimer = 0f;

            }
            else if (segundoUso)
            {
                GameManager.applicationController.updateGameState(GameState.asegurarTuboEnBoca);
            }
            
        }
    }
}
