using Oculus.Interaction;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LaryngoscopeBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject myHands;
    [SerializeField] private GameObject rootHands;
    [SerializeField] private GameObject myCollider;
    [SerializeField] private Rigidbody rbParent;
    [SerializeField] private Grabbable myGrab;

    [Header("Transformers")]
    [SerializeField] private GrabFreeTransformer basicGrab;
    [SerializeField] private GrabFreeTransformer firstMovement;
    [SerializeField] private GrabFreeTransformer secondMovement;

    [Header("Animators")]
    public Animator laryngoscopeAnimator;
    public Animator patientAnimator;

    [Header("MovementDistances")]
    [SerializeField] private float distanciaPrimerMovimiento;
    [SerializeField] private float distanciaSegundoMovimiento;

    private bool firstMovementActive = false;
    private bool secondMovementActive = false;
    private bool firstTimeReleasing = true;

    //Estos son valores necesarios para la restricción de movimiento
    private Vector3 minPosition;
    private Vector3 maxPosition;

    private void ComprobarActivacionManos(GameState state)
    {
        if (state== GameState.introducirLaringoscopio)
        {
            firstMovementActive = true;
        }
        else if (state == GameState.sacarLaringoscopio)
        {
            rootHands.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= ComprobarActivacionManos;
    }

    void Start()
    {
        GameManager.onGameStateChanged += ComprobarActivacionManos;

        minPosition = transform.localPosition;
        maxPosition = transform.localPosition + new Vector3(0f, -distanciaPrimerMovimiento, 0f);

        basicGrab?.Initialize(myGrab);
        secondMovement?.Initialize(myGrab);

        laryngoscopeAnimator.speed = 0;
        laryngoscopeAnimator.Play("PrimerMovimiento", 0, 0f);

        rootHands.SetActive(false);
    }

    public void OnControllerSelected()
    {
        if (!firstMovementActive && !secondMovementActive) return;

        // Detiene animador del paciente al comenzar
        patientAnimator.speed = 0;

        if (firstMovementActive)
            HandleMovement("PrimerMovimiento", "MoverLengua1", GameState.elevarLaringoscopio, true);
        else if (secondMovementActive)
            HandleMovement("SegundoMovimiento", "MoverLengua2", GameState.introducirTuboOrotraqueal, false);
    }

    private void HandleMovement(string scopeAnim, string patientAnim, GameState nextState, bool isFirst)
    {
        float naturalRange = GetNormalizedPosition(transform.localPosition);
        float movementRange = naturalRange * 100f;

        // Actualizar animaciones
        UpdateAnimator(laryngoscopeAnimator, scopeAnim, naturalRange);
        UpdateAnimator(patientAnimator, patientAnim, naturalRange);

        // Cambio de estado al completar movimiento
        if (movementRange >= 99f)
        {
            if (isFirst)
            {
                StartCoroutine(ChangeHands());
                GameManager.applicationController.updateGameState(nextState);

                firstMovementActive = false;
                secondMovementActive = true;

                minPosition = transform.localPosition;
                maxPosition = transform.localPosition + new Vector3(0f, 0f, distanciaSegundoMovimiento);

                SetGrabTransformer(secondMovement);
            }
            else
            {
                myHands.SetActive(false);
                myCollider.SetActive(false);

                GameManager.applicationController.updateGameState(nextState);
                secondMovementActive = false;

                SetGrabTransformer(basicGrab); // movimiento libre
            }
        }
    }

    private void UpdateAnimator(Animator animator, string clipName, float normalizedTime)
    {
        animator.Play(clipName, 0, normalizedTime);
        animator.Update(0);
    }

    private float GetNormalizedPosition(Vector3 currentPosition)
    {
        Vector3 direction = maxPosition - minPosition;
        Vector3 relativePosition = currentPosition - minPosition;
        float projected = Vector3.Dot(relativePosition, direction.normalized);
        return Mathf.Clamp01(projected / direction.magnitude);
    }

    private IEnumerator ChangeHands()
    {
        myHands.SetActive(false);
        yield return new WaitForSeconds(1f);
        myHands.SetActive(true);
    }

    private void SetGrabTransformer(GrabFreeTransformer transformer)
    {
        transformer?.Initialize(myGrab);
        myGrab.InjectOptionalOneGrabTransformer(transformer);
    }

    public void DisableKinematicsOnRelease()
    {
        if (firstTimeReleasing)
        {
            firstTimeReleasing = false;
            rbParent.isKinematic = false;
            GameManager.applicationController.updateGameState(GameState.conectarJeringa);
        }
    }
}
