using UnityEngine;

public class BandageBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject fixedBandage;

    [SerializeField] private AudioSource connectSound;

    private void Start()
    {
        fixedBandage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BandageDetectable"))
        {
            connectSound.Play();

            fixedBandage.SetActive(true);
            GameManager.applicationController.updateGameState(GameState.simulacionTerminada);
            gameObject.SetActive(false);
        }
    }
}
