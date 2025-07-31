using System.Collections;
using UnityEngine;

public class PatientAnimations : MonoBehaviour
{
    private int handsNumber = 0;

    [SerializeField] private Animator patientAnimator;
    [SerializeField] private GameObject manosPosicionamiento;
    [SerializeField] private GameObject manosAbrirBoca;
    [SerializeField] private float delayBetweenAnimations = 2.0f;

    private void Start()
    {
        manosPosicionamiento.SetActive(true);
        manosAbrirBoca.SetActive(false);
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
                break;

            case PatientState.HeadMoved:
                patientAnimator.SetBool("OpenMouth", true);
                currentState = PatientState.MouthOpened;

                yield return new WaitForSeconds(delayBetweenAnimations);

                manosAbrirBoca.SetActive(false);

                animationCoroutine = null;
                break;
            default:
                animationCoroutine = null;
                break;
        }
    }
}
