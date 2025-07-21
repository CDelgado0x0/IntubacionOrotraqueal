using UnityEngine;

public class ControladorAnimacion : MonoBehaviour
{
    private Vector3 minPosition;
    private Vector3 maxPosition;
    private int stateHash;

    public Animator animator;

    [Range(0f, 1f)]
    public float sliderValue = 0f; // Resultado normalizado

    void Start()
    {
        minPosition = transform.position;
        maxPosition = transform.position - new Vector3(0f, 0.2f, 0f);

        stateHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        animator.speed = 0;
        animator.Play(stateHash, 0, 0f);
    }

    public void OnControllerSelected() //Esto se llama desde el pointable unity event wrapper cuando se selecciona el objeto controlador
    {
        sliderValue = GetNormalizedPosition(transform.position);
        animator.Play(stateHash, 0, sliderValue);
        animator.Update(0);
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
