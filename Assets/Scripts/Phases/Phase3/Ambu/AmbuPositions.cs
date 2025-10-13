using UnityEngine;

public class AmbuPositions : MonoBehaviour
{
    [SerializeField] private AmbuContraints Ambu;

    private Rigidbody myRb;

    [HideInInspector] public bool capnographConnected = false;

    [SerializeField] private AudioSource connectSound;

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AmbuDetectable"))
        {
            Ambu.AmbuCompression();
            myRb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            GameManager.applicationController.updateGameState(GameState.oxigenarPaciente);
        }
        else if (other.CompareTag("AmbuFinalPos") && capnographConnected)
        {
            connectSound.Play();
            Ambu.AmbuCompression();
            myRb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            GameManager.applicationController.updateGameState(GameState.insuflarRealizandoAuscultacion);
        }
    }
}
