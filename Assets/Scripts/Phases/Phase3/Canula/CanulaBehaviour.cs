using System.Collections;
using UnityEngine;

public class CanulaBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject Manos;
    [SerializeField] private ChangeGrabTransforms myGrabs;
    [SerializeField] private float distanciaMovimientoAcotado;
    [SerializeField] private float distanciaRotacionAcotada;

    //Estos son valores necesarios para la restricción de movimiento de la canula
    private Vector3 minPositionMovimientoAcotado;
    private Vector3 maxPositionMovimientoAcotado;

    private Quaternion startRotation;
    private Quaternion endRotation;

    private bool limitedMovement = false;
    private bool limitedRotation = false;

    private Rigidbody myRb;

    [Range(0f, 100f)]
    private float limitedMovementRange = 0f;

    [Range(0f, 100f)]
    private float limitedRotationRange = 0f;


    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CanulaDetectable"))
        {
            StartCoroutine(HandsChange());
            myRb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            myGrabs.JustMovement();
            initializeLimitedMovement();
            other.gameObject.SetActive(false);
        }
    }

    private void initializeLimitedMovement()
    {
        limitedMovement = true;
        minPositionMovimientoAcotado = transform.localPosition;
        maxPositionMovimientoAcotado = transform.localPosition - new Vector3(0f, distanciaMovimientoAcotado, 0f);
    }

    private void initializeLimitedRotation()
    {
        limitedRotation = true;
        startRotation = transform.localRotation;
        endRotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(0f, distanciaRotacionAcotada, 0f));
    }

    public void OnControllerSelected() //Esto se llama desde el pointable unity event wrapper cuando se selecciona el objeto controlador
    {
        if (!limitedMovement && !limitedRotation) return;

        if (limitedMovement){
            limitedMovementRange = GetNormalizedPosition(transform.localPosition) * 100f;

            if (limitedMovementRange >= 99f)
            {
                StartCoroutine(HandsChange());
                GameManager.applicationController.updateGameState(GameState.girarCanula);
                limitedMovement = false;
                myGrabs.JustRotation();
                initializeLimitedRotation();
            }
        }
        else if (limitedRotation)
        {
            limitedRotationRange = GetNormalizedRotation(transform.localRotation) * 100f;
            if (limitedRotationRange >= 99f)
            {
                Manos.SetActive(false);
                GameManager.applicationController.updateGameState(GameState.colocarAmbu);
                limitedRotation = false;
                myGrabs.BasicMovement();
            }
        }

    }

    float GetNormalizedPosition(Vector3 currentPosition)
    {
        Vector3 direction = maxPositionMovimientoAcotado - minPositionMovimientoAcotado;
        Vector3 relativePosition = currentPosition - minPositionMovimientoAcotado;
        float projected = Vector3.Dot(relativePosition, direction.normalized);
        float totalDistance = direction.magnitude;
        return Mathf.Clamp01(projected / totalDistance);
    }

    float GetNormalizedRotation(Quaternion currentRotation)
    {
        float totalAngle = Quaternion.Angle(startRotation, endRotation);
        float currentAngle = Quaternion.Angle(startRotation, currentRotation);

        return Mathf.Clamp01(currentAngle / totalAngle);
    }

    private IEnumerator HandsChange()
    {
        Manos.SetActive(false);
        yield return new WaitForSeconds(1f);
        Manos.SetActive(true);
    }
}
