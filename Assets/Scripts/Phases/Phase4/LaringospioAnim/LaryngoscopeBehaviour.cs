using Oculus.Interaction;
using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LaryngoscopeBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject Manos;
    [SerializeField] private Grabbable myGrab;
    [SerializeField] private ControladorAnimacion LaryngoscopeControlAnimation;

    [Header("Transformers")]
    [SerializeField] private GrabFreeTransformer basicGrab;
    [SerializeField] private GrabFreeTransformer firstMovement;
    [SerializeField] private GrabFreeTransformer secondMovement;

    private bool firstMovementActive = false;
    private bool secondMovementActive = false;

    //Estos son valores necesarios para la restricción de movimiento
    private Vector3 minPosition;
    private Vector3 maxPosition;

    [SerializeField] private float distanciaPrimerMovimiento;
    [SerializeField] private float distanciaSegundoMovimiento;

    [Range(0f, 100f)]
    private float MovementRange = 0f;


    void Start()
    {
        minPosition = transform.localPosition;
        maxPosition = transform.localPosition + new Vector3(0f, distanciaPrimerMovimiento, 0f);
        firstMovementActive = true;
        basicGrab?.Initialize(myGrab);
        secondMovement?.Initialize(myGrab);
        LaryngoscopeControlAnimation.setPositions();
    }
    

    public void OnControllerSelected() //Esto se llama desde el pointable unity event wrapper cuando se selecciona el objeto controlador
    {
        if (!firstMovementActive && !secondMovementActive) return;

        if (firstMovementActive)
        {
            MovementRange = GetNormalizedPosition(transform.localPosition) * 100f;

            if (MovementRange >= 99f)
            {
                StartCoroutine(HandsChange());
                GameManager.applicationController.updateGameState(GameState.elevarLaringoscopio);
                firstMovementActive = false;
                SecondMovement();
                secondMovementActive = true;
                minPosition = transform.localPosition;
                maxPosition = transform.localPosition + new Vector3(0f, 0f, distanciaSegundoMovimiento);
            }
        }
        else if (secondMovementActive)
        {
            MovementRange = GetNormalizedPosition(transform.localPosition) * 100f;
            if (MovementRange >= 99f)
            {
                Manos.SetActive(false);
                GameManager.applicationController.updateGameState(GameState.orientarTuboOrotraqueal);
                secondMovementActive = false;
                FreeMovement();
            }
        }

    }

    float GetNormalizedPosition(Vector3 currentPosition)
    {
        Vector3 direction = maxPosition - minPosition;
        Vector3 relativePosition = currentPosition - minPosition;
        float projected = Vector3.Dot(relativePosition, direction.normalized);
        float totalDistance = direction.magnitude;
        return Mathf.Clamp01(projected / totalDistance);
    }

    private IEnumerator HandsChange()
    {
        Manos.SetActive(false);
        yield return new WaitForSeconds(1f);
        Manos.SetActive(true);
    }

    private void FreeMovement()
    {
        basicGrab?.Initialize(myGrab);
        myGrab.InjectOptionalOneGrabTransformer(basicGrab);
    }

    private void FirstMovement()
    {
        myGrab.InjectOptionalOneGrabTransformer(firstMovement);
    }

    private void SecondMovement()
    {
        secondMovement?.Initialize(myGrab);
        myGrab.InjectOptionalOneGrabTransformer(secondMovement);
    }
}
