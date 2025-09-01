using System.Collections;
using UnityEngine;

public class PatientAnimations : MonoBehaviour
{
    private int handsNumber = 0;

    [SerializeField] private Animator patientAnimator;
    [SerializeField] private GameObject manosPosicionamiento;
    [SerializeField] private GameObject manosAbrirBoca;
    [SerializeField] private float delayBetweenAnimations = 2.0f;
    [SerializeField] private Collider HeadCollider;

    private void ComprobarActivacionPaciente(GameState state)
    {
        if (state == GameState.posicionarCabezaPaciente || state == GameState.abrirBocaPaciente)
        {
            HeadCollider.enabled = true;
        }
        else
        {
            HeadCollider.enabled = false;
        }
    }

    void Start()
    {
        GameManager.onGameStateChanged += ComprobarActivacionPaciente;
        manosPosicionamiento.SetActive(true);
        manosAbrirBoca.SetActive(false);
        HeadCollider.enabled = false;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= ComprobarActivacionPaciente;
    }

    private enum PatientState
    {
        Idle,
        HeadMoved,
        MouthOpened
    }

    private PatientState currentState = PatientState.Idle;
    private Coroutine animationCoroutine;

    public void HandGrabDetection()
    {
        handsNumber++;
        if (handsNumber >= 2 && animationCoroutine == null)
        {
            animationCoroutine = StartCoroutine(TrySetNextAnimation());
        }
    }

    public void HandReleaseDetection()
    {
        handsNumber = Mathf.Max(0, handsNumber - 1);
    }

    private IEnumerator TrySetNextAnimation()
    {
        switch (currentState)
        {
            case PatientState.Idle:
                patientAnimator.SetBool("MovePatient", true);
                currentState = PatientState.HeadMoved;

                yield return new WaitForSeconds(delayBetweenAnimations);

                manosPosicionamiento.SetActive(false);
                manosAbrirBoca.SetActive(true);

                animationCoroutine = null;

                GameManager.applicationController.updateGameState(GameState.abrirBocaPaciente);

                break;

            case PatientState.HeadMoved:
                patientAnimator.SetBool("OpenMouth", true);
                currentState = PatientState.MouthOpened;

                yield return new WaitForSeconds(delayBetweenAnimations);

                manosAbrirBoca.SetActive(false);

                animationCoroutine = null;

                GameManager.applicationController.updateGameState(GameState.introducirCanulaGirada);

                break;
            default:
                animationCoroutine = null;
                break;
        }
    }
}
