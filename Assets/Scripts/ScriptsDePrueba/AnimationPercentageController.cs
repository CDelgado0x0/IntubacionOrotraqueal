using UnityEngine;

public class AnimationPercentageController : MonoBehaviour
{
    public Animator animator;
    [Range(0f, 1f)]
    public float animationProgress = 0f;
    public string animationStateName = "Andando"; // Nombre del estado de la animación en el Animator

    private void Update()
    {
        if (animator)
        {
            animator.Play(animationStateName, 0, animationProgress);
            animator.speed = 0; // Detener el avance automático
        }
    }

    public void OnChangeSlider(float Value)
    {
        animationProgress = Value;
    }
}
