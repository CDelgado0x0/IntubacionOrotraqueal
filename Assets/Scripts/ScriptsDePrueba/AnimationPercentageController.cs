using UnityEngine;

public class AnimationPercentageController : MonoBehaviour
{
    public Animator animator;
    [Range(0f, 1f)]
    public float animationProgress = 0f;

    private int stateHash;

    void Start()
    {
        stateHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        animator.speed = 0; // Para que no avance solo
        animator.Play(stateHash, 0, 0f); // Inicia animación en 0
    }

    public void OnChangeSlider(float value)
    {
        animationProgress = Mathf.Clamp01(value);
        animator.Play(stateHash, 0, animationProgress);
        animator.Update(0);
    }
}
