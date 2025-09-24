using UnityEngine;

public class BandageBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject fixedBandage;

    private void Start()
    {
        fixedBandage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BandageDetectable"))
        {
            fixedBandage.SetActive(true);
            GameManager.applicationController.updateGameState(GameState.simulacionTerminada);
            gameObject.SetActive(false);
        }
    }
}
